using System;
using System.Threading.Tasks;
using iShop.Common.DTOs;
using iShop.Common.Helpers;
using iShop.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iShop.Web.APIs
{
    [Route("/api/[controller]")]
    public class ShoppingCartsController : CrudController<IShoppingCartService, SavedShoppingCartDto>
    {
        public ShoppingCartsController(IShoppingCartService service) : base(service)
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