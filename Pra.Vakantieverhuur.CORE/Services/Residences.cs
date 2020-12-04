using System;
using System.Collections.Generic;
using System.Text;
using Pra.Vakantieverhuur.CORE.Entities;

namespace Pra.Vakantieverhuur.CORE.Services
{
    public class Residences
    {
        private List<Residence> allResidences;

        public List<Residence> AllResidences
        {
            get { return allResidences; }
        }
        public Residences()
        {
            allResidences = new List<Residence>();
            GenerateResidences();
        }

        private void GenerateResidences()
        {
            AllResidences.Add(new VacationHouse
            {
                BasePrice = 50m,
                ReducedPrice = 40m,
                DaysForReduction = 3,
                Deposit = 100m,
                MaxPersons = 4,
                StreetAndNumber = "Herdenkingswijk 22",
                ResidenceName = "Casa Herman",
                Town = "Merelbeke",
                PostalCode = "9820",
                Microwave = true,
                TV = true,
                IsRentable = true,
                DishWasher = true,
                WashingMachine = true,
                WoodStove = true
            });

            AllResidences.Add(new Caravan
            {
                BasePrice = 20m,
                ReducedPrice = 15m,
                DaysForReduction = 5,
                Deposit = 50m,
                MaxPersons = 2,
                StreetAndNumber = "Begoniastraat 1",
                ResidenceName = "Hermans Caravan",
                Town = "Melle",
                PostalCode = "9090",
                Microwave = false,
                TV = false,
                IsRentable = true,
                PrivateSanitaryBlock = true
            });
        }
    }
}
