using ImplicitMapper.Tests.Base;

namespace ImplicitMapper.Tests.DbEntity
{
    public class OrderProduct : BaseEntity
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public Product Product { get; set; }
    }
}