using System;

namespace Conversion.Domain.Entities
{
    public class History
    {
        public DateTime Date { get; set; }
        public int FromCode { get; set; }
        public int ToCode { get; set; }
    }
}
