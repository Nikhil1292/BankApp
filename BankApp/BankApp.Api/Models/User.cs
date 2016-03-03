using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BankApp.Api.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string FirstName{ get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public bool IsLoanUser { get; set; }

        public bool IsBankUser { get; set; }

        public int AccountTypeId { get; set; }

        public string Address { get; set; }

        public string PinCode { get; set; }

        public string City { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "xml")]
        public string MetaData { get; set; }

        public AccountType AccountType { get; set; }

    }
}