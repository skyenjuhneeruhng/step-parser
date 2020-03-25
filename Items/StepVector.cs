
using System;
using System.Collections.Generic;
using System.Xml;
using StepParser.Syntax;

namespace StepParser.Items
{
    public class StepVector : StepGeometricRepresentationItem
    {
        public override StepItemType ItemType => StepItemType.Vector;

        private StepDirection _direction;
        public StepDirection Direction
        {
            get { return _direction; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                _direction = value;
            }
        }
        public double Length { get; set; }

        private StepVector()
            : base(string.Empty, 0)
        {
        }

        public StepVector(string name, StepDirection direction, double length)
            : base(name, 0)
        {
            Direction = direction;
            Length = length;
        }

        internal static StepVector CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList)
        {
            var vector = new StepVector();
            vector.SyntaxList = syntaxList;
            syntaxList.AssertListCount(3);
            vector.Name = syntaxList.Values[0].GetStringValue();
            binder.BindValue(syntaxList.Values[1], v => vector.Direction = v.AsType<StepDirection>());
            vector.Length = syntaxList.Values[2].GetRealVavlue();
            return vector;
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement(ItemType.GetItemTypeElementString());
            _direction.WriteXML(writer);
            writer.WriteEndElement();
        }
    }
}
