using Antlr4.Runtime;
using System.Collections.Generic;
using System.IO;

public abstract class CSharpLexerBase : Lexer
{
	public CSharpLexerBase l;
    ICharStream myinput;

    private int interpolatedStringLevel;
    private Stack<bool> interpolatedVerbatiums = new Stack<bool>();
    private Stack<int> curlyLevels = new Stack<int>();
    private bool verbatium;


    protected CSharpLexerBase(ICharStream input, TextWriter output, TextWriter errorOutput)
        : base(input, output, errorOutput)
    {
	    myinput = input;
	    l = this;
    }

    public CSharpLexerBase(ICharStream input)
        : base(input)
    {
	    myinput = input;
	    l = this;
    }

    public void A1()
    {
        interpolatedStringLevel++;
        interpolatedVerbatiums.Push(false);
        verbatium = false;
    }

    public void A2()
    {
        interpolatedStringLevel++;
        interpolatedVerbatiums.Push(true);
        verbatium = true;
    }

    public void A3()
    {
        if (interpolatedStringLevel > 0)
        {
            curlyLevels.Push(curlyLevels.Pop() + 1);
        }
    }

    public void A4()
    {
        if (interpolatedStringLevel > 0)
        {
            curlyLevels.Push(curlyLevels.Pop() - 1);
            if (curlyLevels.Peek() == 0)
            {
                curlyLevels.Pop();
                Skip();
                PopMode();
            }
        }
    }

    public void A5()
    {
        if (interpolatedStringLevel > 0)
        {
            int ind = 1;
            bool switchToFormatString = true;
            while ((char)myinput.LA(ind) != '}')
            {
                if (myinput.LA(ind) == ':' || myinput.LA(ind) == ')')
                {
                    switchToFormatString = false;
                    break;
                }
                ind++;
            }
            if (switchToFormatString)
            {
                Mode(CSharpLexer.INTERPOLATION_FORMAT);
            }
        }
    }

    public void A6()
    {
        curlyLevels.Push(1);
    }

    public void A7()
    {
        interpolatedStringLevel--;
        interpolatedVerbatiums.Pop();
        verbatium = (interpolatedVerbatiums.Count > 0 ? interpolatedVerbatiums.Peek() : false);
    }

    public void A8()
    {
        curlyLevels.Pop();
    }

    public bool P1()
    {
        return !verbatium;
    }

    public bool P2()
    {
        return verbatium;
    }
}
