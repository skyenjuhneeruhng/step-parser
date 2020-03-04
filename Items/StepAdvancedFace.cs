using System.Linq;
using System.Xml;
using StepParser.Syntax;

namespace StepParser.Items
{
    public class StepAdvancedFace : StepFaceSurface
    {
        public StepAdvancedFace(string name)
            : base(name)
        {
        }

        private StepAdvancedFace()
            : base(string.Empty)
        {
        }

        public override StepItemType ItemType => StepItemType.AdvancedFace;

        internal static StepAdvancedFace CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var face = new StepAdvancedFace();
            syntaxList.AssertListCount(4);
            face.Id = id;
            face.Name = syntaxList.Values[0].GetStringValue();

            var boundsList = syntaxList.Values[1].GetValueList();
            face.Bounds.Clear();
            face.Bounds.AddRange(Enumerable.Range(0, boundsList.Values.Count).Select(_ => (StepFaceBound)null));
            for (int i = 0; i < boundsList.Values.Count; i++)
            {
                var j = i; // capture to avoid rebinding
                binder.BindValue(boundsList.Values[j], v => face.Bounds[j] = v.AsType<StepFaceBound>());
            }
            binder.BindValue(syntaxList.Values[2], v => face.FaceGeometry = v.AsType<StepSurface>());
            face.SameSense = syntaxList.Values[3].GetBooleanValue();

            return face;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement(ItemType.GetItemTypeElementString());
            writer.WriteAttributeString("id", '#' + Id.ToString());            
            for (int idx = 0; idx < Bounds.Count; idx++)
            {
                Bounds[idx].WriteXML(writer);
            }
            base.WriteXML(writer);
            writer.WriteEndElement();
        }
    }
}
