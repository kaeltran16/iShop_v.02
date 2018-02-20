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
    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartsController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        // GET
        [HttpGet("{id}", Name = ApplicationConstants.ControllerName.ShoppingCart)]
        public async Task<IActionResult> Get(string id)
        {
            bool isValid = Guid.TryParse(id, out var shoppingCartId);
            //if (!isValid)
            //    return InvalidId(id);

            //var shoppingCart = await _unitOfWork.ShoppingCartRepository.GetShoppingCart(shoppingCartId);

            //if (shoppingCart == null)
            //    return NotFound(shoppingCartId);

            //var shoppingCartDto = _mapper.Map<ShoppingCart, ShoppingCartDto>(shoppingCart);
            var shoppingCart = await _shoppingCartService.Get(shoppingCartId);
            return Ok(shoppingCart);
        }

        // GET
        [Authorize(Roles = ApplicationConstants.PolicyName.SuperUsers)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var shoppingCarts = await _unitOfWork.ShoppingCartRepository.GetShoppingCarts();

            //var shoppingCartDto =
            //    _mapper.Map<IEnumerable<ShoppingCart>, IEnumerable<ShoppingCartDto>>(shoppingCarts);
            var shoppingCarts = await _shoppingCartService.GetAll();
            return Ok(shoppingCarts);
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

        // POST
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SavedShoppingCartDto shoppingCartDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // do not need to check for duplication in here
            //var shoppingCart = _mapper.Map<SavedShoppingCartDto, ShoppingCart>(shoppingCartDto);

            //await _unitOfWork.ShoppingCartRepository.AddAsync(shoppingCart);

            //if (!await _unitOfWork.CompleteAsync())
            //{
            //    //_logger.LogMessage(LoggingEvents.SavedFail,  ApplicationConstants.ControllerName.ShoppingCart, shoppingCart.Id);
            //    _logger.LogInformation("");

            //    return FailedToSave(shoppingCart.Id);
            //}

            //shoppingCart = await _unitOfWork.ShoppingCartRepository.GetShoppingCart(shoppingCart.Id);

            //var result = (_mapper.Map<ShoppingCart, ShoppingCartDto>(shoppingCart));

            ////_logger.LogMessage(LoggingEvents.Created,  ApplicationConstants.ControllerName.Product, shoppingCart.Id);
            //_logger.LogInformation("");

            var shoppingCart = await _shoppingCartService.CreateAsync(shoppingCartDto);

            return CreatedAtRoute(ApplicationConstants.ControllerName.ShoppingCart, new { id = shoppingCart.Id }, shoppingCart);
        }

        //PUT
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(string id, [FromBody]SavedShoppingCartDto shoppingCartDto)
        {
            bool isValid = Guid.TryParse(id, out var shoppingCartId);
            //if (!isValid)
            //    return InvalidId(id);

            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            //var shoppingCart = await _unitOfWork.ShoppingCartRepository.GetShoppingCart(shoppingCartId);

            //if (shoppingCart == null)
            //    return NotFound(shoppingCartId);

            //_mapper.Map(ShoppingCartDto, shoppingCart);

            //if (!await _unitOfWork.CompleteAsync())
            //{
            //    //_logger.LogMessage(LoggingEvents.SavedFail,  ApplicationConstants.ControllerName.ShoppingCart, shoppingCart.Id);
            //    _logger.LogInformation("");

            //    return FailedToSave(shoppingCart.Id);
            //}

            //shoppingCart = await _unitOfWork.ShoppingCartRepository.GetShoppingCart(shoppingCart.Id);

            //var result = _mapper.Map<ShoppingCart, SavedShoppingCartDto>(shoppingCart);

            ////_logger.LogMessage(LoggingEvents.Updated, ApplicationConstants.ControllerName.ShoppingCart, shoppingCart.Id);
            //_logger.LogInformation("");


            //return Ok(result);

            var shoppingCart = await _shoppingCartService.UpdateAsync(shoppingCartId, shoppingCartDto);
            return Ok(shoppingCart);
        }


        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool isValid = Guid.TryParse(id, out var shoppingCartId);
            //if (!isValid)
            //    return InvalidId(id);

            //var shoppingCart = await _unitOfWork.ShoppingCartRepository.GetShoppingCart(shoppingCartId, false);

            //if (shoppingCart == null)
            //    return NotFound(shoppingCartId);

            //if (shoppingCart.UserId != User.GetUserId())
            //    return UnAuthorized();

            //_unitOfWork.ShoppingCartRepository.Remove(shoppingCart);
            //if (!await _unitOfWork.CompleteAsync())
            //{
            //    //_logger.LogMessage(LoggingEvents.SavedFail,  ApplicationConstants.ControllerName.ShoppingCart, shoppingCart.Id);
            //    _logger.LogInformation("");

            //    return FailedToSave(shoppingCart.Id);
            //}

            ////_logger.LogMessage(LoggingEvents.Deleted,  ApplicationConstants.ControllerName.ShoppingCart, shoppingCart.Id);
            //_logger.LogInformation("");
            await _shoppingCartService.RemoveAsync(shoppingCartId);

            return NoContent();
        }
    }
}