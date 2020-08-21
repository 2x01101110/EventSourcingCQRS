using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingCQRS.BuildingBlocks.Domain
{
    public abstract class IdBase : IAggregateId
    {
        public Guid Id { get; }

        protected IdBase() 
        {
            this.Id = Guid.NewGuid();
        }
        protected IdBase(Guid id)
        {
            this.Id = id;
        }

        public override bool Equals(object obj)
        {
            return obj is IdBase && Equals(this.Id, ((IdBase)obj).Id);
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public Guid GetId() => this.Id;
    }
}
