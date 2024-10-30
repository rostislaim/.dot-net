using rostislaim.TaskPlanner.Domain.Models;

namespace rostislaim.TaskPlanner.DataAccess.Abstractions
{

    public interface IWorkItemsRepository
    {
        Guid Add(WorkItem workItem);
        WorkItem Get(Guid id);
        WorkItem[] GetAll();
        bool Update(WorkItem workItem);
        bool Remove(Guid id);
        public bool RemoveByTitle(string title);

        void SaveChanges();
    }
    public class SomeAmazingClass
    {

    }
}
