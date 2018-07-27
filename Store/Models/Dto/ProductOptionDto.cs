namespace Store.Models.Dto
{
    public class ProductOptionDto : BaseDto
    {
        public string Sku { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string ProductOptionDescription { get; set; }
        public int QuantityInStock { get; set; }
    }
}