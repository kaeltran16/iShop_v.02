using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Common.DTOs;
using iShop.Data.Entities;
using iShop.Repo.Data.Implementations;
using iShop.Repo.Data.Interfaces;
using iShop.Repo.UnitOfWork.Interfaces;
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

        public async Task<OrderDto> CreateAsync(SavedOrderDto orderDto)
        {
            var order = _mapper.Map<SavedOrderDto, Order>(orderDto);

            await _repository.AddAsync(order);

            foreach (var orderItem in orderDto.OrderedItems)
            {
                order.AddItem(orderItem.ProductId, orderItem.Quantity);
            }

            await _unitOfWork.CompleteAsync();

            return await Get(order.Id);
        }

        public async Task<OrderDto> Get(Guid id)
        {
            var order = await _repository.GetOrder(id);
            return _mapper.Map<Order, OrderDto>(order);
        }

        public async Task<IEnumerable<OrderDto>> GetAll()
        {
            var orders = await _repository.GetOrders();
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(orders);
        }

        public async Task RemoveAsync(Guid orderId)
        {
            var order = await _repository.GetOrder(orderId, false);
            _repository.Remove(order);
            await _unitOfWork.CompleteAsync();
        }


        public async Task<OrderDto> UpdateAsync(Guid orderId, SavedOrderDto orderDto)
        {
            var order = await _repository.GetOrder(orderId);
            _mapper.Map(orderDto, order);

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

            await _unitOfWork.CompleteAsync();

            return await Get(order.Id);
        }
    }
}
