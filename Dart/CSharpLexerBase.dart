import 'package:antlr4/antlr4.dart';
import 'dart:io';
import 'dart:convert';
import 'CSharpLexer.dart';

abstract class CSharpLexerBase extends Lexer
{
    int interpolatedStringLevel;
    List<bool> interpolatedVerbatiums = [];
    List<int> curlyLevels = [];
    bool verbatium;

    CSharpLexerBase(CharStream input) : super(input)
    {
    }

    void A1()
    {
        interpolatedStringLevel++;
        interpolatedVerbatiums.add(false);
        verbatium = false;
    }

    void A2()
    {
        interpolatedStringLevel++;
        interpolatedVerbatiums.add(true);
        verbatium = true;
    }

    void A3()
    {
        if (interpolatedStringLevel > 0)
        {
            curlyLevels.add(curlyLevels.removeLast() + 1);
        }
    }

    void A4()
    {
        if (interpolatedStringLevel > 0)
        {
            curlyLevels.add(curlyLevels.removeLast() - 1);
            if (curlyLevels.last == 0)
            {
                curlyLevels.removeLast();
                skip();
                popMode();
            }
        }
    }

    void A5()
    {
        if (interpolatedStringLevel > 0)
        {
            int ind = 1;
            bool switchToFormatString = true;
            while (inputStream.LA(ind) != '}')
            {
                if (inputStream.LA(ind) == ':' || inputStream.LA(ind) == ')')
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

    void A6()
    {
        curlyLevels.add(1);
    }

    void A7()
    {
        interpolatedStringLevel--;
        interpolatedVerbatiums.removeLast();
        verbatium = (interpolatedVerbatiums.length > 0 ? interpolatedVerbatiums.last : false);
    }

    void A8()
    {
        curlyLevels.removeLast();
    }

    bool P1()
    {
        return !verbatium;
    }

    bool P2()
    {
        return verbatium;
    }
}
