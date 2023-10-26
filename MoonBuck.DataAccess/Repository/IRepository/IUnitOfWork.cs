using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoonBuck.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IJobRoleRepository Role { get; }
        ISlotRepository Slot { get; }
        IBidRepository Bid { get; }
        void Save();
    }
}
