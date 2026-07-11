using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Product : AuditableEntity
    {
        public string strName {  get; set; }
        public string strDescription { get; set; }
        public string strColor { get; set; }
        public decimal dblprice { get; set; }

        public virtual ProductType {  get; set; }
    }
}
