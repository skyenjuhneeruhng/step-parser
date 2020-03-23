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
        //public List<StepRepresentationItem> UsedRepresentationItems { get; set; } = new List<StepRepresentationItem>();

        protected StepShapeRepresentation()
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
        internal static StepShapeRepresentation CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var shapeRepresentation = new StepShapeRepresentation();
            syntaxList.AssertListCount(3);
            shapeRepresentation.Id = id;
            shapeRepresentation.Name = syntaxList.Values[0].GetStringValue();
            shapeRepresentation.BindSyntaxList(binder, syntaxList, 1);
            return shapeRepresentation;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            base.WriteXML(writer); //Tien added on Mar 21 2020
        }
    }
}
