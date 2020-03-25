// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Xml;
using StepParser.Syntax;

namespace StepParser.Items
{
    public class StepAxis2Placement3D : StepGeometricRepresentationItem
    {
        public override StepItemType ItemType => StepItemType.AxisPlacement3D;

        private StepAxis2Placement3D()
            : base(string.Empty, 0)
        {
        }


        internal static StepAxis2Placement3D CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var axis = new StepAxis2Placement3D();
            axis.SyntaxList = syntaxList;
            syntaxList.AssertListCount(4);
            axis.Id = id;
            axis.Name = syntaxList.Values[0].GetStringValue();
            axis.BindSyntaxList(binder, syntaxList, 1);
            return axis;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement(ItemType.GetItemTypeElementString());
            base.WriteXML(writer);
            writer.WriteEndElement();
        }
    }
}
