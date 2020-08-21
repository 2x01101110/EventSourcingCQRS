using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingCQRS.Infrastructure.ReadModel.Models
{
    public interface IReadEntity
    {
        Guid Id { get; }
    }
}
