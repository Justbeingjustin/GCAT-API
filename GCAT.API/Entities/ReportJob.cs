using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GCAT.API.Entities
{
    [Table("ReportJobs")]
    public class ReportJob
    {
        [Key]
        public long ReportJobId { get; set; }

        public string UserId { get; set; }

        public long ReportTypeId { get; set; }

        public string Paramaters { get; set; }

        public long Status { get; set; }

        public string StatusMessage { get; set; }

        public byte[] File { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}