using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace XUnitRunerOverload;

public class FrameworkExecutorWithAssemblyFixture : XunitTestFrameworkExecutor
{
    public FrameworkExecutorWithAssemblyFixture(
        AssemblyName assemblyName,
        ISourceInformationProvider sourceInformationProvider,
        IMessageSink diagnosticMessageSink) : base(assemblyName, sourceInformationProvider, diagnosticMessageSink)
    {
    }

    protected override async void RunTestCases(IEnumerable<IXunitTestCase> testCases, IMessageSink executionMessageSink, ITestFrameworkExecutionOptions executionOptions)
    {
        using var assemblyRunner = new AssemblyRunnerWithAssemblyFixture(TestAssembly, testCases, DiagnosticMessageSink, executionMessageSink, executionOptions);

        await assemblyRunner.RunAsync();
    }
}