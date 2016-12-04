

namespace Vera.Domain.Entity
{
    public class GluePrice
    {
        public int Id { get; set; }
        public virtual Format Format { get; set; }
        public virtual Price Price { get; set; }
    }
}