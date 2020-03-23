using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Xml;
using StepParser.Syntax;

namespace StepParser.Items
{
    public abstract partial class StepRepresentationItem
    {
        public virtual StepItemType ItemType { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Keyword { get; set; }

        public Dictionary<string, List<object>> RefObjs { get; set; }

        public List<object> UnRefObj { get; set; }

        protected StepRepresentationItem(string name, int id)
        {
            Name = name;
            Id = id;
            RefObjs = new Dictionary<string, List<object>>();
            UnRefObj = new List<object>();
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
            foreach (var obj in RefObjs)
            {
                if (obj.Value != null && obj.Value.Count > 0)
                {
                    if(obj.Value.Count > 1)
                        writer.WriteStartElement(obj.Key.ToString()+"s"); 
                    foreach (var item in obj.Value)
                    {
                        if (item != null)
                        {
                            if (item is StepRepresentationItem)
                            {
                                if (((StepRepresentationItem)item).ItemType == StepItemType.Unknown)
                                {
                                    writer.WriteStartElement(ToCamel(((StepRepresentationItem)item).Keyword));
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

        public void AddRefObjs(StepRepresentationItem item)
        {
            string key = item.ItemType == StepItemType.Unknown ? ToCamel(item.Keyword) : item.ItemType.ToString();
            if (!RefObjs.ContainsKey(key))
            {
                List<object> list = new List<object>();
                list.Add(item);
                RefObjs.Add(key, list);
            }
            else
            {
                List<object> list = RefObjs[key];
                list.Add(item);
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
                        var refers = stepSyntax.GetValueList();
                        foreach (var refer in refers.Values)
                        {
                            if (refer is StepSimpleItemSyntax || refer is StepEntityInstanceReferenceSyntax || refer is StepAutoSyntax)
                                binder.BindValue(refer, v => AddRefObjs(v.Item));
                            else
                            {
                                UnRefObj.Add(refer);
                                //Debug.WriteLine($"Unsupported binding for syntax {referList.Values[j].SyntaxType} at {referList.Values[j].Line}, {referList.Values[j].Column}, {dynamicItem.Keyword}, {dynamicItem.Id}");
                            }
                        }
                    }
                    else
                    {
                        if (stepSyntax is StepSimpleItemSyntax || stepSyntax is StepEntityInstanceReferenceSyntax || stepSyntax is StepAutoSyntax)
                            binder.BindValue(stepSyntax, v => AddRefObjs(v.Item));
                        else
                        {
                            UnRefObj.Add(stepSyntax);
                            //Debug.WriteLine($"Unsupported binding for syntax {stepSyntax.SyntaxType} at {stepSyntax.Line}, {stepSyntax.Column}, {dynamicItem.Keyword}, {dynamicItem.Id} ");
                        }
                    }
                }
                ind++;
            }
        }


        internal static string ToCamel(string input)
        {
            return new CultureInfo("en").TextInfo.ToTitleCase(input.ToLower().Replace("_", " ")).Replace(" ", "");
        }
    }
}
