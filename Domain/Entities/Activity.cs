using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Activity
    {
        [Key]
        public int iId { get; set; }

        // Name of the entity affected (e.g., "Product", "User")
        public string strEntityName { get; set; }

        // Optional numeric id of the entity
        public int? iEntityId { get; set; }

        // Action type: Create, Update, Delete
        public string strAction { get; set; }

        // Optional user id who performed the action
        public int? iPerformedBy { get; set; }

        // JSON payloads for before / after state (optional)
        public string strDataBefore { get; set; }
        public string strDataAfter { get; set; }

        public DateTime dtTimestamp { get; set; }
    }
}
