using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class PlayerQuest
    {
        public Quest Details { get; set; }
        public bool IsCompleted { get; set; }
        public PlayerQuest(Quest details)
        {
            this.Details = details;
            IsCompleted = false;
        }
    }
}
