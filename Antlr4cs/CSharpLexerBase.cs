using Antlr4.Runtime;
using System.Collections.Generic;
using System.IO;

public abstract class CSharpLexerBase : Lexer
{
    ICharStream myinput;

    private int interpolatedStringLevel;
    private Stack<bool> interpolatedVerbatiums = new Stack<bool>();
    private Stack<int> curlyLevels = new Stack<int>();
    private bool verbatium;


    protected CSharpLexerBase(ICharStream input)
        : base(input)
    {
    }

    protected void A1()
    {
        interpolatedStringLevel++;
        interpolatedVerbatiums.Push(false);
        verbatium = false;
    }

    protected void A2()
    {
        interpolatedStringLevel++;
        interpolatedVerbatiums.Push(true);
        verbatium = true;
    }

    protected void A3()
    {
        if (interpolatedStringLevel > 0)
        {
            curlyLevels.Push(curlyLevels.Pop() + 1);
        }
    }

    protected void A4()
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

    protected void A5()
    {
        if (interpolatedStringLevel > 0)
        {
            int ind = 1;
            bool switchToFormatString = true;
            while ((char)_input.La(ind) != '}')
            {
                if (_input.La(ind) == ':' || _input.La(ind) == ')')
                {
                    switchToFormatString = false;
                    break;
                }
                ind++;
            }
            if (switchToFormatString)
            {
                Mode(Test.CSharpLexer.INTERPOLATION_FORMAT);
            }
        }
    }

    protected void A6()
    {
        curlyLevels.Push(1);
    }

    protected void A7()
    {
        interpolatedStringLevel--;
        interpolatedVerbatiums.Pop();
        verbatium = (interpolatedVerbatiums.Count > 0 ? interpolatedVerbatiums.Peek() : false);
    }

    protected void A8()
    {
        curlyLevels.Pop();
    }

    protected bool P1()
    {
        return !verbatium;
    }

    protected bool P2()
    {
        return verbatium;
    }
}
