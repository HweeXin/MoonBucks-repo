using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoonBuck.Models
{
    public class Bid
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration => EndTime - StartTime;
        public Boolean Status { get; set; }
        public int SlotId { get; set; }
        [ForeignKey("SlotId")]
        [ValidateNever]
        public Slot Slot { get; set; }

        public Bid()
        {
        }
    }
}
