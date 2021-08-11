using Antlr4.Runtime;
using System.IO;

public abstract class CSharpParserBase : Parser
{
	public CSharpParserBase p;
	
    protected CSharpParserBase(ITokenStream input)
        : base(input)
    {
	    p = this;
    }

    public CSharpParserBase(ITokenStream input, TextWriter output, TextWriter errorOutput) : this(input)
    {
	    p = this;
    }

    public bool P1()
    {
        return (((CSharpParser.Right_arrowContext)this.Context).first.TokenIndex + 1) == (((CSharpParser.Right_arrowContext)this.Context).second.TokenIndex);
    }

    public bool P2()
    {
        return (((CSharpParser.Right_shiftContext)this.Context).first.TokenIndex + 1) == (((CSharpParser.Right_shiftContext)this.Context).second.TokenIndex);
    }

    public bool P3()
    {
        return (((CSharpParser.Right_shift_assignmentContext)this.Context).first.TokenIndex + 1) == (((CSharpParser.Right_shift_assignmentContext)this.Context).second.TokenIndex);
    }
}
