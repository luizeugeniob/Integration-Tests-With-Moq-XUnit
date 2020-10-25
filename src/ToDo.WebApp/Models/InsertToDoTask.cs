using System;

namespace ToDo.WebApp.Models
{
    public class InsertToDoTask
    {
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public DateTime Deadline { get; set; }
    }
}
