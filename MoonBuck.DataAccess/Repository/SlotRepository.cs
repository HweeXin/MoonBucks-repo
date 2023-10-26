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
    public class SlotRepository : Repository<Slot>, ISlotRepository
    {
        private ApplicationDbContext _db;
        public SlotRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;        
        }

        public void Update(Slot obj)
        {
            var objFromDb = _db.Slots.FirstOrDefault(u => u.Id == obj.Id);  
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.StartTime = obj.StartTime;
                objFromDb.EndTime = obj.EndTime;
                objFromDb.IsFilled = obj.IsFilled;
                objFromDb.PayRate = obj.PayRate;
            }
        }
    }
}
