using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingCQRS.BuildingBlocks.Infrastructure
{
    public class EventData
    {
        public Guid EventId { get; }
        public Guid AggregateId { get; }
        public DateTime Timestamp { get; }
        public string Type { get; }
        public string Data { get; }

        public EventData(Guid eventId, Guid aggregateId, string type, object data)
        {
            this.EventId = eventId;
            this.AggregateId = aggregateId;
            this.Timestamp = DateTime.UtcNow;
            this.Type = type;
            this.Data = JsonConvert.SerializeObject(data);
        }

        public EventData()
        {

        }
    }
}
