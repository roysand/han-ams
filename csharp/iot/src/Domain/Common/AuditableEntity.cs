using System;

namespace Domain.Common
{
    public abstract class AuditableEntity
    {
        public DateTime? Modified { get; set; }
    }
}