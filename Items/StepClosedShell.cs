using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using StepParser.Syntax;

namespace StepParser.Items
{
    public class StepClosedShell : StepRepresentationItem
    {
        public override StepItemType ItemType => StepItemType.ClosedShell;

        private StepClosedShell()
            : base(string.Empty, 0)
        {
        }

        internal static StepClosedShell CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var closedShell = new StepClosedShell();
            closedShell.SyntaxList = syntaxList;
            syntaxList.AssertListCount(2);
            closedShell.Id = id;
            closedShell.Name = syntaxList.Values[0].GetStringValue();
            closedShell.BindSyntaxList(binder, syntaxList);
            return closedShell;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            base.WriteXML(writer);
        }
    }
}
