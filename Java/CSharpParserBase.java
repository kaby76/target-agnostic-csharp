import org.antlr.v4.runtime.*;
import java.util.Stack;

public abstract class CSharpParserBase extends Parser
{
    protected CSharpParserBase(TokenStream input)
    {
	super(input);
    }

    public boolean P1()
    {
        return (((CSharpParser.Right_arrowContext)this._ctx).first.getTokenIndex() + 1) == (((CSharpParser.Right_arrowContext)this._ctx).second.getTokenIndex());
    }

    public boolean P2()
    {
        return (((CSharpParser.Right_shiftContext)this._ctx).first.getTokenIndex() + 1) == (((CSharpParser.Right_shiftContext)this._ctx).second.getTokenIndex());
    }

    public boolean P3()
    {
        return (((CSharpParser.Right_shift_assignmentContext)this._ctx).first.getTokenIndex() + 1) == (((CSharpParser.Right_shift_assignmentContext)this._ctx).second.getTokenIndex());
    }
}
