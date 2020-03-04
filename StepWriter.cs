using StepParser.Items;
using StepParser.Syntax;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace StepParser
{
    internal class StepWriter
    {
        private StepFile _file;
        private bool _inlineReferences;
        private Dictionary<StepRepresentationItem, int> _itemMap;
        private XmlWriter _xmlWriter;

        public StepWriter(StepFile stepFile, bool inlineReferences, XmlWriter xmlWriter)
        {
            _file = stepFile;
            _itemMap = new Dictionary<StepRepresentationItem, int>();
            _inlineReferences = inlineReferences;
            _xmlWriter = xmlWriter;

            SetRelationShip();
        }

        public void Save()
        {            
            // output header
            WriteHeader();

            // data section
            // Write Parts Section
            _xmlWriter.WriteStartElement("Parts");
            foreach (var item in _file.Items)
            {
                if (item.ItemType == StepItemType.ShapeDefinitionRepresentation)
                {
                    item.WriteXML(_xmlWriter);
                }
            }
            _xmlWriter.WriteEndElement();

            // Write Group Section

            _xmlWriter.WriteStartElement("Structure");
            var rootGroups = GetRootGroups();
            foreach (var item in rootGroups)
            {
                WriteGroup(item);
            }
            _xmlWriter.WriteEndElement();

            // Write Orders

            _xmlWriter.WriteStartElement("OrderList");
            foreach (var item in _file.Items)
            {
                if (item.ItemType == StepItemType.ProductDefinition)
                {
                    ((StepProductDefinition)item).WriteXMLOroderPart(_xmlWriter);
                }
            }
            _xmlWriter.WriteEndElement();
        }

        private void WriteHeader()
        {
            _xmlWriter.WriteStartElement("BasicData");
            WriteXmlElement("FileName", _file.Name);
            WriteXmlElement("ISO", StepFile.MagicHeader);
            WriteXmlElement("Standard", _file.Description);
            WriteXmlElement("DateTime", _file.Timestamp.ToString());
            _xmlWriter.WriteEndElement();
        }

        private void WriteXmlElement(string name, string value)
        {
            _xmlWriter.WriteStartElement(name);
            _xmlWriter.WriteString(value);
            _xmlWriter.WriteEndElement();
        }

        private void WriteGroup(StepRepresentationItem item)
        {
            if (item.ItemType == StepItemType.ProductDefinition)
            {
                ((StepProductDefinition)item).WriteXMLGroup(_xmlWriter);
            }
        }

        private List<StepProductDefinition> GetRootGroups()
        {
            var rootGroups = new List<StepProductDefinition>();

            foreach (var item in _file.Items)
            {
                bool isRoot = true;
                if (item.ItemType == StepItemType.ProductDefinition)
                {
                    foreach(var subItem in _file.Items)
                    {
                        if (subItem.ItemType == StepItemType.ProductDefinition)
                        {
                            if (((StepProductDefinition)subItem).HasProduct((StepProductDefinition)item))
                            {
                                isRoot = false;
                            }
                        }
                    }
                    if (isRoot == true)
                    {
                        rootGroups.Add((StepProductDefinition)item);
                    }
                }
            }

            return rootGroups;
        }

        public void SetRelationShip()
        {
            for(int idx = 0; idx < _file.Items.Count; idx++)
            {
                if (_file.Items[idx].ItemType == StepItemType.ShapeDefinitionRepresentation)
                {
                    var relationShip = GetShapeRelationShip((StepShapeDefinitionRepresentation)_file.Items[idx]);
                    if (relationShip != null)
                    {
                        ((StepShapeDefinitionRepresentation)_file.Items[idx]).SetShapePresentationRelationShip(relationShip);
                    }
                    
                }
                else if (_file.Items[idx].ItemType == StepItemType.AdvancedBrepShapeRepresentation)
                {
                    List<StepStyledItem> styledItems = GetStyledItems((StepAdvancedBrepShapeRepresentation)_file.Items[idx]);
                    ((StepAdvancedBrepShapeRepresentation)_file.Items[idx]).SetStyledItems(styledItems);
                }
                else if (_file.Items[idx].ItemType == StepItemType.ProductDefinition)
                {
                    List<StepProductDefinition> nextComponents = GetNextComponent((StepProductDefinition)_file.Items[idx]);
                    ((StepProductDefinition)_file.Items[idx]).Children = nextComponents;
                }
            }
            
        }

        public StepShapeRepresentationRelationShip GetShapeRelationShip(StepShapeDefinitionRepresentation item)
        {
            foreach (var eachItem in _file.Items)
            {
                if (eachItem.ItemType == StepItemType.ShapeRepresentationRelationship)
                {
                    var itemInstance = (StepShapeRepresentationRelationShip)eachItem;
                    if (itemInstance.UsedRepresentation != null && itemInstance.UsedRepresentation.Id == item.UsedRepresentation.Id)
                    {
                        return itemInstance;
                    }
                }
            }
            return null;
        }

        public List<StepStyledItem> GetStyledItems(StepAdvancedBrepShapeRepresentation item)
        {
            var styledItems = new List<StepStyledItem>();
            if (item.UsedSolidBrepList != null && item.UsedSolidBrepList.Count > 0)
            {
                foreach(var solidBrep in item.UsedSolidBrepList)
                {
                    foreach (var eachItem in _file.Items)
                    {
                        if (eachItem.ItemType == StepItemType.StyledItem)
                        {
                            var itemInstance = (StepStyledItem)eachItem;
                            if (itemInstance.UsedSolidBrep != null && itemInstance.UsedSolidBrep.Id == solidBrep.Id)
                            {
                                styledItems.Add(itemInstance);
                            }
                        }
                    }
                }
            }

            return styledItems;
        }

        public List<StepProductDefinition> GetNextComponent(StepProductDefinition item)
        {
            var nextComponents = new List<StepProductDefinition>();
            foreach (var eachItem in _file.Items)
            {
                if (eachItem.ItemType == StepItemType.NextAssemblyUsageOccurrence)
                {
                    var itemInstance = (StepNextAssemblyUsageOccrrence)eachItem;
                    if (itemInstance.Parent != null && itemInstance.Parent.Id == item.Id)
                    {
                        nextComponents.Add(itemInstance.Child);
                    }
                }
            }
            return nextComponents;
        }

        public StepSyntax GetItemSyntax(StepRepresentationItem item)
        {
            if (_inlineReferences)
            {
                var parameters = new StepSyntaxList(-1, -1, item.GetParameters(this));
                return new StepSimpleItemSyntax(item.ItemType.GetItemTypeString(), parameters, item.Id);
            }
            else
            {
                return new StepEntityInstanceReferenceSyntax(_itemMap[item]);
            }
        }

        public StepSyntax GetItemSyntaxOrAuto(StepRepresentationItem item)
        {
            return item == null
                ? new StepAutoSyntax()
                : GetItemSyntax(item);
        }

        public static StepEnumerationValueSyntax GetBooleanSyntax(bool value)
        {
            var text = value ? "T" : "F";
            return new StepEnumerationValueSyntax(text);
        }

        internal static IEnumerable<string> SplitStringIntoParts(string str, int maxLength = 256)
        {
            var parts = new List<string>();
            if (str != null)
            {
                int offset = 0;
                while (offset < str.Length)
                {
                    var length = Math.Min(maxLength, str.Length - offset);
                    parts.Add(str.Substring(offset, length));
                    offset += length;
                }
            }
            else
            {
                parts.Add(string.Empty);
            }

            return parts;
        }

        private string toCamel(string input)
        {
            return new CultureInfo("en").TextInfo.ToTitleCase(input.ToLower().Replace("_", " ")).Replace(" ", "");
        }
    }
}
