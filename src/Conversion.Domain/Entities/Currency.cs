using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Conversion.Domain.Entities
{
    public class Currency
    {
        public Currency()
        {
            CurrencyId = Guid.NewGuid();
        }
        [Key]
        public Guid CurrencyId { get; set; }
        public int Id { get; set; }
        public int Code { get; set; }
        public string Ccy { get; set; }
        public string CcyNm_RU { get; set; }
        public string CcyNm_UZ { get; set; }
        public string CcyNm_UZC { get; set; }
        public string CcyNm_EN { get; set; }
        public int Nominal { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Rate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Diff { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? Date { get; set; }
    }
}
