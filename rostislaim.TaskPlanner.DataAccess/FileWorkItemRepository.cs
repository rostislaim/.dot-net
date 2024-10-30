using Newtonsoft.Json;
using rostislaim.TaskPlanner.DataAccess.Abstractions;
using rostislaim.TaskPlanner.Domain.Models;

namespace rostislaim.TaskPlanner.DataAccess
{

    public class FileWorkItemRepository : IWorkItemsRepository
    {
        private const string _fileName = "work-items.json";
        private readonly Dictionary<Guid, WorkItem> _dictionary;
        public FileWorkItemRepository()
        {
            _dictionary = new Dictionary<Guid, WorkItem>();

            if (File.Exists(_fileName))
            {
                string data = File.ReadAllText(_fileName);
                List<WorkItem> workItems = JsonConvert.DeserializeObject<List<WorkItem>>(data);

                foreach(WorkItem item in workItems)
                {
                    Add(item);
                }
            }

        }

        public Guid Add(WorkItem workItem)
        {
            WorkItem item = workItem;
            Guid id = Guid.NewGuid();
            _dictionary[id] = item;
            return id;
        }

        public WorkItem Get(Guid id)
        {
            if (_dictionary.ContainsKey(id))
                return _dictionary[id];
            else
                return null;
        }

        public WorkItem[] GetAll()
        {
            List<WorkItem> items = new List<WorkItem>();

            foreach (var item in _dictionary.Values)
            {
                items.Add(item);
            }

            return items.ToArray();

        }

        public bool Remove(Guid id)
        {
            foreach(Guid key in _dictionary.Keys)
            {
                if (key.Equals(id))
                {
                    _dictionary.Remove(key);
                    return true;
                }
            }

            return false;
        }

        public bool RemoveByTitle(string title)
        {
            foreach (KeyValuePair<Guid,WorkItem> pair in _dictionary)
            {
                if (pair.Value.Title.Equals(title))
                {
                    return _dictionary.Remove(pair.Key);
                }
            }

            return false;
        }

        public void SaveChanges()
        {
            string data = JsonConvert.SerializeObject(_dictionary.Values);
            File.WriteAllText(_fileName, data);
        }

        public bool Update(WorkItem workItem)
        {
            foreach (KeyValuePair<Guid,WorkItem> item in _dictionary)
            {
                if (item.Value.Title.Equals(workItem.Title))
                {
                    item.Value.IsCompleted = true;
                    _dictionary[item.Key] = item.Value;
                    return true;
                }
            }
            return false;
        }
    }
}
