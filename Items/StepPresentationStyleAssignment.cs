using StepParser.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace StepParser.Items
{
    public class StepPresentationStyleAssignment: StepRepresentationItem
    {
        public override StepItemType ItemType => StepItemType.PresentationStyleAssignment;
        //public List<StepSurfaceStyleUsage> StyleUsageList { get; set; } = new List<StepSurfaceStyleUsage>();

        private StepPresentationStyleAssignment()
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
        internal static StepPresentationStyleAssignment CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var presentationStyleAssignment = new StepPresentationStyleAssignment();
            syntaxList.AssertListCount(1);
            presentationStyleAssignment.Id = id;
            presentationStyleAssignment.BindSyntaxList(binder, syntaxList, 0);
            return presentationStyleAssignment;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement("StyleAssignment");
            writer.WriteAttributeString("id", "#" + Id.ToString());
            base.WriteXML(writer);
            writer.WriteEndElement();
        }
    }
}
