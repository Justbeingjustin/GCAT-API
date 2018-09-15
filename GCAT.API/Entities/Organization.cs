using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GCAT.API.Entities
{
    [Table("Organizations")]
    public class Organization
    {
        [Key]
        public long OrganizationId { get; set; }

        public long CoinMarketCapOrganizationId { get; set; }

        public string Name { get; set; }

        public string GithubURL { get; set; }
    }
}