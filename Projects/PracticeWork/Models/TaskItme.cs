namespace SimpleTaskApp.Models
{
    public class TaskItme
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime Date { get; set; } 

        public String Status { get; set; } = "Pending";
    }
}
