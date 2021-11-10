using System;

namespace Domain.Common
{
    public class AuditableBaseEntity
    {
        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }

    }
}
