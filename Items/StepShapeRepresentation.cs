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
        public List<StepRepresentationItem> UsedRepresentationItems { get; set; } = new List<StepRepresentationItem>();

        private StepShapeRepresentation()
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
            var referList = syntaxList.Values[1].GetValueList();
            shapeRepresentation.UsedRepresentationItems.Clear();
            shapeRepresentation.UsedRepresentationItems.AddRange(Enumerable.Range(0, referList.Values.Count).Select(_ => (StepRepresentationItem)null));
            for (int i = 0; i < referList.Values.Count; i++)
            {
                var j = i;
                binder.BindValue(referList.Values[j], v => shapeRepresentation.UsedRepresentationItems[j] = v.AsType<StepRepresentationItem>());
            }

            return shapeRepresentation;
        }

        internal override void WriteXML(XmlWriter writer)
        {
        }
    }
}
