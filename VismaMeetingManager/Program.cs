

namespace VismaMeetingManager
{
    class Program
    {
        public static MeetingManager manager = new MeetingManager(new MeetingService(new MeetingRepository("Data.json")));
        public static void Main(string[] args)
        {
            manager.Start();
        }
    }
}