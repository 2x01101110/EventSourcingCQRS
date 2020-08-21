using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingCQRS.BuildingBlocks.Domain
{
    public interface IAggregate<TId>
    {
        TId Id { get; }
    }
}
