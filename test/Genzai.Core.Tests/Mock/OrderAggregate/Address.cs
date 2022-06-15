namespace Genzai.Core.Tests.Mock.OrderAggregate
{
    /// <summary>
    /// Address value object.
    /// </summary>
    public class Address : ValueObject<Address>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Address"/> class.
        /// </summary>
        public Address()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Address"/> class.
        /// </summary>
        /// <param name="street">Street param.</param>
        /// <param name="city">City param.</param>
        /// <param name="country">Country param.</param>
        public Address(string street, string city, string country)
        {
            this.Street = street;
            this.City = city;
            this.Country = country;
        }

        /// <summary>
        /// Street address.
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// City address.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Country address.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// PropertyList
        /// </summary>
        public IEnumerable<object> PropertyList => this.GetAtomicValues();

        /// <summary>
        /// Get Atomic Values.
        /// </summary>
        /// <returns>List values.</returns>
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Street;
            yield return this.City;
            yield return this.Country;
        }
    }
}