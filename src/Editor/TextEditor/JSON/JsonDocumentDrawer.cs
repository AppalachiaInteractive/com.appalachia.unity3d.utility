using System;
using System.Linq;
using Appalachia.Utility.Strings;
using Appalachia.Utility.TextEditor.Core.Drawers;
using Appalachia.Utility.TextEditor.Core.GUICache;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

// ReSharper disable UnusedParameter.Global

namespace Appalachia.Utility.TextEditor.JSON
{
    public class JsonDocumentDrawer : HierarchyDocumentDrawer<EditableJsonDocument>
    {
        [UnityEditor.MenuItem(PKG.Menu.Assets.Base + "Create/Text/JSON", priority = 200)]
        public static void CreateNew()
        {
            CreateNewFile(new JObject().ToString(), "json");
        }

        public override void InitializeGUICollection(GUICollection collection)
        {
            var objectColor = new Color(1f,   0.46f, 0.25f, 1.0f);
            var foldoutColor = new Color(1f,  0.75f, 0.28f, 1.0f);
            var labelColor = new Color(0.48f, 0.8f,  0.8f,  1.0f);

            var foldoutStyle = new GUIStyle(EditorStyles.foldout) {fontStyle = FontStyle.Bold};

            var textStyle =
                new GUIStyle(EditorStyles.toolbarTextField) {fontStyle = FontStyle.Bold};

            collection.AddStyle(JSGUIKeys.Object, foldoutStyle);
            collection.AddColor(JSGUIKeys.Object, objectColor);

            collection.AddStyle(
                JSGUIKeys.Label,
                new GUIStyle(EditorStyles.boldLabel) {padding = new RectOffset(5, 0, 0, 0)}
            );
            collection.AddOptions(JSGUIKeys.Label, GUILayout.ExpandWidth(false));
            collection.AddColor(JSGUIKeys.Label, labelColor);

            collection.AddStyle(JSGUIKeys.NumberField, textStyle);
            collection.AddOptions(JSGUIKeys.NumberField, GUILayout.ExpandWidth(true));

            collection.AddStyle(JSGUIKeys.TextField, textStyle);
            collection.AddOptions(JSGUIKeys.TextField, GUILayout.ExpandWidth(true));

            collection.AddStyle(JSGUIKeys.Foldout, foldoutStyle);
            collection.AddColor(JSGUIKeys.Foldout, foldoutColor);
        }

        protected override void DrawDocument(EditableJsonDocument document)
        {
            EditorGUI.indentLevel++;
            var jObject = document.JsonObject;

            var expanded = IsExpanded(".", true);

            var properties = jObject.Properties().ToArray().Length;

            var foldoutHeader = GetFoldoutHeader(true, false, "object", properties);

            var originalFgColor = GUI.color;
            var color = guiCollection.GetColor(JSGUIKeys.Object);
            GUI.color = color;

            expanded = EditorGUILayout.Foldout(
                expanded,
                foldoutHeader,
                false,
                guiCollection.GetStyle(JSGUIKeys.Object)
            );

            GUI.color = originalFgColor;

            SetExpanded(".", expanded);

            if (expanded)
            {
                EditorGUI.indentLevel++;
                DrawToken(jObject, null, null);
                EditorGUI.indentLevel--;
            }

            EditorGUI.indentLevel--;
        }

        protected string GetFoldoutHeader(
            bool isObject,
            bool isArray,
            string propertyName,
            int valueCount)
        {
            var foldoutHeader = isObject
                ? ZString.Format("{0}  {{{1}}}", propertyName, valueCount)
                : isArray
                    ? ZString.Format("{0}  [{1}]", propertyName, valueCount)
                    : propertyName;

            return foldoutHeader;
        }

        protected void SetLabelWidth(string key, string label)
        {
            var style = guiCollection.GetStyle(key);
            var labelWidth = style.Value.CalcSize(new GUIContent(label)).x;
            var hierarchyOffset = (EditorGUI.indentLevel * 15f) + 7f;
            EditorGUIUtility.labelWidth = hierarchyOffset + labelWidth;
        }

        protected void PrefixLabel(string label, string followingStyleKey, string styleKey)
        {
            var style = guiCollection.GetStyle(styleKey);
            var followingStyle = guiCollection.GetStyle(followingStyleKey);
            SetLabelWidth(styleKey, label);

            var originalFgColor = GUI.color;
            var color = guiCollection.GetColor(styleKey);

            GUI.color = color;

            EditorGUILayout.PrefixLabel(label, followingStyle, style);

            GUI.color = originalFgColor;
        }

        protected void DrawToken(JToken token, int? propertyIndex, int? arrayIndex)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    DrawObject(token.Value<JObject>(), propertyIndex, arrayIndex);
                    break;
                case JTokenType.Array:
                    DrawArray(token.Value<JArray>(), propertyIndex, arrayIndex);
                    break;
                case JTokenType.Property:
                    DrawProperty(token.Value<JProperty>(), propertyIndex, arrayIndex);
                    break;
                case JTokenType.Integer:
                    DrawInteger(token, propertyIndex, arrayIndex);
                    break;
                case JTokenType.Float:
                    DrawFloat(token, propertyIndex, arrayIndex);
                    break;
                case JTokenType.String:
                    DrawString(token, propertyIndex, arrayIndex);
                    break;
                case JTokenType.Boolean:
                    DrawBoolean(token, propertyIndex, arrayIndex);
                    break;
                case JTokenType.Date:
                    DrawDate(token, propertyIndex, arrayIndex);
                    break;
                case JTokenType.Guid:
                    DrawGuid(token, propertyIndex, arrayIndex);
                    break;
                case JTokenType.Uri:
                    DrawUri(token, propertyIndex, arrayIndex);
                    break;
                case JTokenType.TimeSpan:
                    DrawTimeSpan(token, propertyIndex, arrayIndex);
                    break;
            }
        }

        protected void DrawObject(JObject jObject, int? propertyIndex, int? arrayIndex)
        {
            var properties = jObject.Properties().ToArray();
            var propertyTokens = jObject.PropertyValues().ToArray();

            for (var innerPropertyIndex = 0;
                innerPropertyIndex < properties.Length;
                innerPropertyIndex++)
            {
                var property = properties[innerPropertyIndex];
                var propertyToken = propertyTokens[innerPropertyIndex];

                if (propertyToken.HasValues)
                {
                    var values = propertyToken.Values().ToArray();
                    var valueCount = values.Length;

                    var expanded = IsExpanded(property.Path);

                    var foldoutHeader = GetFoldoutHeader(
                        propertyToken.Type == JTokenType.Object,
                        propertyToken.Type == JTokenType.Array,
                        property.Name,
                        valueCount
                    );

                    SetLabelWidth(JSGUIKeys.Foldout, property.Name);

                    var originalFgColor = GUI.color;
                    var color = guiCollection.GetColor(JSGUIKeys.Foldout);

                    GUI.color = color;

                    expanded = EditorGUILayout.Foldout(
                        expanded,
                        foldoutHeader,
                        false,
                        guiCollection.GetStyle(JSGUIKeys.Foldout)
                    );

                    GUI.color = originalFgColor;

                    SetExpanded(property.Path, expanded);

                    if (expanded)
                    {
                        EditorGUI.indentLevel++;
                        DrawToken(propertyToken, innerPropertyIndex, null);
                        EditorGUI.indentLevel--;
                    }
                }
                else
                {
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        DrawToken(property,      innerPropertyIndex, null);
                        DrawToken(propertyToken, innerPropertyIndex, null);
                    }
                }
            }
        }

        protected void DrawProperty(JProperty jProperty, int? propertyIndex, int? arrayIndex)
        {
            PrefixLabel(jProperty.Name, JSGUIKeys.TextField, JSGUIKeys.Label);
        }

        protected void DrawArray(JArray jArray, int? propertyIndex, int? arrayIndex)
        {
            var arrayElements = jArray.Values().ToArray();

            EditorGUI.indentLevel++;
            for (var innerArrayIndex = 0; innerArrayIndex < arrayElements.Length; innerArrayIndex++)
            {
                var element = arrayElements[innerArrayIndex];

                var horizontal = false;
                if ((element.Type != JTokenType.Array) && (element.Type != JTokenType.Object))
                {
                    horizontal = true;
                    EditorGUILayout.BeginHorizontal();
                }

                PrefixLabel(ZString.Format("[{0}]", innerArrayIndex), JSGUIKeys.TextField, JSGUIKeys.Label);
                DrawToken(element, null, innerArrayIndex);

                if (horizontal)
                {
                    EditorGUILayout.EndHorizontal();
                }
            }

            EditorGUI.indentLevel--;
        }

        protected void AssignFieldResult(JToken token, JToken newValue, int? arrayIndex)
        {
            if (arrayIndex.HasValue)
            {
                var jArray = token.Parent.Value<JArray>();
                jArray[arrayIndex.Value] = newValue;
            }
            else
            {
                var jProperty = token.Parent.Value<JProperty>();
                jProperty.Value = newValue;
            }
        }

        protected void DrawInteger(JToken token, int? propertyIndex, int? arrayIndex)
        {
            EditorGUIUtility.labelWidth = 0.1f;
            var style = guiCollection.GetStyle(JSGUIKeys.TextField);
            var options = guiCollection.GetOptions(JSGUIKeys.TextField);

            var value = EditorGUILayout.IntField(" ", token.Value<int>(), style, options);

            AssignFieldResult(token, value, arrayIndex);
        }

        protected void DrawFloat(JToken token, int? propertyIndex, int? arrayIndex)
        {
            EditorGUIUtility.labelWidth = 0.1f;
            var style = guiCollection.GetStyle(JSGUIKeys.TextField);
            var options = guiCollection.GetOptions(JSGUIKeys.TextField);

            var value = EditorGUILayout.FloatField(" ", token.Value<float>(), style, options);

            AssignFieldResult(token, value, arrayIndex);
        }

        protected void DrawString(JToken token, int? propertyIndex, int? arrayIndex)
        {
            EditorGUIUtility.labelWidth = 0.1f;
            var style = guiCollection.GetStyle(JSGUIKeys.TextField);
            var options = guiCollection.GetOptions(JSGUIKeys.TextField);

            var value = EditorGUILayout.TextField(" ", token.Value<string>(), style, options);

            AssignFieldResult(token, value, arrayIndex);
        }

        protected void DrawBoolean(JToken token, int? propertyIndex, int? arrayIndex)
        {
            EditorGUIUtility.labelWidth = 0.1f;
            var style = guiCollection.GetStyle(JSGUIKeys.TextField);
            var options = guiCollection.GetOptions(JSGUIKeys.TextField);

            var value = EditorGUILayout.Toggle(" ", token.Value<bool>(), style, options);

            AssignFieldResult(token, value, arrayIndex);
        }

        protected void DrawDate(JToken token, int? propertyIndex, int? arrayIndex)
        {
            EditorGUIUtility.labelWidth = 0.1f;
            var style = guiCollection.GetStyle(JSGUIKeys.TextField);
            var options = guiCollection.GetOptions(JSGUIKeys.TextField);

            var value = EditorGUILayout.TextField(
                " ",
                token.Value<DateTime>().ToString("yyyy-MM-dd HH:mm:ss"),
                style,
                options
            );

            if (DateTime.TryParse(value, out var parsed))
            {
                AssignFieldResult(token, parsed, arrayIndex);
            }
        }

        protected void DrawGuid(JToken token, int? propertyIndex, int? arrayIndex)
        {
            EditorGUIUtility.labelWidth = 0.1f;
            var style = guiCollection.GetStyle(JSGUIKeys.TextField);
            var options = guiCollection.GetOptions(JSGUIKeys.TextField);

            var value = EditorGUILayout.TextField(
                " ",
                token.Value<Guid>().ToString(),
                style,
                options
            );

            if (Guid.TryParse(value, out var parsed))
            {
                AssignFieldResult(token, parsed, arrayIndex);
            }
        }

        protected void DrawUri(JToken token, int? propertyIndex, int? arrayIndex)
        {
            EditorGUIUtility.labelWidth = 0.1f;
            var style = guiCollection.GetStyle(JSGUIKeys.TextField);
            var options = guiCollection.GetOptions(JSGUIKeys.TextField);

            var value = EditorGUILayout.TextField(
                " ",
                token.Value<Uri>().ToString(),
                style,
                options
            );

            if (Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out var parsed))
            {
                AssignFieldResult(token, parsed, arrayIndex);
            }
        }

        protected void DrawTimeSpan(JToken token, int? propertyIndex, int? arrayIndex)
        {
            EditorGUIUtility.labelWidth = 0.1f;
            var style = guiCollection.GetStyle(JSGUIKeys.TextField);
            var options = guiCollection.GetOptions(JSGUIKeys.TextField);

            var value = EditorGUILayout.TextField(
                " ",
                token.Value<TimeSpan>().ToString(),
                style,
                options
            );

            if (TimeSpan.TryParse(value, out var parsed))
            {
                AssignFieldResult(token, parsed, arrayIndex);
            }
        }

        /*
        protected void OldDraw(EditableJsonDocument document)
        {
            
            DrawObject(document.JsonObject);
            return;


            if (GUILayout.Button("Add New Property"))
            {
                var menu = new GenericMenu();
                menu.AddItem(Content.EmptyObject, false, () => { AddNewProperty<JObject>(document.JsonObject); });
                menu.AddSeparator("");
                menu.AddItem(Content.String,  false, () => { AddNewProperty<string>(document.JsonObject); });
                menu.AddItem(Content.Single,  false, () => { AddNewProperty<float>(document.JsonObject); });
                menu.AddItem(Content.Integer, false, () => { AddNewProperty<int>(document.JsonObject); });
                menu.AddItem(Content.Boolean, false, () => { AddNewProperty<bool>(document.JsonObject); });
                menu.ShowAsContext();
            }
        }
        */

        /*
        private static void DrawObject(JToken container)
        {
            EditorGUI.indentLevel++;
            var tokens = container.Values<JToken>().ToArray();
            var length = container.Count();

            for (var i = 0; i < length; i++)
            {
                var token = tokens[i];

                switch (token.Type)
                {
                    case JTokenType.Array:
                        DrawArray(token);
                        break;
                    case JTokenType.Object:
                        DrawObject(token);
                        break;
                    case JTokenType.Property:
                        DrawProperty(token);
                        DrawObject(token);
                        break;
                    case JTokenType.Null:
                        EditorGUILayout.HelpBox("Null", MessageType.None);
                        break;
                    case JTokenType.Comment:
                        break;
                    default:
                    {
                        using (new EditorGUILayout.HorizontalScope())
                        {
                            var parent = token.Parent.Value<JProperty>();

                            DrawField(token);
                            DrawFieldMenu(token, parent);
                        }

                        break;
                    }
                }
            }

            EditorGUI.indentLevel--;
        }

        private static void DrawProperty(JToken token)
        {
            EditorGUI.indentLevel++;
            var property = token.Value<JProperty>();

            var propertyName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(property.Name.ToLower()) + ":";

            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUIUtility.labelWidth = 140;
                EditorGUILayout.LabelField(propertyName, Options.PropertyLabel);
            }

            EditorGUI.indentLevel--;
        }

        private static void DrawField(JToken token)
        {
            var parentProperty = token.Parent.Value<JProperty>();

            EditorGUIUtility.labelWidth = 40;
            EditorGUILayout.LabelField("►", Styles.Button, Options.MenuButton);

            switch (token.Type)
            {
                case JTokenType.String:
                    var stringValue = token.Value<string>();
                    stringValue = EditorGUILayout.DelayedTextField(stringValue);
                    parentProperty.Value = stringValue;
                    break;
                case JTokenType.Float:
                    var floatValue = token.Value<float>();
                    floatValue = EditorGUILayout.DelayedFloatField(floatValue);
                    parentProperty.Value = floatValue;
                    break;
                case JTokenType.Integer:
                    var intValue = token.Value<int>();
                    intValue = EditorGUILayout.DelayedIntField(intValue);
                    parentProperty.Value = intValue;
                    break;
                case JTokenType.Boolean:
                    var boolValue = token.Value<bool>();
                    boolValue = EditorGUILayout.Toggle(boolValue);
                    parentProperty.Value = boolValue;
                    break;
                default:
                    EditorGUILayout.HelpBox(
                        $"Type '{token.Type.ToString()}' is not supported. Use text editor instead",
                        MessageType.Warning
                    );
                    break;
            }
        }

        private static void DrawFieldMenu(JToken token, JProperty property)
        {
            if (EditorGUILayout.DropdownButton(
                Content.Menu,
                FocusType.Keyboard,
                EditorStyles.miniButton,
                Options.MenuButton
            ))
            {
                var menu = new GenericMenu();
                if (property.Value.Type == JTokenType.Object)
                {
                    var jObject = property.Value.Value<JObject>();
                    menu.AddItem(
                        new GUIContent("Add/Empty Object"),
                        false,
                        () => { AddNewProperty<JObject>(jObject); }
                    );
                    menu.AddSeparator("Add/");
                    menu.AddItem(new GUIContent("Add/String"),  false, () => { AddNewProperty<string>(jObject); });
                    menu.AddItem(new GUIContent("Add/Single"),  false, () => { AddNewProperty<float>(jObject); });
                    menu.AddItem(new GUIContent("Add/Integer"), false, () => { AddNewProperty<int>(jObject); });
                    menu.AddItem(new GUIContent("Add/Boolean"), false, () => { AddNewProperty<bool>(jObject); });
                }

                menu.AddItem(new GUIContent("Remove"), false, token.Remove);

                menu.ShowAsContext();
            }
        }

        private static void AddNewProperty<T>(JObject jObject)
        {
            var typeName = typeof(T).Name.ToLower();
            object value = default(T);

            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Boolean:
                    break;
                case TypeCode.Int32:
                    typeName = "integer";
                    break;
                case TypeCode.Single:
                    break;
                case TypeCode.String:
                    value = "";
                    break;
                default:
                    if (typeof(T) == typeof(JObject))
                    {
                        typeName = "empty object";
                    }

                    value = new JObject();
                    break;
            }

            var propertyName = GetUniqueName(jObject, ZString.Format("new {0}", typeName));
            var property = new JProperty(propertyName, value);
            jObject.Add(property);
        }
        */

        private static string GetUniqueName(JObject jObject, string originalProperty)
        {
            var uniqueName = originalProperty;
            var suffix = 0;

            while (jObject[uniqueName] != null)
            {
                suffix++;
                uniqueName = ZString.Format("{0}-{1:000}", originalProperty, suffix);
            }

            return uniqueName;
        }

        public static void Rename(JToken token, string newName)
        {
            var parent = token.Parent;
            if (parent == null)
            {
                throw new InvalidOperationException("The parent is missing.");
            }

            var newToken = new JProperty(newName, token);
            parent.Replace(newToken);
        }

        private static class JSGUIKeys
        {
            private const string Prefix = "JSGUIKEYS.";
            public const string Object = Prefix + "Object";
            public const string Label = Prefix + "Label";
            public const string NumberField = Prefix + "NumberField";
            public const string TextField = Prefix + "TextField";
            public const string Foldout = Prefix + "Foldout";

            public static string GetLevel(int i)
            {
                const string pfx = Prefix + "Level.";

                return ZString.Format("{0}{1}", pfx, i);
            }
        }
    }
}
