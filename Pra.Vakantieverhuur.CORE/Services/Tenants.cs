using System;
using System.Collections.Generic;
using System.Text;
using Pra.Vakantieverhuur.CORE.Entities;

namespace Pra.Vakantieverhuur.CORE.Services
{
    public class Tenants
    {
        private List<Tenant> allTenants;

        public List<Tenant> AllTenants
        {
            get { return allTenants; }
        }
        public Tenants()
        {
            allTenants = new List<Tenant>();
            GenerateTenants();
        }

        private void GenerateTenants()
        {
            AllTenants.Add(new Tenant
            {
                Address = "Herdenkingswijk 22",
                Town = "Merelbeke",
                Country = "België",
                Email = "herman.de.beukelaer@howest.be",
                Firstname = "Herman", 
                Name = "De Beukelaer",
                Phone = "123456789",
                IsBlacklisted = false
            });

            AllTenants.Add(new Tenant
            {
                Address = "Fopstraat 74",
                Town = "Leugenstad",
                Country = "Neverland",
                Email = "fictiefje@msn.com",
                Firstname = "Fictiefje",
                Name = "De Vervalser",
                Phone = "000000000",
                IsBlacklisted = true
            });

        }
    }
}
