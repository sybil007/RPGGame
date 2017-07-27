using UnityEngine;

public class PauseEvent
{
        public delegate void PauseEventHandler(bool pause);
        public static PauseEventHandler Handler;
}