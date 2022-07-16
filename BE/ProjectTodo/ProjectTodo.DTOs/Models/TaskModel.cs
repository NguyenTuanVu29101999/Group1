using System;
using System.Collections.Generic;

namespace ProjectTodo.DTOs.Models
{
    public partial class TaskModel
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
