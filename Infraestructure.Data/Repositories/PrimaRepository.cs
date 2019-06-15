using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstracts;
using Domain.Entities;
using Infraestructure.Data.Base;

namespace Infraestructure.Data.Repositories
{
    public class PrimaRepository : GenericRepository<Prima>, IPrimaRepository
    {
        public PrimaRepository(IDbContext context) : base(context){}
    }
}
