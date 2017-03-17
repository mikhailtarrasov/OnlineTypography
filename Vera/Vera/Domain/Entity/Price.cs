namespace Vera.Domain.Entity
{
    public class Price
    {
        public int Id { get; set; }
        public decimal Cost { get; set; }
        public virtual Currency Currency { get; set; }
    }
}