using System.Collections.Generic;
using System.Xml;
using StepParser.Syntax;

namespace StepParser.Items
{
    public abstract partial class StepRepresentationItem
    {
        public abstract StepItemType ItemType { get; }

        public int Id { get; set; }

        public string Name { get; set; }

        protected StepRepresentationItem(string name, int id)
        {
            Name = name;
            Id = id;
        }

        internal virtual IEnumerable<StepRepresentationItem> GetReferencedItems()
        {
            yield break;
        }

        internal virtual IEnumerable<StepSyntax> GetParameters(StepWriter writer)
        {
            yield return new StepStringSyntax(Name);
        }

        internal virtual void WriteXML(XmlWriter writer)
        {
        }
    }
}
