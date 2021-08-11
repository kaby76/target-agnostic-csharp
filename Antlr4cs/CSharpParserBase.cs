using Antlr4.Runtime;
using System.IO;

public abstract class CSharpParserBase : Parser
{
    protected CSharpParserBase(ITokenStream input)
        : base(input)
    {
    }

    public CSharpParserBase(ITokenStream input, TextWriter output, TextWriter errorOutput) : this(input)
    {
    }

    public bool P1()
    {
        return (((Test.CSharpParser.Right_arrowContext)this.Context).first.TokenIndex + 1) == (((Test.CSharpParser.Right_arrowContext)this.Context).second.TokenIndex);
    }

    public bool P2()
    {
        return (((Test.CSharpParser.Right_shiftContext)this.Context).first.TokenIndex + 1) == (((Test.CSharpParser.Right_shiftContext)this.Context).second.TokenIndex);
    }

    public bool P3()
    {
        return (((Test.CSharpParser.Right_shift_assignmentContext)this.Context).first.TokenIndex + 1) == (((Test.CSharpParser.Right_shift_assignmentContext)this.Context).second.TokenIndex);
    }
}
