namespace Genzai.Core.Tests.Mock.OrderAggregate
{
    /// <summary>
    /// Order entity.
    /// </summary>
    public class Order : Entity<Order, int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Order"/> class.
        /// </summary>
        public Order()
        {
            this.OrderItems = new List<OrderItem>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Order"/> class.
        /// </summary>
        /// <param name="buyerId">Buyer id.</param>
        /// <param name="description">Description.</param>
        /// <param name="address">Address.</param>
        public Order(int buyerId, string description, Address address)
            : this()
        {
            this.BuyerId = buyerId;
            this.Description = description;
            this.Address = address;

            this.OrderItems = new HashSet<OrderItem>();
        }

        /// <summary>
        /// Buyer id.
        /// </summary>
        public int BuyerId { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Address.
        /// </summary>
        public Address Address { get; }

        /// <summary>
        /// Order item collections.
        /// </summary>
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        /// <summary>
        /// Add order item.
        /// </summary>
        /// <param name="productId">Product id.</param>
        /// <param name="productName">Product name.</param>
        /// <param name="unitPrice">Unit price.</param>
        /// <param name="units">Units number.</param>
        public void AddOrderItem(int productId, string productName, decimal unitPrice, int units = 1)
        {
            var existingOrder = this.OrderItems.SingleOrDefault(x => x.ProductId == productId);

            if (existingOrder == null)
            {
                this.OrderItems.Add(new OrderItem(productId, productName, unitPrice, units));
            }
            else
            {
                existingOrder.AddUnits(units);
            }
        }
    }
}