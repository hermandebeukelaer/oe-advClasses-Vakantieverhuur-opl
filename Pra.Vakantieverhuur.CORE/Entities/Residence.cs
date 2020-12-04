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
                    throw new ArgumentOutOfRangeException("BasePrice", "Prijs mag niet negatief zijn");
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
                    throw new ArgumentOutOfRangeException("ReducedPrice", "Prijs mag niet negatief zijn");
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
                    throw new ArgumentOutOfRangeException("DaysForReduction", "Aantal dagen mag niet negatief zijn");
                }
                if(value > 100)
                {
                    throw new ArgumentOutOfRangeException("ReducedPrice", "Aantal dagen tot kortingsprijs mag niet hoger zijn dan 100");
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
                    throw new ArgumentOutOfRangeException("Deposit", "Waarborg mag niet negatief zijn");
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
                if(value < 1 || value > 20)
                {
                    throw new ArgumentOutOfRangeException("MaxPersons", "Maximaal aantal personen moet een waarden van 1 t/m 20 hebben.");
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
