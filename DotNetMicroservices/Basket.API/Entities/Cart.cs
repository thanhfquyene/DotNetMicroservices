namespace Basket.API.Entities
{
    public class Cart
    {
        public string UserName { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public Cart()
        {

        }
        public Cart(string userName)
        {
            UserName = userName;
        }

        public decimal TotalPrice { 
            get{
                decimal total = 0;
                foreach(var item in CartItems)
                {
                    total += item.Quantity * item.Price;
                }

                return total;
            } 
        }
    }
}
