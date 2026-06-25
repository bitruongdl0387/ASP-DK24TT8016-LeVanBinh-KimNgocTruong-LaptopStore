namespace LaptopStore.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int? BrandId { get; set; }

        public string? Cpu { get; set; }

        public string? Ram { get; set; }

        public string? Ssd { get; set; }

        public string? Vga { get; set; }

        public string? DisplaySize { get; set; }

        public decimal Price { get; set; }
        public decimal? SalePrice { get; set; }
        public int Quantity { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }
    }
}