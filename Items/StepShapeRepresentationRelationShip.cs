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
        public StepShapeRepresentation UsedRepresentation { get; set; }
        public StepAdvancedBrepShapeRepresentation AdvancedBrepShapeRepresentation { get; set; }

        private StepShapeRepresentationRelationShip()
            : base(string.Empty, 0)
        {
        }

        internal override IEnumerable<StepSyntax> GetParameters(StepWriter writer)
        {
            foreach (var parameter in base.GetParameters(writer))
            {
                yield return parameter;
            }
        }
        internal static StepShapeRepresentationRelationShip CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var shapeRepresentationRelationShip = new StepShapeRepresentationRelationShip();
            syntaxList.AssertListCount(4);
            shapeRepresentationRelationShip.Id = id;
            shapeRepresentationRelationShip.Name = syntaxList.Values[0].GetStringValue();
            shapeRepresentationRelationShip.Description = syntaxList.Values[1].GetStringValue();

            binder.BindValue(syntaxList.Values[2], v => shapeRepresentationRelationShip.UsedRepresentation = v.AsType<StepShapeRepresentation>());
            binder.BindValue(syntaxList.Values[3], v => shapeRepresentationRelationShip.AdvancedBrepShapeRepresentation = v.AsType<StepAdvancedBrepShapeRepresentation>());

            return shapeRepresentationRelationShip;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            AdvancedBrepShapeRepresentation.WriteXML(writer);
        }
    }
}
