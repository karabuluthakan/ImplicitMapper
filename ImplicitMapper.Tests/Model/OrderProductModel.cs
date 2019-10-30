using ImplicitMapper.Tests.DbEntity;

namespace ImplicitMapper.Tests.Model
{
    public class OrderProductModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }

        public static implicit operator OrderProductModel(OrderProduct data)
        {
            var model = data.CloneTo<OrderProductModel>();

            if (data.Product != null)
            {
                model.ProductName = data.Product.Name;
                model.ProductPrice = data.Product.Price;
            }

            return model;
        }
    }
}