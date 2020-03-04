using System.Collections.Generic;
using StepParser.Tokens;

namespace StepParser.Syntax
{
    internal class StepAutoSyntax : StepSyntax
    {
        public override StepSyntaxType SyntaxType => StepSyntaxType.Auto;

        public StepAsteriskToken Token { get; private set; }

        public StepAutoSyntax()
            : this(StepAsteriskToken.Instance)
        {
        }

        public StepAutoSyntax(StepAsteriskToken token)
            : base(token.Line, token.Column)
        {
            Token = token;
        }

        public override IEnumerable<StepToken> GetTokens()
        {
            yield return Token;
        }
    }
}
