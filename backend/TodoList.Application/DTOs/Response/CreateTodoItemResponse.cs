using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Application.DTOs.Response
{
    public class CreateTodoItemResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
    }
}
