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
        public StepProductDefFormationWithSpecSource DefinitionFormation { get; set; }
        private List<StepProductDefinition> _children;
        public int Count { get; set; } = 0;

        public List<StepProductDefinition> Children
        {
            get { return _children; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                _children = value;
            }
        }


        public StepProductDefinition(string name, int id)
            : base(name, id)
        {
        }

        private StepProductDefinition()
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
        internal static StepProductDefinition CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var productDefinition = new StepProductDefinition();
            syntaxList.AssertListCount(4);
            productDefinition.Id = id;
            productDefinition.Name = syntaxList.Values[0].GetStringValue();
            productDefinition.Description = syntaxList.Values[1].GetStringValue();

            binder.BindValue(syntaxList.Values[2], v => productDefinition.DefinitionFormation = v.AsType<StepProductDefFormationWithSpecSource>());

            return productDefinition;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            if (DefinitionFormation != null)
            {
                DefinitionFormation.WriteXML(writer);
            }            
        }

        public void WriteXMLGroup(XmlWriter writer)
        {
            if (_children.Count > 0)
            {
                writer.WriteStartElement("Group");
                //writer.WriteAttributeString("id", "#" + Id.ToString());
                //writer.WriteAttributeString("name", Name);
                DefinitionFormation.WriteXMLGroup(writer);
                foreach (var child in _children)
                {
                    child.WriteXMLGroup(writer);
                }
                writer.WriteEndElement();
            }
            else
            {
                Count++;
                writer.WriteStartElement("Part");
                //writer.WriteAttributeString("id", "#" + Id.ToString());
                //writer.WriteAttributeString("name", Name);
                DefinitionFormation.WriteXMLGroup(writer);
                writer.WriteEndElement();
            }
        }

        public void WriteXMLOroderPart(XmlWriter writer)
        {
            if (Count > 0)
            {
                writer.WriteStartElement("OrderPart");
                writer.WriteAttributeString("Amount", Count.ToString());
                DefinitionFormation.WriteXMLOroderPart(writer);
                writer.WriteEndElement();
            }
        }

        public bool HasProduct(StepProductDefinition item)
        {
            foreach (var child in _children)
            {
                if (child.Id == item.Id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
