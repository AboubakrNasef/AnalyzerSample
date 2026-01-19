using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Immutable;

namespace Ab.Analyzers
{
	[DiagnosticAnalyzer(LanguageNames.CSharp)]
	public class ClassNameUnderscoreAnalyzer : DiagnosticAnalyzer
	{
		public const string DiagnosticId = "CLS001";
		public static DiagnosticDescriptor ClassNameRule = new DiagnosticDescriptor(
			DiagnosticId, "Class Name contains _",
			"class '{0}' contains _", "Maintenance",
			DiagnosticSeverity.Warning, isEnabledByDefault: true,
			description: "Class Name shouldn't have _.");

		public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
		{
			get { return ImmutableArray.Create(ClassNameRule); }
		}
		public override void Initialize(AnalysisContext analysisContext)
		{
			analysisContext.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
			analysisContext.EnableConcurrentExecution();
			analysisContext.RegisterSymbolAction(action, SymbolKind.NamedType);
		}

		private void action(SymbolAnalysisContext context)
		{
			var vistior = new NameVisitor();
			if (context.Symbol.Accept(vistior))
			{
				var diagnostic = Diagnostic.Create(ClassNameRule, context.Symbol.Locations[0], context.Symbol.Name);
				context.ReportDiagnostic(diagnostic);
			}
		}
	}

	internal class NameVisitor : SymbolVisitor<bool>
	{
		public override bool VisitNamedType(INamedTypeSymbol symbol)
		{
			if (symbol.Name.Contains("_"))
			{
				return true;
			}
			return false;
		}
	}
}
