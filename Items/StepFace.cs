using System.Collections.Generic;
using System.Linq;
using StepParser.Syntax;

namespace StepParser.Items
{
    public abstract class StepFace : StepTopologicalRepresentationItem
    {
        public List<StepFaceBound> Bounds { get; } = new List<StepFaceBound>();

        public StepFace(string name)
            : base(name, 0)
        {
        }
    }
}
