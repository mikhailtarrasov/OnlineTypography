namespace Vera.Domain.Entity
{
    public class Job
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public virtual Price Pay { get; set; } // Оплата - стоимость работы
    }
}