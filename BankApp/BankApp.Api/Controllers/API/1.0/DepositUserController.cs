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

namespace BankApp.Api.Controllers
{
    public class DepositUserController : ApiController
    {
        #region CONSTANT

        private const string INVALID_USER = "Provided user is not valid";
        private const string USER_DEPOSIT_SUCCESS_MESSAGE = "User deposit was successfully added.";
        private const string INVALID_USER_DEPOSITE = "User deposit was invalid entry.";
        private const string INVALIDE_USER = "Invalide user provided.";
        private const string UPDATE_MESSAGE = "User deposit was successfully updated.";

        #endregion

        #region HTTP METHOD

        /// <summary>
        /// Get user deposit by userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/DepositUser/")]
        public IQueryable<UserDepositDto> GetUserDeposits(long userId)
        {

            List<UserDepositDto> returnUserDeposits = new List<UserDepositDto>();

            using (var db = new BankEntities())
            {
                var userList = db.UserDeposit.Where(u => u.UserId == userId && !u.IsDeleted).ToList();

                foreach (UserDeposit user in userList)
                {
                    UserDepositDto rc = new UserDepositDto(user);

                    returnUserDeposits.Add(rc);
                }
            }
            return returnUserDeposits.AsQueryable();
        }

        /// <summary>
        /// Get user deposit by userDepositId. 
        /// </summary>
        /// <param name="userDepositId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/DepositUser/{id}")]
        public UserDepositDto GetUserDeposit(long id)
        {
            using (var db = new BankEntities())
            {
                var user = db.UserDeposit.FirstOrDefault(u => u.UseDepositId == id && !u.IsDeleted);

                if (user == null)
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }

                var userDepositDto = new UserDepositDto(user);

                return userDepositDto;
            }
        }

        /// <summary>
        /// Add new user deposit amount.
        /// </summary>
        /// <param name="userDeposit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/DepositUser/")]
        public HttpResponseMessage PostUserDeposit(UserDepositDto userDeposit)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            using (var db = new BankEntities())
            {
                // Validate user.
                var user = db.User.AsNoTracking().FirstOrDefault(a => a.UserId == userDeposit.UserId);

                if (user == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, INVALID_USER);
                }

                // Convert userDeposit DTO to entity.
                var userDepositEntity = userDeposit.ToEntity();

                userDepositEntity.CreatedDate = DateTime.UtcNow;
                userDepositEntity.UpdatedDate = DateTime.UtcNow;

                try
                {
                    db.UserDeposit.Add(userDepositEntity);
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
                UserDepositDto returnUserDeposit = new UserDepositDto(userDepositEntity)
                {
                    Message = USER_DEPOSIT_SUCCESS_MESSAGE
                };

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, returnUserDeposit);

                return response;
            }
        }

        /// <summary>
        /// Update user deposit.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userDeposit"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/DepositUser/{id}")]
        public HttpResponseMessage PutUserDeposit(long id, UserDepositDto userDeposit)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (userDeposit.UseDepositId != id)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            using (var db = new BankEntities())
            {
                // Get original user deposit detail.
                var fullUserDeposit = db.UserDeposit.FirstOrDefault(u => u.UseDepositId == id && !u.IsDeleted);

                if (fullUserDeposit == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, INVALID_USER_DEPOSITE);
                }

                // Validate user.
                var user = db.User.AsNoTracking().FirstOrDefault(a => a.UserId == userDeposit.UserId);

                if (user == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, INVALIDE_USER);
                }

                // Convert userDeposit DTO to entity.
                var userDepositEntity = userDeposit.ToEntity();

                userDepositEntity.CreatedDate = fullUserDeposit.CreatedDate;
                userDepositEntity.UpdatedDate = DateTime.UtcNow;

                try
                {
                    db.Entry(fullUserDeposit).CurrentValues.SetValues(userDepositEntity);
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
                UserDepositDto returnUserDeposit = new UserDepositDto(userDepositEntity)
                {
                    Message = UPDATE_MESSAGE
                };

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, returnUserDeposit);

                return response;
            }
        }

        /// <summary>
        /// Delete user deposit.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/DepositUser/{id}")]
        public HttpResponseMessage DeleteUserDeposit(long id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            using (var db = new BankEntities())
            {
                // Get original user deposit detail.
                var fullUserDeposit = db.UserDeposit.FirstOrDefault(u => u.UseDepositId == id && !u.IsDeleted);

                if (fullUserDeposit == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, INVALID_USER_DEPOSITE);
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

                // Bind return user deposit DTO.
                UserDepositDto returnUserDeposit = new UserDepositDto()
                {
                    Message = "User depoist was successfully deleted."
                };

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, returnUserDeposit);

                return response;
            }

        }

        #endregion
    }
}
