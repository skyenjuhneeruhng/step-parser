namespace StepParser.Items
{
    public abstract class StepVertex : StepTopologicalRepresentationItem
    {
        protected StepVertex(string name)
            : base(name, 0)
        {
        }
    }
}
