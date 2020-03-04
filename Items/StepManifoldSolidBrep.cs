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
        public StepClosedShell ClosedShell { get; set; }

        private StepManifoldSolidBrep()
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
        internal static StepManifoldSolidBrep CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var manifoldSolidBrep = new StepManifoldSolidBrep();
            syntaxList.AssertListCount(2);
            manifoldSolidBrep.Id = id;
            manifoldSolidBrep.Name = syntaxList.Values[0].GetStringValue();
            binder.BindValue(syntaxList.Values[1], v => manifoldSolidBrep.ClosedShell = v.AsType<StepClosedShell>());

            return manifoldSolidBrep;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            ClosedShell.WriteXML(writer);
        }
    }
}
