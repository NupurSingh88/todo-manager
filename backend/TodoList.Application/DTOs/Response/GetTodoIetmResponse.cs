using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Application.DTOs.Response
{
    public class GetTodoIetmResponse
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        public bool IsCompleted { get; set; }

    }
}
