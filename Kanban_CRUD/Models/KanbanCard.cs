using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kanban_CRUD.Models
{
    public class KanbanCard
    {
        public int Id { get; set; }
        public string Summary { get; set; }
        public int RankId { get; set; }
        public string Assignee { get; set; }
        public string Status { get; set; }
    }
}
