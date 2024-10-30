using rostislaim.TaskPlanner.Domain.Models;
using rostislaim.TaskPlanner.DataAccess.Abstractions;

namespace rostislaim.TaskPlanner.Domain.Logic
{
    public class SimpleTaskPlanner
    {
        private IWorkItemsRepository _workItemsRepository;
        public SimpleTaskPlanner(IWorkItemsRepository workItemsRepository)
        {
            _workItemsRepository = workItemsRepository;
        }
        public WorkItem[] CreatePlan()
        {
            List<WorkItem> workItems = _workItemsRepository.GetAll().ToList();

            workItems.Sort(CompareWorkItemsByPriority);

            WorkItem[] sortetItems = workItems.Where(x => x.IsCompleted == false).ToArray();
            return sortetItems;
        }

        public int CompareWorkItemsByPriority(WorkItem firstItem, WorkItem secondItem)
        {
            if(firstItem.Priority > secondItem.Priority)
            {
                return -1;
            }
            else if(firstItem.Priority < secondItem.Priority)
            {
                return 1;
            }
            return 0;
        }
    }
}
