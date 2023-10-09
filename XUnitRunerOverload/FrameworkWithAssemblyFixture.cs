using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace XUnitRunerOverload;

public class FrameworkWithAssemblyFixture : XunitTestFramework
{
    public FrameworkWithAssemblyFixture(IMessageSink messageSink) : base(messageSink)
    {
    }

    protected override ITestFrameworkExecutor CreateExecutor(AssemblyName assemblyName)
    {
        return new FrameworkExecutorWithAssemblyFixture(assemblyName, SourceInformationProvider, DiagnosticMessageSink);
    }
}