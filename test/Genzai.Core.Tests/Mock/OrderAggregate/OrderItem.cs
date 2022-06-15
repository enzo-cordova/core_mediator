namespace Genzai.Core.Tests.Mock.OrderAggregate
{
    /// <summary>
    /// Order Item entity.
    /// </summary>
    public class OrderItem : Entity<OrderItem, int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderItem"/> class.
        /// </summary>
        public OrderItem()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderItem"/> class.
        /// </summary>
        /// <param name="productId">Product id.</param>
        /// <param name="productName">Product name.</param>
        /// <param name="unitPrice">Unit price.</param>
        /// <param name="units">Unit number.</param>
        public OrderItem(int productId, string productName, decimal unitPrice, int units = 1)
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.UnitPrice = unitPrice;
            this.Units = units;
        }

        /// <summary>
        /// Product id.
        /// </summary>
        public int ProductId { get; private set; }

        /// <summary>
        /// Product name.
        /// </summary>
        public string ProductName { get; private set; }

        /// <summary>
        /// Unit price.
        /// </summary>
        public decimal UnitPrice { get; private set; }

        /// <summary>
        /// Units number.
        /// </summary>
        public int Units { get; private set; }

        /// <summary>
        /// Order navigation.
        /// </summary>
        public virtual Order Order { get; set; }

        /// <summary>
        /// Order id.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Set product.
        /// </summary>
        /// <param name="productId">Product id.</param>
        /// <param name="productName">Product name.</param>
        /// <param name="price">product price</param>
        public void SetProduct(int productId, string productName, decimal price)
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.UnitPrice = price;
        }

        /// <summary>
        /// Add units.
        /// </summary>
        /// <param name="units">Unit price.</param>
        public void AddUnits(int units)
        {
            this.Units += units;
        }
    }
}