using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScooterRent.Models;

namespace ScooterRent.Tests
{
    [TestClass]
    public class ScooterTests
    {
        private Scooter _target;

        [TestMethod]
        public void Id_BuildNewScooterWithId123456Price0dot05_Id123456Expected()
        {
            // Arrange
            _target = new Scooter("123456", 0.05m);
            var expected = "123456";
            // Act
            var outcome = _target.Id;
            // Assert
            Assert.AreEqual(expected, outcome);
        }

        [TestMethod]
        public void PricePerMinute_BuildNewScooterWithId123456Price0dot05_Price0dot05Expected()
        {
            // Arrange
            _target = new Scooter("123456", (decimal)0.05);
            var expected = (decimal)0.05;
            // Act
            var outcome = _target.PricePerMinute;
            // Assert
            Assert.AreEqual(expected, outcome);
        }

        [TestMethod]
        public void IsRented_BuildNewScooterWithId123456Price0dot05_FalseExpected()
        {
            // Arrange
            _target = new Scooter("123456", (decimal)0.05);
            var expected = false;
            // Act
            var outcome = _target.IsRented;
            // Assert
            Assert.AreEqual(expected, outcome);
        }

        [TestMethod]
        public void IsRented_BuildNewScooterWithId123456Price0dot05_TrueExpected()
        {
            // Arrange
            _target = new Scooter("123456", (decimal)0.05);
            var expected = true;
            // Act
            _target.IsRented = true;
            var outcome = _target.IsRented;
            // Assert
            Assert.AreEqual(expected, outcome);
        }
    }
}
