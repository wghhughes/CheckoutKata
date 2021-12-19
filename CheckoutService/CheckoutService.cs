using System.Collections.Generic;
using System.Linq;

namespace CheckoutService
{
    public class CheckoutService
    {
        private readonly Dictionary<char, int> _catalogue = new();
        private readonly Dictionary<string, decimal> _discounts = new();
        private readonly List<char> _basket = new();

        public CheckoutService(Dictionary<char, int> catalogue, Dictionary<string, decimal> discounts)
        {
            _catalogue = catalogue;
            _discounts = discounts;
        }

        public void AddToBasket(string items)
        {
            _basket.AddRange(items);

            _basket.Sort();
        }

        // Modulo discounting has not been used in case of discounts involving multiple items (e.g. AB)
        public decimal GetTotal()
        {
            decimal total = 0m;

            var itemsToBeProcessed = _basket;

            foreach (KeyValuePair<string, decimal> discount in _discounts)
            {
                var discountItems = discount.Key.ToCharArray();

                // Check whether the discount items form a subset of the remaining items in the basket
                while (discountItems.ToLookup(x => x).All(x => x.Count() <= itemsToBeProcessed.ToLookup(x => x)[x.Key].Count()))
                {
                    total += discount.Value;

                    foreach (char item in discountItems)
                    {
                        itemsToBeProcessed.Remove(item);
                    }
                }
            }

            foreach (char item in itemsToBeProcessed)
            {
                total += _catalogue[item];
            }

            return total;
        }
    }
}