using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class ProductType : AuditableEntity
    {
        [Required]
        [MaxLength(100)]
        public string strType { get; set; }

        [MaxLength(1000)]
        public string strDescription { get; set; }
    }
}
