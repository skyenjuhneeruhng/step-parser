using StepParser.Syntax;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace StepParser.Items
{
    public class StepProductDefinition : StepComponentAssemble
    {
        public override StepItemType ItemType => StepItemType.ProductDefinition;

        public StepProductDefinition(string name, int id)
            : base(name, id)
        {
        }

        private StepProductDefinition()
            : base(string.Empty, 0)
        {
        }

        internal static StepProductDefinition CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var productDefinition = new StepProductDefinition();
            productDefinition.SyntaxList = syntaxList;
            syntaxList.AssertListCount(4);
            productDefinition.Id = id;
            productDefinition.Name = syntaxList.Values[0].GetStringValue();
            productDefinition.Description = syntaxList.Values[1].GetStringValue();

            productDefinition.BindSyntaxList(binder, syntaxList, 1);

            return productDefinition;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement(ItemType.GetItemTypeElementString());
            writer.WriteAttributeString("id", "#"+Id.ToString());
            base.WriteXML(writer);
            writer.WriteEndElement();
        }

        public override void WriteXMLGroup(XmlWriter writer)
        {
            if(ChildItems.Count > 0)
            {
                writer.WriteStartElement("Group");
                writer.WriteAttributeString("id", "#" + GetShapeDefinitionRepresentationId());
                writer.WriteAttributeString("name", Name);
                foreach (var item in ChildItems)
                {
                    item.WriteXMLGroup(writer);
                }
                writer.WriteEndElement();
            }
            else
            {
                Count++;
                writer.WriteStartElement("Part");
                writer.WriteAttributeString("id", "#" + GetShapeDefinitionRepresentationId());
                writer.WriteAttributeString("name", Name);
                writer.WriteEndElement();
            }
        }

        public override void WriteXMLOrderPart(XmlWriter writer)
        {
            if (ChildItems.Count > 0)
            {
                foreach (var item in ChildItems)
                {
                    item.WriteXMLOrderPart(writer);
                }
            }
            else
            {
                if (Count > 0)
                {
                    writer.WriteStartElement("OrderPart");
                    writer.WriteAttributeString("Amount", Count.ToString());
                    writer.WriteString("#" + GetShapeDefinitionRepresentationId());
                    writer.WriteEndElement();
                    Count = 0;
                }
            }
            
        }

        private string GetShapeDefinitionRepresentationId()
        {
            foreach(var refItem in RefParentItems)
            {
                foreach (var refParentItem in refItem.RefParentItems)
                {
                    if (refParentItem.GetStepItemTypeStr() == StepItemType.ShapeDefinitionRepresentation.ToString())
                        return refParentItem.Id.ToString();
                }
            }
            return "UnknowId";
        }
    }
}
