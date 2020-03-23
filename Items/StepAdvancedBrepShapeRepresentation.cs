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

        internal override IEnumerable<StepSyntax> GetParameters(StepWriter writer)
        {
            foreach (var parameter in base.GetParameters(writer))
            {
                yield return parameter;
            }
        }
        internal static StepAdvancedBrepShapeRepresentation CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var advancedBrepShapeRepresentation = new StepAdvancedBrepShapeRepresentation();
            syntaxList.AssertListCount(3);
            advancedBrepShapeRepresentation.Id = id;
            advancedBrepShapeRepresentation.Name = syntaxList.Values[0].GetStringValue();
            advancedBrepShapeRepresentation.BindSyntaxList(binder, syntaxList, 1);

            return advancedBrepShapeRepresentation;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            base.WriteXML(writer);
        }
    }
}
