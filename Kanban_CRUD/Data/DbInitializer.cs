using Kanban_CRUD.Models;
using System.Collections.Generic;
using System.Linq;

namespace Kanban_CRUD.Data
{
    public static class DbInitializer
    {
        public static void Initialize(KanbanDataContext context)
        {
            context.Database.EnsureCreated();

            if (context.KanbanCards.Any())
            {
                return;   // DB has been seeded
            }

            List<KanbanCard> cards = new List<KanbanCard>()
            {
                new KanbanCard{ Id=1, Summary="Analyze the new requirements gathered from the customer.", RankId= 1, Assignee = "Andrew Fuller", Status="Open" },
                new KanbanCard{ Id=2, Summary="Improve application performance.", RankId= 1, Assignee = "Andrew Fuller", Status="InProgress" },
                new KanbanCard{ Id=3, Summary="Fixed internally reported issues.", RankId= 1, Assignee = "Andrew Fuller", Status="Review" },
                new KanbanCard{ Id=4, Summary="Test the application in the IE browser.", RankId= 1, Assignee = "Andrew Fuller", Status="Close" }
            };

            foreach (KanbanCard card in cards)
            {
                context.KanbanCards.Add(card);
            }
            context.SaveChanges();
        }
    }
}
