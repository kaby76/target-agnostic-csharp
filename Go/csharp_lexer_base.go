package parser

import (
	"github.com/antlr/antlr4/runtime/Go/antlr"
)

// A simple integer stack

type IntStack []int

var ErrEmptyStack = "Stack is empty"

func (s *IntStack) Pop() int {
	l := len(*s) - 1
	if l < 0 {
		return 0
	}
	v := (*s)[l]
	*s = (*s)[0:l]
	return v
}

func (s *IntStack) Peek() int {
	l := len(*s) - 1
	if l < 0 {
		return 0
	}
	v := (*s)[l]
	return v
}

func (s *IntStack) Push(e int) {
	*s = append(*s, e)
}

func (s *IntStack) Count() int {
	return len(*s)
}

type BoolStack []bool

func (s *BoolStack) Pop() bool {
	l := len(*s) - 1
	if l < 0 {
		return false
	}
	v := (*s)[l]
	*s = (*s)[0:l]
	return v
}

func (s *BoolStack) Peek() bool {
	l := len(*s) - 1
	if l < 0 {
		return false
	}
	v := (*s)[l]
	return v
}

func (s *BoolStack) Push(e bool) {
	*s = append(*s, e)
}

func (s *BoolStack) Count() int {
	return len(*s)
}

// CSharpLexerBase implementation.
type CSharpLexerBase struct {
	*antlr.BaseLexer
	interpolatedStringLevel int
	interpolatedVerbatiums  BoolStack
	curlyLevels             IntStack
	verbatium               bool
}

func (p *CSharpLexer) A1() {
	x := p.CSharpLexerBase
	x.interpolatedStringLevel++
	x.interpolatedVerbatiums.Push(false)
	x.verbatium = false
	p.CSharpLexerBase = x
}

func (p *CSharpLexer) A2() {
	x := p.CSharpLexerBase
	x.interpolatedStringLevel++
	x.interpolatedVerbatiums.Push(true)
	x.verbatium = true
	p.CSharpLexerBase = x
}

func (p *CSharpLexer) A3() {
	x := p.CSharpLexerBase
	if x.interpolatedStringLevel > 0 {
		x.curlyLevels.Push(x.curlyLevels.Pop() + 1)
	}
	p.CSharpLexerBase = x
}

func (p *CSharpLexer) A4() {
	x := p.CSharpLexerBase
	if x.interpolatedStringLevel > 0 {
		x.curlyLevels.Push(x.curlyLevels.Pop() - 1)
		if x.curlyLevels.Peek() == 0 {
			x.curlyLevels.Pop()
			p.Skip()
			p.PopMode()
		}
	}
	p.CSharpLexerBase = x
}

func (p *CSharpLexer) A5() {
	x := p.CSharpLexerBase
	if x.interpolatedStringLevel > 0 {
		ind := 1
		switchToFormatString := true
		for x.GetInputStream().LA(ind) != '}' {
			if x.GetInputStream().LA(ind) == ':' || x.GetInputStream().LA(ind) == ')' {
				switchToFormatString = false
				break
			}
			ind++
		}
		if switchToFormatString {
			p.SetMode(CSharpLexerINTERPOLATION_FORMAT)
		}
	}
	p.CSharpLexerBase = x
}

func (p *CSharpLexer) A6() {
	x := p.CSharpLexerBase
	x.curlyLevels.Push(1)
	p.CSharpLexerBase = x
}

func (p *CSharpLexer) A7() {
	x := p.CSharpLexerBase
	x.interpolatedStringLevel--
	x.interpolatedVerbatiums.Pop()
	if x.interpolatedVerbatiums.Count() > 0 {
		x.verbatium = x.interpolatedVerbatiums.Peek()
	} else {
		x.verbatium = false
	}
	p.CSharpLexerBase = x
}

func (p *CSharpLexer) A8() {
	x := p.CSharpLexerBase
	x.curlyLevels.Pop()
	p.CSharpLexerBase = x
}

func (p *CSharpLexer) P1() bool {
	x := p.CSharpLexerBase
	return !x.verbatium
}

func (p *CSharpLexer) P2() bool {
	x := p.CSharpLexerBase
	return x.verbatium
}
