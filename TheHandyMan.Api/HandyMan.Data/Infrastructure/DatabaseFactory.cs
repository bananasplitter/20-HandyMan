using HandyMan.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyMan.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private readonly HandyManDataContext _dataContext;
        
        public HandyManDataContext GetDataContext()
        {
            return _dataContext ?? new HandyManDataContext();
        }

        public DatabaseFactory()
        {
            _dataContext = new HandyManDataContext();
        }

        protected override void DisposeCore()
        {
            if (_dataContext != null) _dataContext.Dispose();
        }
    }
}
