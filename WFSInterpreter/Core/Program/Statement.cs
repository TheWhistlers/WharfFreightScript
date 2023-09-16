using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFSInterpreter.Core.Lexcials;

namespace WFSInterpreter.Core.Program;

public class Statement
{
    public List<Token> TokenStream { get; set; }

    public Statement(params Token[] tokens)
    {
        TokenStream = new List<Token>(tokens);
    }
}
