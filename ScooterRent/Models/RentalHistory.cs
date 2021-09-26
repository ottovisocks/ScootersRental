using ScooterRent.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRent.Models
{
    public static class RentalHistory
    {
        private static List<string> _rentHistoryFinished = new List<string>();
        private static List<string> _rentHistoryUnfinished = new List<string>();

        public static void AddFinishedHistory(string rentalData)
        {
            _rentHistoryFinished.Add(rentalData);
        }

        public static List<string> ReturnFinishedHistory()
        {
            return _rentHistoryFinished;
        }

        public static void AddUnfinishedHistory(string rentalData)
        {
            _rentHistoryUnfinished.Add(rentalData);
        }

        public static void ClearUnfinishedHistoryById(string id)
        {
            foreach (var item in _rentHistoryUnfinished)
            {
                var getId = item.Split(' ')[0];
                if (getId == id)
                    _rentHistoryUnfinished.Remove(item);

                break;
            }
        }

        public static List<string> ReturnUnfinishedHistory()
        {
            return _rentHistoryUnfinished;
        }

        public static void ClearAllLists()
        {
            _rentHistoryFinished.Clear();
            _rentHistoryUnfinished.Clear();
        }
    }
}
