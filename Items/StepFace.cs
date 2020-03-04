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

        internal override IEnumerable<StepSyntax> GetParameters(StepWriter writer)
        {
            foreach (var parameter in base.GetParameters(writer))
            {
                yield return parameter;
            }

            yield return new StepSyntaxList(Bounds.Select(b => writer.GetItemSyntax(b)));
        }
    }
}
