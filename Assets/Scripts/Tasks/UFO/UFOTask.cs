using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tasks.UFO
{
    public class UFOTask : ITask
    {
        public TaskState State { get; set; }
        public InternalState DetailedState { get; set; }
        public string DisplayName { get { return "Rozbite UFO"; } }

        public enum InternalState
        {
            NotStarted = 0,
            Accepted = 1,
            TrackingAlien = 2,
            AlienFound = 3,
            MedicineTaken = 4,
            AlienKilled = 5,
            AlienCured = 6
        }
    }
}
