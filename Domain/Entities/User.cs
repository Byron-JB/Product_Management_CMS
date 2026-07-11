using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class User : AuditableEntity
    {
        public string strFirstName {  get; set; }
        public string strLastName { get; set; }
        public string strEmail { get; set; }
        public string strPasswordHash { get; set; }
    }
}
