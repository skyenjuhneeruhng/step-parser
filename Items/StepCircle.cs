using System;
using System.Collections.Generic;
using System.Xml;
using StepParser.Syntax;

namespace StepParser.Items
{
    public class StepCircle : StepConic
    {
        public override StepItemType ItemType => StepItemType.Circle;

        public double Radius { get; set; }

        private StepCircle()
            : base(string.Empty)
        {
        }

        internal static StepCircle CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var circle = new StepCircle();
            circle.SyntaxList = syntaxList;
            circle.Id = id;
            syntaxList.AssertListCount(3);
            circle.Name = syntaxList.Values[0].GetStringValue();
            circle.BindSyntaxList(binder, syntaxList, 1, 2);
            circle.Radius = syntaxList.Values[2].GetRealVavlue();
            return circle;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement(ItemType.GetItemTypeElementString());

            writer.WriteStartElement("Type");
            writer.WriteString(ItemType.GetItemTypeElementString());
            writer.WriteEndElement();

            base.WriteXML(writer);

            writer.WriteStartElement("Radius");
            writer.WriteString(Radius.ToString());
            writer.WriteEndElement();

            writer.WriteEndElement();
        }
    }
}
