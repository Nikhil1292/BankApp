using BankApp.Api.Models;
using System;

namespace BankApp.Api.Dtos
{
    public class UserDepositDto
    {
        #region PERSISTANT PROPERTY

        public string Message { get; set; }

        public long UseDepositId { get; set; }

        public long UserId { get; set; }

        public decimal Amount { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string MetaData { get; set; }

        #endregion

        #region CONSTRUCTOR

        public UserDepositDto()
        {

        }

        public UserDepositDto(UserDeposit userDeposit)
        {
            UseDepositId = userDeposit.UseDepositId;
            UserId = userDeposit.UserId;
            Amount = userDeposit.Amount;
            CreatedDate = userDeposit.CreatedDate;
            UpdatedDate = userDeposit.UpdatedDate;
        }

        #endregion

        #region TO_ENTITY

        public UserDeposit ToEntity()
        {
            var userDeposit = new UserDeposit
            {
                UseDepositId = this.UseDepositId,
                UserId = this.UserId,
                Amount = this.Amount
            };

            return userDeposit;
        }

        #endregion
    }
}