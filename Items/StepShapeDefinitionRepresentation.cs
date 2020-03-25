using StepParser.Syntax;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace StepParser.Items
{
    public class StepShapeDefinitionRepresentation: StepRepresentationItem
    {
        public override StepItemType ItemType => StepItemType.ShapeDefinitionRepresentation;
        
        private StepShapeDefinitionRepresentation()
            : base(string.Empty, 0)
        {
        }


        internal static StepShapeDefinitionRepresentation CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var shapeDefinitionRepresentation = new StepShapeDefinitionRepresentation();
            shapeDefinitionRepresentation.SyntaxList = syntaxList;
            syntaxList.AssertListCount(2);
            shapeDefinitionRepresentation.Id = id;

            shapeDefinitionRepresentation.BindSyntaxList(binder, syntaxList, 0);
            return shapeDefinitionRepresentation;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement("Part");
            writer.WriteAttributeString("Id", "#" + Id.ToString());
            base.WriteXML(writer);
            writer.WriteEndElement();
        }
    }
}
