using System.Collections.Generic;
using System.Xml;
using StepParser.Syntax;

namespace StepParser.Items
{
    public abstract class StepFaceSurface : StepFace
    {
        public StepSurface FaceGeometry { get; set; }

        public bool SameSense { get; set; }

        public StepFaceSurface(string name)
            : base(name)
        {
        }

        internal override IEnumerable<StepSyntax> GetParameters(StepWriter writer)
        {
            foreach (var parameter in base.GetParameters(writer))
            {
                yield return parameter;
            }

            yield return writer.GetItemSyntax(FaceGeometry);
            yield return StepWriter.GetBooleanSyntax(!SameSense);
        }

        internal override void WriteXML(XmlWriter writer)
        {            
            if (FaceGeometry != null)
            {
                writer.WriteStartElement("Surface");
                writer.WriteAttributeString("id", '#' + FaceGeometry.Id.ToString());
                FaceGeometry.WriteXML(writer);
                writer.WriteEndElement();
            }            
        }
    }
}
