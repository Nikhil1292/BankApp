using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Api.Models
{
    public class LoanDeposit
    {
        [Key]
        public long LoanDepositId { get; set; }

        [Required]
        public long LoanId { get; set; }

        public decimal EMI{ get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        [Column(TypeName = "xml")]
        public string MetaData { get; set; }

        public Loan Loan { get; set; }
    }
}
