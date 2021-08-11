package parser

import (
	"github.com/antlr/antlr4/runtime/Go/antlr"
)

// CSharpParserBase implementation.
type CSharpParserBase struct {
	*antlr.BaseParser
}

func (p *CSharpParser) P1() bool {
	x := p.CSharpParserBase
	y := x.BaseParser
	z := y.GetParserRuleContext()
	d := z.(IRight_arrowContext)
	return (d.GetFirst().GetTokenIndex() + 1) == (d.GetSecond().GetTokenIndex())
}

func (p *CSharpParser) P2() bool {
	x := p.CSharpParserBase
	y := x.BaseParser
	z := y.GetParserRuleContext()
	d := z.(IRight_shiftContext)
	return (d.GetFirst().GetTokenIndex() + 1) == (d.GetSecond().GetTokenIndex())
}

func (p *CSharpParser) P3() bool {
	x := p.CSharpParserBase
	y := x.BaseParser
	z := y.GetParserRuleContext()
	d := z.(IRight_shift_assignmentContext)
	return (d.GetFirst().GetTokenIndex() + 1) == (d.GetSecond().GetTokenIndex())
}
