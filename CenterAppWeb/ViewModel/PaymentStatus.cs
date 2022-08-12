namespace CenterAppWeb.ViewModel
{
    public class PaymentStatus
    {
        public int Id { get; set; }
        public bool IsPaid { get; set; }
        public string MaterialName { get; set; }
        public double  Price { get; set; }
        public string  Date { get; set; }
    }
}
