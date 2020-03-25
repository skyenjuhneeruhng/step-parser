using StepParser.Syntax;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace StepParser.Items
{
    public class StepProductDefinitionShape: StepComponentAssemble
    {
        public override StepItemType ItemType => StepItemType.ProductDefinitionShape;
        private StepProductDefinitionShape()
            : base(string.Empty, 0)
        {
        }

        internal static StepProductDefinitionShape CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var productDefinitionShape = new StepProductDefinitionShape();
            productDefinitionShape.SyntaxList = syntaxList;
            syntaxList.AssertListCount(3);
            productDefinitionShape.Id = id;
            productDefinitionShape.Name = syntaxList.Values[0].GetStringValue();
            productDefinitionShape.Description = syntaxList.Values[1].GetStringValue();

            productDefinitionShape.BindSyntaxList(binder, syntaxList, 2);

            return productDefinitionShape;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement(ItemType.GetItemTypeElementString());
            base.WriteXML(writer);
            writer.WriteEndElement();
        }
    }
}
