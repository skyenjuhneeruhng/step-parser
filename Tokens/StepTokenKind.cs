using System;
using System.Collections.Generic;
using System.Text;

namespace StepParser.Tokens
{
    internal enum StepTokenKind
    {
        Semicolon,
        Omitted,
        Integer,
        Real,
        String,
        ConstantInstance,
        ConstantValue,
        EntityInstance,
        InstanceValue,
        Enumeration,
        LeftParen,
        RightParen,
        Comma,
        Equals,
        Asterisk,
        Keyword
    }
}
