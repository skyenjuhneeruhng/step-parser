using StepParser.Syntax;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace StepParser.Items
{
    public class StepColourRGB: StepRepresentationItem
    {
        public override StepItemType ItemType => StepItemType.ColourRGB;
        public double R { get; set; }
        public double G { get; set; }
        public double B { get; set; }

        private StepColourRGB()
            : base(string.Empty, 0)
        {
        }

        internal static StepColourRGB CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var colourRGB = new StepColourRGB();
            colourRGB.SyntaxList = syntaxList;
            syntaxList.AssertListCount(4);
            colourRGB.Id = id;
            colourRGB.Name = syntaxList.Values[0].GetStringValue();
            colourRGB.R = syntaxList.Values[1].GetRealVavlue();
            colourRGB.G = syntaxList.Values[2].GetRealVavlue();
            colourRGB.B = syntaxList.Values[3].GetRealVavlue();

            return colourRGB;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement("Color");
            writer.WriteAttributeString("r", R.ToString());
            writer.WriteAttributeString("g", G.ToString());
            writer.WriteAttributeString("b", B.ToString());
            writer.WriteEndElement();
        }
    }
}
