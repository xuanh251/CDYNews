using CDYNews.Data.Infrastructure;
using CDYNews.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDYNews.Data.Repositories
{
    public interface IErrorLogRepository : IRepository<ErrorLog>
    {
    }

    public class ErrorLogRepository : RepositoryBase<ErrorLog>, IErrorLogRepository
    {
        public ErrorLogRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
