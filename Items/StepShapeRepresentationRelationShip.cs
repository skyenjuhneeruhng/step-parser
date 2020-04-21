using StepParser.Syntax;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace StepParser.Items
{
    public class StepShapeRepresentationRelationShip: StepComponentAssemble
    {
        public override StepItemType ItemType => StepItemType.ShapeRepresentationRelationship;

        private StepShapeRepresentationRelationShip()
            : base(string.Empty, 0)
        {
        }

        internal static StepShapeRepresentationRelationShip CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var shapeRepresentationRelationShip = new StepShapeRepresentationRelationShip();
            shapeRepresentationRelationShip.SyntaxList = syntaxList;
            syntaxList.AssertListCount(4);
            shapeRepresentationRelationShip.Id = id;
            shapeRepresentationRelationShip.Name = syntaxList.Values[0].GetStringValue();
            shapeRepresentationRelationShip.Description = syntaxList.Values[1].GetStringValue();

            shapeRepresentationRelationShip.BindSyntaxList(binder, syntaxList, 2);
            return shapeRepresentationRelationShip;
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
