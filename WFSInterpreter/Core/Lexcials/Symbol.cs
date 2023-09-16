using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFSInterpreter.Core.Lexcials;

public struct Symbol
{
    public string Name { get; set; }
    public string Text { get; set; }

    public Symbol(string name, string text)
    {
        this.Name = name;
        this.Text = text;
    }

    public const string SPACE = " ";

    public const string SEMICOLON = ";";
    public const string COLON = ":";
    public const string L_PARENTHESES = "(";
    public const string R_PARENTHESES = ")";
    public const string EQUALS = "=";
    public const string COMMA = ",";
    public const string QUOTE = "'";
    public const string MEMBER_ACCESS_OPERATOR = ".";
    public const string MODULE_ACCESS_OPERATOR = "::";

    public static readonly List<string> SYMBOLS= new List<string>()
    {
        SPACE,
        SEMICOLON,
        COLON,
        L_PARENTHESES,
        R_PARENTHESES,
        EQUALS,
        COMMA,
        QUOTE,
        MEMBER_ACCESS_OPERATOR,
        MODULE_ACCESS_OPERATOR
    };

    public static Symbol Parse(string text)
    {
        return Token.Symbols.SYMBOLS.Find(smbl => smbl.Text.Equals(text));
    }

    public override string ToString()
    {
        return
            $"{{ Symbol( " +
            $"Name: " +
            $"{this.Name}, " +
            $"Text: {this.Text}" +
            $") }}";
    }
}
