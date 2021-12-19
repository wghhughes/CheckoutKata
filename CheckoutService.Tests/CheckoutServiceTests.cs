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

        private readonly Dictionary<string, decimal> _discounts = new()
        {
            { "BBB", 40 },
            { "DD", 82.5m }
        };

        [Theory]
        [InlineData("A", 10)]
        [InlineData("B", 15)]
        [InlineData("C", 40)]
        [InlineData("D", 55)]
        public void AddToBasket_AddSingleItem_AddsItemAmount(string input, decimal expected)
        {
            var checkoutService = new CheckoutService(_catalogue, _discounts);

            checkoutService.AddToBasket(input);

            Assert.Equal(expected, checkoutService.GetTotal());
        }

        [Fact]
        public void AddToBasket_AddOneOfEachItem_CalculatesAggregateTotal()
        {
            var checkoutService = new CheckoutService(_catalogue, _discounts);

            checkoutService.AddToBasket("ABCD");

            Assert.Equal(120, checkoutService.GetTotal());

        }

        [Fact]
        public void AddToBasket_AddThreeOfItemB_AppliesDiscount()
        {
            var checkoutService = new CheckoutService(_catalogue, _discounts);

            checkoutService.AddToBasket("BBB");

            Assert.Equal(40, checkoutService.GetTotal());

        }

        [Fact]
        public void AddToBasket_AddTwoOfItemD_AppliesDiscount()
        {
            var checkoutService = new CheckoutService(_catalogue, _discounts);

            checkoutService.AddToBasket("DD");

            Assert.Equal(82.5m, checkoutService.GetTotal());
        }

        [Theory]
        [InlineData("AABBBBBBBCDDD", 292.5)]
        [InlineData("DDBACCDDD", 325)]
        public void AddToBasket_AddVariousItems_AppliesAppropriateDiscounts(string input, decimal expected)
        {
            var checkoutService = new CheckoutService(_catalogue, _discounts);

            checkoutService.AddToBasket(input);

            Assert.Equal(expected, checkoutService.GetTotal());
        }
    }
}