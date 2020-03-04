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
        public StepComponentAssemble Definition { get; set; }

        private StepProductDefinitionShape()
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
        internal static StepProductDefinitionShape CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var productDefinitionShape = new StepProductDefinitionShape();
            syntaxList.AssertListCount(3);
            productDefinitionShape.Id = id;
            productDefinitionShape.Name = syntaxList.Values[0].GetStringValue();
            productDefinitionShape.Description = syntaxList.Values[1].GetStringValue();

            binder.BindValue(syntaxList.Values[2], v => productDefinitionShape.Definition = v.AsType<StepComponentAssemble>());

            return productDefinitionShape;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            Definition.WriteXML(writer);
        }
    }
}
