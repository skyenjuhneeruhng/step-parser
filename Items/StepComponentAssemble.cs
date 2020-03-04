using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace StepParser.Items
{
    public abstract class StepComponentAssemble: StepRepresentationItem
    {
        private string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                _description = value;
            }
        }

        protected StepComponentAssemble(string name, int id)
            : base(name, id)
        {
        }

        internal override void WriteXML(XmlWriter writer)
        {
        }
    }
}
