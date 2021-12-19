using System.Collections.Generic;

namespace CheckoutService
{
    public class CheckoutService
    {
        private readonly Dictionary<char, int> _catalogue = new();
        private readonly List<char> _basket = new();

        public CheckoutService(Dictionary<char, int> catalogue)
        {
            _catalogue = catalogue;
        }

        public void AddToBasket(string items)
        {
            _basket.AddRange(items);
        }

        public int GetTotal()
        {
            int total = 0;

            foreach (char sku in _basket)
            {
                total += _catalogue[sku];
            }

            return total;
        }
    }
}