// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Xml;
using StepParser.Syntax;

namespace StepParser.Items
{
    public class StepEdgeCurve : StepEdge
    {
        public override StepItemType ItemType => StepItemType.EdgeCurve;

        private StepCurve _edgeGeometry;

        public StepCurve EdgeGeometry
        {
            get { return _edgeGeometry; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                _edgeGeometry = value;
            }
        }

        public bool IsSameSense { get; set; }

        private StepEdgeCurve()
            : base()
        {
        }

        public StepEdgeCurve(string name, StepVertex edgeStart, StepVertex edgeEnd, StepCurve edgeGeometry, bool isSameSense)
            : base(name, edgeStart, edgeEnd)
        {
            EdgeGeometry = edgeGeometry;
            IsSameSense = isSameSense;
        }
        
        internal static StepEdgeCurve CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var edgeCurve = new StepEdgeCurve();
            edgeCurve.SyntaxList = syntaxList;
            edgeCurve.Id = id;
            syntaxList.AssertListCount(5);
            edgeCurve.Name = syntaxList.Values[0].GetStringValue();
            binder.BindValue(syntaxList.Values[1], v => edgeCurve.EdgeStart = v.AsType<StepVertex>());
            binder.BindValue(syntaxList.Values[2], v => edgeCurve.EdgeEnd = v.AsType<StepVertex>());
            binder.BindValue(syntaxList.Values[3], v => edgeCurve.EdgeGeometry = v.AsType<StepCurve>());
            edgeCurve.IsSameSense = syntaxList.Values[4].GetBooleanValue();
            return edgeCurve;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            base.WriteXML(writer);
            writer.WriteStartElement("CurveGeometry");
            _edgeGeometry.WriteXML(writer);
            writer.WriteEndElement();
        }
    }
}
