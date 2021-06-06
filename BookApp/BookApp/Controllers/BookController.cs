﻿using BookApp.Helper;
using Interfaces.Services;
using Models.DomainModels;
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
// The error messages "Error updating book" and "Error deleting book" were both erroneously assigned the error code "3" - fixed via enums.

namespace BookApp.Controllers
{
    [System.Web.Http.RoutePrefix("api/books")]
    [EnableCors(origins: "*", headers: "accept,Auth-Key", methods: "*")]
    public class BookController : ApiController
    {
        private IBookService BookService;

        public BookController()
        {
        }
        public BookController(IBookService bookService)
        {
            BookService = bookService;

        }

        [HttpGet]
        [Route("GetBookById")]
        public HttpResponseMessage GetBookById(Guid bookId)
        {
            if (bookId == null || bookId == Guid.Empty)
                throw new APIException()
                {
                    ErrorCode = (int)HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request. Provide valid guid. Can't be empty guid.",
                    HttpStatus = HttpStatusCode.BadRequest
                };
            var book = BookService.GetBookById(bookId);
            if (book != null)
                return Request.CreateResponse(HttpStatusCode.OK, book, JsonFormatter);
            else
                throw new APIDataException((int)ErrorCodes_Book.FindingBook, "No book found", HttpStatusCode.NotFound);
        }

        [HttpPost]
        [Route("CreateBook")]
        public HttpResponseMessage SaveBook([FromBody] Book book)
        {
            if (book == null)
                throw new APIException()
                {
                    ErrorCode = (int)HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request. Provide valid book object. Object can't be null.",
                    HttpStatus = HttpStatusCode.BadRequest
                };
            book = BookService.AddBook(book);
            if (book != null)
                return Request.CreateResponse(HttpStatusCode.OK, book, JsonFormatter);
            else
                throw new APIDataException((int)ErrorCodes_Book.SavingBook, "Error Saving Book", HttpStatusCode.NotFound);
        }

        [HttpPut]
        [Route("UpdateBook")]
        public HttpResponseMessage UpdateBook([FromBody] Book book)
        {
            if (book == null)
                throw new APIException()
                {
                    ErrorCode = (int)HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request. Provide valid book object. Object can't be null.",
                    HttpStatus = HttpStatusCode.BadRequest
                };
            book = BookService.UpdateBook(book);
            if (book != null)
                return Request.CreateResponse(HttpStatusCode.OK, book, JsonFormatter);
            else
                throw new APIDataException((int)ErrorCodes_Book.UpdatingBook, "Error Updating Book", HttpStatusCode.NotFound);
        }

        [HttpPost]
        [Route("DeleteBook")]
        public HttpResponseMessage DeleteBook([FromBody] Guid bookId)
        {
            if (bookId == null || bookId == Guid.Empty)
                throw new APIException()
                {
                    ErrorCode = (int)HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request. Provide valid bookId guid. Can't be empty guid.",
                    HttpStatus = HttpStatusCode.BadRequest
                };
            var book = BookService.GetBookById(bookId);
            if (book != null)
            {
                var result = BookService.DeleteBook(book);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, "Book was deleted", JsonFormatter);
                else
                    throw new APIDataException((int)ErrorCodes_Book.DeletingBook, "Error Deleting Book", HttpStatusCode.NotFound);
            }
            else
                throw new APIDataException((int)ErrorCodes_Book.FindingBook, "No book found", HttpStatusCode.NotFound);
        }



        protected JsonMediaTypeFormatter JsonFormatter
        {
            get
            {
                var formatter = new JsonMediaTypeFormatter();
                var json = formatter.SerializerSettings;

                json.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
                json.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
                json.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                json.Formatting = Newtonsoft.Json.Formatting.Indented;
                json.ContractResolver = new CamelCasePropertyNamesContractResolver();
                json.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                return formatter;
            }

        }

    }
}
