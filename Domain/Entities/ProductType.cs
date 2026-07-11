using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ProductType : AuditableEntity
    {
        public string strType {  get; set; }
        public string strDescription { get; set; }
    }
}
