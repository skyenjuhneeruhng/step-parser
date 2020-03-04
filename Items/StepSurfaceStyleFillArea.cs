using StepParser.Syntax;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace StepParser.Items
{
    public class StepSurfaceStyleFillArea: StepRepresentationItem
    {
        public override StepItemType ItemType => StepItemType.SurfaceStyleFillArea;
        public StepFillAreaStyle FillAreaStyle { get; set; }

        private StepSurfaceStyleFillArea()
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
        internal static StepSurfaceStyleFillArea CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var surfaceStyleFillArea = new StepSurfaceStyleFillArea();
            syntaxList.AssertListCount(1);
            surfaceStyleFillArea.Id = id;

            binder.BindValue(syntaxList.Values[0], v => surfaceStyleFillArea.FillAreaStyle = v.AsType<StepFillAreaStyle>());

            return surfaceStyleFillArea;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            FillAreaStyle.WriteXML(writer);
        }
    }
}
