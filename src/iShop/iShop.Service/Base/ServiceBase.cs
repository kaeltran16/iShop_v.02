using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Common.Base;
using iShop.Common.DTOs;
using iShop.Data.Base;
using iShop.Repo.UnitOfWork.Interfaces;

namespace iShop.Service.Base
{
    public abstract class ServiceBase<TEntity, TDto>
    where TEntity : IEntityBase, new()
    where TDto: IBaseDto
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        protected ServiceBase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public abstract Task<TDto> GetAsync(Guid id);
        public abstract Task<IEnumerable<TDto>> GetAllAsync();
        public abstract Task<TDto> CreateAsync();
        public abstract Task<TDto> UpdateAsync(Guid id, TDto dto);
        public abstract Task RemoveAsync(Guid id);


    }
}
