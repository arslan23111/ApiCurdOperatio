using SimpleTaskApp.InterFaces;
using SimpleTaskApp.Models;
using System;
using System.Data;
using impleTaskApp.DbLayer
namespace SimpleTaskApp.Services
{
    public class TaskServices : ITaskService
    {
        private readonly IDBService _db;

        public TaskServices(IDBService db)
        {
            _db = db;
        }
    }
}
