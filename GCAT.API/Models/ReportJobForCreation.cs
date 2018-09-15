using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GCAT.API.Models
{
    public class ReportJobForCreation
    {
        public long ReportTypeId { get; set; }

        public string Paramaters { get; set; }
    }
}