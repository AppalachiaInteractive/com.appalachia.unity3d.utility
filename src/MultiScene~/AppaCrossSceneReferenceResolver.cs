using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Appalachia.Utility.MultiScene
{
    public static class AppaCrossSceneReferenceResolver
    {
        public delegate bool ResolveCrossSceneReferenceDelegate(RuntimeCrossSceneReference xRef);

        static AppaCrossSceneReferenceResolver()
        {
            _resolvers.Clear();
            _resolvers.Add(DefaultResolve);
        }

        #region Fields

        private static List<ResolveCrossSceneReferenceDelegate> _resolvers =
            new List<ResolveCrossSceneReferenceDelegate>();

        #endregion

        public static void AddCustomResolver(ResolveCrossSceneReferenceDelegate customResolver)
        {
            if (!_resolvers.Contains(customResolver))
            {
                _resolvers.Add(customResolver);
            }
        }

#if UNITY_EDITOR
        public static void EditorOnly_ResolveToField(
            object fromObject,
            Object toObject,
            string fromFieldPath,
            RuntimeCrossSceneReference debugThis)
        {
            ResolveToField(fromObject, toObject, fromFieldPath, debugThis);
        }
#endif

        public static void Resolve(RuntimeCrossSceneReference xRef)
        {
            var fromObject = xRef.fromObject;
            if (!fromObject)
            {
                throw new ResolveException(string.Format("Cross-Scene Ref: {0}. fromObject is null.", xRef));
            }

            var toObject = xRef.toObject;
            if (!toObject)
            {
                throw new ResolveException(
                    string.Format("Cross-Scene Ref: {0}. Could not Resolve toObject {1}", xRef, toObject)
                );
            }

            for (var i = _resolvers.Count - 1; i >= 0; --i)
            {
                if (_resolvers[i](xRef))
                {
                    break;
                }
            }
        }

        private static void AssignField(
            object fromObject,
            Object toObject,
            FieldInfo field,
            PropertyInfo property,
            int arrayIndex)
        {
            var fieldName = field != null ? field.Name : property.Name;
            var fieldType = field != null ? field.FieldType : property.PropertyType;

            var isArray = arrayIndex >= 0;
            if (isArray)
            {
                var listObj = field != null
                    ? field.GetValue(fromObject)
                    : property.GetValue(fromObject, null);
                var list = listObj as System.Collections.IList;
                if (list == null)
                {
                    throw new ResolveException(
                        string.Format(
                            "Expected collection of elements for property {0} but field type is {1}",
                            fieldName,
                            fieldType.Name
                        )
                    );
                }

                if (list.Count <= arrayIndex)
                {
                    list.Insert(arrayIndex, toObject);
                    return;
                }

                try
                {
                    list[arrayIndex] = toObject;
                }
                catch (System.Exception ex)
                {
                    throw new ResolveException(
                        string.Format(
                            "Cross-Scene Reference Resolve FAIL on {0}.'{1}'[{2}]. Manual fix required. Exception said '{3}'",
                            fromObject,
                            fieldName,
                            arrayIndex,
                            ex
                        )
                    );
                }

                return;
            }
#if !NETFX_CORE

            if (toObject && !fieldType.IsAssignableFrom(toObject.GetType()))
#else
			else if ( toObject && fieldType.GetTypeInfo().IsAssignableFrom( toObject.GetType().GetTypeInfo() ) )
#endif
            {
                throw new ResolveException(
                    string.Format(
                        "Field {0} of type {1} is not compatible with {2} of type {3}",
                        fieldName,
                        fieldType,
                        toObject,
                        toObject.GetType().Name
                    )
                );
            }

            if (field != null)
            {
                field.SetValue(fromObject, toObject);
            }
            else if (property != null)
            {
                property.SetValue(fromObject, toObject, null);
            }
        }

        private static bool DefaultResolve(RuntimeCrossSceneReference xRef)
        {
            ResolveToField(xRef.fromObject, xRef.toObject, xRef.sourceField, xRef);
            return true;
        }

        private static bool GetFieldFromObject(
            object fromObject,
            string fromField,
            out FieldInfo field,
            out PropertyInfo property,
            out int arrayIndex)
        {
            arrayIndex = -1;
            field = null;
            property = null;

            var parseField = fromField.Split(',');
            var fieldName = parseField[0];

            if (parseField.Length > 1)
            {
                if (!int.TryParse(parseField[1], out arrayIndex))
                {
                    return false;
                }
            }

            var objectType = fromObject.GetType();
            while ((objectType != null) && (field == null) && (property == null))
            {
#if !NETFX_CORE
                field = objectType.GetField(
                    fieldName,
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Instance |
                    BindingFlags.FlattenHierarchy
                );
                if ((field == null) && fieldName.StartsWith("m_"))
                {
                    var propertyName = char.ToLower(fieldName[2]) + fieldName.Substring(3);
                    property = objectType.GetProperty(
                        propertyName,
                        BindingFlags.Public |
                        BindingFlags.NonPublic |
                        BindingFlags.Instance |
                        BindingFlags.FlattenHierarchy
                    );
                }

                objectType = objectType.BaseType;
#else
				var typeInfo = objectType.GetTypeInfo();

				field = typeInfo.GetDeclaredField( fieldName );
				if ( field == null && fieldName.StartsWith("m_") )
					property = typeInfo.GetDeclaredProperty( fieldName.Substring(2) );

				objectType = typeInfo.BaseType;
#endif
            }

            return (field != null) || (property != null);
        }

        private static object GetObjectFromField(object fromObject, string fromField)
        {
            int arrayIndex;
            FieldInfo field;
            PropertyInfo property;

            if (!GetFieldFromObject(fromObject, fromField, out field, out property, out arrayIndex))
            {
                throw new ResolveException(string.Format("Could not find Field {0}", fromField));
            }

            var fieldName = field != null ? field.Name : property.Name;
            var fieldType = field != null ? field.FieldType : property.PropertyType;

            var isArray = arrayIndex >= 0;
            if (isArray)
            {
                var list =
                    (field != null ? field.GetValue(fromObject) : property.GetValue(fromObject, null)) as
                    System.Collections.IList;
                if (list == null)
                {
                    throw new ResolveException(
                        string.Format(
                            "Expected collection of elements for property {0} but field type is {1}",
                            fieldName,
                            fieldType.Name
                        )
                    );
                }

                if (list.Count <= arrayIndex)
                {
                    throw new ResolveException(
                        string.Format(
                            "Expected collection of at least {0} elements from property {1}",
                            arrayIndex + 1,
                            fieldName
                        )
                    );
                }

                return list[arrayIndex];
            }

            return field != null ? field.GetValue(fromObject) : property.GetValue(fromObject, null);
        }

        private static void ResolveToField(
            object fromObject,
            Object toObject,
            string fromFieldPath,
            RuntimeCrossSceneReference debugThis)
        {
            var splitPaths = fromFieldPath.Split('.');

            for (var i = 0; i < (splitPaths.Length - 1); ++i)
            {
                try
                {
                    fromObject = GetObjectFromField(fromObject, splitPaths[i]);
                    if (fromObject == null)
                    {
                        throw new ResolveException(
                            string.Format(
                                "Cross-Scene Ref: {0}. Could not follow path {1} because {2} was null",
                                debugThis,
                                fromFieldPath,
                                splitPaths[i]
                            )
                        );
                    }
#if !NETFX_CORE

                    if (!fromObject.GetType().IsClass)
#else
					else if ( !fromObject.GetType().GetTypeInfo().IsClass )
#endif
                    {
                        throw new ResolveException(
                            string.Format(
                                "Cross-Scene Ref: {0}. Could not follow path {1} because {2} was not a class (probably a struct). This is unsupported.",
                                debugThis,
                                fromFieldPath,
                                splitPaths[i]
                            )
                        );
                    }
                }
                catch (System.Exception ex)
                {
                    throw new ResolveException(
                        string.Format("Cross-Scene Ref: {0}. {1}", debugThis, ex.Message)
                    );
                }
            }

            FieldInfo field;
            PropertyInfo property;
            int arrayIndex;
            var fieldName = splitPaths[splitPaths.Length - 1];

            if (!GetFieldFromObject(fromObject, fieldName, out field, out property, out arrayIndex))
            {
                throw new ResolveException(
                    string.Format(
                        "Cross-Scene Ref: {0}. Could not parse piece of path {1} from {2}",
                        debugThis,
                        fieldName,
                        fromFieldPath
                    )
                );
            }

            AssignField(fromObject, toObject, field, property, arrayIndex);
        }
    }
}
