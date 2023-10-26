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
    public class BidRepository : Repository<Bid>, IBidRepository
    {
        private ApplicationDbContext _db;
        public BidRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;        
        }

        public void Update(Bid obj)
        {
            var objFromDb = _db.Bids.FirstOrDefault(u => u.Id == obj.Id);  
            if (objFromDb != null)
            {
                objFromDb.StartTime = obj.StartTime;
                objFromDb.EndTime = obj.EndTime;
                objFromDb.Status = obj.Status;
            }
        }
    }
}
