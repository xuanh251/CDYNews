using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDYNews.Data.Infrastructure;
using CDYNews.Model.Models;

namespace CDYNews.Data.Repositories
{
    public interface IFeedbackRepository:IRepository<Feedback>
    {

    }
    public class FeedbackRepository : RepositoryBase<Feedback>
    {
        public FeedbackRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
