using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
     public class Location
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Location (int id, string name, string description)
        {
            this.ID = id;
            this.Name = name;
            this.Description = description;
        }
    }
}
