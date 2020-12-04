using System;
using System.Collections.Generic;
using System.Text;

namespace Pra.Vakantieverhuur.CORE.Entities
{
    public class Residence
    {

        public string ID { get; } = Guid.NewGuid().ToString();

        private decimal basePrice;

        public decimal BasePrice
        {
            get { return basePrice; }
            set
            { 
                if(value < 0)
                {
                    value = 0;
                }
                basePrice = value; 
            }
        }

        private decimal reducedPrice;

        public decimal ReducedPrice
        {
            get { return reducedPrice; }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                reducedPrice = value;
            }
        }

        private byte daysForReduction;

        public byte DaysForReduction
        {
            get { return daysForReduction; }
            set
            { 
                if(value < 0)
                {
                    value = 0;
                }
                if(value > 100)
                {
                    value = 100;
                }
                daysForReduction = value; 
            }
        }

        private decimal deposit;

        public decimal Deposit
        {
            get { return deposit; }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                deposit = value;
            }
        }

        private int maxPersons;

        public int MaxPersons
        {
            get { return maxPersons; }
            set
            {
                if(value < 1)
                {
                    value = 1;
                }
                if(value > 20)
                {
                    value = 20;
                }
                maxPersons = value;
            }
        }

        public string StreetAndNumber { get; set; }

        public string ResidenceName { get; set; }

        public string Town { get; set; }

        public string PostalCode { get; set; }

        public bool? Microwave { get; set; }

        public bool? TV { get; set; }

        public bool IsRentable { get; set; }

        public override string ToString()
        {
            return $"{ResidenceName} - {Town}";
        }

    }
}
