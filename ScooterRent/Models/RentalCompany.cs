using ScooterRent.Interfaces;
using ScooterRent.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScooterRent.Models
{
    public class RentalCompany : IRentalCompany
    {
        public string Name { get; }
        private int _costPerDay = 20;
        private List<string> _rentDateList = new List<string>();
        private PaymentPreparation _paymentPreparation = new PaymentPreparation();
        private RentalReport _rentalReport = new RentalReport();
        private IScooterService _service;

        public RentalCompany(IScooterService serviss, string name)
        {
            _service = serviss;
            Name = name;
        }

        public void StartRent(string id)
        {
            var scooter = _service.GetScooterById(id);

            if (scooter.IsRented)
            {
                throw new ScooterIsNotAvailableException();
            }
            else
            {
                scooter.IsRented = true;
                GetDateTimeAndAddToList();
                RentalHistory.AddUnfinishedHistory($"{id} {Name} {scooter.PricePerMinute} " +
                    $"{_rentDateList[0]} {_rentDateList[1]}");
            }
        }

        public decimal EndRent(string id)
        {
            var scooter = _service.GetScooterById(id);

            if (scooter is null)
                throw new ScooterIsNotAvailableException("Scooter is not found!");

            if (!scooter.IsRented)
                throw new ScooterIsNotAvailableException("Scooter is not rented!");

            GetDateTimeAndAddToList();
            RentalHistory.ClearUnfinishedHistoryById(id);
            scooter.IsRented = false;
            decimal totalCost = _paymentPreparation.CalculatePayment(_rentDateList, scooter.PricePerMinute, _costPerDay);
            RentalHistory.AddFinishedHistory($"{Name} {_rentDateList[0]} {_rentDateList[1]} " +
                $"{_rentDateList[2]} {_rentDateList[3]} {scooter.PricePerMinute} {totalCost}");

            return totalCost;
        }

        public void GetDateTimeAndAddToList()
        {
            var dateTimeNow = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss").Split(' ').ToList();

            foreach (var item in dateTimeNow)
            {
                _rentDateList.Add(item);
            }
        }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            var dateTimeNow = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss").Split(' ').ToList();
            decimal income = 0m;

            if (year is null)
            {
                if (includeNotCompletedRentals)
                {
                    income += _rentalReport.ReturnUnfinishedRentalIncome(dateTimeNow, _costPerDay);
                }

                income += _rentalReport.ReturnAllFinishedRentalIncome();

                return income;
            }

            if (includeNotCompletedRentals)
            {
                income += _rentalReport.ReturnUnfinishedRentalIncomeByYear(dateTimeNow, _costPerDay, (int)year);
            }

            income += _rentalReport.ReturnAllFinishedRentalIncomeByYear((int)year, _costPerDay);

            return income;
        }

        public List<string> ReturnRentalDataList()
        {
            return _rentDateList;
        }
    }
}
