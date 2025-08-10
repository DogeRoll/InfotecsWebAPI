using System.Globalization;
using System.Net.Security;

using InfotecsWebAPI.Application.Abstractions.CsvParser;
using InfotecsWebAPI.Application.Dtos.CsvParsing;
using InfotecsWebAPI.Application.Exceptions;
using Microsoft.Extensions.Options;
using nietras.SeparatedValues;

namespace InfotecsWebApi.SepCsvParser
{
    public class SepCsvParser : ICsvParser<TimescaleDto>
    {
        private readonly bool _useHeaders;
        private readonly string _dateFormat;

        private static readonly List<string> _requiredHeaders = new List<string> { "Date", "ExecutionTime", "Value" };

        public SepCsvParser(IOptions<CsvParserSettings> options)
        {
            var settings = options.Value;
            _useHeaders = settings.UseHeaders;
            _dateFormat = settings.DateFormat;
        }

        /// <summary>
        /// Parses csv file content string into TimescaleDto. Check appsettings.json to change date format and parsing parameters.
        /// </summary>
        /// <param name="csvString">csv string</param>
        /// <returns>TimescaleDto generator</returns>
        /// <exception cref="CsvParsingException"></exception>
        public IEnumerable<TimescaleDto> Parse(string csvString)
        {
            using var reader = Sep.Reader(o => o with { HasHeader = _useHeaders }).FromText(csvString);

            var actualHeaders = reader.Header.ColNames.OrderBy(x => x);

            if ((!_useHeaders && (reader.Current.ColCount != 3))
              || (_useHeaders && !actualHeaders.SequenceEqual(_requiredHeaders)))
            {
                throw new CsvParsingException($"Incorrect csv file format. The correct format is 'Date;ExecutionTime;Value', thee current format is {actualHeaders}");
            }

            SepReader.RowFunc<TimescaleDto> rowParser = _useHeaders 
                ? row => ParseWithHeaders(row)
                : row => ParseWithNoHeaders(row);

            foreach (var row in reader)
            {
                yield return rowParser(row);
            }
        }

        private TimescaleDto ParseWithHeaders(SepReader.Row row)
        {
            var strDate = row["Date"].ToString();
            bool success = DateTime.TryParseExact(strDate, _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out var dt);
            success &= row["ExecutionTime"].TryParse<uint>(out var et);
            success &= row["Value"].TryParse<float>(out var v);
            if (!success)
                throw new CsvParsingException($"Could not parse row {row.ToString()}; Values: {dt};{et};{v}");
            return new TimescaleDto { Date = dt, ExecutionTime = et, Value = v};
        }

        private TimescaleDto ParseWithNoHeaders(SepReader.Row row)
        {
            var strDate = row[0].ToString();
            bool success = DateTime.TryParseExact(strDate, _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out var dt);
            success &= row[1].TryParse<uint>(out var et);
            success &= row[2].TryParse<float>(out var v);
            if (!success)
                throw new CsvParsingException($"Could not parse row {row.ToString()}; Values: {dt};{et};{v}");
            return new TimescaleDto { Date = dt, ExecutionTime = et, Value = v };
        }
    }
}
