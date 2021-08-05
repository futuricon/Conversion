using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Conversion.Domain.Entities
{
    public class Exchange
    {
        public Exchange()
        {
            ExchangeId = Guid.NewGuid();
        }
        [Key]
        public Guid ExchangeId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal IncomeAmount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal OutcomeAmount { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Date { get; set; }
        public virtual Currency FromCurrency { get; set; }
        public virtual Currency ToCurrency { get; set; }
    }
}
