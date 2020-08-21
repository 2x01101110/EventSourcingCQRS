using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingCQRS.BuildingBlocks.Domain
{
    public interface IAggregateId
    {
        Guid Id { get; }

        Guid GetId();
    }
}
