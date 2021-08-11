import 'package:antlr4/antlr4.dart';
import 'dart:io';
import 'dart:convert';
import 'CSharpParser.dart';

abstract class CSharpParserBase extends Parser
{
    CSharpParserBase(TokenStream input) : super(input)
    {
    }

    bool P1()
    {
	//$first.index + 1 == $second.index
	Right_arrowContext c = context;
	return (c.first.tokenIndex + 1) == (c.second.tokenIndex);
    }

    bool P2()
    {
	//$first.index + 1 == $second.index
	Right_shiftContext c = context;
	return (c.first.tokenIndex + 1) == (c.second.tokenIndex);
    }

    bool P3()
    {
	//$first.index + 1 == $second.index
	Right_shift_assignmentContext c = context;
	return (c.first.tokenIndex + 1) == (c.second.tokenIndex);
    }
}

