using System.Collections.Generic;
using StepParser.Tokens;

namespace StepParser.Syntax
{
    internal class StepEntityInstanceReferenceSyntax : StepSyntax
    {
        public override StepSyntaxType SyntaxType => StepSyntaxType.EntityInstanceReference;

        public int Id { get; }

        public StepEntityInstanceReferenceSyntax(int id)
            : base(-1, -1)
        {
            Id = id;
        }

        public StepEntityInstanceReferenceSyntax(StepEntityInstanceToken itemInstance)
            : base(itemInstance.Line, itemInstance.Column)
        {
            Id = itemInstance.Id;
        }

        public override IEnumerable<StepToken> GetTokens()
        {
            yield return new StepEntityInstanceToken(Id, -1, -1);
        }
    }
}
