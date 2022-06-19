using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaMeetingManager
{
    public class BaseModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public BaseModel(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
