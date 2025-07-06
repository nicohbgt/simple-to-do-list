using System;

namespace Focient.Models
{
    public class ActivityModel
    {
        public int Id { get; set; }
        public int PlanId { get; set; }
        public string Name { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public bool IsCompleted { get; set; }
    }
}