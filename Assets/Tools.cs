using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools
{
    public class SceneConstants
    {
        public const string GAME_SCENE = "GalagaTestScreen";
        public const string MENU_SCENE = "Menu";

    }

    public static Vector2 GetUnitVector2(float rotation)
    {
        var angleRadians = rotation * Mathf.Deg2Rad;

        var x = Mathf.Cos(angleRadians);
        var y = Mathf.Sin(angleRadians);

        return new Vector2(x, y);
    }

    public static Vector3 GetUnitVector3(float rotation)
    {
        return GetUnitVector2(rotation);
    }
    public static float FindAngleBetweenTwoTransforms(Transform origin, Transform target)
    {
        var vectorToTarget = target.position - origin.position;
        var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90; // I have no idea why, but this is 90 degrees larger than it should be. Fuck trigonometry
        return angle;
    }
    public static float FindAngleBetweenTwoPositions(Vector3 origin, Vector3 target)
    {
        var vectorToTarget = target - origin;
        var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90; // I have no idea why, but this is 90 degrees larger than it should be. Fuck trigonometry
        return angle;
    }
}
