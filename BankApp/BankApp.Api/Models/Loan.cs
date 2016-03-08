using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Api.Models
{
    public class Loan
    {
        [Key]
        public long LoanId{ get; set; }

        [Required]
        public long UserId { get; set; }

        public int EMIs { get; set; }

        [Required]
        public int InterestTypeId { get; set; }

        public decimal LoanAmount { get; set; }

        public bool IsBilled { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        [Column(TypeName = "xml")]
        public string MetaData { get; set; }

        public User Users { get; set; }

        public InterestType InterestTypes { get; set; }

        public ICollection<LoanDeposit> LoanDeposits { get; set; }

        public ICollection<BillingPeriod> BillingPeriods { get; set; }

    }
}
