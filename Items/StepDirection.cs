using StepParser.Syntax;
using System.Xml;

namespace StepParser.Items
{
    public class StepDirection : StepTriple
    {
        public override StepItemType ItemType => StepItemType.Direction;
        protected override int MinimumValueCount => 2;

        private StepDirection()
        {
        }

        public StepDirection(string name, double x, double y, double z)
            : base(name, x, y, z)
        {
        }

        internal static StepDirection CreateFromSyntaxList(StepSyntaxList syntaxList)
        {
            return (StepDirection)AssignTo(new StepDirection(), syntaxList);
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement(ItemType.GetItemTypeElementString());
            base.WriteXML(writer);
            writer.WriteEndElement();
        }
    }
}
