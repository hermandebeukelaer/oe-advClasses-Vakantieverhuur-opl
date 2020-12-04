﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Pra.Vakantieverhuur.CORE.Entities
{
    public class Rental
    {

        public Residence HoidayResidence { get; set; }
        public Tenant HolidayTenant { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public bool IsDepositPaid { get; set; }
        public decimal Paid { get; set; }
        public decimal ToPay { get; set; }

    }
}
