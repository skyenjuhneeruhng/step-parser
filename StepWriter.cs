using StepParser.Items;
using StepParser.Syntax;
using StepParser.ViewModel;
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
        private List<StepRepresentationItem> _relatedItems = new List<StepRepresentationItem>();

        public StepWriter(StepFile stepFile, bool inlineReferences, XmlWriter xmlWriter)
        {
            try
            {
                _file = stepFile;
                _itemMap = new Dictionary<StepRepresentationItem, int>();
                _inlineReferences = inlineReferences;
                _xmlWriter = xmlWriter;

                SetRelationShip();
            }
            catch (Exception ex)
            {
                LogWriter.Instance.WriteErrorLog(ex);
            }
        }

        /// <summary>
        /// Save
        /// </summary>
        public void Save()
        {  
            try
            {
                // output header
                WriteHeader();

                // data section
                // Write Parts Section
                _xmlWriter.WriteStartElement("Parts");
                int countPart = 0;
                List<StepRepresentationItem> _singleItems = GetSingleGroups(_relatedItems);
                foreach (var item in _singleItems)
                {
                    if(item.RefParentItems != null)
                    {
                        foreach (var parentItem in item.RefParentItems)
                        {
                            foreach (var refParentItem in parentItem.RefParentItems)
                            {
                                if (refParentItem.GetStepItemTypeStr() == StepItemType.ShapeDefinitionRepresentation.ToString())
                                {
                                    refParentItem.WriteXML(_xmlWriter);
                                    countPart++;
                                }
                            }
                        }
                    }                    
                }
                _xmlWriter.WriteEndElement();

                // Write Group Section

                _xmlWriter.WriteStartElement("Structure");
                var rootGroups = GetRootGroups(_relatedItems);
                foreach (var item in rootGroups)
                {
                    if (item.ItemType == StepItemType.ProductDefinition)
                    {
                        ((StepProductDefinition)item).WriteXMLGroup(_xmlWriter);
                    }
                }
                _xmlWriter.WriteEndElement();

                // Write Orders

                _xmlWriter.WriteStartElement("OrderList");
                foreach (var item in rootGroups)
                {
                    if (item.ItemType == StepItemType.ProductDefinition)
                    {
                        ((StepProductDefinition)item).WriteXMLOrderPart(_xmlWriter);
                    }
                }
                _xmlWriter.WriteEndElement();
            }
            catch (Exception ex)
            {
                LogWriter.Instance.WriteErrorLog(ex);
            }
        }

        /// <summary>
        /// WriteHeader
        /// </summary>
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
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<StepRepresentationItem> GetRootGroups(List<StepRepresentationItem> stepRepresentationItems)
        {
            var rootGroups = new List<StepRepresentationItem>();

            foreach (var item in stepRepresentationItems)
            {               
                if (item.ParentItems.Count == 0)
                    rootGroups.Add(item);
            }
            return rootGroups;
        }

        /// <summary>
        /// GetSigleGroups
        /// </summary>
        /// <param name="stepRepresentationItems"></param>
        /// <returns></returns>
        private List<StepRepresentationItem> GetSingleGroups(List<StepRepresentationItem> stepRepresentationItems)
        {
            var singleGroups = new List<StepRepresentationItem>();

            foreach (var item in stepRepresentationItems)
            {
                if(item.ChildItems.Count == 0)
                    singleGroups.Add(item);
            }
            return singleGroups;
        }

        /// <summary>
        /// SetRelationShip
        /// </summary>
        public void SetRelationShip()
        {
            try
            {
                List<StepRepresentationItem> stepStyleItems = GetStyledItems();
                for (int idx = 0; idx < _file.Items.Count; idx++)
                {
                    StepRepresentationItem item = _file.Items[idx];
                    UpdateBindStyleItems(item, stepStyleItems);
                    if (item.GetStepItemTypeStr() == StepItemType.ProductDefinition.ToString())
                    {
                        AddRelatedItems(item);
                    }
                    else if (item.GetStepItemTypeStr() == StepItemType.NextAssemblyUsageOccurrence.ToString())
                    {
                        ((StepNextAssemblyUsageOccrrence)item).Parent.ChildItems.Add(((StepNextAssemblyUsageOccrrence)item).Child);
                        ((StepNextAssemblyUsageOccrrence)item).Child.ParentItems.Add(((StepNextAssemblyUsageOccrrence)item).Parent);
                        AddRelatedItems(((StepNextAssemblyUsageOccrrence)item).Child); //this is ProductDefinition item
                        AddRelatedItems(((StepNextAssemblyUsageOccrrence)item).Parent); //this is ProductDefinition item
                    }
                    else if (item.GetStepItemTypeStr() == StepItemType.ShapeDefinitionRepresentation.ToString())
                    {
                        SetShapRelationShip(item, StepItemType.ShapeRepresentationRelationship);
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.Instance.WriteErrorLog(ex);
            }
            
        }

        /// <summary>
        /// AddRelatedItems
        /// </summary>
        /// <param name="item"></param>
        private void AddRelatedItems(StepRepresentationItem item)
        {
            if (_relatedItems.Find(x => x.Id == item.Id) == null)
                _relatedItems.Add(item);
        }

        /// <summary>
        /// SetShapRelationShip
        /// </summary>
        /// <param name="inputItem"></param>
        /// <param name="itemType"></param>
        public void SetShapRelationShip(StepRepresentationItem inputItem, StepItemType itemType)
        {
            foreach (var eachItem in _file.Items)
            {
                bool isGetRelationShip = false;
                if (inputItem == eachItem)
                    continue;
                if (eachItem.GetStepItemTypeStr() == itemType.ToString())
                {
                    if(eachItem.RefChildItems.Count > 0)
                    {
                        foreach (var refObj in eachItem.RefChildItems)
                        {
                            if(refObj.Value != null && refObj.Value.Count > 0)
                            {
                                foreach (var refItem in refObj.Value)
                                {
                                    if(!isGetRelationShip)
                                    {
                                        foreach (var inputRefObj in inputItem.RefChildItems)
                                        {
                                            if (inputRefObj.Value != null && inputRefObj.Value.Count > 0)
                                            {
                                                if (inputRefObj.Value.Find(x => ((StepRepresentationItem)x).Id == ((StepRepresentationItem)refItem).Id) != null)
                                                {
                                                    isGetRelationShip = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        inputItem.AddRefObjs((StepRepresentationItem)refItem);
                                        inputItem.ContainCrossRef = true; //Contains cross ref item
                                        ((StepRepresentationItem)refItem).IsCrossRef = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// UpdateBindStyleItems
        /// </summary>
        /// <param name="item"></param>
        /// <param name="stepStyleItems"></param>
        public void UpdateBindStyleItems(StepRepresentationItem item, List<StepRepresentationItem> stepStyleItems)
        {
            List<StepRepresentationItem> bindStyleItems = new List<StepRepresentationItem>();
            foreach (var obj in item.RefChildItems)
            {
                if (obj.Value != null && obj.Value.Count > 0)
                {
                    foreach (var stepItem in stepStyleItems)
                    {
                        foreach (var objStepItem in stepItem.RefChildItems)
                        {
                            if (objStepItem.Value != null && objStepItem.Value.Count > 0)
                            {
                                foreach (var iStepItem in objStepItem.Value)
                                {
                                    if (obj.Value.Find(x => ((StepRepresentationItem)x).Id == ((StepRepresentationItem)iStepItem).Id) != null)
                                    {
                                        bindStyleItems.Add(stepItem);
                                    }
                                }
                            }
                        }

                    }

                }
            }
            if (bindStyleItems.Count > 0)
            {
                foreach (var stepItem in bindStyleItems)
                {
                    item.AddRefObjs(stepItem);
                }
            }
        }


        /// <summary>
        /// GetStyledItems
        /// </summary>
        /// <returns></returns>
        public List<StepRepresentationItem> GetStyledItems()
        {
            var styledItems = new List<StepRepresentationItem>();
            foreach (var eachItem in _file.Items)
            {
                if (eachItem.GetStepItemTypeStr() == StepItemType.StyledItem.ToString())
                {
                    styledItems.Add(eachItem);
                }
            }
            return styledItems;
        }

        /// <summary>
        /// SplitStringIntoParts
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
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
    }
}
