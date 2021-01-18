using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CookieBook.Infrastructure.Commands.Statistics
{
    public class TimePeriod
    {
        /// <summary>
        /// Date representing the beginning of the range (inclusive)
        /// </summary>
        /// <example>2020-12-01</example>
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Date representing the end of the range (inclusive)
        /// </summary>
        /// <example>2021-01-08</example>
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}