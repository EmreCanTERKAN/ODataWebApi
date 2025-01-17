namespace ODataWebApi.Models
{
    public sealed class Product
    {
        public Product()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }

        //navigation
        public Category Category { get; set; }
    }
}
