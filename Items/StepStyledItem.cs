using StepParser.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;

namespace StepParser.Items
{
    public class StepStyledItem: StepRepresentationItem
    {
        public override StepItemType ItemType => StepItemType.StyledItem;
        public List<StepPresentationStyleAssignment> StyleAssignments { get; set; } = new List<StepPresentationStyleAssignment>();

        public StepManifoldSolidBrep _usedSolidBrep = null;
        public StepManifoldSolidBrep UsedSolidBrep {
            get
            {
                if(_usedSolidBrep == null)
                {
                    foreach (var obj in RefObjs)
                    {
                        if(obj.Value != null)
                        {
                            foreach (var item in obj.Value)
                            {
                                if (item is StepManifoldSolidBrep)
                                {
                                    _usedSolidBrep = (StepManifoldSolidBrep)item;
                                    break;
                                }
                            }
                        }
                    }
                }
                return _usedSolidBrep;
            }
            set
            {
                _usedSolidBrep = value;
            }
        }

        private StepStyledItem()
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
        internal static StepStyledItem CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var styledItem = new StepStyledItem();
            syntaxList.AssertListCount(3);
            styledItem.Id = id;
            styledItem.Name = syntaxList.Values[0].GetStringValue();

            styledItem.BindSyntaxList(binder, syntaxList, 1);
            return styledItem;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            foreach (var obj in RefObjs)
            {
                if (obj.Value != null && obj.Value.Count > 0)
                {
                    if(obj.Key == StepItemType.PresentationStyleAssignment.ToString())
                    {
                        if (obj.Value.Count > 1)
                            writer.WriteStartElement(obj.Key.ToString() + "s");
                        foreach (var item in obj.Value)
                        {
                            if (item != null)
                            {
                                if (item is StepRepresentationItem)
                                {
                                    ((StepRepresentationItem)item).WriteXML(writer);
                                }
                                else
                                {
                                    Debug.WriteLine($"Unsupported writexlm for item {item.ToString()}");
                                }
                            }
                        }
                        if (obj.Value.Count > 1)
                            writer.WriteEndElement();
                    }
                }
            }
        }
    }
}
