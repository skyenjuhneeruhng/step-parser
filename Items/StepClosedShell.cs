using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using StepParser.Syntax;

namespace StepParser.Items
{
    public class StepClosedShell : StepRepresentationItem
    {
        public override StepItemType ItemType => StepItemType.ClosedShell;
        private List<StepAdvancedFace> AdvancedFaces { get; set; }  = new List<StepAdvancedFace>();

        public StepClosedShell(string name, int id)
            : base(name, id)
        {
        }

        private StepClosedShell()
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
        internal static StepClosedShell CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var closedShell = new StepClosedShell();
            syntaxList.AssertListCount(2);
            closedShell.Id = id;
            closedShell.Name = syntaxList.Values[0].GetStringValue();

            //var referList = syntaxList.Values[1].GetValueList();
            //closedShell.AdvancedFaces.Clear();
            //closedShell.AdvancedFaces.AddRange(Enumerable.Range(0, referList.Values.Count).Select(_ => (StepAdvancedFace)null));
            //for (int i = 0; i < referList.Values.Count; i++)
            //{
            //    var j = i; // capture to avoid rebinding
            //    //binder.BindValue(referList.Values[j], v => closedShell.AdvancedFaces[j] = v.AsType<StepAdvancedFace>());
            //    binder.BindValue(referList.Values[j], v => closedShell.RefObjs.Add(v.Item));//Tien changed march 21 2020
            //}
            closedShell.BindSyntaxList(binder, syntaxList);
            return closedShell;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            ////writer.WriteStartElement("AdvancedFaces");
            //writer.WriteStartElement("Faces");//Tien changed on Mar 21 2020
            //writer.WriteAttributeString("id", '#' + Id.ToString());
            ////for(int idx = 0; idx < AdvancedFaces.Count; idx++)
            ////{
            ////    AdvancedFaces[idx].WriteXML(writer);
            ////}
            //foreach (var obj in RefObjs)
            //{
            //    ((StepRepresentationItem)obj).WriteXML(writer);
            //}
            //writer.WriteEndElement();
            base.WriteXML(writer);
        }
    }
}
