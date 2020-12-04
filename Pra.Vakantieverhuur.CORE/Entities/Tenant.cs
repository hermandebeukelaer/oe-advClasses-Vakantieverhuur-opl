using System;
using System.Collections.Generic;
using System.Text;

namespace Pra.Vakantieverhuur.CORE.Entities
{
    public class Tenant : Person
    {

        public bool IsBlacklisted { get; set; }

        public override string ToString()
        {
            return $"{Name} {Firstname} - {Country}";
        }

    }
}
