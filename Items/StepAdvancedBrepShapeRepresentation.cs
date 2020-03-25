using StepParser.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace StepParser.Items
{
    public class StepAdvancedBrepShapeRepresentation: StepRepresentationItem
    {
        public override StepItemType ItemType => StepItemType.AdvancedBrepShapeRepresentation;

        private StepAdvancedBrepShapeRepresentation()
            : base(string.Empty, 0)
        {
        }

        internal static StepAdvancedBrepShapeRepresentation CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var advancedBrepShapeRepresentation = new StepAdvancedBrepShapeRepresentation();
            advancedBrepShapeRepresentation.SyntaxList = syntaxList;
            syntaxList.AssertListCount(3);
            advancedBrepShapeRepresentation.Id = id;
            advancedBrepShapeRepresentation.Name = syntaxList.Values[0].GetStringValue();
            advancedBrepShapeRepresentation.BindSyntaxList(binder, syntaxList, 1);

            return advancedBrepShapeRepresentation;
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
