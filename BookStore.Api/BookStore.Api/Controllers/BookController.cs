using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        readonly BookRepository _bookRepository = new();

        [HttpGet("list")]
        [ProducesResponseType(typeof(ListResponse<BookModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetBooks(int pageIndex = 1, int pageSize = 10, string keyword = "")
        {
            var books = _bookRepository.GetBooks(pageIndex, pageSize, keyword);
            ListResponse<BookModel> response = new()
            {
                records = books.records.Select(c => new BookModel(c)).ToList(),
                totalRecords = books.totalRecords
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        public IActionResult GetBook(int id)
        {
            var book = _bookRepository.GetBook(id);
            if (book == null)
            {
                return NotFound(new { message = "Book not found!" });
            }
            BookModel response = new(book);
            return Ok(response);
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddBook(BookModel model)
        {
            if (model == null)
            {
                return BadRequest(new { message = "Please fill out the required infomation!" });
            }
            Book book = new()
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Base64image = model.Base64image,
                Categoryid = model.Categoryid,
                Publisherid = model.Publisherid,
                Quantity = model.Quantity
        };
            var response = _bookRepository.AddBook(book);
            BookModel bookModel = new(response);
            return Ok(bookModel);
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        public IActionResult UpdateBook(BookModel model)
        {
            Book book = new()
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Base64image = model.Base64image,
                Categoryid = model.Categoryid,
                Publisherid = model.Publisherid,
                Quantity = model.Quantity
            };
            var response = _bookRepository.UpdateBook(book);
            if (response == null)
            {
                return NotFound(new { message = "Book not found!" });
            }
            BookModel bookModel = new(response);
            return Ok(bookModel);
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        public IActionResult DeleteBook(int id)
        {
            var response = _bookRepository.DeleteBook(id);
            if (response == false)
            {
                return NotFound(new { message = "Book not found!" });
            }
            return Ok(response);
        }
    }
}
