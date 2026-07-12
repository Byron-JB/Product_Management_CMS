using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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


        // FK to the user who created the record (nullable to allow system seeding)
        public int? iAddedBy { get; set; }

        public DateTime dtCreatedDate { get; set; }

        // Nullable FK to the user who last modified the record
        public int? iModifiedBy { get; set; }

        public DateTime dtModifiedDate { get; set; }

        // Navigation properties to the user who added/modified the entity
        [ForeignKey(nameof(iAddedBy))]
        public virtual User AddedByUser { get; set; }

        [ForeignKey(nameof(iModifiedBy))]
        public virtual User ModifiedByUser { get; set; }
    }
}
