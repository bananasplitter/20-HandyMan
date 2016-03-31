﻿using HandyMan.Core.Domain;
using HandyMan.Core.Repository;
using HandyMan.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyMan.Data.Repository
{
    public class TimeEntryRepository : Repository<TimeEntry>, ITimeEntryRepository
    {
        public TimeEntryRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }
    }
}