using System.Collections.Generic;
using StepParser.Tokens;

namespace StepParser.Syntax
{
    internal class StepSimpleItemSyntax : StepItemSyntax
    {
        public override StepSyntaxType SyntaxType => StepSyntaxType.SimpleItem;

        public int Id { get; }
        public string Keyword { get; }
        public StepSyntaxList Parameters { get; }

        public StepSimpleItemSyntax(string keyword, StepSyntaxList parameters, int id)
            : base(-1, -1)
        {
            Keyword = keyword;
            Parameters = parameters;
            Id = id;
        }

        public StepSimpleItemSyntax(StepKeywordToken keyword, StepSyntaxList parameters, int id)
            : base(keyword.Line, keyword.Column)
        {
            Keyword = keyword.Value;
            Parameters = parameters;
            Id = id;
        }

        public override IEnumerable<StepToken> GetTokens()
        {
            yield return new StepKeywordToken(Keyword, -1, -1);
            foreach (var token in Parameters.GetTokens())
            {
                yield return token;
            }
        }
    }
}
