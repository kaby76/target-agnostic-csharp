// Eclipse Public License - v 1.0, http://www.eclipse.org/legal/epl-v10.html
// Copyright (c) 2013, Christian Wulf (chwchw@gmx.de)
// Copyright (c) 2016-2017, Ivan Kochurkin (kvanttt@gmail.com), Positive Technologies.

parser grammar CSharpPreprocessorParser;

options { tokenVocab=CSharpLexer; }

preprocessor_directive
    : DEFINE CONDITIONAL_SYMBOL directive_new_line_or_sharp     #preprocessorDeclaration
    | UNDEF CONDITIONAL_SYMBOL directive_new_line_or_sharp      #preprocessorDeclaration
    | IF expr=preprocessor_expression directive_new_line_or_sharp   #preprocessorConditional
    | ELIF expr=preprocessor_expression directive_new_line_or_sharp #preprocessorConditional
    | ELSE directive_new_line_or_sharp              #preprocessorConditional
    | ENDIF directive_new_line_or_sharp             #preprocessorConditional
    | LINE (DIGITS STRING? | DEFAULT | DIRECTIVE_HIDDEN) directive_new_line_or_sharp    #preprocessorLine
    | ERROR TEXT directive_new_line_or_sharp            #preprocessorDiagnostic
    | WARNING TEXT directive_new_line_or_sharp          #preprocessorDiagnostic
    | REGION TEXT? directive_new_line_or_sharp          #preprocessorRegion
    | ENDREGION TEXT? directive_new_line_or_sharp           #preprocessorRegion
    | PRAGMA TEXT directive_new_line_or_sharp           #preprocessorPragma
    | NULLABLE TEXT directive_new_line_or_sharp         #preprocessorNullable
    ;

directive_new_line_or_sharp
    : DIRECTIVE_NEW_LINE
    | EOF
    ;

preprocessor_expression
    : TRUE
    | FALSE
    | CONDITIONAL_SYMBOL
    | OPEN_PARENS expr=preprocessor_expression CLOSE_PARENS
    | BANG expr=preprocessor_expression
    | expr1=preprocessor_expression OP_EQ expr2=preprocessor_expression
    | expr1=preprocessor_expression OP_NE expr2=preprocessor_expression
    | expr1=preprocessor_expression OP_AND expr2=preprocessor_expression
    | expr1=preprocessor_expression OP_OR expr2=preprocessor_expression
    ;
