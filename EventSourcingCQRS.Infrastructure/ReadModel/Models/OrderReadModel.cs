using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingCQRS.Infrastructure.ReadModel.Models
{
    public class OrderReadModel : IReadEntity
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public DateTime StatusUpdated { get; set; }
    }
}
