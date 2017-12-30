using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Tasks.WrongShoes
{
    public class WrongShoes : ITask
    {
        public TaskState State { get; set; }
        public string DisplayName { get { return "Niepasujące buty"; } }
        public bool HasShoe { get; set; }
    }
}
