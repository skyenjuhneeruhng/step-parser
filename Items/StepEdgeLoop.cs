// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Xml;
using StepParser.Syntax;

namespace StepParser.Items
{
    public class StepEdgeLoop : StepLoop
    {
        public override StepItemType ItemType => StepItemType.EdgeLoop;

        public List<StepOrientedEdge> EdgeList { get; private set; }

        public StepEdgeLoop(string name, IEnumerable<StepOrientedEdge> edgeList)
            : base(name)
        {
            EdgeList = new List<StepOrientedEdge>(edgeList);
        }

        public StepEdgeLoop(string name, params StepOrientedEdge[] edgeList)
            : this(name, (IEnumerable<StepOrientedEdge>)edgeList)
        {
        }

        internal static StepEdgeLoop CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            syntaxList.AssertListCount(2);
            var edgeSyntaxList = syntaxList.Values[1].GetValueList();
            var edgeLoop = new StepEdgeLoop(string.Empty, new StepOrientedEdge[edgeSyntaxList.Values.Count]);
            edgeLoop.SyntaxList = syntaxList;
            edgeLoop.Id = id;
            edgeLoop.Name = syntaxList.Values[0].GetStringValue();
            for (int i = 0; i < edgeSyntaxList.Values.Count; i++)
            {
                var j = i;
                binder.BindValue(edgeSyntaxList.Values[j], v => edgeLoop.EdgeList[j] = v.AsType<StepOrientedEdge>());
            }

            return edgeLoop;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement(ItemType.GetItemTypeElementString());
            for (int idx = 0; idx < EdgeList.Count; idx++)
            {
                EdgeList[idx].WriteXML(writer);
            }
            writer.WriteEndElement();
        }
    }
}
