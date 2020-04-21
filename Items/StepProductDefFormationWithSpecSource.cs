using StepParser.Syntax;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace StepParser.Items
{
    public class StepProductDefFormationWithSpecSource: StepComponentAssemble
    {
        public override StepItemType ItemType => StepItemType.ProductDefinitionFormationWithSpecifiedSource;

        private StepProductDefFormationWithSpecSource()
            : base(string.Empty, 0)
        {
        }

        internal static StepProductDefFormationWithSpecSource CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var productDefFormationWithSpecSource = new StepProductDefFormationWithSpecSource();
            productDefFormationWithSpecSource.SyntaxList = syntaxList;
            syntaxList.AssertListCount(4);
            productDefFormationWithSpecSource.Id = id;
            productDefFormationWithSpecSource.Name = syntaxList.Values[0].GetStringValue();
            productDefFormationWithSpecSource.Description = syntaxList.Values[1].GetStringValue();

            productDefFormationWithSpecSource.BindSyntaxList(binder, syntaxList, 2);

            return productDefFormationWithSpecSource;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement(ItemType.GetItemTypeElementString());
            writer.WriteAttributeString("Id", "#" + Id.ToString());
            base.WriteXML(writer);
            writer.WriteEndElement();
        }

        public override void WriteXMLGroup(XmlWriter writer)
        {
            base.WriteXMLGroup(writer);
        }

        public override void WriteXMLOrderPart(XmlWriter writer)
        {
            base.WriteXMLOrderPart(writer);
        }
    }
}
