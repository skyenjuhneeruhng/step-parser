using System.Collections.Generic;
using System.Xml;
using StepParser.Syntax;

namespace StepParser.Items
{
    public class StepCylindricalSurface : StepElementarySurface
    {
        public override StepItemType ItemType => StepItemType.CylindricalSurface;

        public double Radius { get; set; }

        private StepCylindricalSurface()
            : base()
        {
        }

        public StepCylindricalSurface(string name, StepAxis2Placement3D position, double radius)
            : base(name, position)
        {
            Radius = radius;
        }

        internal override IEnumerable<StepSyntax> GetParameters(StepWriter writer)
        {
            foreach (var parameter in base.GetParameters(writer))
            {
                yield return parameter;
            }

            yield return new StepRealSyntax(Radius);
        }

        internal static StepRepresentationItem CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            syntaxList.AssertListCount(3);
            var surface = new StepCylindricalSurface();
            surface.Id = id;
            surface.Name = syntaxList.Values[0].GetStringValue();
            binder.BindValue(syntaxList.Values[1], v => surface.Position = v.AsType<StepAxis2Placement3D>());
            surface.Radius = syntaxList.Values[2].GetRealVavlue();
            return surface;
        }

        internal override void WriteXML(XmlWriter writer)
        {            
            writer.WriteStartElement("Type");
            writer.WriteString(ItemType.GetItemTypeElementString());
            writer.WriteEndElement();

            writer.WriteStartElement("Radius");
            writer.WriteString(Radius.ToString());
            writer.WriteEndElement();

            base.WriteXML(writer);
        }
    }
}
