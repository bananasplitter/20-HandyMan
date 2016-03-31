using HandyMan.Core.Domain;
using HandyMan.Core.Repository;
using HandyMan.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyMan.Data.Repository
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        public ServiceRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }
    }
}
