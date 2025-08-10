using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfotecsWebAPI.Domain.Entities
{
    public class TimescaleResults
    {
        public long TimeDelta { get; set; }
        public DateTime EarliestTime { get; set; }
        public float AverageExecutionTime { get; set; }
        public float MedianValue { get; set; }
        public float MaximumValue { get; set; }
        public float MinimumValue { get; set; }
        public float AverageValue { get; set; }

        /// <summary>
        /// Calculates resulting data (averages, values, median etc.) from the list of values.
        /// </summary>
        /// <param name="values">List of TimescaleValues.</param>
        /// <returns>TimescaleResults object with computed fields.</returns>
        public static TimescaleResults FromValues(List<TimescaleValues> values)
        {
            DateTime minDate = DateTime.MaxValue, maxDate = DateTime.MinValue;
            float avgExecutionTime = 0, avgValue = 0;
            int size = values.Count;

            var sortedValues = new float[size];
            for (int i = 0; i < size; i++)
            {
                sortedValues[i] = values[i].Value;

                avgExecutionTime += (float)values[i].ExecutionTime / size;
                avgValue += values[i].Value / size;

                if (minDate > values[i].Date) minDate = values[i].Date;
                if (maxDate < values[i].Date) maxDate = values[i].Date;
            }

            Array.Sort(sortedValues);

            float medValue = (size % 2 == 0)
                ? (sortedValues[size / 2 - 1] + sortedValues[size / 2]) / 2
                : sortedValues[size / 2];

            return new TimescaleResults
            {
                TimeDelta = (long)(maxDate - minDate).TotalSeconds,
                EarliestTime = minDate,
                AverageExecutionTime = avgExecutionTime,
                MedianValue = medValue,
                AverageValue = avgValue,
                MinimumValue = sortedValues[0],
                MaximumValue = sortedValues[^1]
            };
        }
    }
}
