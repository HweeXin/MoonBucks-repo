using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoonBuck.Models.ViewModels
{
    public class BidVM
    {
        public Bid Bid { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> SlotList { get; set; }
    }
}
