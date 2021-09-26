using System;
using System.Collections.Generic;

namespace ScooterRent.Models
{
    public class PaymentPreparation
    {
        public decimal CalculatePayment(List<string> rentDateList, decimal pricePerMinute, int costPerDay)
        {
            var startDay = rentDateList[0].Split('/');
            var endDay = rentDateList[2].Split('/');
            var days = (new DateTime(Convert.ToInt32(endDay[2]), Convert.ToInt32(endDay[0]),
                Convert.ToInt32(endDay[1])) - new DateTime(Convert.ToInt32(startDay[2]),
                Convert.ToInt32(startDay[0]), Convert.ToInt32(startDay[1]))).TotalDays;

            //(days - 1), 01/01/2021 - 01/01/2021 = 0, 01/01/2021 - 01/02/2021 = 1, (day 3 = 2 - 1) || -1 from day 3 //
            var payPerDays = (decimal)(days - 1) * costPerDay;
            var payPerMinutesFirstDay = (ConvertTimeToMinutes("24:00:00") - ConvertTimeToMinutes(rentDateList[1])) * pricePerMinute;
            var payPerMinutesLastDay = ConvertTimeToMinutes(rentDateList[3]) * pricePerMinute;
            var startAndEndRentSameDay = (ConvertTimeToMinutes(rentDateList[3]) - ConvertTimeToMinutes(rentDateList[1])) * pricePerMinute;
            payPerMinutesFirstDay = payPerMinutesFirstDay > costPerDay ? costPerDay : payPerMinutesFirstDay;
            payPerMinutesLastDay = payPerMinutesLastDay > costPerDay ? costPerDay : payPerMinutesLastDay;
            startAndEndRentSameDay = startAndEndRentSameDay > costPerDay ? costPerDay : startAndEndRentSameDay;

            if (days == 0)
                return startAndEndRentSameDay;

            if (days == 1)
                return payPerMinutesLastDay + payPerMinutesFirstDay;

            return payPerDays + payPerMinutesLastDay + payPerMinutesFirstDay;
        }

        public int ConvertTimeToMinutes(string time)
        {
            var list = time.Split(':');
            var secToMin = 0;

            if (Convert.ToInt32(list[2]) != 0)
            {
                secToMin = 1;
            }

            return (Convert.ToInt32(list[0]) * 60) + Convert.ToInt32(list[1]) + secToMin;
        }
    }
}
