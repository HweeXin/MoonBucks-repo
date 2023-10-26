using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MoonBuck.Models
{
    public class Slot
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration => EndTime - StartTime;
        public Boolean IsFilled { get; set; }
        public double PayRate { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        [ValidateNever]
        public JobRole Role { get; set; }
        //public List<Bid> PendingBids { get; set; }
        //public Bid ApprovedBid { get; set; }
        //public List<Bid> RejectedBids { get; set; }
        //public ApplicationUser ApprovedCafeStaff { get; set; }

        public Slot()
        {
            // Initialize IsFilled to false when a Slot instance is created.
            IsFilled = false;
        }
    }
}
