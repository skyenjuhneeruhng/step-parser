using System.Collections.Generic;
using StepParser.Tokens;

namespace StepParser.Syntax
{
    internal class StepOmittedSyntax : StepSyntax
    {
        public override StepSyntaxType SyntaxType => StepSyntaxType.Omitted;

        public StepOmittedSyntax(StepOmittedToken value)
            : base(value.Line, value.Column)
        {
        }

        public override IEnumerable<StepToken> GetTokens()
        {
            yield return StepOmittedToken.Instance;
        }
    }
}
