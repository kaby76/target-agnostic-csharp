# Template generated code from trgen 0.8.9
JAR = c:/users/kenne/downloads/antlr-4.9.2-complete.jar
CLASSPATH = $(JAR)\;.
.SUFFIXES: .g4 .go
ANTLRGRAMMARS ?= $(wildcard *.g4)
GENERATED = parser/CSharpLexer.go parser/CSharpParser.go 
SOURCES = $(GENERATED) Program.go
default: classes
classes: $(SOURCES)
	@if [ "$(GOPATH)" = "" ]; then echo "GOPATH must be defined, usually c:/users/youruserid/."; exit 1; fi
	export GO111MODULE=auto; go get github.com/antlr/antlr4/runtime/Go/antlr
clean:
	rm -f *.tokens *.interp
	rm -f $(GENERATED)
run:
	export GO111MODULE=auto; trwdog go run Program.go $(RUNARGS)
parser/CSharpLexer.go : parser/CSharpLexer.g4
	java -jar $(JAR) -encoding utf-8 -Dlanguage=Go -o parser -lib parser  $<
	sed 's/\*CSharpLexerBase/CSharpLexerBase/' parser/csharp_lexer.go > foobar
	mv foobar parser/csharp_lexer.go
parser/CSharpParser.go : parser/CSharpParser.g4
	java -jar $(JAR) -encoding utf-8 -Dlanguage=Go -o parser -lib parser  $<
	sed 's/\*CSharpLexerBase/CSharpLexerBase/' parser/csharp_lexer.go > foobar
	mv foobar parser/csharp_lexer.go
	export GO111MODULE=auto; go build Program.go
test:
	bash test.sh
