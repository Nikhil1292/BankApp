using BankApp.Api.DTO;
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

    public class UserController : ApiController
    {
        #region CONSTANT

        private const string ACCOUNT_TYPE_ERROR = "Account type is not valide";
        private const string USER_SUCCESS_MESSAGE = "User was successfully created.";
        private const string USER_NOT_FOUND = "User not exist.";

        #endregion

        #region HTTP METHOD

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/user")]
        public IQueryable<UserDto> GetUsers()
        {

            List<UserDto> returnUsers = new List<UserDto>();

            using (var db = new BankEntities())
            {
                var userList = db.User.Where(u => !u.IsDeleted).ToList();

                foreach (User user in userList)
                {
                    UserDto rc = new UserDto(user);

                    returnUsers.Add(rc);
                }
            }
            return returnUsers.AsQueryable();
        }

        /// <summary>
        /// Get user data by providing userId.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/user/{id}")]
        public UserDto GetUser(long id)
        {
            using (var db = new BankEntities())
            {
                var user = db.User.FirstOrDefault(u => u.UserId == id && !u.IsDeleted);

                if (user == null)
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }

                var userDto = new UserDto(user);

                return userDto;
            }
        }

        /// <summary>
        /// Create new user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/user/")]
        public HttpResponseMessage PostUser(UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            using (var db = new BankEntities())
            {
                // Validate account type.
                var accountType = db.AccountType.AsNoTracking().FirstOrDefault(a => a.Code == user.AccountTypeId);

                if (accountType == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ACCOUNT_TYPE_ERROR);
                }

                // Convert user DTO to entity.
                var userEntity = user.ToEntity();

                userEntity.CreatedDate = DateTime.UtcNow;
                userEntity.UpdatedDate = DateTime.UtcNow;

                try
                {
                    db.User.Add(userEntity);
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

                // Bind return user DTO.
                UserDto returnUser = new UserDto(userEntity)
                {
                    Message = USER_SUCCESS_MESSAGE
                };

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, returnUser);

                return response;
            }
        }

        /// <summary>
        /// Update user.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/user/{id}")]
        public HttpResponseMessage PutUser(long id, UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (user.UserId != id)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            using (var db = new BankEntities())
            {
                // Get original user detail.
                var fullUser = db.User.FirstOrDefault(u => u.UserId == id && !u.IsDeleted);

                if (fullUser == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, USER_NOT_FOUND);
                }

                // Validate account type.
                var accountType = db.AccountType.AsNoTracking().FirstOrDefault(a => a.Code == user.AccountTypeId);

                if (accountType == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ACCOUNT_TYPE_ERROR);
                }

                // Convert user DTO to entity.
                var userEntity = user.ToEntity();

                userEntity.CreatedDate = fullUser.CreatedDate;
                userEntity.UpdatedDate = DateTime.UtcNow;

                try
                {
                    db.Entry(fullUser).CurrentValues.SetValues(userEntity);
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

                // Bind return user DTO.
                UserDto returnUser = new UserDto(userEntity)
                {
                    Message = USER_SUCCESS_MESSAGE
                };

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, returnUser);

                return response;
            }
        }

        /// <summary>
        /// Delete user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/User/{id}")]
        public HttpResponseMessage DeleteUser(long id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            using (var db = new BankEntities())
            {
                // Get original user detail.
                var fullUser = db.User.FirstOrDefault(u => u.UserId == id && !u.IsDeleted);

                if (fullUser == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, USER_NOT_FOUND);
                }

                fullUser.CreatedDate = fullUser.CreatedDate;
                fullUser.UpdatedDate = DateTime.UtcNow;
                fullUser.IsDeleted = true;

                try
                {
                    db.Entry(fullUser).State = EntityState.Modified;
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

                // Bind return user DTO.
                UserDto returnUser = new UserDto(fullUser)
                {
                    Message = USER_SUCCESS_MESSAGE
                };

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, returnUser);

                return response;
            }

        }

        #endregion
    }
}
