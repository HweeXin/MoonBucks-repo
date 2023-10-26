using MoonBuck.DataAccess.Data;
using MoonBuck.DataAccess.Repository.IRepository;
using MoonBuck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoonBuck.DataAccess.Repository
{
    public class JobRoleRepository : Repository<JobRole>, IJobRoleRepository
    {
        private ApplicationDbContext _db;
        public JobRoleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;        
        }

        public void Update(JobRole obj)
        {
            _db.JobRoles.Update(obj);
        }
    }
}
