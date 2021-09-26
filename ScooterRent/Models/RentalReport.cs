using System;
using System.Collections.Generic;
using System.Linq;

namespace ScooterRent.Models
{
    public class RentalReport
    {
        private PaymentPreparation _paymentPreparation = new PaymentPreparation();

        public decimal ReturnAllFinishedRentalIncome()
        {
            var income = 0m;

            foreach (var item in RentalHistory.ReturnFinishedHistory())
            {
                income += Convert.ToDecimal(item.Split(' ').ToList().Last());
            }

            return income;
        }

        public decimal ReturnUnfinishedRentalIncome(List<string> dateTimeNowList, int costPerDay)
        {
            var income = 0m;

            foreach (var item in RentalHistory.ReturnUnfinishedHistory())
            {
                var splitedHistory = item.Split(' ').ToList();
                var dateTimeList = new List<string>() { splitedHistory[3], splitedHistory.Last(), dateTimeNowList[0], dateTimeNowList[1] };
                var pricePerMinute = Convert.ToDecimal(splitedHistory[2]);
                income += _paymentPreparation.CalculatePayment(dateTimeList, pricePerMinute, costPerDay);
            }

            return income;
        }

        public decimal ReturnAllFinishedRentalIncomeByYear(int year, int costPerDay)
        {
            var income = 0m;
            int daysPerYear = new DateTime(year, 12, 31).DayOfYear;

            foreach (var item in RentalHistory.ReturnFinishedHistory())
            {
                var startDate = Convert.ToInt32(item.Split(' ')[1].Split('/').Last());
                var endDate = Convert.ToInt32(item.Split(' ')[3].Split('/').Last());
                var itemToList = item.Split(' ').ToList();
                var dummyItemList = new List<string>();

                if (startDate == year && endDate == year)
                    income += ReturnAllFinishedRentalIncome();

                if (startDate < year && endDate > year)
                    income += daysPerYear * costPerDay;

                if (startDate < year && endDate == year)
                {
                    dummyItemList.Add($"01/01/{endDate}");
                    dummyItemList.Add("00:00:00");
                    dummyItemList.Add($"{itemToList[3]}");
                    dummyItemList.Add($"{itemToList[4]}");

                    income += _paymentPreparation.CalculatePayment(dummyItemList,
                        Convert.ToDecimal(itemToList[2]), costPerDay);
                    dummyItemList.Clear();
                }

                if (startDate == year && endDate > year)
                {
                    dummyItemList.Add($"{itemToList[1]}");
                    dummyItemList.Add($"{itemToList[2]}");
                    dummyItemList.Add($"12/31/{startDate}");
                    dummyItemList.Add("24:00:00");

                    income += _paymentPreparation.CalculatePayment(dummyItemList,
                        Convert.ToDecimal(itemToList[2]), costPerDay);
                    dummyItemList.Clear();
                }
            }

            return income;
        }

        public decimal ReturnUnfinishedRentalIncomeByYear(List<string> dateTimeNowList, int costPerDay, int year)
        {
            var income = 0m;

            foreach (var item in RentalHistory.ReturnUnfinishedHistory())
            {
                var startDate = Convert.ToInt32(item.Split(' ').ToList()[1].Split('/').Last());
                var itemToList = item.Split(' ').ToList();
                var dummyItemList = new List<string>();

                if (startDate == year)
                {
                    dummyItemList.Add($"{itemToList[itemToList.Count - 2]}");
                    dummyItemList.Add($"{itemToList.Last()}");
                    dummyItemList.Add($"{dateTimeNowList[0]}");
                    dummyItemList.Add($"{dateTimeNowList[1]}");

                    income += _paymentPreparation.CalculatePayment(dummyItemList, Convert.ToDecimal(itemToList[itemToList.Count - 3]), costPerDay);
                    dummyItemList.Clear();
                }

                if (startDate < year)
                {
                    dummyItemList.Add($"01/01{year}");
                    dummyItemList.Add("00:00");
                    dummyItemList.Add($"{dateTimeNowList[0]}");
                    dummyItemList.Add($"{dateTimeNowList[1]}");

                    income += _paymentPreparation.CalculatePayment(dummyItemList, Convert.ToDecimal(itemToList[itemToList.Count - 3]), costPerDay);
                    dummyItemList.Clear();
                }
            }

            return income;
        }
    }
}
