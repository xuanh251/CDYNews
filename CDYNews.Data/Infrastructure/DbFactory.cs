using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDYNews.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private CDYNewsDbContext dbContext;

        public CDYNewsDbContext Init()
        {
            return dbContext ?? (dbContext = new CDYNewsDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
