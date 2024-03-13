using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals
{
    public const string GAME_SCENE = "GalagaTestScreen";
    public const string MENU_SCENE = "Menu";
    /// <summary>
    /// Add this to the Team enum to get which layer the bullet belongs to
    /// </summary>
    public const int BULLET_TEAM_LAYER_NUM = 9;

    public static int gravityScale = 1;

    public static Bounds PlayerBounds;
}
