using StepParser.Syntax;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace StepParser.Items
{
    public class StepManifoldSolidBrep: StepRepresentationItem
    {
        public override StepItemType ItemType => StepItemType.ManiFoldSolidBrep;

        private StepManifoldSolidBrep()
            : base(string.Empty, 0)
        {
        }

        internal static StepManifoldSolidBrep CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var manifoldSolidBrep = new StepManifoldSolidBrep();
            manifoldSolidBrep.SyntaxList = syntaxList;
            syntaxList.AssertListCount(2);
            manifoldSolidBrep.Id = id;
            manifoldSolidBrep.Name = syntaxList.Values[0].GetStringValue();
            manifoldSolidBrep.BindSyntaxList(binder, syntaxList, 1);
            return manifoldSolidBrep;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            base.WriteXML(writer);
        }
    }
}
