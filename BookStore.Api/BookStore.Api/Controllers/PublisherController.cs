using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        readonly PublisherRepository _publisherRepository = new();

        [HttpGet("list")]
        [ProducesResponseType(typeof(ListResponse<PublisherModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetPublishers(int pageIndex = 1, int pageSize = 10, string keyword = "")
        {
            var publishers = _publisherRepository.GetPublishers(pageIndex, pageSize, keyword);
            ListResponse<PublisherModel> response = new()
            {
                records = publishers.records.Select(c => new PublisherModel(c)).ToList(),
                totalRecords = publishers.totalRecords
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PublisherModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        public IActionResult GetPublisher(int id)
        {
            var publisher = _publisherRepository.GetPublisher(id);
            if (publisher == null)
            {
                return NotFound(new { message = "Publisher not found!" });
            }
            PublisherModel response = new(publisher);
            return Ok(response);
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(PublisherModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddPublisher(PublisherModel model)
        {
            if (model == null)
            {
                return BadRequest(new { message = "Please fill out the required infomation!" });
            }
            Publisher publisher = new()
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Contact = model.Contact,
            };
            var response = _publisherRepository.AddPublisher(publisher);
            PublisherModel publisherModel = new(response);
            return Ok(publisherModel);
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(PublisherModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        public IActionResult UpdatePublisher(PublisherModel model)
        {
            Publisher publisher = new()
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Contact = model.Contact,
            };
            var response = _publisherRepository.UpdatePublisher(publisher);
            if (response == null)
            {
                return NotFound(new { message = "Publisher not found!" });
            }
            PublisherModel publisherModel = new(response);
            return Ok(publisherModel);
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        public IActionResult DeletePublisher(int id)
        {
            var response = _publisherRepository.DeletePublisher(id);
            if (response == false)
            {
                return NotFound(new { message = "Publisher not found!" });
            }
            return Ok(response);
        }
    }
}
