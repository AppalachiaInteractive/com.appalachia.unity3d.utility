using System;
using Appalachia.CI.TextEditor.Core.Documents;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Appalachia.CI.TextEditor.JSON
{
    [Serializable]
    public class EditableJsonDocument : EditableDocument<EditableJsonDocument, JsonDocumentDrawer>
    {
        public JObject JsonObject;

        private EditableJsonDocument()
        {
        }

        protected override string Serialize()
        {
            return JsonObject.ToString();
        }

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                JsonObject = null;
            }
        }

        protected override bool CheckIsValid(string text)
        {
            text = text.Trim();
            JToken.Parse(text);
            return true;
        }

        protected internal override void ParseText(string text)
        {
            JsonObject = JsonConvert.DeserializeObject<JObject>(text);
        }

        protected override JsonDocumentDrawer GetDocumentDrawer()
        {
            return drawerInstance ??= new JsonDocumentDrawer();
        }
    }
}
