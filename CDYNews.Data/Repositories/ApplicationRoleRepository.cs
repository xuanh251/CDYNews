using System.Collections.Generic;
using System.Linq;
using CDYNews.Data.Infrastructure;
using CDYNews.Model.Models;

namespace CDYNews.Data.Repositories
{
    public interface IApplicationRoleRepository : IRepository<ApplicationRole>
    {
        IEnumerable<ApplicationRole> GetListRoleByGroupId(int groupId);
        string GetNameOfRole(string roleId);
    }
    public class ApplicationRoleRepository : RepositoryBase<ApplicationRole>, IApplicationRoleRepository
    {
        public ApplicationRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        public IEnumerable<ApplicationRole> GetListRoleByGroupId(int groupId)
        {
            var query = from r in DbContext.ApplicationRoles
                        join rg in DbContext.ApplicationRoleGroups
                        on r.Id equals rg.RoleId
                        where rg.GroupId == groupId
                        select r;
            return query;
        }

        public string GetNameOfRole(string roleId)
        {
            return DbContext.Roles.Where(s => s.Id == roleId).Select(s => s.Name).SingleOrDefault();
        }
    }
}
