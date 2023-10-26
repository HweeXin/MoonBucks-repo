using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoonBuck.DataAccess.Data
{
    public class CustomRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
