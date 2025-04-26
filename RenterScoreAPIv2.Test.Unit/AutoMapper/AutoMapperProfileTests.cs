namespace RenterScoreAPIv2.Test.Unit.AutoMapper;

using RenterScoreAPIv2.AutoMapper;

[TestFixture]
public class AutoMapperProfileTests
{
    [Test]
    public void TestConstructor()
    {
        var profile = new AutoMapperProfile();
        Assert.That(profile, Is.Not.Null);
    }
}