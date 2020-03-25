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
        //public StepColourRGB Colour { get; set; }

        private StepFillAreaStyleColour()
            : base(string.Empty, 0)
        {
        }

        internal static StepFillAreaStyleColour CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var fillAreaStyleColor = new StepFillAreaStyleColour();
            fillAreaStyleColor.SyntaxList = syntaxList;
            syntaxList.AssertListCount(2);
            fillAreaStyleColor.Id = id;
            fillAreaStyleColor.Name = syntaxList.Values[0].GetStringValue();

            fillAreaStyleColor.BindSyntaxList(binder, syntaxList, 1);
            return fillAreaStyleColor;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement("FillStyle");
            base.WriteXML(writer);
            writer.WriteEndElement();
        }
    }
}
