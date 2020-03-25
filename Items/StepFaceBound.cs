// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Xml;
using StepParser.Syntax;

namespace StepParser.Items
{
    public class StepFaceBound : StepTopologicalRepresentationItem
    {
        public override StepItemType ItemType => StepItemType.FaceBound;

        //public StepLoop Bound { get; set; }
        public bool Orientation { get; set; }

        protected StepFaceBound()
            : base(string.Empty, 0)
        {
        }
        
        internal static StepFaceBound CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            syntaxList.AssertListCount(3);
            var faceBound = new StepFaceBound();
            faceBound.SyntaxList = syntaxList;
            faceBound.Id = id;
            faceBound.Name = syntaxList.Values[0].GetStringValue();
            faceBound.BindSyntaxList(binder, syntaxList, 1);
            faceBound.Orientation = syntaxList.Values[2].GetBooleanValue();
            return faceBound;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement(ItemType.GetItemTypeElementString());

            base.WriteXML(writer);
            writer.WriteStartElement("Orientation");
            writer.WriteString(Orientation ? "true" : "false");
            writer.WriteEndElement();

            writer.WriteEndElement();
        }
    }
}
