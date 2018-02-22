using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Common.DTOs;
using iShop.Common.Exceptions;
using iShop.Common.Extensions;
using iShop.Data.Entities;
using iShop.Repo.Data.Interfaces;
using iShop.Repo.UnitOfWork.Interfaces;
using iShop.Service.Commons;
using iShop.Service.Interfaces;

namespace iShop.Service.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _repository;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = _unitOfWork.GetRepository<IOrderRepository>();
        }

        public async Task<IServiceResult> CreateAsync(SavedOrderDto orderDto)
        {
            try
            {
                var order = _mapper.Map<SavedOrderDto, Order>(orderDto);

                await _repository.AddAsync(order);

                foreach (var orderItem in orderDto.OrderedItems)
                {
                    order.AddItem(orderItem.ProductId, orderItem.Quantity);
                }

                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new SaveFailedException(nameof(order));
                }

                var result = await GetSingleAsync(order.Id.ToString());
                return new ServiceResult(payload: result.Payload);
            }
            catch (Exception e)
            {
                return new ServiceResult(false, e.Message);
            }
          
        }

        public async Task<IServiceResult> GetSingleAsync(string id)
        {
            try
            {
                var orderId = id.ToGuid(nameof(id));

                var order = await _repository.GetOrder(orderId);
                if (order == null)
                    throw new NotFoundException(nameof(order), id);

                var orderDto = _mapper.Map<Order, OrderDto>(order);
                return new ServiceResult(payload: orderDto);
            }
            catch (Exception e)
            {
                return new ServiceResult(false, e.Message);
            }
            
        }

        public async Task<IServiceResult> GetAllAsync()
        {
            try
            {
                var orders = await _repository.GetOrders();
                var ordersDto = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(orders);

                return new ServiceResult(payload: ordersDto);
            }
            catch (Exception e)
            {
                return new ServiceResult(false, e.Message);
            }            
        }

        public async Task<IServiceResult> UpdateAsync(string id, SavedOrderDto orderDto)
        {
            try
            {
                var orderId = id.ToGuid(nameof(id));
                var order = await _repository.GetOrder(orderId);
                _mapper.Map(orderDto, order);

                AddOrRemoveOrderedItems(order, orderDto);

                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new SaveFailedException(nameof(order));
                }

                var result = await GetSingleAsync(order.Id.ToString());
                return new ServiceResult(payload: result.Payload);
            }
            catch (Exception e)
            {
                return new ServiceResult(false, e.Message);
            }
        }

        public async Task<IServiceResult> RemoveAsync(string id)
        {
            try
            {
                var orderId = id.ToGuid(nameof(id));

                var order = await _repository.GetOrder(orderId, false);
                if (order == null)
                    throw new NotFoundException(nameof(order), orderId);

                _repository.Remove(order);
                
                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new SaveFailedException(nameof(order));
                }
                return new ServiceResult();
            }
            catch (Exception e)
            {
                return new ServiceResult(false, e.Message);
            }
           
        }

        private void AddOrRemoveOrderedItems(Order order, SavedOrderDto orderDto)
        {
            var addedOrderItems =
                orderDto.OrderedItems.Where(oid => order.OrderedItems.All(oi => oi.ProductId != oid.ProductId))
     
                    .ToList();
            foreach (var orderItemDto in addedOrderItems)
                order.AddItem(orderItemDto.ProductId, orderItemDto.Quantity);

            var removedOrderedItems =
                order.OrderedItems.Where(oi => orderDto.OrderedItems.Any(oir => oir.ProductId != oi.ProductId))
                    .ToList();
            foreach (var item in removedOrderedItems)
                order.RemoveItem(item);
        }

    }
}
