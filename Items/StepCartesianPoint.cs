using StepParser.Syntax;
using System.Xml;

namespace StepParser.Items
{
    public class StepCartesianPoint : StepTriple
    {
        public override StepItemType ItemType => StepItemType.CartesianPoint;
        protected override int MinimumValueCount => 1;

        private StepCartesianPoint()
        {
        }

        public StepCartesianPoint(string label, double x, double y, double z)
            : base(label, x, y, z)
        {
        }

        internal static StepCartesianPoint CreateFromSyntaxList(StepSyntaxList syntaxList)
        {
            return (StepCartesianPoint)AssignTo(new StepCartesianPoint(), syntaxList);
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement(ItemType.GetItemTypeElementString());
            base.WriteXML(writer);
            writer.WriteEndElement();
        }
    }
}
