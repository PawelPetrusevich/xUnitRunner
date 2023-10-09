using Xunit.Abstractions;
using Xunit.Sdk;

namespace XUnitRunerOverload;

public class AssemblyRunnerWithAssemblyFixture :XunitTestAssemblyRunner
{
    private readonly Dictionary<Type, object?> assemblyFixtureMapping = new Dictionary<Type, object?>();
    
    public AssemblyRunnerWithAssemblyFixture(
        ITestAssembly testAssembly,
        IEnumerable<IXunitTestCase> testCases,
        IMessageSink diagnosticMessageSink,
        IMessageSink executionMessageSink,
        ITestFrameworkExecutionOptions executionOptions) 
        : base(testAssembly, testCases, diagnosticMessageSink, executionMessageSink, executionOptions)
    {
    }

    protected override async Task AfterTestAssemblyStartingAsync()
    {
        await base.AfterTestAssemblyStartingAsync();
        
        Aggregator.Run(() =>
        {
            var fixturesAttr = ((IReflectionAssemblyInfo) TestAssembly.Assembly).Assembly
                .GetCustomAttributes(typeof(GlobalFixtureAttribute), false)
                .Cast<GlobalFixtureAttribute>()
                .ToList();

            var programType = ((IReflectionAssemblyInfo) TestAssembly.Assembly).Assembly
                .GetType("TestProject.Program");

            var program = Activator.CreateInstance(programType);

            foreach (var fixtureAttribute in fixturesAttr)
            {
                assemblyFixtureMapping[fixtureAttribute.FixtureType] = Activator.CreateInstance(fixtureAttribute.FixtureType);
            }
        });
    }

    protected override Task BeforeTestAssemblyFinishedAsync()
    {
        foreach (var disposable in assemblyFixtureMapping.Values.OfType<IDisposable>())
        {
            Aggregator.Run(disposable.Dispose);
        }
        
        return base.BeforeTestAssemblyFinishedAsync();
    }

    protected override Task<RunSummary> RunTestCollectionAsync(
        IMessageBus messageBus,
        ITestCollection testCollection,
        IEnumerable<IXunitTestCase> testCases,
        CancellationTokenSource cancellationTokenSource)
    {
        return new CollectionRunnerWithAssemblyFixture(
            assemblyFixtureMapping,
            testCollection,
            testCases,
            DiagnosticMessageSink,
            messageBus,
            TestCaseOrderer,
            new ExceptionAggregator(Aggregator),
            cancellationTokenSource).RunAsync();
    }
}