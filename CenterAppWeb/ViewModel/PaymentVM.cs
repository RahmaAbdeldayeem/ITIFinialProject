namespace CenterAppWeb.ViewModel
{
    public class PaymentVM
    {
        public double Price { get; set; }
        public int StudentId { get; set; }
        public int MateriaId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPaid { get; set; }
    }
}
