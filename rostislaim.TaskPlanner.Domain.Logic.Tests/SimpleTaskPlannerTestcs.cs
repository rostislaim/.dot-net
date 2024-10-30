using Moq;
using rostislaim.TaskPlanner.DataAccess.Abstractions;
using rostislaim.TaskPlanner.Domain.Models;

namespace rostislaim.TaskPlanner.Domain.Logic.Tests
{
    public class SimpleTaskPlannerTestcs
    {
        [Fact]
        public void SimplePlannerTest()
        {
            var mockRepo = new Mock<IWorkItemsRepository>();

            var expectedWorkItems = new WorkItem[]
            {
                new WorkItem { Title = "Task 1", Description = "first task", DueTime = DateTime.Today, Priority = Priority.Low },
                new WorkItem { Title = "Task 2", Description = "second task", DueTime = DateTime.Today, Priority = Priority.High },
                new WorkItem { Title = "Task 3", Description = "third task", DueTime = DateTime.Today, Priority = Priority.Medium, IsCompleted= true }
            };

            mockRepo.Setup(repo => repo.GetAll()).Returns(expectedWorkItems);

            var actualWorkItems = mockRepo.Object.GetAll();

            Assert.Equal(expectedWorkItems, actualWorkItems);

            SimpleTaskPlanner taskPlanner = new SimpleTaskPlanner(mockRepo.Object);
            WorkItem[] plan = taskPlanner.CreatePlan();

            bool isSorted = IsSortedCorrectly(plan);
            bool isRelevantPresent = AreRelevantPresent(expectedWorkItems,plan);
            bool isIrrelevantAbsent = AreIrrelevantAbsent(plan);

            Assert.True(isSorted);
            Assert.True(isRelevantPresent);
            Assert.True(isIrrelevantAbsent);
        }

        private bool IsSortedCorrectly(WorkItem[] plan)
        {
            for (int i =0; i< plan.Length - 1; i++)
            {
                if (plan[i].Priority < plan[i + 1].Priority)
                {
                    return false;
                }
            }

            return true;
        }

        private bool AreRelevantPresent(WorkItem[] collection, WorkItem[] plan)
        {
            int relevantCollectionCount = collection.Count(w => w.IsCompleted == false);
            int relevantPlanCount = plan.Count(w => w.IsCompleted == false);

            if(relevantCollectionCount == relevantPlanCount)
                return true;
            else
                return false;
        }

        private bool AreIrrelevantAbsent(WorkItem[] plan)
        {
            int irrelevantCount = plan.Count(w => w.IsCompleted == true);

            if (irrelevantCount == 0)
                return true;
            else
                return false;
        }
    }
}
