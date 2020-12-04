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

        public List<Residence> GetAllVacationHouses()
        {
            List<Residence> vacationHouses = new List<Residence>();
            foreach (Residence residence in AllResidences)
            {
                if(residence is VacationHouse house)
                {
                    vacationHouses.Add(house);
                }
            }
            return vacationHouses;
        }

        public List<Residence> GetAllCaravans()
        {

            List<Residence> caravans = new List<Residence>();
            foreach (Residence residence in AllResidences)
            {
                /*if (residence is Caravan)
                {
                    caravans.Add(residence);
                }*/
                if (residence is Caravan caravan)
                {
                    caravans.Add(caravan);
                }
            }
            return caravans;
        }

        public bool Remove(Residence residence)
        {
            return AllResidences.Remove(residence);
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
