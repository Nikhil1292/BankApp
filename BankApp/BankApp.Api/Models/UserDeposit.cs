using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Api.Models
{
    public class UserDeposit
    {
        [Key]
        public long UseDepositId { get; set; }

        [Required]
        public long UserId { get; set; }

        public decimal Amount { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        [Column(TypeName = "xml")]
        public string MetaData { get; set; }

        public User Users { get; set; }
    }
}