using WFSInterpreter.Core.Lexcials;
using System.Text.RegularExpressions;
namespace WFSInterpreter.Core.Parser;

public static class Tokenizer
{
    public static readonly Regex IDENTIFIER_REGEX = new(@"[_@\p{L}][0-9_\p{L}]*");

    public static void ContextBasedTokenParse(string token_text, ref List<Token> tokens)
    {
        if (string.IsNullOrEmpty(token_text))
            return;

        // 已经完成了对符号 (Symbol)和字符串字面量 (string_literal)的解析
        // 还剩下关键字和概述词 (IDENTIFIER和NUMBER_LITERAL)的解析
        if (double.TryParse(token_text, out var value))
        {
            tokens.Add(new Token(Token.TokenTypes.NUMBER_LITERAL, value));
            return;
        }

        if (Token.KEYWORDS.Contains(token_text))
        {
            tokens.Add(new Token(Token.TokenTypes.KEYWORD, token_text));
            return;
        }

        if (IDENTIFIER_REGEX.IsMatch(token_text))
            tokens.Add(new Token(Token.TokenTypes.IDNETIFIER, token_text));
    }

    public static List<Token> Tokenize(string source)
    {
        var tokens = new List<Token>();

        var last_end_pos = 0;

        for (int i = 0; i < source.Length; i++)
        {
            var curr_pos = i;
            var curr_char = source[curr_pos].ToString();
            var last_char =
                curr_pos == 0
                ? curr_char
                : source[i - 1].ToString();
            var next_char =
                curr_pos >= source.Length - 1
                ? curr_char
                : source[i + 1].ToString();

            var token_text = string.Empty;

            // 如果当前字符是一个符号字符 (包括空格)，则视为一个词素结束
            if (Symbol.SYMBOLS.Contains(curr_char))
            {
                // 先截断字符串，并解析为Token
                token_text = source[last_end_pos..curr_pos];
                last_end_pos = curr_pos + 1;

                ContextBasedTokenParse(token_text, ref tokens);

                // 如果当前字符可以作为一个单独的Token，则解析它
                if (curr_char != Symbol.SPACE)
                {
                    if (curr_char == Symbol.COLON)
                    {
                        if (next_char == Symbol.COLON)
                        {
                            tokens.Add(new Token(Token.TokenTypes.SYMBOL, Symbol.MODULE_ACCESS_OPERATOR));
                            i++;
                        }
                        else
                        {
                            tokens.Add(new Token(Token.TokenTypes.SYMBOL, Symbol.COLON));
                        }
                    }
                    else
                    {
                        tokens.Add(new Token(Token.TokenTypes.SYMBOL, Symbol.Parse(curr_char)));
                    }

                    // 如果当前字符是一个'单引号'，则进入字符串字面量解析
                    // 否则只加入这个符号的Token
                    if (curr_char == Symbol.QUOTE)
                    {
                        // 截取第一个单引号之后的文本，并找到最近的下一个单引号，并截取这之间的内容  
                        var rest = source[(curr_pos + 1)..];
                        var next_quote_pos = rest.IndexOf(rest.First(c => c.ToString() == Symbol.QUOTE));
                        var literal_value = rest[..next_quote_pos];

                        i = curr_pos + next_quote_pos + 1;
                        last_end_pos = i + 1;

                        tokens.Add(new Token(Token.TokenTypes.STRING_LITERAL, literal_value));
                        tokens.Add(new Token(Token.TokenTypes.SYMBOL, Token.Symbols.QUOTE));
                    }
                }

                
            }
        }

        return tokens;
    }
}
