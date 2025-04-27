namespace RenterScoreAPIv2.Test.Unit.Utilities;

using NUnit.Framework;
using RenterScoreAPIv2.Utilities;

[TestFixture]
public class StringExtensionsTest
{
    [Test]
    public void ToSnakeCase_NullInput_ReturnsNull()
    {
        string input = null;
        string result = input.ToSnakeCase();
        Assert.That(result, Is.Null);
    }

    [Test]
    public void ToSnakeCase_EmptyString_ReturnsEmptyString()
    {
        string input = string.Empty;
        string result = input.ToSnakeCase();
        Assert.That(result, Is.EqualTo(string.Empty));
    }

    [Test]
    public void ToSnakeCase_NoUppercase_ReturnsSameString()
    {
        string input = "lowercase";
        string result = input.ToSnakeCase();
        Assert.That(result, Is.EqualTo("lowercase"));
    }

    [Test]
    public void ToSnakeCase_MixedCase_ReturnsSnakeCase()
    {
        string input = "ThisIsATest";
        string result = input.ToSnakeCase();
        Assert.That(result, Is.EqualTo("this_is_a_test"));
    }

    [Test]
    public void ToSnakeCase_ConsecutiveUppercase_ReturnsSnakeCase()
    {
        string input = "URLParser";
        string result = input.ToSnakeCase();
        Assert.That(result, Is.EqualTo("u_r_l_parser"));
    }

    [Test]
    public void ToSnakeCase_WithSpecialCharacters_ReturnsSnakeCase()
    {
        string input = "HelloWorld!";
        string result = input.ToSnakeCase();
        Assert.That(result, Is.EqualTo("hello_world!"));
    }
}
