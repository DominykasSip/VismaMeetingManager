using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaMeetingManager
{
    public class ManagerException : Exception
    {
        public ManagerException()
        {
        }

        public ManagerException(string message) : base(message)
        {
        }

        public ManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
