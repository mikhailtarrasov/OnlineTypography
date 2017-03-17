namespace Vera.Domain.Entity
{
    public class Sewing     //Шитье
    {
        public int Id { get; set; }
        public virtual Price Price { get; set; }
        public virtual FormingType FormingType { get; set; }
    }
}