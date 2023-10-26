using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoonBuck.Models
{
    public class JobRole
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        [DisplayName("Role Name")]
        public string Name { get; set; }
        [Range(1,100, ErrorMessage ="Display Order must be between 1-100 !")]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }

    }
}
