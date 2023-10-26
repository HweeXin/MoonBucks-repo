﻿using MoonBuck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoonBuck.DataAccess.Repository.IRepository
{
    public interface IBidRepository : IRepository<Bid> 
    {
        void Update(Bid obj);
    }
}
