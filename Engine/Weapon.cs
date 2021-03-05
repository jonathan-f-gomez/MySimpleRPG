using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class Weapon :Item
    {
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }

        public Weapon(int minimumDamage,int maximumDamage, int id, string name, string namePlural) : base(id, name, namePlural)
        {
            this.MinimumDamage = minimumDamage;
            this.MaximumDamage = maximumDamage;
        }
    }
}
