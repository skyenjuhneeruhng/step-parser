using StepParser.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace StepParser.Items
{
    public class StepStyledItem: StepRepresentationItem
    {
        public override StepItemType ItemType => StepItemType.StyledItem;
        public List<StepPresentationStyleAssignment> StyleAssignments { get; set; } = new List<StepPresentationStyleAssignment>();
        public StepManifoldSolidBrep UsedSolidBrep { get; set; }

        private StepStyledItem()
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
        internal static StepStyledItem CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var styledItem = new StepStyledItem();
            syntaxList.AssertListCount(3);
            styledItem.Id = id;
            styledItem.Name = syntaxList.Values[0].GetStringValue();

            var referList = syntaxList.Values[1].GetValueList();
            styledItem.StyleAssignments.Clear();
            styledItem.StyleAssignments.AddRange(Enumerable.Range(0, referList.Values.Count).Select(_ => (StepPresentationStyleAssignment)null));
            for (int i = 0; i < referList.Values.Count; i++)
            {
                var j = i; // capture to avoid rebinding
                binder.BindValue(referList.Values[j], v => styledItem.StyleAssignments[j] = v.AsType<StepPresentationStyleAssignment>());
            }

            binder.BindValue(syntaxList.Values[2], v => styledItem.UsedSolidBrep = v.AsType<StepManifoldSolidBrep>());

            return styledItem;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement("StyleAssignments");
            foreach (var styleAssignment in StyleAssignments)
            {
                styleAssignment.WriteXML(writer);
            }
            writer.WriteEndElement();
        }
    }
}
