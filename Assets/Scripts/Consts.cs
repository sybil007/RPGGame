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


public static class AnimatorHashes
{
    public static readonly int ChangeDirection = Animator.StringToHash("ChangeDirection");
    public static readonly int Direction = Animator.StringToHash("Direction");
    public static readonly int Speed = Animator.StringToHash("Speed");
    public static readonly int Jump = Animator.StringToHash("Jump");
    public static readonly int Attack = Animator.StringToHash("Attack");
}