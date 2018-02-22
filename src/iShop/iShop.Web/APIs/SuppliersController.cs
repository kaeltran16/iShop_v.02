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
    public class SuppliersController : Controller
    {
        private readonly ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }


        // GET
        //[Authorize(Policy = ApplicationConstants.PolicyName.SuperUsers)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var suppliers = await _supplierService.GetAll();

            return Ok(suppliers);
        }


        // GET
        [Authorize(Policy = ApplicationConstants.PolicyName.SuperUsers)]
        [HttpGet("{id}", Name = ApplicationConstants.ControllerName.Supplier)]
        public async Task<IActionResult> Get(string id)
        {
            bool isValid = Guid.TryParse(id, out var supplierId);

            //if (!isValid)
            //    return InvalidId(id);



            //var supplier = await _unitOfWork.SupplierRepository.GetSupplier(supplierId);

            //if (supplier == null)
            //    return NotFound(supplierId);

            //var SupplierDto = _mapper.Map<Supplier, SupplierDto>(supplier);

            var supplier = await _supplierService.Get(supplierId);
            return Ok(supplier);
        }



        // POST
        //[Authorize(Policy = ApplicationConstants.PolicyName.SuperUsers)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SupplierDto supplierDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //var supplier = _mapper.Map<SupplierDto, Supplier>(SupplierDto);

            //await _unitOfWork.SupplierRepository.AddAsync(supplier);

            //// if something happens and the new item can not be saved, return the error
            //if (!await _unitOfWork.CompleteAsync())
            //{
            //    //_logger.LogMessage(LoggingEvents.SavedFail,  ApplicationConstants.ControllerName.Supplier, supplier.Id);
            //    _logger.LogInformation("");

            //    return FailedToSave(supplier.Id);
            //}

            //supplier = await _unitOfWork.SupplierRepository.GetSupplier(supplier.Id);

            //var result = _mapper.Map<Supplier, SupplierDto>(supplier);

            ////_logger.LogMessage(LoggingEvents.Created,  ApplicationConstants.ControllerName.Supplier, supplier.Id);
            //_logger.LogInformation("");

            var supplier = await _supplierService.CreateAsync(supplierDto);

            return CreatedAtRoute(ApplicationConstants.ControllerName.Supplier, new { id = supplier.Id }, supplier);
        }

        // DELETE
        //[Authorize(Policy = ApplicationConstants.PolicyName.SuperUsers)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool isValid = Guid.TryParse(id, out var supplierId);

            //if (!isValid)
            //    return InvalidId(id);
            //var supplier = await _unitOfWork.SupplierRepository.GetSupplier(supplierId);

            //if (supplier == null)
            //    return NotFound(supplierId);

            //_unitOfWork.SupplierRepository.Remove(supplier);
            //if (!await _unitOfWork.CompleteAsync())
            //{
            //    //_logger.LogMessage(LoggingEvents.Fail,  ApplicationConstants.ControllerName.Supplier, supplier.Id);
            //    _logger.LogInformation("");

            //    return FailedToSave(supplierId);
            //}

            ////_logger.LogMessage(LoggingEvents.Deleted,  ApplicationConstants.ControllerName.Supplier, supplier.Id);
            //_logger.LogInformation("");

            await _supplierService.RemoveAsync(supplierId);

            return NoContent();
        }



        // PUT
        //[Authorize(Policy = ApplicationConstants.PolicyName.SuperUsers)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] SupplierDto supplierDto)
        {
            bool isValid = Guid.TryParse(id, out var supplierId);
            //if (!isValid)
            //    return InvalidId(id);

            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            //var supplier = await _unitOfWork.SupplierRepository.GetSupplier(supplierId);

            //if (supplier == null)
            //    return NotFound(supplierId);


            //_mapper.Map<SupplierDto, Supplier>(SupplierDto, supplier);

            //if (!await _unitOfWork.CompleteAsync())
            //{
            //    //_logger.LogMessage(LoggingEvents.SavedFail,  ApplicationConstants.ControllerName.Supplier, supplier.Id);
            //    _logger.LogInformation("");

            //    return FailedToSave(supplier.Id);
            //}

            //supplier = await _unitOfWork.SupplierRepository.GetSupplier(supplier.Id);

            //var result = _mapper.Map<Supplier, SupplierDto>(supplier);
            ////_logger.LogMessage(LoggingEvents.Updated,  ApplicationConstants.ControllerName.Supplier, supplier.Id);
            //_logger.LogInformation("");

            //return Ok(result);

            var supplier = await _supplierService.UpdateAsync(supplierId, supplierDto);
            return Ok(supplier);
        }

    }
}
