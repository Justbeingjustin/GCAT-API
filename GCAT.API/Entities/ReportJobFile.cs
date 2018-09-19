using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GCAT.API.Entities
{
    [Table("ReportJobFiles")]
    public class ReportJobFile
    {
        [Key]
        public long ReportJobId { get; set; }

        public byte[] File { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}