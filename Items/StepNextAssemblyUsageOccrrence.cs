using StepParser.Syntax;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace StepParser.Items
{
    class StepNextAssemblyUsageOccrrence: StepComponentAssemble
    {
        public override StepItemType ItemType => StepItemType.NextAssemblyUsageOccurrence;
        public string AssembleId { get; set; }
        public StepProductDefinition Parent { get; set; }
        public StepProductDefinition Child { get; set; }

        private StepNextAssemblyUsageOccrrence()
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
        internal static StepNextAssemblyUsageOccrrence CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var assemblyUsageOccrrence = new StepNextAssemblyUsageOccrrence();
            syntaxList.AssertListCount(6);
            assemblyUsageOccrrence.Id = id;
            assemblyUsageOccrrence.AssembleId = syntaxList.Values[0].GetStringValue();
            assemblyUsageOccrrence.Name = syntaxList.Values[1].GetStringValue();
            assemblyUsageOccrrence.Description = syntaxList.Values[2].GetStringValue();

            binder.BindValue(syntaxList.Values[3], v => assemblyUsageOccrrence.Parent = v.AsType<StepProductDefinition>());
            binder.BindValue(syntaxList.Values[4], v => assemblyUsageOccrrence.Child = v.AsType<StepProductDefinition>());

            return assemblyUsageOccrrence;
        }

        internal override void WriteXML(XmlWriter writer)
        {
        }
    }
}
