namespace LaptopStore.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string? Status { get; set; }

        public string? Note { get; set; }

        public string? Phone { get; set; }

        public Customer? Customer { get; set; }
    }
}