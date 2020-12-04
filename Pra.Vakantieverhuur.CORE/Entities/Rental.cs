using System;
using System.Collections.Generic;
using System.Text;

namespace Pra.Vakantieverhuur.CORE.Entities
{
    public class Rental
    {

        public Residence HolidayResidence { get; set; }
        public Tenant HolidayTenant { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public bool IsDepositPaid { get; set; }
        public decimal Paid { get; set; }
        public decimal ToPay { get; set; }

        public int CalculateNumberOfNights()
        {
            TimeSpan timeBetween = DateEnd - DateStart;
            return (int) timeBetween.TotalDays;
        }

        public decimal CalculateTotalPrice()
        {
            int totalNights = CalculateNumberOfNights();
            int nightsReducedPrice = Math.Min(totalNights, HolidayResidence.DaysForReduction);
            int nightsFullPrice = totalNights - nightsReducedPrice;

            return nightsReducedPrice * HolidayResidence.ReducedPrice + nightsFullPrice * HolidayResidence.BasePrice;
        }

    }
}
