namespace WFSInterpreter.Core.Lexcials;

public class Token
{
    public enum TokenTypes
    {
        KEYWORD,
        IDNETIFIER, // [_@\p{L}][0-9_\p{L}]*
        STRING_LITERAL, // \"(\S*)\"
        NUMBER_LITERAL, // text which fits double.TryParse()

        SYMBOL,

        // SPEC (special_words
        EOF, // means this file ended
        EOL, // means current line ended
        BOB // means current the beginning of a certain block
    }

    // Keywords
    public const string DECLARE = "declare";
    public const string VAR = "var";
    public const string CONST = "const";
    public const string MODULE = "module";
    public const string FUNC = "func";
    public const string LOCATION = "location";
    public const string TYPE = "type";
    public const string TRUE = "true";
    public const string FALSE = "false";
    public const string IF = "if";
    public const string ELSE = "else";
    public const string END = "end";

    public static readonly List<string> KEYWORDS = new List<string>()
    {
        DECLARE,
        VAR,
        CONST,
        MODULE,
        FUNC,
        LOCATION,
        TYPE,
        TRUE,
        FALSE,
        IF,
        ELSE,
        END
    };

    public static class Symbols
    {
        public static readonly Symbol SEMICOLON = new("SEMICOLON", Symbol.SEMICOLON);
        public static readonly Symbol COLON = new("COLON", Symbol.COLON);
        public static readonly Symbol L_PARENTHESES = new("L_PARENTHESES", Symbol.L_PARENTHESES);
        public static readonly Symbol R_PARENTHESES = new("R_PARENTHESES", Symbol.R_PARENTHESES);
        public static readonly Symbol EQUALS = new("EQUALS", Symbol.EQUALS);
        public static readonly Symbol COMMA = new("COMMA", Symbol.COMMA);
        public static readonly Symbol QUOTE = new("QUOTE", Symbol.QUOTE);
        public static readonly Symbol MEMBER_ACCESS_OPERATOR = new("MEMBER_ACCESS_OPERATOR", Symbol.MEMBER_ACCESS_OPERATOR);
        public static readonly Symbol MODULE_ACCESS_OPERATOR = new("MODULE_ACCESS_OPERATOR", Symbol.MODULE_ACCESS_OPERATOR);

        public static readonly List<Symbol> SYMBOLS = new List<Symbol>()
        {
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
    }

    public TokenTypes TokenType { get; set; }
    public object? TokenValue { get; set; }

    public bool HasValue
    {
        get => this.TokenValue != null;
    }

    public Token(TokenTypes tokenType, object? tokenValue = null)
    {
        this.TokenType = tokenType;
        this.TokenValue = tokenValue;
    }

    public bool InstanceOf(TokenTypes target) => this.TokenType == target;
    public bool IsValueOf(object? target) => this.TokenValue == target;
    public bool Is(TokenTypes type, object? value) => this.InstanceOf(type) && this.IsValueOf(value);

    public override string ToString()
    {
        return
            $"{{ " +
            $"TokenType: " +
            $"{this.TokenType}, " +
            $"TokenValue: {(this.TokenValue != null ? this.TokenValue.ToString() : "null")}" +
            $" }}";
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;

        var tar = (Token)obj;

        return
            this.TokenType == tar.TokenType &&
            this.TokenValue == tar.TokenValue;
    }

    public override int GetHashCode() => base.GetHashCode();
}
