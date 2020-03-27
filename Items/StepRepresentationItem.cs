using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Xml;
using StepParser.Base;
using StepParser.Syntax;
using StepParser.ViewModel;

namespace StepParser.Items
{
    public abstract partial class StepRepresentationItem
    {
        public virtual StepItemType ItemType { get; set; }

        public int Id { get; set; }

        private string _name = string.Empty;
        public string Name
        {
            get { return _name; }
            set { 
                if(!string.IsNullOrEmpty(value))
                {
                    _name = Utils.FromStringToReadableString(value);
                }
                else
                    _name = value; 
            }
        }


        public string Keyword { get; set; }

        public Dictionary<string, List<StepRepresentationItem>> RefChildItems { get; set; }
        public List<StepRepresentationItem> RefParentItems { get; set; }

        public List<object> UnRefObjs { get; set; }

        internal StepSyntaxList SyntaxList { get; set; }

        private int _count = 0;
        public int Count { get => _count; set => _count = value; }

        public bool IsCrossRef { get; set; }

        public bool ContainCrossRef { get; set; }

        public List<StepRepresentationItem> ParentItems { get; set; }

        public List<StepRepresentationItem> ChildItems { get; set; }

        protected StepRepresentationItem(string name, int id)
        {
            Name = name;
            Id = id;
            RefChildItems = new Dictionary<string, List<StepRepresentationItem>>();
            UnRefObjs = new List<object>();
            IsCrossRef = false;
            ContainCrossRef = true;
            ParentItems = new List<StepRepresentationItem>();
            ChildItems = new List<StepRepresentationItem>();
            RefParentItems = new List<StepRepresentationItem>();
        }

        internal virtual IEnumerable<StepRepresentationItem> GetReferencedItems()
        {
            yield break;
        }

        internal virtual IEnumerable<StepSyntax> GetParameters(StepWriter writer)
        {
            yield return new StepStringSyntax(Name);
        }

        internal virtual void WriteXML(XmlWriter writer)
        {
            try
            {

                foreach (var obj in UnRefObjs)
                {
                    if (obj is StepSyntax)
                    {
                        StepSyntax stepSyntax = obj as StepSyntax;
                        try
                        {
                            if (obj is StepStringSyntax)
                            {
                                string output = Utils.FromStringToReadableString(stepSyntax.GetStringValue());
                                if(!string.IsNullOrEmpty(output)) {
                                    writer.WriteStartElement(stepSyntax.SyntaxType.ToString());
                                    writer.WriteAttributeString("value", Utils.FromStringToReadableString(stepSyntax.GetStringValue()));
                                    writer.WriteEndElement();
                                }
                            }
                            else
                            {
                                writer.WriteStartElement(stepSyntax.SyntaxType.ToString());
                                if (obj is StepRealSyntax)
                                    writer.WriteAttributeString("value", stepSyntax.GetRealVavlue().ToString());
                                if (obj is StepIntegerSyntax)
                                    writer.WriteAttributeString("value", stepSyntax.GetIntegerValue().ToString());
                                if (obj is StepEnumerationValueSyntax)
                                    writer.WriteAttributeString("value", stepSyntax.GetEnumerationValue().ToString());
                                writer.WriteEndElement();
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            LogWriter.Instance.WriteErrorLog(ex + " step syntax type " + stepSyntax.SyntaxType.ToString());
                        }
                    }
                }
                foreach (var obj in RefChildItems)
                {
                    if (obj.Value != null && obj.Value.Count > 0)
                    {
                        if (obj.Value.Count > 1)
                            writer.WriteStartElement(obj.Key.ToString() + "s");
                        foreach (var item in obj.Value)
                        {
                            if (item != null)
                            {
                                if (item is StepRepresentationItem)
                                {
                                    if (((StepRepresentationItem)item).ItemType == StepItemType.Unknown)
                                    {
                                        writer.WriteStartElement(((StepRepresentationItem)item).GetStepItemTypeStr());
                                        writer.WriteAttributeString("id", '#' + ((StepRepresentationItem)item).Id.ToString());
                                        ((StepRepresentationItem)item).WriteXML(writer);
                                        writer.WriteEndElement();
                                    }
                                    else
                                    {
                                        ((StepRepresentationItem)item).WriteXML(writer);
                                    }
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
            catch (Exception ex)
            {
                LogWriter.Instance.WriteErrorLog(ex);
            }
        }

        public void AddRefObjs(StepRepresentationItem item)
        {
            try
            {
                if(item != null)
                {
                    string key = item.ItemType == StepItemType.Unknown ? ToCamel(item.Keyword) : item.ItemType.ToString();
                    if (!RefChildItems.ContainsKey(key))
                    {
                        List<StepRepresentationItem> list = new List<StepRepresentationItem>();
                        list.Add(item);
                        RefChildItems.Add(key, list);
                    }
                    else
                    {
                        List<StepRepresentationItem> list = RefChildItems[key];
                        list.Add(item);
                    }
                    if (item.RefParentItems.Find(x => x.Id == Id) == null)
                        item.RefParentItems.Add(this);
                }
                else
                {
                    //do nothing
                }
            }
            catch (Exception ex)
            {
                LogWriter.Instance.WriteErrorLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="syntaxList"></param>
        internal void BindSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int startInd = 1, int endInd = -1)
        {
            endInd = endInd == -1 ? syntaxList.Values.Count : endInd;
            int ind = 0;
            foreach (StepSyntax stepSyntax in syntaxList.Values)
            {
                if(ind >= startInd && ind < endInd)
                {
                    if (stepSyntax.SyntaxType == StepSyntaxType.List)
                    {
                        BindSyntaxList(binder, (StepSyntaxList)stepSyntax, 0);
                    }
                    else
                    {
                        if (stepSyntax is StepSimpleItemSyntax || stepSyntax is StepEntityInstanceReferenceSyntax || stepSyntax is StepAutoSyntax)
                            binder.BindValue(stepSyntax, v => AddRefObjs(v.Item));
                        else
                        {
                            UnRefObjs.Add(stepSyntax);
                        }
                    }
                }
                ind++;
            }
        }

        /// <summary>
        /// WriteXMLGroup
        /// </summary>
        /// <param name="writer"></param>
        public virtual void WriteXMLGroup(XmlWriter writer)
        {
            if (RefChildItems.Count > 0)
            {
                foreach (var obj in RefChildItems)
                {
                    if (obj.Value != null && obj.Value.Count > 0)
                    {
                        if (obj.Value.Count > 1)
                        {
                            writer.WriteStartElement("Group");
                            writer.WriteAttributeString("id", "#" + Id.ToString());
                            writer.WriteAttributeString("name", Name);
                        }
                        foreach (var item in obj.Value)
                        {
                            if (item != null)
                            {
                                if (item is StepRepresentationItem)
                                {
                                    ((StepRepresentationItem)item).WriteXMLGroup(writer);
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
            else
            {
                writer.WriteStartElement("Part");
                writer.WriteAttributeString("id", "#" + Id.ToString());
                writer.WriteAttributeString("name", Name);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        public virtual void WriteXMLOrderPart(XmlWriter writer)
        {
            
        }


        /// <summary>
        /// GetStepItemTypeStr
        /// </summary>
        /// <returns></returns>
        internal string GetStepItemTypeStr()
        {
            if (ItemType == StepItemType.Unknown)
                return new CultureInfo("en").TextInfo.ToTitleCase(Keyword.ToLower().Replace("_", " ")).Replace(" ", "");
            else
                return ItemType.ToString();
        }

        internal static string ToCamel(string input)
        {
            return new CultureInfo("en").TextInfo.ToTitleCase(input.ToLower().Replace("_", " ")).Replace(" ", "");
        }
    }
}
