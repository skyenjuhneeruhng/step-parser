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
        public List<StepManifoldSolidBrep> UsedSolidBrepList { get; set; } = new List<StepManifoldSolidBrep>();
        public List<StepStyledItem> UsedStyledItems { get; set; }  = new List<StepStyledItem>();

        private StepAdvancedBrepShapeRepresentation()
            : base(string.Empty, 0)
        {
        }

        public void SetStyledItems(List<StepStyledItem> _styledItems)
        {
            UsedStyledItems = _styledItems;
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

            var referList = syntaxList.Values[1].GetValueList();
            advancedBrepShapeRepresentation.UsedSolidBrepList.Clear();
            advancedBrepShapeRepresentation.UsedSolidBrepList.AddRange(Enumerable.Range(0, referList.Values.Count).Select(_ => (StepManifoldSolidBrep)null));
            for (int i = 0; i < referList.Values.Count; i++)
            {
                var j = i;
                binder.BindValue(referList.Values[j], v => advancedBrepShapeRepresentation.UsedSolidBrepList[j] = v.AsType<StepManifoldSolidBrep>());
            }

            return advancedBrepShapeRepresentation;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            foreach(var solidBrep in UsedSolidBrepList)
            {
                solidBrep.WriteXML(writer);
            }
            
            foreach (var styledItem in UsedStyledItems)
            {
                styledItem.WriteXML(writer);
            }
        }
    }
}
