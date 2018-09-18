using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GCAT.API.Contexts;
using GCAT.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GCAT.API.Services
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private CryptoContext _context;

        public OrganizationRepository(CryptoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Organization>> GetOrganizationsAsync()
        {
            return await _context.Organizations.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}