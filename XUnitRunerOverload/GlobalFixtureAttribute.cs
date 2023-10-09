namespace XUnitRunerOverload;

[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public class GlobalFixtureAttribute : Attribute
{
    public GlobalFixtureAttribute(Type testFixtureType)
    {
        FixtureType = testFixtureType;
    }

    public Type FixtureType { get; set; }
}