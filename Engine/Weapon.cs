using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    class Weapon :Item
    {
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }
    }
}
