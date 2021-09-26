using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScooterRent.Exceptions;
using ScooterRent.Models;
using System;

namespace ScooterRent.Tests
{
    [TestClass]
    public class RentalCompanyTest
    {
        private RentalCompany _target;

        [TestMethod]
        public void StartRent_BuildNewScooterSetIsRentedTrue_ExceptionExpected()
        {
            // Arrange
            var scooter = new ScooterService();
            scooter.AddScooter("123", (decimal)0.05);
            scooter.GetScooterById("123").IsRented = true;
            _target = new RentalCompany(scooter, "name");
            var expectedMessage = "Scooter is not available";
            // Assert
            ScooterIsNotAvailableException e = Assert.ThrowsException<ScooterIsNotAvailableException>(() => _target.StartRent("123"));
            Assert.AreEqual(expectedMessage, e.Message);
        }

        [TestMethod]
        public void StartRent_BuildNewScooter_IsRentedTrueExpected()
        {
            // Arrange
            var scooter = new ScooterService();
            scooter.AddScooter("123", (decimal)0.05);
            _target = new RentalCompany(scooter, "name");
            var expected = true;
            // Act
            _target.StartRent("123");
            var outcome = scooter.GetScooterById("123").IsRented;
            // Assert
            Assert.AreEqual(expected, outcome);
        }

        [TestMethod]
        public void StartRent_BuildNewScooter_2Expected()
        {
            // Arrange
            var scooter = new ScooterService();
            scooter.AddScooter("123", (decimal)0.05);
            _target = new RentalCompany(scooter, "name");
            var expected = 2;
            // Act
            _target.StartRent("123");
            var outcome = _target.ReturnRentalDataList().Count;
            // Assert
            Assert.AreEqual(expected, outcome);
        }

        [TestMethod]
        public void EndRent_BuildNewScooterEndRentWithWrongId_ExceptionExpected()
        {
            // Arrange
            var scooter = new ScooterService();
            scooter.AddScooter("123", (decimal)0.05);
            _target = new RentalCompany(scooter, "name");
            _target.StartRent("123");
            var expectedMessage = "Scooter is not found!";
            // Assert
            ScooterIsNotAvailableException e = Assert.ThrowsException<ScooterIsNotAvailableException>(() => _target.EndRent("12"));
            Assert.AreEqual(expectedMessage, e.Message);
        }

        [TestMethod]
        public void EndRent_BuildNewScooterScooterIsNotRented_ExceptionExpected()
        {
            // Arrange
            var scooter = new ScooterService();
            scooter.AddScooter("123", (decimal)0.05);
            _target = new RentalCompany(scooter, "name");
            var expectedMessage = "Scooter is not rented!";
            // Assert
            ScooterIsNotAvailableException e = Assert.ThrowsException<ScooterIsNotAvailableException>(() => _target.EndRent("123"));
            Assert.AreEqual(expectedMessage, e.Message);
        }

        [TestMethod]
        public void EndRent_BuildNewScooterEndRent_4Expected()
        {
            // Arrange
            var scooter = new ScooterService();
            scooter.AddScooter("123", (decimal)0.05);
            _target = new RentalCompany(scooter, "name");
            _target.StartRent("123");
            var expected = 4;
            // Act
            _target.EndRent("123");
            var outcome = _target.ReturnRentalDataList().Count;
            // Assert
            Assert.AreEqual(expected, outcome);
        }

        [TestMethod]
        public void GetDateTimeAndAddToList_OnlyCheckThisMethod_2Expected()
        {
            // Arrange
            var scooter = new ScooterService();
            scooter.AddScooter("123", (decimal)0.05);
            _target = new RentalCompany(scooter, "name");
            var expected = 2;
            // Act
            _target.GetDateTimeAndAddToList();
            var outcome = _target.ReturnRentalDataList().Count;
            // Assert
            Assert.AreEqual(expected, outcome);
        }

        [TestMethod]
        public void CalculateIncome_YearAndIncludeNotCompletedRentals_Expected()
        {
            // Arrange
            RentalHistory.AddFinishedHistory("Name 01/02/2019 08:23:05 01/05/2022 22:12:42 0,05 124,50");
            RentalHistory.AddFinishedHistory("Name 01/02/2021 08:23:05 01/05/2021 22:12:42 0,05 1234,40");
            RentalHistory.AddFinishedHistory("Name 01/02/2020 08:23:05 01/05/2021 22:12:42 0,05 14,00");
            RentalHistory.AddFinishedHistory("Name 01/02/2021 08:23:05 01/05/2022 22:12:42 0,05 1254,50");
            RentalHistory.AddUnfinishedHistory("id name 0,05 05/02/2020 08:23:05");
            RentalHistory.AddUnfinishedHistory("id name 0,05 09/03/2021 09:23:05");
            RentalHistory.AddUnfinishedHistory("id name 0,05 04/07/2020 10:23:05");
            RentalHistory.AddUnfinishedHistory("id name 0,05 07/04/2020 11:23:00");
            var scooter = new ScooterService();
            scooter.AddScooter("123", (decimal)0.05);
            _target = new RentalCompany(scooter, "name");
            var expected = 1000m;
            // Act
            var outcome = _target.CalculateIncome(2021, true);
            RentalHistory.ClearAllLists();
            // Assert
            Assert.AreEqual(expected, outcome);
        }

        [TestMethod]
        public void CalculateIncome_OnlyIncludeNotCompletedRentals_Expected()
        {
            // Arrange
            RentalHistory.AddFinishedHistory("Name 01/02/2019 08:23:05 01/05/2022 22:12:42 0,05 124,50");
            RentalHistory.AddFinishedHistory("Name 01/02/2021 08:23:05 01/05/2021 22:12:42 0,05 1234,40");
            RentalHistory.AddFinishedHistory("Name 01/02/2020 08:23:05 01/05/2021 22:12:42 0,05 14,00");
            RentalHistory.AddFinishedHistory("Name 01/02/2021 08:23:05 01/05/2022 22:12:42 0,05 1254,50");
            RentalHistory.AddUnfinishedHistory("id name 0,05 05/02/2020 08:23:05");
            RentalHistory.AddUnfinishedHistory("id name 0,05 09/03/2021 09:23:05");
            RentalHistory.AddUnfinishedHistory("id name 0,05 04/07/2020 10:23:05");
            RentalHistory.AddUnfinishedHistory("id name 0,05 07/04/2020 11:23:00");
            var scooter = new ScooterService();
            scooter.AddScooter("123", (decimal)0.05);
            _target = new RentalCompany(scooter, "name");
            var expected = 1000m;
            // Act
            var outcome = _target.CalculateIncome(null, true);
            RentalHistory.ClearAllLists();
            // Assert
            Assert.AreEqual(expected, outcome);
        }

        [TestMethod]
        public void CalculateIncome_OnlyYear_Expected()
        {
            // Arrange
            RentalHistory.AddFinishedHistory("Name 01/02/2019 08:23:05 01/05/2022 22:12:42 0,05 124,50");
            RentalHistory.AddFinishedHistory("Name 01/02/2021 08:23:05 01/05/2021 22:12:42 0,05 1234,40");
            RentalHistory.AddFinishedHistory("Name 01/02/2020 08:23:05 01/05/2021 22:12:42 0,05 14,00");
            RentalHistory.AddFinishedHistory("Name 01/02/2021 08:23:05 01/05/2022 22:12:42 0,05 1254,50");
            var scooter = new ScooterService();
            scooter.AddScooter("123", (decimal)0.05);
            _target = new RentalCompany(scooter, "name");
            var expected = 1000m;
            // Act
            var outcome = _target.CalculateIncome(2021, false);
            RentalHistory.ClearAllLists();
            // Assert
            Assert.AreEqual(expected, outcome);
        }

        [TestMethod]
        public void CalculateIncome_WithoutYearAndIncludeNotCompletedRentals_2627dot40Expected()
        {
            // Arrange
            RentalHistory.AddFinishedHistory("Name 01/02/2019 08:23:05 01/05/2022 22:12:42 0,05 124,50");
            RentalHistory.AddFinishedHistory("Name 01/02/2021 08:23:05 01/05/2021 22:12:42 0,05 1234,40");
            RentalHistory.AddFinishedHistory("Name 01/02/2020 08:23:05 01/05/2021 22:12:42 0,05 14,00");
            RentalHistory.AddFinishedHistory("Name 01/02/2021 08:23:05 01/05/2022 22:12:42 0,05 1254,50");
            var scooter = new ScooterService();
            scooter.AddScooter("123", (decimal)0.05);
            _target = new RentalCompany(scooter, "name");
            var expected = 2627.40m;
            // Act
            var outcome = _target.CalculateIncome(null, false);
            RentalHistory.ClearAllLists();
            // Assert
            Assert.AreEqual(expected, outcome);
        }
    }
}
