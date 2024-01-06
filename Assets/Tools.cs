using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools
{
    public static Vector2 getUnitVector2(float rotation)
    {
        var angleRadians = rotation * Mathf.Deg2Rad;

        var x = Mathf.Cos(angleRadians);
        var y = Mathf.Sin(angleRadians);

        return new Vector3(x, y, 0);
    }

    public static Vector3 getUnitVector3(float rotation)
    {
        var angleRadians = rotation * Mathf.Deg2Rad;

        var x = Mathf.Cos(angleRadians);
        var y = Mathf.Sin(angleRadians);

        return new Vector3(x, y, 0);
    }
}
