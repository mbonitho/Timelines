using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timelines
{
    public class Timeline
    {
        public Timeline Parent { get; set; }

        public string Name { get; set; }

        public List<TimelineEvent> Events { get; set; }

        public Timeline()
        {
            Events = new List<TimelineEvent>();
        }
    }

    public class TimelineEvent
    {
        public enum EventType
        {
            Task,
            Appointment,
            Milestone
        }

        public string ShortName { get; set; }

        public DateTime Date { get; set; }

        public EventType TypeOfEvent { get; set; }

        public string Description { get; set; }

        public TimelineEvent(string name, DateTime date, EventType type, string description = "")
        {
            ShortName = name;
            Date = date;
            TypeOfEvent = type;
            Description = description;
        }
    }
}
