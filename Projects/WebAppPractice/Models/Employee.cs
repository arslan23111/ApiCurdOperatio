namespace WebAppPractice.Models
{
    public class Employee
    {
        public int Id { get; set; } // ✅ Add this property
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
