

using UnityEngine;

public class Tools
{
    public static Vector2 GetUnitVector2(float rotation)
    {
        var angleRadians = rotation * Mathf.Deg2Rad;

        var x = Mathf.Cos(angleRadians);
        var y = Mathf.Sin(angleRadians);

        return new Vector2(x, y);
    }
    public static float FindAngleBetweenTwoPositions(Vector3 origin, Vector3 target)
    {
        var vectorToTarget = target - origin;
        var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90; // I have no idea why, but this is 90 degrees larger than it should be. Fuck trigonometry
        return angle;
    }
    public static bool FlipACoin()
    {
        var randFloat = Random.Range(0, 1.0f);
        int randInt = (int)(randFloat * 2);
        Debug.Log($"{randInt}, {randFloat}");
        return randInt % 2 == 0;
    }
}
