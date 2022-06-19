using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaMeetingManager
{
    public class MeetingRepository : BaseRepository<Meeting> , IMeetingRepository
    {
        public MeetingRepository(string pathToFile) : base(pathToFile)
        {

        }

        public string GetMeetingIdByName(string meetingName)
        {
            List<Meeting> all = (List<Meeting>)GetAll();
            Meeting meeting = all.Find(x => x.Name == meetingName);
            if(meeting == null)
            {
                throw new ManagerException("No such meeting");
            }
            return meeting.Id;
        }

    }
}
