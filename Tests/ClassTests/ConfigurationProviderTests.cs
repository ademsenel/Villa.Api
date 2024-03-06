using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Villas.DomainLayers.Exceptions;
using Villas.DomainLayers.Managers.ConfigurationProviders;
using ConfigurationProvider = Villas.DomainLayers.Managers.ConfigurationProviders.ConfigurationProvider;
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA1859 // Use concrete types when possible for improved performance

namespace Villas.ClassTests;

[TestClass]
public sealed class ConfigurationProviderTests
{
    private const string ConnectionStringSection = "VillaApiAppSettings:";
    private const string ConnectionStringKey = "DbConnectionString";

    private static ConfigurationProviderBase CreateConfigurationProvider(string section, string key, string value) =>
        new ConfigurationProvider(ConnectionStringSection, CreateConfigurationRoot(section, key, value));

    private static IConfigurationRoot CreateConfigurationRoot(string section, string key, string value)
    {
        Dictionary<string, string> appSettingsDictionary = new()
        {
            { section + key, value },
        };

        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.AddInMemoryCollection(appSettingsDictionary);
        return configurationBuilder.Build();
    }

    [TestMethod]
    [TestCategory("Class Test")]
    public void GetSettingValue_WhenCallReturnDbConnectionString_Succeed()
    {
        // Arrange
        var expectedConnectionString = @"Server=(localdb)\MSSQLLocalDB;Database=Db_Villa;Trusted_Connection=True;TrustServerCertificate=True;";
        var configurationProvider = CreateConfigurationProvider(ConnectionStringSection, ConnectionStringKey, expectedConnectionString);

        // Act
        var actualConnectionString = configurationProvider.GetSettingValue(ConnectionStringKey);

        // Assert
        Assert.AreEqual(expectedConnectionString, actualConnectionString, $"We were expecting the actual connection string to be: {expectedConnectionString}, but it was: {actualConnectionString}");
    }

    [TestMethod]
    [TestCategory("Class Test")]
    public void GetSettingValue_WhenCallWithFakeConnetionKeyDbConnectionString_ShouldThrow()
    {
        // Arrange
        var expectedConnectionString = @"Server=(localdb)\MSSQLLocalDB;Database=Database;Trusted_Connection=True;TrustServerCertificate=True;";
        var configurationProvider = CreateConfigurationProvider(ConnectionStringSection, "FakeConnectionKey", expectedConnectionString);

        try
        {
            // Act
            var actualConnectionString = configurationProvider.GetSettingValue(ConnectionStringKey);
        }
        catch (ConfigurationSettingMissingException e)
        {

            // Assert
            StringAssert.Contains(e.Message, $"Key: {ConnectionStringKey}, is missing from the configuration file.");
        }
    }
}
