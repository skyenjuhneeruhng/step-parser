using System.Collections.Generic;
using System.Linq;
using System.Xml;
using StepParser.Syntax;

namespace StepParser.Items
{
    public class StepFaceSurface : StepFace
    {
        public override StepItemType ItemType => StepItemType.FaceSurface;

        public bool SameSense { get; set; }

        public StepFaceSurface(string name)
            : base(name)
        {
        }

        internal static StepFaceSurface CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var face = new StepFaceSurface(string.Empty);
            face.SyntaxList = syntaxList;
            syntaxList.AssertListCount(4);
            face.Id = id;
            face.Name = syntaxList.Values[0].GetStringValue();
            face.BindSyntaxList(binder, syntaxList);
            face.SameSense = syntaxList.Values[3].GetBooleanValue();

            return face;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement(ItemType.GetItemTypeElementString());
            writer.WriteAttributeString("id", '#' + Id.ToString());
            base.WriteXML(writer);
            writer.WriteEndElement();
        }
    }
}
