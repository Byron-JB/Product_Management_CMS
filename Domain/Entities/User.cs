using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class User : AuditableEntity
    {
        [MaxLength(100)]
        public string strFirstName { get; set; }

        [MaxLength(100)]
        public string strLastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string strEmail { get; set; }

        [Required]
        public string strPasswordHash { get; set; }
    }
}
