using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Api.Models
{
    public class BillingPeriod
    {
        public long BillingPeriodId { get; set; }

        [Required]
        public long LoanId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal EMI { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        [Column(TypeName = "xml")]
        public string MetaData { get; set; }

        public Loan Loan { get; set; }

    }
}
