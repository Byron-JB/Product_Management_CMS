using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class ProductSize : AuditableEntity
    {
        [Required]
        [MaxLength(50)]
        public string strSize { get; set; }

        public int iStockOnHand { get; set; }

        public int iIncomingStock { get; set; }
    }
}
