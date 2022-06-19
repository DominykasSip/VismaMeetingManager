

namespace VismaMeetingManager
{
    class Program
    {
        public static string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString() + "/Data.json";
        public static MeetingManager manager = new MeetingManager(new MeetingService(new MeetingRepository(path)));
        public static void Main(string[] args)
        {
            manager.Start();
        }
    }
}