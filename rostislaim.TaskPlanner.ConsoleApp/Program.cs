using rostislaim.TaskPlanner.Domain.Models;
using rostislaim.TaskPlanner.Domain.Logic;
using rostislaim.TaskPlanner.DataAccess;

namespace rostislaim.TaskPlanner.ConsoleApp
{
    internal class Program
    {
        private FileWorkItemRepository _workItemRepository;
        private SimpleTaskPlanner _taskPlanner;
        

        public Program()
        {
            _workItemRepository = new FileWorkItemRepository();
            _taskPlanner = new SimpleTaskPlanner(_workItemRepository);
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            

            while (true)
            {
                Console.WriteLine("\nWhat do you want to do? \n1 - add work item \n" +
                    "2 - build a plan \n3 - mark work item as completed \n4 - remove work item\n" +
                    "5 - quit the app");
                string answer = Console.ReadLine();

                bool result = program.ChooseOption(answer);

                if (result)
                {
                    program._workItemRepository.SaveChanges();
                    break;
                }
            }
        }


        private void RemoveItem()
        {
            Console.WriteLine("Enter work item title:");
            string  title = Console.ReadLine();

            bool result = _workItemRepository.RemoveByTitle(title);
            if (result)
            {
                Console.WriteLine("Item was removed.");
            }
            else
            {
                Console.WriteLine("Item with such title was not found");
            }
        }
        private void MarkItem()
        {
            WorkItem workItem = new WorkItem();

            Console.WriteLine("Enter work item title:");
            workItem.Title = Console.ReadLine();

            bool result = _workItemRepository.Update(workItem);
            if (result)
            {
                Console.WriteLine("Item was updated.");
            }
            else
            {
                Console.WriteLine("Item with such title was not found");
            }
        }

        private bool ChooseOption(string answer)
        {
            int index = int.Parse(answer);
            switch (index)
            {
                case 1:
                    AddWorkItem();
                    return false;
                case 2:
                    CreatePlan();
                    return false;
                case 3:
                    MarkItem();
                    return false;
                case 4:
                    RemoveItem();
                    return false;
                case 5:
                    return true;
                default:
                    return false;
            }
        }

        private void CreatePlan()
        {
            List<WorkItem> workItems = new List<WorkItem>();
            workItems = _taskPlanner.CreatePlan().ToList();

            foreach (WorkItem item in workItems)
            {
                Console.WriteLine(item.ToString());
            }
        }

        private  void AddWorkItem()
        {
            WorkItem workItem = new WorkItem();
            Console.WriteLine("Enter work item title:");
            workItem.Title = Console.ReadLine();

            Console.WriteLine("Enter work item description:");
            workItem.Description = Console.ReadLine();

            Console.WriteLine("Enter work item priority:(none, low, medium, high, urgent)");
            workItem.Priority = Enum.Parse<Priority>(Console.ReadLine(), true);

            Console.WriteLine("Enter work item due time (yyyy/MM/dd or MM/dd/yyyy or dd/MM/yyyy):");
            workItem.DueTime = DateTime.Parse(Console.ReadLine());

            workItem.Id = Guid.NewGuid();
            _workItemRepository.Add(workItem);
            
        }
    }
}
