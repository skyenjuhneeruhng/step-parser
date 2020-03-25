using StepParser.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Xml;

namespace StepParser.Items
{
    public class StepDynamicItem : StepRepresentationItem
    {
        public override StepItemType ItemType  => StepItemType.Unknown;

        private StepDynamicItem()
            : base(string.Empty, 0)
        {
        }

        internal static StepDynamicItem CreateFromSyntaxList(StepBinder binder, StepSimpleItemSyntax stepSimpleSyntax, int id)
        {
            StepSyntaxList syntaxList = stepSimpleSyntax.Parameters;            
            var dynamicItem = new StepDynamicItem();
            dynamicItem.SyntaxList = syntaxList;
            dynamicItem.Id = id;
            dynamicItem.Keyword = stepSimpleSyntax.Keyword;
            dynamicItem.BindSyntaxList(binder, syntaxList, 0);
            return dynamicItem;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            base.WriteXML(writer);
        }
    }
}
