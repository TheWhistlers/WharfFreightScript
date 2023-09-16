using WFSInterpreter.Core.Parser;
using WFSInterpreter.Core.Lexcials;

var code = @"std::println('Hello World!');";
var code2 = "if (super_or_equal(i, 2)):" +
    "\n" +
    "   std::println('I am super or equals to 2!');" +
    "\n" +
    "end;";

Console.WriteLine(code2);
Tokenizer.Tokenize(code2).ForEach(Console.WriteLine);