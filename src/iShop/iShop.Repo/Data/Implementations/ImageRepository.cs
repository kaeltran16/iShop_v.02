﻿//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using iShop.Data.Entities;
//using iShop.Repo.Data.Base;
//using iShop.Repo.Data.Interfaces;
//using Microsoft.EntityFrameworkCore;

//namespace iShop.Repo.Data.Implementations
//{
//    public class ImageRepository: DataRepositoryBase<Image>, IImagesRepository
//    {
//        public ImageRepository(ApplicationDbContext context)
//            : base(context)
//        {
//            Context = context;
//        }

//        public async Task<IEnumerable<Image>> GetProductImages(Guid productId, bool isIncludeRelavtive)
//        {
//            var spec = 
//                isIncludeRelavtive       
//                    ? new Specification<Image>(predicate: i => i.ProductId == productId,
//                        includes: null)
//                    : new Specification<Image>(predicate: i => i.ProductId == productId,
//                        includes: source => source.Include(i => i.Product));

//            return await GetAllAsync(spec);
//        }

//        public async Task<Image> Get(Guid id, bool isIncludeRelavtive)
//        {
//            var spec = 
//                isIncludeRelavtive       
//                    ? new Specification<Image>(predicate: i => i.Id == id,
//                        includes: null)
//                    : new Specification<Image>(predicate: i => i.Id == id,
//                        includes: source => source.Include(i => i.Product));

//            return await GetSingleAsync(spec);
//        }
//    }
//}
