﻿using CDYNews.Data.Infrastructure;
using CDYNews.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDYNews.Data.Repositories
{
    public interface IApplicationGroupRepository : IRepository<ApplicationGroup>
    {
        IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId);
        IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId);
        IEnumerable<ApplicationGroup> GetListGroupByRoleId(string roleId);
    }
    public class ApplicationGroupRepository : RepositoryBase<ApplicationGroup>, IApplicationGroupRepository
    {
        public ApplicationGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId)
        {
            var query = from g in DbContext.ApplicationGroups
                        join ug in DbContext.ApplicationUserGroups
                        on g.ID equals ug.GroupId
                        join u in DbContext.Users
                        on ug.UserId equals u.Id
                        where ug.GroupId == groupId
                        select u;
            return query;
        }

        public IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId)
        {
            var query = from g in DbContext.ApplicationGroups
                        join ug in DbContext.ApplicationUserGroups
                        on g.ID equals ug.GroupId
                        where ug.UserId == userId
                        select g;
            return query;
        }
        public IEnumerable<ApplicationGroup> GetListGroupByRoleId(string roleId)
        {
            var query = from g in DbContext.ApplicationGroups
                        from rg in DbContext.ApplicationRoleGroups.Where(s => s.GroupId == g.ID)
                        where rg.RoleId == roleId
                        select g;
            return query;
        }
    }
}
