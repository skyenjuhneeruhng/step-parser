using System;
using System.Xml;

namespace StepParser.Items
{
    public abstract class StepAxis2Placement : StepPlacement
    {
        protected StepAxis2Placement(string name)
            : base(name)
        {
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement(ItemType.GetItemTypeElementString());
            base.WriteXML(writer);
            writer.WriteEndElement();
        }
    }
}
