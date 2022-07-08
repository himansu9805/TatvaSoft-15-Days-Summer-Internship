using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartRepository _cartRepository = new();

        [HttpGet("list")]
        [ProducesResponseType(typeof(IEnumerable<CartModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetCartItems(int UserId)
        {
            var cart = _cartRepository.GetCartItems(UserId);
            ListResponse<CartModel> response = new()
            {
                records = cart.records.Select(c => new CartModel(c)).ToList(),
                totalRecords = cart.totalRecords
            };
            return Ok(response);
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(CartModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddToCart(CartModel cartModel)
        {
            if (cartModel == null)
            {
                return BadRequest(new { message = "Cannot add empty to cart!" });
            }
            var response = _cartRepository.AddCart(cartModel);
            if (response == null)
            {
                return BadRequest();
            }
            return Ok(response);
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(CartModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateCart(CartModel model)
        {
            if (model == null) {
                return BadRequest(new { message = "Cannot update empty to cart!" });
            }
            Cart cart = new()
            {
                Id = model.Id,
                Quantity = model.Quantity,
                Bookid = model.BookId,
                Userid = model.UserId
            };
            cart = _cartRepository.UpdateCart(cart);
            return Ok(new CartModel(cart));
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteCart(int id)
        {
            if (id == 0)
            {
                return BadRequest(new { message = "Cart ID cannot be 0!" });
            }
            bool response = _cartRepository.DeleteCart(id);
            return Ok(response);
        }
    }
}
