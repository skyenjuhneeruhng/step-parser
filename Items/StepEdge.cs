// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Xml;
using StepParser.Syntax;

namespace StepParser.Items
{
    public abstract class StepEdge : StepTopologicalRepresentationItem
    {
        public StepVertex EdgeStart { get; set; }
        public StepVertex EdgeEnd { get; set; }

        protected StepEdge()
            : base(string.Empty, 0)
        {
        }

        protected StepEdge(string name, StepVertex edgeStart, StepVertex edgeEnd)
            : base(name, 0)
        {
            EdgeStart = edgeStart;
            EdgeEnd = edgeEnd;
        }

        internal override IEnumerable<StepRepresentationItem> GetReferencedItems()
        {
            if (EdgeStart != null)
            {
                yield return EdgeStart;
            }

            if (EdgeEnd != null)
            {
                yield return EdgeEnd;
            }
        }

        internal override IEnumerable<StepSyntax> GetParameters(StepWriter writer)
        {
            foreach (var parameter in base.GetParameters(writer))
            {
                yield return parameter;
            }

            yield return writer.GetItemSyntaxOrAuto(EdgeStart);
            yield return writer.GetItemSyntaxOrAuto(EdgeEnd);
        }

        internal override void WriteXML(XmlWriter writer)
        {
            // Start Point
            writer.WriteStartElement("StartPoint");
            EdgeStart.WriteXML(writer);
            writer.WriteEndElement();

            // End Point
            writer.WriteStartElement("EndPoint");
            EdgeStart.WriteXML(writer);
            writer.WriteEndElement();
        }
    }
}
