using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class Product : AuditableEntity
    {
        [Required]
        [MaxLength(200)]
        public string strName { get; set; }

        [MaxLength(1000)]
        public string strDescription { get; set; }

        [MaxLength(50)]
        public string strColor { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal dblprice { get; set; }

        // Foreign key to ProductType
        [Required]
        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }

        // Foreign key to ProductSize
        public int? ProductSizeId { get; set; }
        public virtual ProductSize ProductSize { get; set; }
    }
}
