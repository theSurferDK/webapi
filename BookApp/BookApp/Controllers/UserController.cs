using BookApp.Helper;
using Interfaces.Repositories;
using Interfaces.Services;
using Models.DomainModels;
using Models.ExtendedModels;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

// Error codes changed from inline (magic numbers) to "(int)ErrorCodes_User.ENUM_VALUE" at "throw new APIDataException"-lines.

namespace BookApp.Controllers
{
    [System.Web.Http.RoutePrefix("api/account")]
    [EnableCors(origins: "*", headers: "accept,Auth-Key", methods: "*")]
    public class UserController : ApiController
    {
        private GlobalMediaTypeFormatters globalMediaTypeFormatters = new GlobalMediaTypeFormatters();
    
        private IBookRepository BookRepository;
        private readonly IBookService BookService;
        private readonly IUserService UserService;

        public UserController() {
        }
        public UserController(IBookRepository bookRepository, IUserService userService, IBookService bookService) {
            BookRepository = bookRepository;
            UserService = userService;
            BookService = bookService;
        }
        /* Local JsonFormatter has been replaced by global JsonFormatter using GlobalMediaTypeFormatter */
        [HttpGet]
        [Route("GetUserById")]
        public HttpResponseMessage GetUserById(Guid userId)
        {
            if (userId == null || userId == Guid.Empty)
                throw new APIException() { ErrorDescription = "Bad Request. Provide valid guid. Can't be empty guid." };
            User user = UserService.GetUserById(userId);
            if (user != null)
                return Request.CreateResponse(HttpStatusCode.OK, user, globalMediaTypeFormatters.JsonFormatter);
            else
                throw new APIDataException() { ErrorCode = (int)ErrorCodes_User.FindingUser, ErrorDescription = "No user found" };
        }

        [HttpPost]
        [Route("CreateUser")]
        public HttpResponseMessage SaveUser([FromBody] User user)
        {
            if (user == null)
                throw new APIException();
            user = UserService.AddUser(user);
            if (user != null)
                return Request.CreateResponse(HttpStatusCode.OK, user, globalMediaTypeFormatters.JsonFormatter);
            else
                throw new APIDataException() { ErrorCode = (int)ErrorCodes_User.SavingUser, ErrorDescription = "Error Saving User" };
        }

        [HttpPut]
        [Route("UpdateUser")]
        public HttpResponseMessage UpdateUser([FromBody] User user)
        {
            if (user == null)
                throw new APIException();
            user = UserService.UpdateUser(user);
            if (user != null)
                return Request.CreateResponse(HttpStatusCode.OK, user, globalMediaTypeFormatters.JsonFormatter);
            else
                throw new APIDataException() { ErrorCode = (int)ErrorCodes_User.UpdatingUser, ErrorDescription = "Error Updating User" };
        }

        [HttpPost]
        [Route("DeleteUser")]
        public HttpResponseMessage DeleteUser([FromBody] Guid userId)
        {
            if (userId == null || userId == Guid.Empty)
                throw new APIException() { ErrorDescription = "Bad Request. Provide valid guid. Can't be empty guid." };
            User user = UserService.GetUserById(userId);
            if (user != null)
            {
                bool result = UserService.DeleteUser(user);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, "User was deleted", globalMediaTypeFormatters.JsonFormatter);
                else
                    throw new APIDataException() { ErrorCode = (int)ErrorCodes_User.DeletingUser, ErrorDescription = "Error Deleting User" };
            }
            else
                throw new APIDataException() { ErrorCode = (int)ErrorCodes_User.FindingUser, ErrorDescription = "No user found" };
        }

    }
}
