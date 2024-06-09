using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant
{
    // tag
    public const string TAG_PLAYER = "Player";
    public const string TAG_BOT = "Bot";

    //Axis
    public const string AXIS_HORIZONTAL = "Horizontal";
    public const string AXIS_VETICAL = "Vertical";

    // animation
    public const string ANIM_NONE = "None";
    public const string ANIM_SPEED = "Speed";
    public const string ANIM_IDLE = "Idle";
    public const string ANIM_WIN = "Win";
    public const string ANIM_LOSE = "Lose";

    // bot state machine
    public static IState<Bot> idleState = new IdleBot();
    public static IState<Bot> findBrickState = new FindBrickBot();
    public static IState<Bot> buildBrickState = new BuildBrickBot();
}

public enum EBrickType
{
    None = 0,
    Red = 1,
    Green = 2,
    Blue = 3,
    Yellow = 4,
    Black = 5,
    Pink = 6
}

public enum ELight
{
    Day = 0,
    Night = 1
}

public enum EGameState
{
    None = 0,
    MainMenu = 1,
    Setting = 2,
    Victory = 3,
    Fail = 4,
    GamePlay = 5
}

public enum EWeaponType
{
    Sword = 0,
    Gun = 1,
    Arrow = 2
}