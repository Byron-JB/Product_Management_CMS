using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ProductSize : AuditableEntity
    {
        public string strSize {  get; set; }
        public int iStockOnHand { get; set; }
        public int iIncomingStock {  get; set; }

    }
}
