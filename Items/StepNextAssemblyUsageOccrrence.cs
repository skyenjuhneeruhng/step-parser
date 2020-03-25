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
        public StepRepresentationItem Parent { get; set; }
        public StepRepresentationItem Child { get; set; }

        private StepNextAssemblyUsageOccrrence()
            : base(string.Empty, 0)
        {
        }

        internal static StepNextAssemblyUsageOccrrence CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var assemblyUsageOccrrence = new StepNextAssemblyUsageOccrrence();
            assemblyUsageOccrrence.SyntaxList = syntaxList;
            syntaxList.AssertListCount(6);
            assemblyUsageOccrrence.Id = id;
            assemblyUsageOccrrence.AssembleId = syntaxList.Values[0].GetStringValue();
            assemblyUsageOccrrence.Name = syntaxList.Values[1].GetStringValue();
            assemblyUsageOccrrence.Description = syntaxList.Values[2].GetStringValue();

            binder.BindValue(syntaxList.Values[3], v => assemblyUsageOccrrence.Parent = v.Item);
            binder.BindValue(syntaxList.Values[4], v => assemblyUsageOccrrence.Child = v.Item);

            return assemblyUsageOccrrence;
        }

        internal override void WriteXML(XmlWriter writer)
        {
        }
    }
}
