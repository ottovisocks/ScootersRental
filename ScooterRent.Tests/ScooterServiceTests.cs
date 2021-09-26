using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScooterRent.Exceptions;
using ScooterRent.Models;
using System.Linq;

namespace ScooterRent.Tests
{
    [TestClass]
    public class ScooterServiceTests
    {
        private ScooterService _target;

        [TestMethod]
        public void AddScooter_BuildNewScooterWithId123456Price0dot05_1Expected()
        {
            // Arrange
            _target = new ScooterService();
            var expected = 1;
            // Act
            _target.AddScooter("123", (decimal)0.05);
            var outcome = _target.GetScooters().Count();
            // Assert
            Assert.AreEqual(expected, outcome);
        }

        [TestMethod]
        public void RemoveScooter_BuildNewScooterWithId123456Price0dot05_0Expected()
        {
            // Arrange
            _target = new ScooterService();
            var expected = 0;
            // Act
            _target.AddScooter("123", (decimal)0.05);
            _target.RemoveScooter("123");
            var outcome = _target.GetScooters().Count();
            // Assert
            Assert.AreEqual(expected, outcome);
        }

        [TestMethod]
        public void RemoveScooter_BuildNewScooterWithId123456Price0dot05_ExceptionExpected()
        {
            // Arrange
            _target = new ScooterService();
            var expectedMessage = "Scooter is not found!";
            // Act
            _target.AddScooter("123", (decimal)0.05);
            // Assert
            ScooterIsNotAvailableException e = Assert.ThrowsException<ScooterIsNotAvailableException>(() => _target.RemoveScooter("13"));
            Assert.AreEqual(expectedMessage, e.Message);

        }

        [TestMethod]
        public void RemoveScooter_BuildNewScooterWithId123456Price0dot05IsRentedTrue_1Expected()
        {
            // Arrange
            _target = new ScooterService();
            var expectedMessage = "Scooter is rented!";
            // Act
            _target.AddScooter("123", (decimal)0.05);
            _target.GetScooterById("123").IsRented = true;
            // Assert
            ScooterIsNotAvailableException e = Assert.ThrowsException<ScooterIsNotAvailableException>(() => _target.RemoveScooter("123"));
            Assert.AreEqual(expectedMessage, e.Message);
        }
    }
}
