import org.antlr.v4.runtime.*;
import java.util.Stack;

public abstract class CSharpLexerBase extends Lexer
{
    private int interpolatedStringLevel;
    private Stack<Boolean> interpolatedVerbatiums = new Stack<Boolean>();
    private Stack<Integer> curlyLevels = new Stack<Integer>();
    private boolean verbatium;

    public CSharpLexerBase(CharStream input)
    {
	super(input);
    }
    
    protected void A1()
    {
	interpolatedStringLevel++;
	interpolatedVerbatiums.push(false);
	verbatium = false;
    }

    protected void A2()
    {
	interpolatedStringLevel++;
	interpolatedVerbatiums.push(true);
	verbatium = true;
    }

    protected void A3()
    {
	if (interpolatedStringLevel > 0)
	{
	    curlyLevels.push(curlyLevels.pop() + 1);
	}
    }

    protected void A4()
    {
	if (interpolatedStringLevel > 0)
	{
	    curlyLevels.push(curlyLevels.pop() - 1);
	    if (curlyLevels.peek() == 0)
	    {
		curlyLevels.pop();
		skip();
		popMode();
	    }
	}
    }

    protected void A5()
    {
	if (interpolatedStringLevel > 0)
	{
	    int ind = 1;
	    boolean switchToFormatString = true;
	    while ((char)_input.LA(ind) != '}')
	    {
		if (_input.LA(ind) == ':' || _input.LA(ind) == ')')
		{
		    switchToFormatString = false;
		    break;
		}
		ind++;
	    }
	    if (switchToFormatString)
	    {
		mode(CSharpLexer.INTERPOLATION_FORMAT);
	    }
	}
    }

    protected void A6()
    {
	curlyLevels.push(1);
    }

    protected void A7()
    {
	interpolatedStringLevel--;
	interpolatedVerbatiums.pop();
	verbatium = (interpolatedVerbatiums.size() > 0 ? interpolatedVerbatiums.peek() : false);
    }

    protected void A8()
    {
	curlyLevels.pop();
    }

    protected boolean P1()
    {
	return !verbatium;
    }

    protected boolean P2()
    {
	return verbatium;
    }
}
