using ScooterRent.Interfaces;
using ScooterRent.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace ScooterRent.Models
{
    public class ScooterService : IScooterService
    {
        private List<Scooter> _scootersList;

        public ScooterService()
        {
            _scootersList = new List<Scooter>();
        }

        public void AddScooter(string id, decimal pricePerMinute)
        {
            _scootersList.Add(new Scooter(id, pricePerMinute));
        }

        public void RemoveScooter(string id)
        {
            Scooter scooter = GetScooterById(id);


            if (scooter == null)
            {
                throw new ScooterIsNotAvailableException("Scooter is not found!");
            }
            if (scooter.IsRented)
            {
                throw new ScooterIsNotAvailableException("Scooter is rented!");
            }
            else
            {
                _scootersList.Remove(scooter);
            }
        }

        public IList<Scooter> GetScooters()
        {
            return _scootersList;
        }

        public Scooter GetScooterById(string scooterId)
        {
            return _scootersList.SingleOrDefault(s => s.Id == scooterId);
        }
    }
}
