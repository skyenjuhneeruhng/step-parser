using StepParser.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace StepParser.Items
{
    public class StepFillAreaStyle: StepRepresentationItem
    {
        public override StepItemType ItemType => StepItemType.FillAreaStyle;
        public string Description { get; set; }
        public List<StepFillAreaStyleColour> FillAreaStyleColours { get; set; } = new List<StepFillAreaStyleColour>();

        private StepFillAreaStyle()
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
        internal static StepFillAreaStyle CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var fillAreaStyle = new StepFillAreaStyle();
            syntaxList.AssertListCount(2);
            fillAreaStyle.Id = id;
            fillAreaStyle.Name = syntaxList.Values[0].GetStringValue();

            var referList = syntaxList.Values[1].GetValueList();
            fillAreaStyle.FillAreaStyleColours.Clear();
            fillAreaStyle.FillAreaStyleColours.AddRange(Enumerable.Range(0, referList.Values.Count).Select(_ => (StepFillAreaStyleColour)null));
            for (int i = 0; i < referList.Values.Count; i++)
            {
                var j = i;
                binder.BindValue(referList.Values[j], v => fillAreaStyle.FillAreaStyleColours[j] = v.AsType<StepFillAreaStyleColour>());
            }

            return fillAreaStyle;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement("Element");
            writer.WriteAttributeString("type", "FILL_AREA");
            foreach (var fillAreaStyleColour in FillAreaStyleColours)
            {
                fillAreaStyleColour.WriteXML(writer);
            }
            writer.WriteEndElement();
        }
    }
}
