using System;
using System.Collections.Generic;
using StepParser.Tokens;

namespace StepParser.Syntax
{
    internal class StepFileSyntax : StepSyntax
    {
        public override StepSyntaxType SyntaxType => StepSyntaxType.File;

        public StepHeaderSectionSyntax Header { get; }
        public StepDataSectionSyntax Data { get; }

        public StepFileSyntax(StepHeaderSectionSyntax header, StepDataSectionSyntax data)
            : base(header.Line, header.Column)
        {
            Header = header;
            Data = data;
        }

        public override IEnumerable<StepToken> GetTokens()
        {
            throw new NotSupportedException();
        }
    }
}
