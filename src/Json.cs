using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace todo
{
    public class Task
    {
        public Task(int id, DateTime time, string detail)
        {
            Id = id;
            Time = time;
            Details = detail;
            SubTaskIds = new List<int>();
        }
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string Details { get; set; }
        public List<int> SubTaskIds { get; set; }

    }
    public class DB
    {
        private List<string> Data = new List<string>();
        public void insert(string data) { Data.Append(data); }
    }
    public class API
    {
        private DB db = new DB();
        public void RequestCreate(string note)
        {
            int id = 0;
            DateTime time = DateTime.Now;
            string details = note;
            Task data = new Task(id, time, details);
            string formatted = JsonSerializer.Serialize(data);
            db.insert(formatted);
            Console.WriteLine(formatted);
        }
        public string RequestRead()
        {
            return "";
        }
        public void RequestUpdate(Task data)
        {
        }
        public void RequestDelete(int id)
        {

        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            API api = new API();
            api.RequestCreate("test");
        }
    }
}