using System;
using Appalachia.Utility.TextEditor.Core.Documents;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Appalachia.Utility.TextEditor.JSON
{
    [Serializable]
    public class EditableJsonDocument : EditableDocument<EditableJsonDocument, JsonDocumentDrawer>
    {
        private EditableJsonDocument()
        {
        }

        #region Fields and Autoproperties

        public JObject JsonObject;

        #endregion

        /// <inheritdoc />
        protected internal override void ParseText(string text)
        {
            JsonObject = JsonConvert.DeserializeObject<JObject>(text);
        }

        /// <inheritdoc />
        protected override bool CheckIsValid(string text)
        {
            text = text.Trim();
            JToken.Parse(text);
            return true;
        }

        /// <inheritdoc />
        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                JsonObject = null;
            }
        }

        /// <inheritdoc />
        protected override JsonDocumentDrawer GetDocumentDrawer()
        {
            return drawerInstance ??= new JsonDocumentDrawer();
        }

        /// <inheritdoc />
        protected override string Serialize()
        {
            return JsonObject.ToString();
        }
    }
}
