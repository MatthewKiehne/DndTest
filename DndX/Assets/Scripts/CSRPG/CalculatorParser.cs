//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.10.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from Calculator.g4 by ANTLR 4.10.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Calculator {
using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.10.1")]
[System.CLSCompliant(false)]
public partial class CalculatorParser : Parser {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2, POW=3, MUL=4, DIV=5, ADD=6, SUB=7, NUMBER=8, WHITESPACE=9;
	public const int
		RULE_start = 0, RULE_expression = 1;
	public static readonly string[] ruleNames = {
		"start", "expression"
	};

	private static readonly string[] _LiteralNames = {
		null, "'('", "')'", "'^'", "'*'", "'/'", "'+'", "'-'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, "POW", "MUL", "DIV", "ADD", "SUB", "NUMBER", "WHITESPACE"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "Calculator.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override int[] SerializedAtn { get { return _serializedATN; } }

	static CalculatorParser() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}

		public CalculatorParser(ITokenStream input) : this(input, Console.Out, Console.Error) { }

		public CalculatorParser(ITokenStream input, TextWriter output, TextWriter errorOutput)
		: base(input, output, errorOutput)
	{
		Interpreter = new ParserATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	public partial class StartContext : ParserRuleContext {
		public ExpressionContext exp;
		[System.Diagnostics.DebuggerNonUserCode] public ExpressionContext expression() {
			return GetRuleContext<ExpressionContext>(0);
		}
		public StartContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_start; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ICalculatorVisitor<TResult> typedVisitor = visitor as ICalculatorVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitStart(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public StartContext start() {
		StartContext _localctx = new StartContext(Context, State);
		EnterRule(_localctx, 0, RULE_start);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 4;
			_localctx.exp = expression(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ExpressionContext : ParserRuleContext {
		public ExpressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_expression; } }
	 
		public ExpressionContext() { }
		public virtual void CopyFrom(ExpressionContext context) {
			base.CopyFrom(context);
		}
	}
	public partial class NumberContext : ExpressionContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode NUMBER() { return GetToken(CalculatorParser.NUMBER, 0); }
		public NumberContext(ExpressionContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ICalculatorVisitor<TResult> typedVisitor = visitor as ICalculatorVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitNumber(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class AdditionOrSubtractionContext : ExpressionContext {
		public ExpressionContext left;
		public IToken @operator;
		public ExpressionContext right;
		[System.Diagnostics.DebuggerNonUserCode] public ExpressionContext[] expression() {
			return GetRuleContexts<ExpressionContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public ExpressionContext expression(int i) {
			return GetRuleContext<ExpressionContext>(i);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode ADD() { return GetToken(CalculatorParser.ADD, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode SUB() { return GetToken(CalculatorParser.SUB, 0); }
		public AdditionOrSubtractionContext(ExpressionContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ICalculatorVisitor<TResult> typedVisitor = visitor as ICalculatorVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitAdditionOrSubtraction(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class MultiplicationOrDivisionContext : ExpressionContext {
		public ExpressionContext left;
		public IToken @operator;
		public ExpressionContext right;
		[System.Diagnostics.DebuggerNonUserCode] public ExpressionContext[] expression() {
			return GetRuleContexts<ExpressionContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public ExpressionContext expression(int i) {
			return GetRuleContext<ExpressionContext>(i);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode MUL() { return GetToken(CalculatorParser.MUL, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode DIV() { return GetToken(CalculatorParser.DIV, 0); }
		public MultiplicationOrDivisionContext(ExpressionContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ICalculatorVisitor<TResult> typedVisitor = visitor as ICalculatorVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitMultiplicationOrDivision(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class ParenthesesContext : ExpressionContext {
		public ExpressionContext inner;
		[System.Diagnostics.DebuggerNonUserCode] public ExpressionContext expression() {
			return GetRuleContext<ExpressionContext>(0);
		}
		public ParenthesesContext(ExpressionContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ICalculatorVisitor<TResult> typedVisitor = visitor as ICalculatorVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitParentheses(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class PowerContext : ExpressionContext {
		public ExpressionContext left;
		public IToken @operator;
		public ExpressionContext right;
		[System.Diagnostics.DebuggerNonUserCode] public ExpressionContext[] expression() {
			return GetRuleContexts<ExpressionContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public ExpressionContext expression(int i) {
			return GetRuleContext<ExpressionContext>(i);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode POW() { return GetToken(CalculatorParser.POW, 0); }
		public PowerContext(ExpressionContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ICalculatorVisitor<TResult> typedVisitor = visitor as ICalculatorVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitPower(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ExpressionContext expression() {
		return expression(0);
	}

	private ExpressionContext expression(int _p) {
		ParserRuleContext _parentctx = Context;
		int _parentState = State;
		ExpressionContext _localctx = new ExpressionContext(Context, _parentState);
		ExpressionContext _prevctx = _localctx;
		int _startState = 2;
		EnterRecursionRule(_localctx, 2, RULE_expression, _p);
		int _la;
		try {
			int _alt;
			EnterOuterAlt(_localctx, 1);
			{
			State = 12;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case NUMBER:
				{
				_localctx = new NumberContext(_localctx);
				Context = _localctx;
				_prevctx = _localctx;

				State = 7;
				Match(NUMBER);
				}
				break;
			case T__0:
				{
				_localctx = new ParenthesesContext(_localctx);
				Context = _localctx;
				_prevctx = _localctx;
				State = 8;
				Match(T__0);
				State = 9;
				((ParenthesesContext)_localctx).inner = expression(0);
				State = 10;
				Match(T__1);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			Context.Stop = TokenStream.LT(-1);
			State = 25;
			ErrorHandler.Sync(this);
			_alt = Interpreter.AdaptivePredict(TokenStream,2,Context);
			while ( _alt!=2 && _alt!=global::Antlr4.Runtime.Atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( ParseListeners!=null )
						TriggerExitRuleEvent();
					_prevctx = _localctx;
					{
					State = 23;
					ErrorHandler.Sync(this);
					switch ( Interpreter.AdaptivePredict(TokenStream,1,Context) ) {
					case 1:
						{
						_localctx = new PowerContext(new ExpressionContext(_parentctx, _parentState));
						((PowerContext)_localctx).left = _prevctx;
						PushNewRecursionContext(_localctx, _startState, RULE_expression);
						State = 14;
						if (!(Precpred(Context, 3))) throw new FailedPredicateException(this, "Precpred(Context, 3)");
						State = 15;
						((PowerContext)_localctx).@operator = Match(POW);
						State = 16;
						((PowerContext)_localctx).right = expression(4);
						}
						break;
					case 2:
						{
						_localctx = new MultiplicationOrDivisionContext(new ExpressionContext(_parentctx, _parentState));
						((MultiplicationOrDivisionContext)_localctx).left = _prevctx;
						PushNewRecursionContext(_localctx, _startState, RULE_expression);
						State = 17;
						if (!(Precpred(Context, 2))) throw new FailedPredicateException(this, "Precpred(Context, 2)");
						State = 18;
						((MultiplicationOrDivisionContext)_localctx).@operator = TokenStream.LT(1);
						_la = TokenStream.LA(1);
						if ( !(_la==MUL || _la==DIV) ) {
							((MultiplicationOrDivisionContext)_localctx).@operator = ErrorHandler.RecoverInline(this);
						}
						else {
							ErrorHandler.ReportMatch(this);
						    Consume();
						}
						State = 19;
						((MultiplicationOrDivisionContext)_localctx).right = expression(3);
						}
						break;
					case 3:
						{
						_localctx = new AdditionOrSubtractionContext(new ExpressionContext(_parentctx, _parentState));
						((AdditionOrSubtractionContext)_localctx).left = _prevctx;
						PushNewRecursionContext(_localctx, _startState, RULE_expression);
						State = 20;
						if (!(Precpred(Context, 1))) throw new FailedPredicateException(this, "Precpred(Context, 1)");
						State = 21;
						((AdditionOrSubtractionContext)_localctx).@operator = TokenStream.LT(1);
						_la = TokenStream.LA(1);
						if ( !(_la==ADD || _la==SUB) ) {
							((AdditionOrSubtractionContext)_localctx).@operator = ErrorHandler.RecoverInline(this);
						}
						else {
							ErrorHandler.ReportMatch(this);
						    Consume();
						}
						State = 22;
						((AdditionOrSubtractionContext)_localctx).right = expression(2);
						}
						break;
					}
					} 
				}
				State = 27;
				ErrorHandler.Sync(this);
				_alt = Interpreter.AdaptivePredict(TokenStream,2,Context);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			UnrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public override bool Sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 1: return expression_sempred((ExpressionContext)_localctx, predIndex);
		}
		return true;
	}
	private bool expression_sempred(ExpressionContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0: return Precpred(Context, 3);
		case 1: return Precpred(Context, 2);
		case 2: return Precpred(Context, 1);
		}
		return true;
	}

	private static int[] _serializedATN = {
		4,1,9,29,2,0,7,0,2,1,7,1,1,0,1,0,1,1,1,1,1,1,1,1,1,1,1,1,3,1,13,8,1,1,
		1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,5,1,24,8,1,10,1,12,1,27,9,1,1,1,0,1,
		2,2,0,2,0,2,1,0,4,5,1,0,6,7,30,0,4,1,0,0,0,2,12,1,0,0,0,4,5,3,2,1,0,5,
		1,1,0,0,0,6,7,6,1,-1,0,7,13,5,8,0,0,8,9,5,1,0,0,9,10,3,2,1,0,10,11,5,2,
		0,0,11,13,1,0,0,0,12,6,1,0,0,0,12,8,1,0,0,0,13,25,1,0,0,0,14,15,10,3,0,
		0,15,16,5,3,0,0,16,24,3,2,1,4,17,18,10,2,0,0,18,19,7,0,0,0,19,24,3,2,1,
		3,20,21,10,1,0,0,21,22,7,1,0,0,22,24,3,2,1,2,23,14,1,0,0,0,23,17,1,0,0,
		0,23,20,1,0,0,0,24,27,1,0,0,0,25,23,1,0,0,0,25,26,1,0,0,0,26,3,1,0,0,0,
		27,25,1,0,0,0,3,12,23,25
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
} // namespace Calculator