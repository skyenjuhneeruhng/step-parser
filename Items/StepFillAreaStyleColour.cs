using StepParser.Syntax;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace StepParser.Items
{
    public class StepFillAreaStyleColour: StepRepresentationItem
    {
        public override StepItemType ItemType => StepItemType.FillAreaStyleColour;
        public StepColourRGB Colour { get; set; }

        private StepFillAreaStyleColour()
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
        internal static StepFillAreaStyleColour CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var fillAreaStyleColor = new StepFillAreaStyleColour();
            syntaxList.AssertListCount(2);
            fillAreaStyleColor.Id = id;
            fillAreaStyleColor.Name = syntaxList.Values[0].GetStringValue();

            binder.BindValue(syntaxList.Values[1], v => fillAreaStyleColor.Colour = v.AsType<StepColourRGB>());

            return fillAreaStyleColor;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement("FillStyle");
            Colour.WriteXML(writer);
            writer.WriteEndElement();
        }
    }
}
