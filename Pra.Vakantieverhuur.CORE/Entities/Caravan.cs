using System;
using System.Collections.Generic;
using System.Text;

namespace Pra.Vakantieverhuur.CORE.Entities
{
    public class Caravan : Residence
    {

        public bool? PrivateSanitaryBlock { get; set; }

        public override string ToString()
        {
            return $"C - {base.ToString()}";
        }

    }
}
