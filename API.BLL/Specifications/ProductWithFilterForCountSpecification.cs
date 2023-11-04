using API.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.BLL.Specifications
{
    public class ProductWithFilterForCountSpecification:BaseSpecifications<Product>
    {
        public ProductWithFilterForCountSpecification(ProductSpecificationPrams productprams)
            : base(x =>
            (string.IsNullOrEmpty(productprams.Search) || x.Name.ToLower().Contains(productprams.Search)) &&
            (!productprams.BrandId.HasValue || x.ProductBrandId == productprams.BrandId) &&
            (!productprams.TypeId.HasValue || x.ProductTypeId == productprams.TypeId)
            )
        {

        }
    }
}
