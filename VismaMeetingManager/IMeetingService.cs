using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaMeetingManager
{
    internal interface IMeetingService
    {
        Meeting GetMeetingByName(string meetingName);
        IEnumerable<Meeting> GetAll();
        string GetMeetingId(string meetingName);
        void Add(Meeting meeting);
        void Remove(string meetingName, string user);
        bool AddAttendantToMeeting(string attendant, string meetingName);
        void RemoveAttendantFromMeeting(string attendant, string meetingName);
    }
}
