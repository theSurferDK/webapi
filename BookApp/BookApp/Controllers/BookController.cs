using BookApp.Helper;
using Interfaces.Services;
using Models.DomainModels;
using Models.ExtendedModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
        private GlobalMediaTypeFormatters globalMediaTypeFormatters = new GlobalMediaTypeFormatters();
        private IBookService BookService;
        //private IBookRepository BookRepository;

        public BookController()
        {
        }
        public BookController(IBookService bookService)
        {//, IBookRepository bookRepository) {
            BookService = bookService;
        }

        /* This method was from UserController because it makes more sense to have it here since it is a book-operation and not user-operation */
        [HttpGet]
        [Route("GetUserBooks")]
        public HttpResponseMessage GetUserBooks(Guid userId)
        {
            if (userId == null || userId == Guid.Empty)
                throw new APIException() { ErrorDescription = "Bad Request. Provide valid guid. Can't be empty guid." };
            IEnumerable<BookExtended> books = BookService.GetBooksByUserId(userId);
            if (books != null)
                return Request.CreateResponse(HttpStatusCode.OK, books, globalMediaTypeFormatters.JsonFormatter);
            else
                throw new APIDataException() { ErrorCode = (int)ErrorCodes_Book.FindingBook, ErrorDescription = "No books found" };
        }

        /* This method was from UserController because it makes more sense to have it here since it is a book-operation and not user-operation */
        [HttpPost]
        [Route("CreateBook")]
        public HttpResponseMessage SaveBook([FromBody] Book book)
        {
            if (book == null)
                throw new APIException() { ErrorDescription = "Bad Request. Provide valid book object. Object can't be null." };
            book = BookService.AddBook(book);
            if (book != null)
                return Request.CreateResponse(HttpStatusCode.OK, book, globalMediaTypeFormatters.JsonFormatter);
            else
                throw new APIDataException() { ErrorCode = (int)ErrorCodes_Book.SavingBook, ErrorDescription = "Error Saving Book" };
        }

        [HttpPut]
        [Route("UpdateBook")]
        public HttpResponseMessage UpdateBook([FromBody] Book book)
        {
            if (book == null)
                throw new APIException() { ErrorDescription = "Bad Request. Provide valid book object. Object can't be null." };
            book = BookService.UpdateBook(book);
            if (book != null)
                return Request.CreateResponse(HttpStatusCode.OK, book, globalMediaTypeFormatters.JsonFormatter);
            else
                throw new APIDataException() { ErrorCode = (int)ErrorCodes_Book.UpdatingBook, ErrorDescription = "Error Updating Book" };
        }

        [HttpPost]
        [Route("DeleteBook")]
        public HttpResponseMessage DeleteBook([FromBody] Guid bookId)
        {
            if (bookId == null || bookId == Guid.Empty)
                throw new APIException() { ErrorDescription = "Bad Request. Provide valid bookId guid. Can't be empty guid." };
            Book book = BookService.GetBookById(bookId);
            if (book != null)
            {
                bool result = BookService.DeleteBook(book);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, "Book was deleted", globalMediaTypeFormatters.JsonFormatter);
                else
                    throw new APIDataException() { ErrorCode = (int)ErrorCodes_Book.DeletingBook, ErrorDescription = "Error Deleting Book" };
            }
            else
                throw new APIDataException() { ErrorCode = (int)ErrorCodes_Book.FindingBook, ErrorDescription = "No book found" };
        }

    }
}
