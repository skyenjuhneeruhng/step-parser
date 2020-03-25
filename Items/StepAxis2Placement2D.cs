using System.Collections.Generic;
using System.Xml;
using StepParser.Syntax;

namespace StepParser.Items
{
    public class StepAxis2Placement2D : StepGeometricRepresentationItem
    {
        public override StepItemType ItemType => StepItemType.AxisPlacement2D;

        private StepAxis2Placement2D()
            : base(string.Empty, 0)
        {
        }

        internal static StepAxis2Placement2D CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var axis = new StepAxis2Placement2D();
            axis.SyntaxList = syntaxList;
            axis.Id = id;
            syntaxList.AssertListCount(3);
            axis.Name = syntaxList.Values[0].GetStringValue();
            axis.BindSyntaxList(binder, syntaxList, 1);
            return axis;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement(ItemType.GetItemTypeElementString());
            base.WriteXML(writer);
            writer.WriteEndElement();
        }
    }
}
