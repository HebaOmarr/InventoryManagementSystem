namespace InventoryManagementSystem.API.DTOs.Product
{
    public class CreateProductDTO
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int LowStockThreshold { get; set; }

        public int categoryId { get; set; }
    }
}
