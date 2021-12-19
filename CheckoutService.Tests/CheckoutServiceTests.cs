using System.Collections.Generic;
using Xunit;

namespace CheckoutService.Tests
{
    public class CheckoutServiceTests
    {
        private readonly Dictionary<char, int> _catalogue = new()
        {
            { 'A', 10 },
            { 'B', 15 },
            { 'C', 40 },
            { 'D', 55 }
        };

        [Theory]
        [InlineData("A", 10)]
        [InlineData("B", 15)]
        [InlineData("C", 40)]
        [InlineData("D", 55)]
        public void AddToBasket_AddSingleItem_AddsItemAmount(string input, int expected)
        {
            var checkoutService = new CheckoutService(_catalogue);

            checkoutService.AddToBasket(input);

            Assert.Equal(expected, checkoutService.GetTotal());
        }

        [Fact]
        public void AddToBasket_AddOneOfEachItem_CalculatesAggregateTotal()
        {
            var checkoutService = new CheckoutService(_catalogue);

            checkoutService.AddToBasket("ABCD");

            Assert.Equal(120, checkoutService.GetTotal());

        }

        [Fact]
        public void AddToBasket_AddThreeOfItemB_AppliesDiscount()
        {
            var checkoutService = new CheckoutService(_catalogue);

            checkoutService.AddToBasket("BBB");

            Assert.Equal(40, checkoutService.GetTotal());

        }

        [Fact]
        public void AddToBasket_AddTwoOfItemD_AppliesDiscount()
        {
            var checkoutService = new CheckoutService(_catalogue);

            checkoutService.AddToBasket("DD");

            Assert.Equal(82.5, checkoutService.GetTotal());
        }
    }
}