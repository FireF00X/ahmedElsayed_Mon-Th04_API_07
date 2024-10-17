using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specification.Product
{
    public class ProducrWithSpecifications : BaseSpecification<Store.Data.Entities.Product>
    {
        public ProducrWithSpecifications(ProductSpecification specs) :
            base(product => (!specs.BrandId.HasValue || product.BrandId == specs.BrandId.Value) &&
                            (!specs.TypeId.HasValue || product.TypeId == specs.TypeId.Value) &&
                            (string.IsNullOrEmpty(specs.Search) || product.Name.Trim().ToLower().Contains(specs.Search))
            )
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Type);
            AddOrderBY(x => x.Name);

            ApplayPagination(specs.PageSize * (specs.PageIndex - 1), specs.PageSize);


            if (!string.IsNullOrEmpty(specs.Sort))
            {
                switch (specs.Sort)
                {
                    case "PriceAsc":
                        AddOrderBY(x => x.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDescending(x => x.Price);
                        break;
                    default:
                        AddOrderBY(x => x.Name);
                        break;
                }
            }

        }

        public ProducrWithSpecifications(int? id) : base(product => product.Id == id )
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Type);
        }

    }
}
