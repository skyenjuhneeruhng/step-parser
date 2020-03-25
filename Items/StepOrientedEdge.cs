// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Xml;
using StepParser.Syntax;

namespace StepParser.Items
{
    public class StepOrientedEdge : StepEdge
    {
        public override StepItemType ItemType => StepItemType.OrientedEdge;

        private StepEdge _edgeElement;

        public StepEdge EdgeElement
        {
            get { return _edgeElement; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                _edgeElement = value;
            }
        }

        public bool Orientation { get; set; }

        private StepOrientedEdge()
        {
        }

        public StepOrientedEdge(string name, StepVertex edgeStart, StepVertex edgeEnd, StepEdge edgeElement, bool orientation)
            : base(name, edgeStart, edgeEnd)
        {
            EdgeElement = edgeElement;
            Orientation = orientation;
        }


        internal static StepOrientedEdge CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var orientedEdge = new StepOrientedEdge();
            orientedEdge.SyntaxList = syntaxList;
            orientedEdge.Id = id;
            syntaxList.AssertListCount(5);
            orientedEdge.Name = syntaxList.Values[0].GetStringValue();
            binder.BindValue(syntaxList.Values[1], v => orientedEdge.EdgeStart = v.AsType<StepVertex>());
            binder.BindValue(syntaxList.Values[2], v => orientedEdge.EdgeEnd = v.AsType<StepVertex>());
            binder.BindValue(syntaxList.Values[3], v => orientedEdge.EdgeElement = v.AsType<StepEdge>());
            orientedEdge.Orientation = syntaxList.Values[4].GetBooleanValue();
            return orientedEdge;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement(_edgeElement.ItemType.GetItemTypeElementString());
            _edgeElement.WriteXML(writer);
            writer.WriteEndElement();
        }
    }
}
