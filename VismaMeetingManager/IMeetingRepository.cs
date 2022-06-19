using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaMeetingManager
{
    public interface IMeetingRepository : IBaseRepository<Meeting>
    {
        string GetMeetingIdByName(string meetingName);
    }
}
