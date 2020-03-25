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

        internal static StepSurfaceStyleFillArea CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var surfaceStyleFillArea = new StepSurfaceStyleFillArea();
            surfaceStyleFillArea.SyntaxList = syntaxList;
            syntaxList.AssertListCount(1);
            surfaceStyleFillArea.Id = id;

            surfaceStyleFillArea.BindSyntaxList(binder, syntaxList, 0);
            return surfaceStyleFillArea;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement("SurfaceStyleFillArea");
            base.WriteXML(writer);
            writer.WriteEndElement();
        }
    }
}
