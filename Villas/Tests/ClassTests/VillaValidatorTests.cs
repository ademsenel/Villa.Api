using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using Villas.DomainLayers.Exceptions;
using Villas.DomainLayers.Managers.Validators;
using Villas.DomainLayers.Models;
#pragma warning disable CA1707 // Identifiers should not contain underscores

namespace Villas.ClassTests;


[TestClass]
public class VillaValidatorTests
{
    [TestMethod]
    [TestCategory("Class Test")]
    public void ValidateStringProperty_WhenCalledWithNullString_ShouldThrow()
    {
        // Arrange 
        string str = null;

        try
        {
            // Act
            VillaValidator.EnsureNameIsValid(nameof(Villa.Name), str);
            Assert.Fail($"We were expecting a {nameof(VillaValidationException)} exception to be thrown, but no exception was thrown.");
        }
        catch (VillaValidationException e)
        {
            // Assert
            StringAssert.Contains(e.Message, $"must be a valid {nameof(Villa.Name)} and can not be null.");
        }
    }

    [TestMethod]
    [TestCategory("Class Test")]
    public void ValidateStringProperty_WhenCalledWithEmptyString_ShouldThrow()
    {
        // Arrange 
        var str = string.Empty;

        try
        {
            // Act
            VillaValidator.EnsureNameIsValid(nameof(Villa.Name), str);
            Assert.Fail($"We were expecting a {nameof(VillaValidationException)} exception to be thrown, but no exception was thrown.");
        }
        catch (VillaValidationException e)
        {
            // Assert
            StringAssert.Contains(e.Message, $"must be a valid {nameof(Villa.Name)} and can not be empty.");
        }
    }

    [TestMethod]
    [TestCategory("Class Test")]
    public void ValidateStringProperty_WhenCalledWithWhiteSpaceString_ShouldThrow()
    {
        // Arrange 
        var str = " ";

        try
        {
            // Act
            VillaValidator.EnsureNameIsValid(nameof(Villa.Name), str);
            Assert.Fail($"We were expecting a {nameof(VillaValidationException)} exception to be thrown, but no exception was thrown.");
        }
        catch (VillaValidationException e)
        {
            // Assert
            StringAssert.Contains(e.Message, $"must be a valid {nameof(Villa.Name)} and can not be whitespaces.");
        }
    }

    [TestMethod]
    [TestCategory("Class Test")]
    public void ValidateIntegerProperty_WhenCalledWithInvalidInteger_ShouldThrow()
    {
        // Arrange 
        var integer = -1;

        try
        {
            // Act
            VillaValidator.EnsureIdIsValid(nameof(Villa.Id), integer);
            Assert.Fail($"We were expecting a {nameof(VillaValidationException)} exception to be thrown, but no exception was thrown.");
        }
        catch (VillaValidationException e)
        {
            // Assert
            StringAssert.Contains(e.Message, $"must be a valid {nameof(Villa.Id)} and can not be less than {integer}.");
        }
    }

    [TestMethod]
    [TestCategory("Class Test")]
    public void EnsureVillaIsValid_WhenCalledWithNullVilla_ShouldThrow()
    {
        // Arrange 
        Villa villa = null;

        try
        {
            // Act
            VillaValidator.EnsureVillaIsValid(villa);
            Assert.Fail($"We were expecting a {nameof(VillaNullException)} exception to be thrown, but no exception was thrown.");
        }
        catch (VillaNullException e)
        {
            // Assert
            StringAssert.Contains(e.Message, $"The {nameof(Villa).ToLower(CultureInfo.InvariantCulture)} parameter can not be null.");
        }
    }
}
