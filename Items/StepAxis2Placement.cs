using System;
using System.Xml;

namespace StepParser.Items
{
    public abstract class StepAxis2Placement : StepPlacement
    {
        private StepCartesianPoint _location;
        private StepDirection _refDirection;

        public StepCartesianPoint Location
        {
            get { return _location; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                _location = value;
            }
        }

        public StepDirection RefDirection
        {
            get { return _refDirection; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                _refDirection = value;
            }
        }

        protected StepAxis2Placement(string name)
            : base(name)
        {
        }

        internal override void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement("Location");
            _location.WriteXML(writer);
            writer.WriteEndElement();

            writer.WriteStartElement("RefDirection");
            _refDirection.WriteXML(writer);
            writer.WriteEndElement();
        }
    }
}
