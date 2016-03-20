using BankApp.Api.Models;
using System;

namespace BankApp.Api.DTO
{
    /// <summary>
    /// User DTO that cast user detail.
    /// </summary>
    public class UserDto
    {
        #region PERSISTENT PROPERTY

        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public bool IsLoanUser { get; set; }

        public bool IsBankUser { get; set; }

        public int AccountTypeId { get; set; }

        public string Address { get; set; }

        public string PinCode { get; set; }

        public string City { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string MetaData { get; set; }

        public string Message { get; set; }

        #endregion

        #region CONSTRUCTOR

        public UserDto()
        {

        }

        public UserDto(User user)
        {
            UserId = user.UserId;
            FirstName = user.FirstName;
            MiddleName = user.MiddleName;
            LastName = user.LastName;
            IsLoanUser = user.IsLoanUser;
            IsBankUser = user.IsBankUser;
            AccountTypeId = user.AccountTypeId;
            Address = user.Address;
            PinCode = user.PinCode;
            City = user.City;
            CreatedDate = user.CreatedDate;
            UpdatedDate = user.UpdatedDate;
        }

        #endregion

        #region TO_ENTITY

        /// <summary>
        /// To Entity.
        /// </summary>
        /// <returns></returns>
        public User ToEntity()
        {
            var user = new User
            {
                UserId = this.UserId,
                FirstName = this.FirstName,
                MiddleName = this.MiddleName,
                LastName = this.LastName,
                AccountTypeId = this.AccountTypeId,
                IsBankUser = this.IsBankUser,
                IsLoanUser = this.IsLoanUser,
                PinCode = this.PinCode,
                Address = this.Address,
                City = this.City,
            };

            return user;
        }

        #endregion
    }
}
