using System;
using System.ComponentModel.DataAnnotations;

namespace LinkAggregatorv5.Models
{
    public class Link
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLink { get; set; }

        [Required]
        [StringLength(999, MinimumLength = 4, ErrorMessage = "Must be at least 4 characters long.")]
        public string Description { get; set; }

        [Required]
        public string LinkURL { get; set; }

        public int LikeCount { get; set; }

        public string WhoAdd { get; set; }

        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss}")]
        [DataType(DataType.Date)]
        public DateTime AddDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}