using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tasks
{
    public class TaskTextboxChangeEvent
    {
        public delegate void TaskTextboxChangeEventHandler(string newText, int seconds);
        public static TaskTextboxChangeEventHandler Handler;
    }
}
