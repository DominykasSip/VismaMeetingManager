using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaMeetingManager
{
    internal class MeetingManager
    {
        private readonly IMeetingService _meetingService;
        private string? _user;

        public MeetingManager(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        public string GetInputString()
        {
            string input;
            while(true)
            {
                input = Console.ReadLine();
                if(input != null && input != "")
                {
                    break;
                }
            }
            return input;
        }

        public int GetInputInt()
        {
            int input = -1;
            while (true)
            {
                try
                {
                    input = int.Parse(Console.ReadLine());
                }
                catch { }
                if (input != -1)
                {
                    break;
                }

            }
            return input;
        }

        public DateTime GetInputDateTime()
        {
            DateTime date;
            while (true)
            {
                if (DateTime.TryParse(Console.ReadLine(), out date))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("You have entered an incorrect value.");
                }
            }
            return date;
        }

        public void PrintMeetings(List<Meeting> meetings)
        {
            foreach (Meeting meeting in meetings)
            {
                Console.WriteLine();
                Console.WriteLine("Meeting name: " + meeting.Name);
                Console.WriteLine("Responsible Person: " + meeting.ResponsiblePerson);
                Console.WriteLine("Description: " + meeting.Description);
                Console.WriteLine("Category: " + meeting.Category);
                Console.WriteLine("Type: " + meeting.Type);
                Console.Write("Attendants: ");
                foreach(String attendant in meeting.Attendants)
                {
                    Console.Write(attendant + " ");
                }
                Console.WriteLine();
                Console.WriteLine("Meeting Start date: " + meeting.StartDate);
                Console.WriteLine("Meeting End date: " + meeting.EndDate);
                Console.WriteLine();
            }
        }

        public void Start()
        {
            Login();
            Menu();
        }

        public void Login()
        {
            Console.WriteLine("Please enter your Name:");
            _user = GetInputString();
        }

        public void Menu()
        {
            while(true)
            {
                Console.WriteLine("Press 1: Create a new meeting");
                Console.WriteLine("Press 2: Delete a meeting");
                Console.WriteLine("Press 3: Add person to meeting");
                Console.WriteLine("Press 4: Remove person from meeting");
                Console.WriteLine("Press 5: Show all meetings");
                Console.WriteLine("Press 6: Exit program");
                string input = GetInputString();
                string[] goodInput = { "1", "2", "3", "4", "5", "6" };
                if(input == "6")
                {
                    Console.WriteLine("Program Exited");
                    break;
                }
                if (goodInput.Contains(input))
                {
                    HandleInput(input);
                }
            }
            
        }

        public void HandleInput(string input)
        {
            switch (input)
            {
                case "1":
                    CreateMeeting();
                    break;
                case "2":
                    DeleteMeeting();
                    break;
                case "3":
                    AddPersonToMeeting();
                    break;
                case "4":
                    RemovePersonFromMeeting();
                    break;
                case "5":
                    ShowMeetings();
                    break;
                default:
                    break;
            }
        }

        public void CreateMeeting()
        {
            Console.WriteLine("Enter meeting Name:");
            string name = GetInputString();

            Console.WriteLine("Who is responsible for the meeting:");
            string responsiblePerson = GetInputString();

            Console.WriteLine("Enter Description:");
            string description = GetInputString();

            Console.WriteLine("Choose Category:");
            foreach (Category cat in Enum.GetValues(typeof(Category)))
            {
                Console.WriteLine("Press " + (int)cat + ": " + cat.ToString());
            }
            int choice = GetInputInt();
            string? category = Enum.GetName(typeof(Category), choice);

            Console.WriteLine("Choose Type:");
            foreach (Type t in Enum.GetValues(typeof(Type)))
            {
                Console.WriteLine("Press " + (int)t + ": " + t.ToString());
            }
            choice = GetInputInt();
            string? type = Enum.GetName(typeof(Type), choice);

            Console.WriteLine("Enter meeting start date (YYYY/MM/DD HH:MM):");
            DateTime startDate = GetInputDateTime();
            
            Console.WriteLine("Enter meeting end date (YYYY/MM/DD HH:MM):");
            DateTime endDate = GetInputDateTime();

            Meeting meeting = new(name, responsiblePerson, description, category, type, startDate, endDate, Guid.NewGuid().ToString());
            try
            {
                _meetingService.Add(meeting);
                Console.WriteLine("\nMeeting scheduled :)\n");
            }
            catch(ManagerException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteMeeting()
        {
            Console.WriteLine("Enter meeting name:");
            string name = GetInputString();

            try
            {
                _meetingService.Remove(name, _user);
            }
            catch (ManagerException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void AddPersonToMeeting()
        {
            Console.WriteLine("Enter persons name:");
            string personName = GetInputString();

            Console.WriteLine("Enter meeting name:");
            string meetingName = GetInputString();
            try
            {
                bool avalable = _meetingService.AddAttendantToMeeting(personName, meetingName);
                if(!avalable)
                {
                    Console.WriteLine("\nPerson added to the meeting\nWarning: Person wont be avalable at that time\n");
                }
                else
                {
                    Console.WriteLine("\nPerson will be avalable and was added to the meeting\n");
                }
            }
            catch(ManagerException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void RemovePersonFromMeeting()
        {
            Console.WriteLine("Enter persons name:");
            string personName = GetInputString();

            Console.WriteLine("Enter meeting name:");
            string meetingName = GetInputString();

            try
            {
                _meetingService.RemoveAttendantFromMeeting(personName, meetingName);
            }
            catch(ManagerException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ShowMeetings()
        {
            List<Meeting> meetings = (List<Meeting>)_meetingService.GetAll();
            while(true)
            {
                Console.WriteLine("Do you want to filter the meetings? y/n");
                string filtering = GetInputString();
                if(filtering == "y")
                {
                    while(true)
                    {
                        Console.WriteLine("What would you like to filter by?\n");
                        Console.WriteLine("Press 1: To filter by Description");
                        Console.WriteLine("Press 2: To filter by Responsible person");
                        Console.WriteLine("Press 3: To filter by Category");
                        Console.WriteLine("Press 4: To filter by Type");
                        Console.WriteLine("Press 5: To filter by Date");
                        Console.WriteLine("Press 6: To filter by Number of attendees");
                        string choise = GetInputString();
                        string[] goodInput = { "1", "2", "3", "4", "5", "6" };
                        if(goodInput.Contains(choise))
                        {
                            string parameter = string.Empty;
                            if(choise != "5" && choise != "6")
                            {
                                Console.WriteLine("Enter filter parameter:");
                                parameter = GetInputString();
                            }    
                            
                            switch(choise)
                            {
                                case "1":
                                    PrintFilteredMeetings(meetings, x => x.Description.Contains(parameter));
                                    break;
                                case "2":
                                    PrintFilteredMeetings(meetings, x => x.ResponsiblePerson.Contains(parameter));
                                    break;
                                case "3":
                                    PrintFilteredMeetings(meetings, x => x.Category.Contains(parameter));
                                    break;
                                case "4":
                                    PrintFilteredMeetings(meetings, x => x.Type.Contains(parameter));
                                    break;
                                case "5":
                                    Console.WriteLine("From what date? (YYYY/MM/DD HH:MM)");
                                    DateTime fromDate = GetInputDateTime();
                                    Console.WriteLine("To what date? (YYYY/MM/DD HH:MM)");
                                    DateTime toDate = GetInputDateTime();
                                    PrintFilteredMeetings(meetings, x => x.StartDate >= fromDate && x.StartDate < toDate);
                                    break;
                                case "6":
                                    Console.WriteLine("Enter number of attendees:");
                                    int attendantCount = GetInputInt();
                                    PrintFilteredMeetings(meetings, x => x.Attendants.Count() >= attendantCount);
                                    break;
                                default:
                                    break;
                            }

                            break;
                        }
                    }
                    break;
                }
                if(filtering == "n")
                {
                    PrintMeetings(meetings);
                    break;
                }
            }
        }

        private void PrintFilteredMeetings(List<Meeting> meetings, Func<Meeting, bool> filter)
        {
            var filteredMeetings = meetings.Where(filter).ToList();
            PrintMeetings(filteredMeetings);
        }
    }
}
