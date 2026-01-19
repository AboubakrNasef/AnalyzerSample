
using Ab.Analyzers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using System.Threading.Tasks;

namespace AbAnalyzers.Xunit.Test
{
	public class ClassNameAnaylzerTests
	{
		[Fact]
		public async Task Should_Return_Diagnostic_For_WrongClassName()
		{
			var testContext = new CSharpAnalyzerTest<ClassNameUnderscoreAnalyzer, DefaultVerifier>
			{
				ReferenceAssemblies = ReferenceAssemblies.Net.Net90
			};
			testContext.TestState.OutputKind = OutputKind.ConsoleApplication;
			testContext.CompilerDiagnostics = CompilerDiagnostics.None;
			testContext.TestCode =/* lang=c#-test */ @"
    namespace Foo
{
		var x = new Request() { MyProperty = 1 };

		class {|#0:Request_New|}
		{
			public int MyProperty { get; set; }
		}
}
";
			testContext.ExpectedDiagnostics.Add(new DiagnosticResult(ClassNameUnderscoreAnalyzer.ClassNameRule).WithLocation(0).WithArguments("Request_New"));
			await testContext.RunAsync();
		}

	}
}
