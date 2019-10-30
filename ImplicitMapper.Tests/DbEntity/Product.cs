using ImplicitMapper.Tests.Base;

namespace ImplicitMapper.Tests.DbEntity
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}