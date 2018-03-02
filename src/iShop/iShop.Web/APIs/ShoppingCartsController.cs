using System;
using System.Threading.Tasks;
using iShop.Data.Entities;
using iShop.Service.DTOs;
using iShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iShop.Web.APIs
{
    [Route("/api/[controller]")]
    public class ShoppingCartsController : CrudController<ShoppingCart, SavedShoppingCartDto, IShoppingCartService>
    {
        public ShoppingCartsController(IShoppingCartService service, ILogger<IShoppingCartService> logger) 
            : base(service, logger)
        {
        }
        
        // GET
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserShoppingCarts(string id)
        {
            bool isValid = Guid.TryParse(id, out var userId);
            //if (!isValid)
            //    return InvalidId(id);

            //if (userId != User.GetUserId())
            //    return UnAuthorized();

            //var shoppingCarts = await _unitOfWork.ShoppingCartRepository.GetUserShoppingCarts(userId);

            //var shoppingCartDto =
            //    _mapper.Map<IEnumerable<ShoppingCart>, IEnumerable<ShoppingCartDto>>(shoppingCarts);



            //return Ok(shoppingCartDto);
            return Ok();
        }


    }
}