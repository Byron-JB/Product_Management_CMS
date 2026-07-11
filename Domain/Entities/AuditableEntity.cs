using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    /// <summary>
    /// Contains the properties that are shared by all db entities
    /// </summary>
    public abstract class AuditableEntity
    {
        [Key]
        public int iId { get; set; }
        public int iAddedBY { get; set; }
        public DateTime dtCreatedDate { get; set; }
        public string iModifiedBy { get; set; }
        public DateTime dtModifiedDate { get; set; }
    }
}
