using System.Data;
using TaskManagement.DBLayer;
using TaskManagement.Iservices;
using TaskManagement.Models;

namespace TaskManagement.services
{
    public class TaskServices: ITaskservices
    {

        private readonly IDBServices _dbServices;

        public TaskServices(IDBServices dBServices)
        {
            _dbServices = dBServices;
        }


        public async Task<List<TaskItem>> GetAllAsync()
        {
            string sql = "SELECT * FROM Tasks";
            var table = await _dbServices.QueryAsync(sql);

            if (table == null || table.Rows.Count == 0)
                return new List<TaskItem>(); // empty list, no error

            return table.AsEnumerable()
                        .Select(row => new TaskItem
                        {
                            Id = row.Field<int>("Id"),
                            Title = row.Field<string>("Title") ?? "",
                            Description = row.Field<string>("Description") ?? "",
                            Date = row.Field<DateTime>("Date"),
                            Status = row.Field<string>("Status") ?? "Pending"
                        }).ToList();
        }


    }
}
