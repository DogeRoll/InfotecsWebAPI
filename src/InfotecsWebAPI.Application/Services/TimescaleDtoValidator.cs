using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfotecsWebAPI.Application.Dtos.CsvParsing;
using FluentValidation;

namespace InfotecsWebAPI.Application.Services
{
    internal class TimescaleDtoValidator : AbstractValidator<TimescaleDto>
    {
        private static DateTime _dateFrom = new DateTime(2001, 1, 1);
        public TimescaleDtoValidator()
        {
            RuleFor(x => x.Date)
                .GreaterThanOrEqualTo(_dateFrom)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Date must be in range between 2001.01.01 and UTC now.");
            RuleFor(x => x.Value)
                .GreaterThanOrEqualTo(0f)
                .WithMessage("Value must be equal to or greater than 0.");
        }
    }
}
