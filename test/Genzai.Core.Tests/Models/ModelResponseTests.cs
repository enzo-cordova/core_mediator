namespace Genzai.Core.Tests.Models
{
    /// <summary>
    /// Model response tests.
    /// </summary>
    public class ModelResponseTests
    {
        /// <summary>
        /// Test model response.
        /// </summary>
        [Fact]
        public void Test_Model_Response()
        {
            var manager = new Faker<ManagerTest>()
                .CustomInstantiator(x => new ManagerTest(1, x.Name.FirstName(Name.Gender.Male), x.Name.LastName()));
            var arrange = new Faker<ManagerResponse>()
                .CustomInstantiator(_ => new ManagerResponse(manager.Generate(1).FirstOrDefault()));

            var action = arrange.Generate(1).FirstOrDefault();

            action.Item.Should().NotBeNull();
        }

        /// <summary>
        /// Test model list response.
        /// </summary>
        [Fact]
        public void Test_Enumerable_Response()
        {
            var manager = new Faker<ManagerTest>()
                .CustomInstantiator(x => new ManagerTest(1, x.Name.FirstName(Name.Gender.Male), x.Name.LastName()));
            var arrange = new Faker<ManagerEnumerableResponse>()
                .CustomInstantiator(_ => new ManagerEnumerableResponse(manager.Generate(10)));

            var action = arrange.Generate(1).FirstOrDefault();

            action.Items.Should().NotBeEmpty();
        }
    }
}