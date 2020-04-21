using StepParser.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace StepParser.Items
{
    public class StepShapeRepresentation: StepRepresentationItem
    {
        public override StepItemType ItemType => StepItemType.ShapeRepresentation;

        protected StepShapeRepresentation()
            : base(string.Empty, 0)
        {
        }

        internal static StepShapeRepresentation CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var shapeRepresentation = new StepShapeRepresentation();
            shapeRepresentation.SyntaxList = syntaxList;
            syntaxList.AssertListCount(3);
            shapeRepresentation.Id = id;
            shapeRepresentation.Name = syntaxList.Values[0].GetStringValue();
            shapeRepresentation.BindSyntaxList(binder, syntaxList, 1);
            return shapeRepresentation;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement(ItemType.GetItemTypeElementString());
            writer.WriteAttributeString("Id", "#" + Id.ToString());
            base.WriteXML(writer);
            writer.WriteEndElement();
        }
    }
}
