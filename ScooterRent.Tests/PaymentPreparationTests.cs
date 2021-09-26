using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScooterRent.Models;
using System;
using System.Collections.Generic;

namespace ScooterRent.Tests
{
    [TestClass]
    public class PaymentPreparationTests
    {
        private PaymentPreparation _target = new PaymentPreparation();

        [TestMethod]
        public void CalculatePayment_AddFullYearToCalculate_7300Expected()
        {
            // Arrange
            var list = new List<string>() { "01/01/2021", "00:00:00", "12/31/2021", "23:00:00" };
            var expected = 7300.00m;
            // Act
            var outcome = _target.CalculatePayment(list, 0.05m, 20);
            // Assert
            Assert.AreEqual(expected, outcome);
        }

        [TestMethod]
        public void CalculatePayment_AddTwoYearToCalculate_7300Expected()
        {
            // Arrange
            var list = new List<string>() { "01/01/2021", "00:00:00", "12/31/2022", "23:00:00" };
            var expected = 14600.00m;
            // Act
            var outcome = _target.CalculatePayment(list, 0.05m, 20);
            // Assert
            Assert.AreEqual(expected, outcome);
        }

        [TestMethod]
        public void CalculatePayment_AddFullDayToCalculate_20Expected()
        {
            // Arrange
            var list = new List<string>() { "01/01/2021", "00:00:00", "01/02/2021", "00:00:00" };
            var expected = 20.00m;
            // Act
            var outcome = _target.CalculatePayment(list, 0.05m, 20);
            // Assert
            Assert.AreEqual(expected, outcome);
        }

        [TestMethod]
        public void CalculatePayment_Add5hoursToCalculate_15Expected()
        {
            // Arrange
            var list = new List<string>() { "01/01/2021", "14:00:00", "01/01/2021", "19:00:00" };
            var expected = 15.00m;
            // Act
            var outcome = _target.CalculatePayment(list, 0.05m, 20);
            // Assert
            Assert.AreEqual(expected, outcome);
        }

        [TestMethod]
        public void CalculatePayment_AddTwoDaysToCalculate_40Expected()
        {
            // Arrange
            var list = new List<string>() { "01/01/2021", "14:00:00", "01/02/2021", "19:00:00" };
            var expected = 40.00m;
            // Act
            var outcome = _target.CalculatePayment(list, 0.05m, 20);
            // Assert
            Assert.AreEqual(expected, outcome);
        }

        [TestMethod]
        public void CalculatePayment_AddTwoDaysToCalculate_26dot70Expected()
        {
            // Arrange
            var list = new List<string>() { "01/01/2021", "21:45:01", "01/02/2021", "14:12:46" };
            var expected = 26.70m;
            // Act
            var outcome = _target.CalculatePayment(list, 0.05m, 20);
            // Assert
            Assert.AreEqual(expected, outcome);
        }

        [TestMethod]
        public void CalculatePayment_AddThreeDaysToCalculate_46Expected()
        {
            // Arrange
            var list = new List<string>() { "01/01/2021", "22:00:00", "01/03/2021", "07:00:00" };
            var expected = 46.00m;
            // Act
            var outcome = _target.CalculatePayment(list, 0.05m, 20);
            // Assert
            Assert.AreEqual(expected, outcome);
        }

        [TestMethod]
        public void CalculatePayment_AddFourDaysToCalculate_66Expected()
        {
            // Arrange
            var list = new List<string>() { "01/01/2021", "22:00:00", "01/04/2021", "07:00:00" };
            var expected = 66.00m;
            // Act
            var outcome = _target.CalculatePayment(list, 0.05m, 20);
            // Assert
            Assert.AreEqual(expected, outcome);
        }

        [TestMethod]
        public void CalculatePayment_AddFiveDaysToCalculate_75Expected()
        {
            // Arrange
            var list = new List<string>() { "01/01/2021", "23:00:00", "01/05/2021", "04:00:00" };
            var expected = 75.00m;
            // Act
            var outcome = _target.CalculatePayment(list, 0.05m, 20);
            // Assert
            Assert.AreEqual(expected, outcome);
        }

        [TestMethod]
        public void ConvertTimeToMinutes_Add5hoursToCalculateMinutes_300Expected()
        {
            // Arrange
            var expected = 300;
            // Act
            var outcome = _target.ConvertTimeToMinutes("05:00:00");
            // Assert
            Assert.AreEqual(expected, outcome);
        }

        [TestMethod]
        public void ConvertTimeToMinutes_Add5hours40min5secToCalculateMinutes_641Expected()
        {
            // Arrange
            var expected = 641;
            // Act
            var outcome = _target.ConvertTimeToMinutes("10:40:05");
            // Assert
            Assert.AreEqual(expected, outcome);
        }
    }
}
