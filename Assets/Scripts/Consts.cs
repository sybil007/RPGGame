using UnityEngine;

public enum Direction
{
    None = 0,
    Forward = 8,
    Backward = 4,
    Left = 1,
    Right = 2
}

public enum Speed
{
    Idle = 0,
    Walk = 1,
    Jog = 2,
    Sprint = 3
}

public enum IdlingType
{
    ArmInFront = 0,
    GrabFromGround = 1,
    Angry = 2,
    Afraid = 3,
    Neutral = 4,
    Death = 5
}

public enum TaskState
{
    NotStarted = 0,
    Opened = 1,
    Finished = 2,
    Failed = 3
}

public enum Axis
{
    Z = 0,
    Y = 1,
    X = 2
}

public static class AnimatorHashes
{
    public static readonly int ChangeDirection = Animator.StringToHash("ChangeDirection");
    public static readonly int Direction = Animator.StringToHash("Direction");
    public static readonly int Speed = Animator.StringToHash("Speed");
    public static readonly int Jump = Animator.StringToHash("Jump");
    public static readonly int Attack = Animator.StringToHash("Attack");
    public static readonly int Type = Animator.StringToHash("Type");
    public static readonly int Reset = Animator.StringToHash("Reset");
	public static readonly int Death = Animator.StringToHash("Death");

	public static readonly int PlayerInRange = Animator.StringToHash("PlayerInRange");
}

public static class TaskNames
{
    public static readonly string WrongShoes = "WrongShoes";
    public static readonly string OpenDoor = "OpenDoor";
}