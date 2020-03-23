using System.Collections.Generic;
using System.Linq;
using System.Xml;
using StepParser.Syntax;

namespace StepParser.Items
{
    public class StepFaceSurface : StepFace
    {
        //public StepSurface FaceGeometry { get; set; }

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

            //yield return writer.GetItemSyntax(FaceGeometry);
            yield return StepWriter.GetBooleanSyntax(!SameSense);
        }

        public override StepItemType ItemType => StepItemType.FaceSurface;

        internal static StepFaceSurface CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var face = new StepFaceSurface(string.Empty);
            syntaxList.AssertListCount(4);
            face.Id = id;
            face.Name = syntaxList.Values[0].GetStringValue();

            //var boundsList = syntaxList.Values[1].GetValueList();
            //face.Bounds.Clear();
            //face.Bounds.AddRange(Enumerable.Range(0, boundsList.Values.Count).Select(_ => (StepFaceBound)null));
            //for (int i = 0; i < boundsList.Values.Count; i++)
            //{
            //    var j = i; // capture to avoid rebinding
            //    //binder.BindValue(boundsList.Values[j], v => face.Bounds[j] = v.AsType<StepFaceBound>());
            //    binder.BindValue(boundsList.Values[j], v => face.RefObjs.Add(v.Item));
            //}
            ////binder.BindValue(syntaxList.Values[2], v => face.FaceGeometry = v.AsType<StepSurface>());
            //binder.BindValue(syntaxList.Values[2], v => face.RefObjs.Add(v.Item));
            face.BindSyntaxList(binder, syntaxList);
            face.SameSense = syntaxList.Values[3].GetBooleanValue();

            return face;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            //if (FaceGeometry != null)
            //{
            //    writer.WriteStartElement("Surface");
            //    writer.WriteAttributeString("id", '#' + FaceGeometry.Id.ToString());
            //    FaceGeometry.WriteXML(writer);
            //    writer.WriteEndElement();
            //}  
            base.WriteXML(writer);
        }
    }
}
