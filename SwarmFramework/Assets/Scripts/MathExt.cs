using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class MathExt
{
    public static Vector3 Vec3From(Vector2 v)
    {
        return new Vector3(v.x, v.y, 0);
    }
    public static Vector2 Vec2From(Vector3 v)
    {
        return new Vector2(v.x, v.y);
    }

    // Exclusive upper
    public static int Wrap(int v, int upper)
    {
        if (v >= upper)
            return 0;
        if (v < 0)
            return upper - 1;
        return v;
    }
}
