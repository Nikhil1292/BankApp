using BankApp.Api.Models;
using System;

namespace BankApp.Api.Dtos
{
    public class LoanDepositDto
    {
        #region PERSISTENT PROPERTY

        public long LoanDepositId { get; set; }

        public long LoanId { get; set; }

        public decimal EMI { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string Message { get; set; }

        #endregion

        #region CONSTRUCTOR

        public LoanDepositDto()
        {

        }

        public LoanDepositDto(LoanDeposit loanDeposit)
        {
            LoanDepositId = loanDeposit.LoanDepositId;
            LoanId = loanDeposit.LoanId;
            EMI = loanDeposit.EMI;
            CreatedDate = loanDeposit.CreatedDate;
            UpdatedDate = loanDeposit.UpdatedDate;
        }

        #endregion

        #region TO ENTITY

        /// <summary>
        /// Loan deposit DTO to entity.
        /// </summary>
        /// <returns></returns>
        public LoanDeposit ToEntity()
        {
            var loanDeposit = new LoanDeposit
            {
                LoanDepositId = this.LoanDepositId,
                LoanId = this.LoanId,
                EMI = this.EMI
            };

            return loanDeposit;
        }

        #endregion
    }
}