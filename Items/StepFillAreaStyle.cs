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
        //public List<StepFillAreaStyleColour> FillAreaStyleColours { get; set; } = new List<StepFillAreaStyleColour>();

        private StepFillAreaStyle()
            : base(string.Empty, 0)
        {
        }

        internal static StepFillAreaStyle CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var fillAreaStyle = new StepFillAreaStyle();
            fillAreaStyle.SyntaxList = syntaxList;
            syntaxList.AssertListCount(2);
            fillAreaStyle.Id = id;
            fillAreaStyle.Name = syntaxList.Values[0].GetStringValue();
            
            fillAreaStyle.BindSyntaxList(binder, syntaxList, 1);
            return fillAreaStyle;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement("Element");
            writer.WriteAttributeString("type", "FILL_AREA");
            //foreach (var fillAreaStyleColour in FillAreaStyleColours)
            //{
            //    fillAreaStyleColour.WriteXML(writer);
            //}
            base.WriteXML(writer);
            writer.WriteEndElement();
        }
    }
}
