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
        public StepProduct Product { get; set; }

        private StepProductDefFormationWithSpecSource()
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
        internal static StepProductDefFormationWithSpecSource CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var productDefFormationWithSpecSource = new StepProductDefFormationWithSpecSource();
            syntaxList.AssertListCount(4);
            productDefFormationWithSpecSource.Id = id;
            productDefFormationWithSpecSource.Name = syntaxList.Values[0].GetStringValue();
            productDefFormationWithSpecSource.Description = syntaxList.Values[1].GetStringValue();

            binder.BindValue(syntaxList.Values[2], v => productDefFormationWithSpecSource.Product = v.AsType<StepProduct>());

            return productDefFormationWithSpecSource;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            Product.WriteXML(writer);
        }

        public void WriteXMLGroup(XmlWriter writer)
        {
            Product.WriteXMLGroup(writer);
        }

        public void WriteXMLOroderPart(XmlWriter writer)
        {
            Product.WriteXMLOroderPart(writer);
        }
    }
}
