using API.BLL.Entities;
using API.BLL.Interfaces;
using API.BLL.Specifications;
using API.DTO;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   
    public class ProductController : BaseAPIController
    {
        private readonly IGenricReposatory<Product> productReposatory;
        private readonly IGenricReposatory<ProductBrand> brandReposatory;
        private readonly IGenricReposatory<ProductType> typeReposatory;
        private readonly IMapper mapper;

        public ProductController(IGenricReposatory<Product> productReposatory,IGenricReposatory<ProductBrand> BrandReposatory,IGenricReposatory<ProductType> typeReposatory,IMapper mapper)
        {
            this.productReposatory = productReposatory;
            brandReposatory = BrandReposatory;
            this.typeReposatory = typeReposatory;
            this.mapper = mapper;
        }
        public IProductReposatory productReposatory1 { get; }
        [HttpGet]
        [cashe(700)]
        public async Task<ActionResult<Pagination<ProductDTO>>> GetProducts([FromQuery]ProductSpecificationPrams productPrams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productPrams);
            var countspec = new ProductWithFilterForCountSpecification(productPrams);
            var totalitems=await productReposatory.CountAsync(countspec);
            var data=await productReposatory.listwithspecificationasync(spec);
            var mappedData = mapper.Map<IReadOnlyList<ProductDTO>>(data);
            var paginatedlist = new Pagination<ProductDTO>(productPrams.PageIndex, productPrams.PageSize,totalitems,mappedData);
           
            return Ok(paginatedlist);
        }
        [HttpGet]
        [Route("id")]
        
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var data = await productReposatory.GetEntityWithSpecifcation(spec);
            if(data is null)
                return NotFound(new APIResponse(404));
            var mappedData = mapper.Map<ProductDTO>(data);
            return mappedData;
        }

        [HttpGet]
        [Route("Brand")]
        public async Task<IReadOnlyList<ProductBrand>> GetProductBrand()
        {
            var data = await brandReposatory.GetAllAsync();
            return data;
        }

        [HttpGet]
        [Route("types")]
        public async Task<IReadOnlyList<ProductType>> GetProducttype()
        {
            var data = await typeReposatory.GetAllAsync();
            return data;
        }
    }
}
