using System;

namespace InfotecsWebAPI.Domain.Entities
{
    public class TimescaleValues
    {
        public DateTime Date { get; set; }
        public uint ExecutionTime { get; set; }
        public float Value { get; set; }

        public TimescaleValues(DateTime date, uint executionTime, float value)
        {
            Date = date;
            ExecutionTime = executionTime;
            Value = value;
        }
    }
}
