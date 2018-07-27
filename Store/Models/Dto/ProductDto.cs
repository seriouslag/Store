using System.Collections.Generic;

namespace Store.Models.Dto
{
    public class ProductDto : BaseDto
    {
        public string Name { get; set; }
        public IEnumerable<ProductOptionDto> ProductOptions { get; set; }
    }
}