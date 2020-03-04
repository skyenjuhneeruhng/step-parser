using System.Collections.Generic;
using StepParser.Tokens;

namespace StepParser.Syntax
{
    internal class StepIntegerSyntax : StepSyntax
    {
        public override StepSyntaxType SyntaxType => StepSyntaxType.Integer;

        public int Value { get; }

        public StepIntegerSyntax( int value )
            : base(-1, -1)
        {
            Value = value;
        }

        public StepIntegerSyntax(StepIntegerToken value)
            : base(value.Line, value.Column)
        {
            Value = value.Value;
        }

        public override IEnumerable<StepToken> GetTokens()
        {
            yield return new StepIntegerToken(Value, -1, -1);
        }
    }
}
