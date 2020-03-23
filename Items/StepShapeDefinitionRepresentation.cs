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
        public StepProductDefinitionShape DefinitionShape { get; set; }
        public StepRepresentationItem UsedRepresentation { get; set; } //it maybe is StepShapeRepresentation or StepAdvancedBrepShapeRepresentation
        public StepShapeRepresentationRelationShip UsedRepresentationRelationShip { get; set; }

        private StepShapeDefinitionRepresentation()
            : base(string.Empty, 0)
        {
        }

        public void SetShapePresentationRelationShip(StepShapeRepresentationRelationShip _relationShip)
        {
            UsedRepresentationRelationShip = _relationShip;
        }

        internal override IEnumerable<StepSyntax> GetParameters(StepWriter writer)
        {
            foreach (var parameter in base.GetParameters(writer))
            {
                yield return parameter;
            }
        }
        internal static StepShapeDefinitionRepresentation CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var shapeDefinitionRepresentation = new StepShapeDefinitionRepresentation();
            syntaxList.AssertListCount(2);
            shapeDefinitionRepresentation.Id = id;

            binder.BindValue(syntaxList.Values[0], v => shapeDefinitionRepresentation.DefinitionShape = v.AsType<StepProductDefinitionShape>());
            //var itemInstance = (StepEntityInstanceReferenceSyntax)syntaxList.Values[1];
            //if(itemInstance)
            //binder.BindValue(syntaxList.Values[1], v => shapeDefinitionRepresentation.UsedRepresentation = v.AsType<StepShapeRepresentation>());
            binder.BindValue(syntaxList.Values[1], v => shapeDefinitionRepresentation.UsedRepresentation = v.Item);
            return shapeDefinitionRepresentation;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            if (DefinitionShape.Definition.ItemType == StepItemType.ProductDefinition)
            {
                if (((StepProductDefinition)DefinitionShape.Definition).Children.Count == 0)
                {
                    writer.WriteStartElement("Part");

                    DefinitionShape.WriteXML(writer);

                    if (UsedRepresentationRelationShip != null)
                    {
                        UsedRepresentationRelationShip.WriteXML(writer);
                    }
                    else //some stp file does not contain UsedRepresentationRelationShip entity
                    {
                        if (UsedRepresentation != null)
                            UsedRepresentation.WriteXML(writer);
                    }

                    writer.WriteEndElement();
                }
            }
            
        }
    }
}
