using StepParser.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace StepParser.Items
{
    public class StepSurfaceSideStyle: StepRepresentationItem
    {
        public override StepItemType ItemType => StepItemType.SurfaceSideStyle;
        public List<StepSurfaceStyleFillArea> SurfaceStyleFillAreaList { get; set; } = new List<StepSurfaceStyleFillArea>();

        private StepSurfaceSideStyle()
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
        internal static StepSurfaceSideStyle CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var surfaceSideStyle = new StepSurfaceSideStyle();
            syntaxList.AssertListCount(2);
            surfaceSideStyle.Id = id;
            surfaceSideStyle.Name = syntaxList.Values[0].GetStringValue();

            var referList = syntaxList.Values[1].GetValueList();
            surfaceSideStyle.SurfaceStyleFillAreaList.Clear();
            surfaceSideStyle.SurfaceStyleFillAreaList.AddRange(Enumerable.Range(0, referList.Values.Count).Select(_ => (StepSurfaceStyleFillArea)null));
            for (int i = 0; i < referList.Values.Count; i++)
            {
                var j = i; // capture to avoid rebinding
                binder.BindValue(referList.Values[j], v => surfaceSideStyle.SurfaceStyleFillAreaList[j] = v.AsType<StepSurfaceStyleFillArea>());
            }

            return surfaceSideStyle;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement("Style");
            writer.WriteAttributeString("predefined", "false");
            foreach (var styleFillAare in SurfaceStyleFillAreaList)
            {
                styleFillAare.WriteXML(writer);
            }
            writer.WriteEndElement();
        }
    }
}
