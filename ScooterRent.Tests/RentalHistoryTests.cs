using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScooterRent.Models;
using System;

namespace ScooterRent.Tests
{
    [TestClass]
    public class RentalHistoryTests
    {
        [TestMethod]
        public void AddFinishedHistoryAndReturnFinishedHistory_AddDataToList_1Expected()
        {
            // Arrange
            RentalHistory.AddFinishedHistory("Hystory data");
            var expected = 1;
            // Act
            var outcome = RentalHistory.ReturnFinishedHistory().Count;
            RentalHistory.ClearAllLists();
            // Assert
            Assert.AreEqual(expected, outcome);
        }

        [TestMethod]
        public void AddUnfinishedHistoryAndReturnUnfinishedHistory_AddDataToList_1Expected()
        {
            // Arrange
            RentalHistory.AddUnfinishedHistory("Hystory data");
            var expected = 1;
            // Act
            var outcome = RentalHistory.ReturnUnfinishedHistory().Count;
            RentalHistory.ClearAllLists();
            // Assert
            Assert.AreEqual(expected, outcome);
        }

        [TestMethod]
        public void ClearUnfinishedHistoryById_FindById_0Expected()
        {
            // Arrange
            RentalHistory.AddUnfinishedHistory("123 History Data");
            var expected = 0;
            // Act
            RentalHistory.ClearUnfinishedHistoryById("123");
            var outcome = RentalHistory.ReturnUnfinishedHistory().Count;
            RentalHistory.ClearAllLists();
            // Assert
            Assert.AreEqual(expected, outcome);
        }

        [TestMethod]
        public void ClearUnfinishedHistoryById_FindByWrongId_1Expected()
        {
            // Arrange
            RentalHistory.AddUnfinishedHistory("123 History Data");
            var expected = 1;
            // Act
            RentalHistory.ClearUnfinishedHistoryById("12");
            var outcome = RentalHistory.ReturnUnfinishedHistory().Count;
            RentalHistory.ClearAllLists();
            // Assert
            Assert.AreEqual(expected, outcome);
        }
    }
}
