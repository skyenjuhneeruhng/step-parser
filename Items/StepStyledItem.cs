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

        private StepStyledItem()
            : base(string.Empty, 0)
        {
        }

        internal static StepStyledItem CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var styledItem = new StepStyledItem();
            styledItem.SyntaxList = syntaxList;
            syntaxList.AssertListCount(3);
            styledItem.Id = id;
            styledItem.Name = syntaxList.Values[0].GetStringValue();
            styledItem.BindSyntaxList(binder, syntaxList, 1);
            return styledItem;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement(GetStepItemTypeStr());

            writer.WriteAttributeString("id", '#' + Id.ToString());
            foreach (var obj in RefChildItems)
            {
                if (obj.Value != null && obj.Value.Count > 0)
                {
                    if (obj.Key == StepItemType.PresentationStyleAssignment.ToString())
                    {
                        if (obj.Value.Count > 1)
                            writer.WriteStartElement(obj.Key.ToString() + "s");
                        foreach (var item in obj.Value)
                        {
                            if (item != null)
                            {
                                if (item is StepRepresentationItem)
                                {
                                    writer.WriteStartElement(((StepRepresentationItem)item).GetStepItemTypeStr());
                                    writer.WriteAttributeString("id", '#' + ((StepRepresentationItem)item).Id.ToString());
                                    ((StepRepresentationItem)item).WriteXML(writer);
                                    writer.WriteEndElement();
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

            writer.WriteEndElement();
            
        }
    }
}
