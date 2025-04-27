namespace RenterScoreAPIv2.Test.Unit.DataModels;

using RenterScoreAPIv2.Property;
using RenterScoreAPIv2.User;
using RenterScoreAPIv2.UserProfile;
using RenterScoreAPIv2.PropertyDetails;
using RenterScoreAPIv2.PropertyDetailsWithImages;
using RenterScoreAPIv2.PropertyImage;

[TestFixture]
public class DataModelTests
{
    [TestCase(typeof(Property))]
    [TestCase(typeof(User))]
    [TestCase(typeof(UserProfile))]
    [TestCase(typeof(UserProfileViewModel))]
    [TestCase(typeof(AddressViewModel))]
    [TestCase(typeof(PropertyDetails))]
    [TestCase(typeof(PropertyDetailsWithImagesViewModel))]
    [TestCase(typeof(PropertyImage))]
    [TestCase(typeof(PropertyDetailsWithImages))]
    public void TestGettersAndSetters(Type type)
    {
        var instance = Activator.CreateInstance(type);
        Assert.That(instance, Is.Not.Null);
        foreach (var property in type.GetProperties())
        {
            if (property.CanRead)
            {
                property.GetValue(instance);
            }
            if (property.CanWrite)
            {
                object defaultValue = null;
                if (property.PropertyType.IsValueType)
                {
                    defaultValue = Activator.CreateInstance(property.PropertyType);
                }
                property.SetValue(instance, defaultValue);
            }
        }
    }
}