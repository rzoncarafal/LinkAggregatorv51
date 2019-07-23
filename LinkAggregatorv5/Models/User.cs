using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace LinkAggregatorv5.Models
{
    public class User : IdentityUser
    {
        public string Password { get; set; }

        [DataType(DataType.Date)]
        public DateTime AddDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}