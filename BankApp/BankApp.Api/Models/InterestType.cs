using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankApp.Api.Models
{
    public class InterestType
    {
        [Key]
        public int Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Loan> Loans { get; set; }
    }
}