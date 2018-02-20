using System;
using System.Threading.Tasks;
using iShop.Common.DTOs;
using iShop.Common.Helpers;
using iShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace iShop.Web.APIs
{
    [Route("/api/[controller]")]
    public class ShippingsController : Controller
    {
        private readonly IShippingService _shippingService;


        public ShippingsController(IShippingService shippingService)
        {
            _shippingService = shippingService;
        }


        // GET
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var shippings = await _shippingService.GetAll();
            return Ok(shippings);
        }


        // GET
        [HttpGet("{id}", Name = ApplicationConstants.ControllerName.Shipping)]
        public async Task<IActionResult> Get(string id)
        {
            bool isValid = Guid.TryParse(id, out var shippingId);

            //if (!isValid)
            //    return InvalidId(id);

            //var shipping = await _unitOfWork.ShippingRepository.GetShipping(shippingId);

            //if (shipping == null)
            //    return NullOrEmpty();

            var shipping = await _shippingService.Get(shippingId);

            return Ok(shipping);
        }

        // POST

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ShippingDto shippingResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //var shipping = _mapper.Map<ShippingDto, Shipping>(shippingResource);

            //await _unitOfWork.ShippingRepository.AddAsync(shipping);

            //// if something happens and the new item can not be saved, return the error
            //if (!await _unitOfWork.CompleteAsync())
            //{
            //    //_logger.LogMessage(LoggingEvents.SavedFail, ApplicationConstants.ControllerName.Shipping, shipping.Id);
            //    _logger.LogInformation("");
            //    return FailedToSave(shipping.Id);
            //}

            //shipping = await _unitOfWork.ShippingRepository.GetShipping(shipping.Id);

            //var result = _mapper.Map<Shipping, ShippingDto>(shipping);

            ////_logger.LogMessage(LoggingEvents.Created, ApplicationConstants.ControllerName.Shipping, shipping.Id);
            //_logger.LogInformation("");
            var shipping = await _shippingService.CreateAsync(shippingResource);
            return CreatedAtRoute(ApplicationConstants.ControllerName.Shipping, new {id = shipping.Id}, shipping);
        }

        // DELETE
        //[Authorize(Roles = ApplicationConstants.RoleName.SuperUser)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool isValid = Guid.TryParse(id, out var shippingId);

            //if (!isValid)
            //    return InvalidId(id);

            //var shipping = await _unitOfWork.ShippingRepository.GetShipping(shippingId);

            //if (shipping == null)
            //    return NullOrEmpty();

            //_unitOfWork.ShippingRepository.Remove(shipping);
            //if (!await _unitOfWork.CompleteAsync())
            //{
            //    //_logger.LogMessage(LoggingEvents.Fail, ApplicationConstants.ControllerName.Shipping, shipping.Id);
            //    _logger.LogInformation("");
            //    return FailedToSave(shipping.Id);
            //}

            ////_logger.LogMessage(LoggingEvents.Deleted, ApplicationConstants.ControllerName.Shipping, shipping.Id);
            //_logger.LogInformation("");
            await _shippingService.RemoveAsync(shippingId);
            return NoContent();
        }



        // PUT
        //[Authorize(Policy = ApplicationConstants.PolicyName.SuperUsers)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ShippingDto shippingDto)
        {
            bool isValid = Guid.TryParse(id, out var shippingId);

            //if (!isValid)
            //    return InvalidId(id);

            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            //var shipping = await _unitOfWork.ShippingRepository.GetShipping(shippingId);

            //if (shipping == null)
            //    return NullOrEmpty();

            //_mapper.Map(shippingResource, shipping);

            //if (!await _unitOfWork.CompleteAsync())
            //{
            //    //_logger.LogMessage(LoggingEvents.SavedFail, ApplicationConstants.ControllerName.Shipping, shipping.Id);
            //    _logger.LogInformation("");
            //    return FailedToSave(shipping.Id);
            //}

            //shipping = await _unitOfWork.ShippingRepository.GetShipping(shipping.Id);

            //var result = _mapper.Map<Shipping, ShippingDto>(shipping);
            ////_logger.LogMessage(LoggingEvents.Updated, ApplicationConstants.ControllerName.Shipping, shipping.Id);
            //_logger.LogInformation("");
            var shipping = await _shippingService.UpdateAsync(shippingId, shippingDto);
            return Ok(shipping);
        }
    }
}
