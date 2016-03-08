namespace BankApp.Api.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BankEntities : DbContext
    {
        public BankEntities()
            : base("name=BankEntities")
        {
        }

        public DbSet<AccountType> AccountType { get; set; }

        public DbSet<InterestType> InterestType { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<UserDeposit> UserDeposit { get; set; }

        public DbSet<Loan> Loans { get; set; }

        public DbSet<LoanDeposit> LoanDeposit { get; set; }

        public DbSet<BillingPeriod> BillingPeriods { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
