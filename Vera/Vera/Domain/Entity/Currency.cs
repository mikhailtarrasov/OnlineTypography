namespace Vera.Domain.Entity
{
    public class Currency               //  валюта
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
    }
}