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
        private List<StepRepresentationItem> _children;
        public int Count { get; set; } = 0;

        public List<StepRepresentationItem> Children
        {
            get { return _children; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                _children = value;
            }
        }
        internal override IEnumerable<StepSyntax> GetParameters(StepWriter writer)
        {
            foreach (var parameter in base.GetParameters(writer))
            {
                yield return parameter;
            }
        }
        internal static StepDynamicItem CreateFromSyntaxList(StepBinder binder, StepSimpleItemSyntax stepSimpleSyntax, int id)
        {
            StepSyntaxList syntaxList = stepSimpleSyntax.Parameters;
            var dynamicItem = new StepDynamicItem();
            dynamicItem.Id = id;
            dynamicItem.Keyword = stepSimpleSyntax.Keyword;
            dynamicItem.BindSyntaxList(binder, syntaxList);
            return dynamicItem;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            base.WriteXML(writer);
        }
    }
}
