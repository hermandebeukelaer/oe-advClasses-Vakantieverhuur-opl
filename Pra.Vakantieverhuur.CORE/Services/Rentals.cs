using System;
using System.Collections.Generic;
using System.Text;
using Pra.Vakantieverhuur.CORE.Entities;

namespace Pra.Vakantieverhuur.CORE.Services
{
    public class Rentals
    {
        private List<Rental> allRentals;

        public List<Rental> AllRentals
        {
            get { return allRentals; }
        }
        public Rentals()
        {
            allRentals = new List<Rental>();
        }

        public bool IsOverbooking(Rental newRental)
        {
            foreach(Rental rental in AllRentals)
            {
                if(rental.HolidayResidence == newRental.HolidayResidence)
                {
                    // other rental found for same house: check overlap
                    bool overlap = !(newRental.DateStart >= rental.DateEnd || newRental.DateEnd <= rental.DateStart);
                    if (overlap)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Add(Rental rental)
        {
            AllRentals.Add(rental);
        }

    }
}
