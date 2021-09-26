using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScooterRent.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScooterRent.Tests
{
    [TestClass]
    public class RentalReportTests
    {
        private List<string> _dateTimeNow = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss").Split(' ').ToList();
        private int _costPerDay = 20;
        private RentalReport _target = new RentalReport();

        [TestMethod]
        public void ReturnAllFinishedRentalIncome_AddRentalHistoryAndReturnAllFinishedIncome_2627dot40Expected()
        {
            // Arrange
            RentalHistory.AddFinishedHistory("Name 01/02/2019 08:23:05 01/05/2022 22:12:42 0,05 124,50");
            RentalHistory.AddFinishedHistory("Name 01/02/2021 08:23:05 01/05/2021 22:12:42 0,05 1234,40");
            RentalHistory.AddFinishedHistory("Name 01/02/2020 08:23:05 01/05/2021 22:12:42 0,05 14,00");
            RentalHistory.AddFinishedHistory("Name 01/02/2021 08:23:05 01/05/2022 22:12:42 0,05 1254,50");
            var expected = 2627.40m;
            // Act
            var outcome = _target.ReturnAllFinishedRentalIncome();
            RentalHistory.ClearAllLists();
            // Assert
            Assert.AreEqual(expected, outcome);
        }

        [TestMethod]
        public void ReturnUnfinishedRentalIncome_AddUnfinishedRentalHistoryAndReturnAllUnfinishedIncome_Expected()
        {
            // Arrange
            RentalHistory.AddUnfinishedHistory("id name 0,05 05/02/2020 08:23:05");
            RentalHistory.AddUnfinishedHistory("id name 0,05 09/03/2021 09:23:05");
            RentalHistory.AddUnfinishedHistory("id name 0,05 04/07/2020 10:23:05");
            RentalHistory.AddUnfinishedHistory("id name 0,05 07/04/2020 11:23:00");
            var expected = 2627.40m;
            // Act
            var outcome = _target.ReturnUnfinishedRentalIncome(_dateTimeNow, _costPerDay);
            RentalHistory.ClearAllLists();
            // Assert
            Assert.AreEqual(expected, outcome);
        }

        [TestMethod]
        public void ReturnAllFinishedRentalIncomeByYear_AddRentalHistoryAndReturnAllFinishedIncomeByYear_Expected()
        {
            // Arrange
            RentalHistory.AddFinishedHistory("Name 01/02/2019 08:23:05 01/05/2022 22:12:42 0,05 124,50");
            RentalHistory.AddFinishedHistory("Name 01/02/2021 08:23:05 01/05/2021 22:12:42 0,05 1234,40");
            RentalHistory.AddFinishedHistory("Name 01/02/2020 08:23:05 01/05/2021 22:12:42 0,05 14,00");
            RentalHistory.AddFinishedHistory("Name 01/02/2021 08:23:05 01/05/2022 22:12:42 0,05 1254,50");
            var expected = 2627.40m;
            // Act
            var outcome = _target.ReturnAllFinishedRentalIncomeByYear(2021, _costPerDay);
            RentalHistory.ClearAllLists();
            // Assert
            Assert.AreEqual(expected, outcome);
        }

        [TestMethod]
        public void ReturnUnfinishedRentalIncomeByYear_AddUnfinishedRentalHistoryAndReturnAllUnfinishedIncomeByYear_Expected()
        {
            // Arrange
            RentalHistory.AddUnfinishedHistory("id name 0,05 05/02/2020 08:23:05");
            RentalHistory.AddUnfinishedHistory("id name 0,05 09/03/2021 09:23:05");
            RentalHistory.AddUnfinishedHistory("id name 0,05 04/07/2020 10:23:05");
            RentalHistory.AddUnfinishedHistory("id name 0,05 07/04/2020 11:23:00");
            var expected = 2627.40m;
            // Act
            var outcome = _target.ReturnUnfinishedRentalIncomeByYear(_dateTimeNow, _costPerDay, 2021);
            RentalHistory.ClearAllLists();
            // Assert
            Assert.AreEqual(expected, outcome);
        }
    }
}