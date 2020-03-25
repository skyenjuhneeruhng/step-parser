using System;
using System.Collections.Generic;
using System.Xml;
using StepParser.Syntax;

namespace StepParser.Items
{
    public class StepEllipse : StepConic
    {
        public override StepItemType ItemType => StepItemType.Ellipse;


        public double SemiAxis1 { get; set; }
        public double SemiAxis2 { get; set; }

        private StepEllipse()
            : base(string.Empty)
        {
        }

        internal static StepEllipse CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var ellipse = new StepEllipse();
            ellipse.SyntaxList = syntaxList;
            ellipse.Id = id;
            syntaxList.AssertListCount(4);
            ellipse.Name = syntaxList.Values[0].GetStringValue();
            ellipse.BindSyntaxList(binder, syntaxList, 1, 2);
            ellipse.SemiAxis1 = syntaxList.Values[2].GetRealVavlue();
            ellipse.SemiAxis2 = syntaxList.Values[3].GetRealVavlue();
            return ellipse;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement(ItemType.GetItemTypeElementString());

            writer.WriteStartElement("Type");
            writer.WriteString(ItemType.GetItemTypeElementString());
            writer.WriteEndElement();

            base.WriteXML(writer);

            writer.WriteStartElement("MajorAxis");
            writer.WriteString(SemiAxis1.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("MinorAxis");
            writer.WriteString(SemiAxis2.ToString());
            writer.WriteEndElement();

            writer.WriteEndElement();
        }
    }
}
