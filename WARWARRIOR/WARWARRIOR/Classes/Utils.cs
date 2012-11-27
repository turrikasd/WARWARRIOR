using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WARWARRIOR
{
    public enum GAMESTATE
    {
        START_SCREEN,
        PLAYING,
        PAUSED
    }

    static class Utils
    {
        public static float GetDistance(Vector2 vect1, Vector2 vect2)
        {
            float x = vect1.X - vect2.X;
            float y = vect1.Y - vect2.Y;

            return (float)Math.Sqrt(x * x + y * y);
        }

        public static float GetDirection(Vector2 vect1, Vector2 vect2)
        {
            float pxRes = vect1.X - vect2.X;
            float pyRes = vect1.Y - vect2.Y;


            if (pxRes > 0.0f)
                return (float)(Math.Atan(pyRes / pxRes) + Math.PI);
            else
                return (float)(Math.Atan(pyRes / pxRes) + (2 * Math.PI));
        }
    }
}
