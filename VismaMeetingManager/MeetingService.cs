using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaMeetingManager
{
    public class MeetingService : IMeetingService
    {
        private readonly IMeetingRepository _meetingRepository;

        public MeetingService(IMeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }

        public void Add(Meeting meeting)
        {
            _meetingRepository.Add(meeting);
        }

        public bool AddAttendantToMeeting(string attendant, string meetingName)
        {
            Meeting meeting = GetMeetingByName(meetingName);

            if (meeting.Attendants.ToList().Contains(attendant))
            {
                throw new ManagerException("Same person can't be added twice");
            }
 
            meeting.Attendants.Add(attendant);

            _meetingRepository.Update(meeting);

            return IsAttendantAvalable(attendant, meeting);
        }

        public IEnumerable<Meeting> GetAll()
        {
           return _meetingRepository.GetAll();
        }

        public Meeting GetMeetingByName(string meetingName)
        {
            return _meetingRepository.GetById(GetMeetingId(meetingName));
        }

        public string GetMeetingId(string meetingName)
        {
            return _meetingRepository.GetMeetingIdByName(meetingName);
        }

        public void Remove(string meetingName, string user)
        {
            Meeting meeting = GetMeetingByName(meetingName);

            if (user == meeting.ResponsiblePerson)
            {
                _meetingRepository.Delete(GetMeetingId(meeting.Name));
            }
            else
            {
                throw new ManagerException("Only the responsible person can delete the meeting");
            }   
        }

        public void RemoveAttendantFromMeeting(string attendant, string meetingName)
        {
            Meeting meeting = GetMeetingByName(meetingName);

            if(meeting.ResponsiblePerson == attendant)
            {
                throw new ManagerException("Responsible person can't be removed");
            }
            else
            {
                meeting.Attendants.Remove(attendant);
                _meetingRepository.Update(meeting);
            }
        }
        private static bool OverlappingPeriods(DateTime aStart, DateTime aEnd, DateTime bStart, DateTime bEnd)
        {
            if (aStart > aEnd)
                throw new ArgumentException("A start can not be after its end.");

            if (bStart > bEnd)
                throw new ArgumentException("B start can not be after its end.");

            return !((aEnd < bStart && aStart < bStart) ||
                        (bEnd < aStart && bStart < aStart));
        }

        private bool IsAttendantAvalable(string attendant, Meeting meeting)
        {
            IEnumerable<Meeting> allMeetings = _meetingRepository.GetAll();

            var meet = allMeetings.FirstOrDefault(x => OverlappingPeriods(x.StartDate, x.EndDate, meeting.StartDate, meeting.EndDate) && x.Attendants.Contains(attendant) && x.Id != meeting.Id);
            return meet == null;
        }
    }
}
