using BankApp.Api.Dtos;
using BankApp.Api.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankApp.Api.Controllers.API._1._0
{
    public class LoanDepositController : ApiController
    {
        #region CONSTANT

        private const string IN_VALID_LOAN = "Loan is no longer use.";
        private const string INVALIDE_LOAN_DEPOSIT = "Invalide loan depoist";
        private const string Invalide_Request = "Invalide request";

        #endregion

        #region HTTP METHOD

        /// <summary>
        /// Get all loan deposit by loanId.
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/loanDeposit/")]
        public IQueryable<LoanDepositDto> GetLoanDeposits(long loanId)
        {

            List<LoanDepositDto> returnLoanDeposit = new List<LoanDepositDto>();

            using (var db = new BankEntities())
            {
                var loanDepositList = db.LoanDeposit.Where(u => u.LoanId == loanId && !u.IsDeleted).ToList();

                foreach (LoanDeposit loan in loanDepositList)
                {
                    LoanDepositDto rc = new LoanDepositDto(loan);

                    returnLoanDeposit.Add(rc);
                }
            }
            return returnLoanDeposit.AsQueryable();
        }

        /// <summary>
        /// Get loan deposit by loanDepositId. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/loanDeposit/{id}")]
        public LoanDepositDto GetLoanDeposit(long id)
        {
            using (var db = new BankEntities())
            {
                var loanDeposit = db.LoanDeposit.FirstOrDefault(u => u.LoanDepositId == id && !u.IsDeleted);

                if (loanDeposit == null)
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }

                var returnDto = new LoanDepositDto(loanDeposit);

                return returnDto;
            }
        }

        /// <summary>
        /// Add loan deposit amount.
        /// </summary>
        /// <param name="loanDeposit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/loanDeposit/")]
        public HttpResponseMessage PostLoanDeposit(LoanDepositDto loanDeposit)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            using (var db = new BankEntities())
            {
                // Validate loan.
                var loan = db.Loans.AsNoTracking().FirstOrDefault(a => a.LoanId == loanDeposit.LoanId);

                if (loan == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, IN_VALID_LOAN);
                }

                // Convert userDeposit DTO to entity.
                var loanDepositEntity = loanDeposit.ToEntity();

                loanDepositEntity.CreatedDate = DateTime.UtcNow;
                loanDepositEntity.UpdatedDate = DateTime.UtcNow;

                try
                {
                    db.LoanDeposit.Add(loanDepositEntity);
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Make sure that you have included all required fields in your request.");
                }
                catch (Exception exGeneral)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exGeneral.GetType().ToString());
                }

                // Bind return user deposit DTO.
                LoanDepositDto returnLoanDeposit = new LoanDepositDto(loanDepositEntity)
                {
                    Message = "Loan deposit was successfully added."
                };

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, returnLoanDeposit);

                return response;
            }
        }

        /// <summary>
        /// Update loan deposit amount.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loanDeposit"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/loanDeposit/{id}")]
        public HttpResponseMessage PutLoanDeposit(long id, LoanDepositDto loanDeposit)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (loanDeposit.LoanDepositId != id)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            } 

            using (var db = new BankEntities())
            {
                // Get original loanDeposit detail.
                var fullLoanDeposit = db.LoanDeposit.FirstOrDefault(u => u.LoanDepositId == id && !u.IsDeleted);

                if (fullLoanDeposit == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, INVALIDE_LOAN_DEPOSIT);
                }

                // Convert userDeposit DTO to entity.
                var loanDepositEntity = loanDeposit.ToEntity();

                loanDepositEntity.CreatedDate = fullLoanDeposit.CreatedDate;
                loanDepositEntity.UpdatedDate = DateTime.UtcNow;

                try
                {
                    db.Entry(fullLoanDeposit).CurrentValues.SetValues(loanDepositEntity);
                    db.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Make sure that you have included all required fields in your request.");
                }
                catch (Exception exGeneral)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exGeneral.GetType().ToString());
                }

                // Bind return deposit DTO.
                LoanDepositDto returnLoanDeposit = new LoanDepositDto(loanDepositEntity)
                {
                    Message = "Loan deposit was successfully updated."
                };

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, returnLoanDeposit);

                return response;
            }
        }

        /// <summary>
        /// Delete user deposit.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/userDeposit/{id}")]
        public HttpResponseMessage DeleteUserDeposit(long id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            using (var db = new BankEntities())
            {
                // Get original user deposit detail.
                var fullUserDeposit = db.User.FirstOrDefault(u => u.UserId == id && !u.IsDeleted);

                if (fullUserDeposit == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Invalide_Request);
                }

                fullUserDeposit.UpdatedDate = DateTime.UtcNow;
                fullUserDeposit.IsDeleted = true;

                try
                {
                    db.Entry(fullUserDeposit).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Make sure that you have included all required fields in your request.");
                }
                catch (Exception exGeneral)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exGeneral.GetType().ToString());
                }

                // Bind return loan deposit DTO.
                LoanDepositDto returnLoanDeposit = new LoanDepositDto()
                {
                    Message = "Loan depoist was successfully deleted."
                };

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, returnLoanDeposit);

                return response;
            }

        }

        #endregion
    }
}
