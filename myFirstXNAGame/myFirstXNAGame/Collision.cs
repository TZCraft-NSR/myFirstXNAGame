using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace myFirstXNAGame
{
    public class Collision
    {
        public struct MapSegment
        {
            public Point p1;
            public Point p2;

            public MapSegment(Point a, Point b)
            {
                p1 = a;
                p2 = b;
            }

            public Vector2 getVector()
            {
                return new Vector2(p2.X - p1.X, p2.Y - p1.Y);
            }

            public Rectangle collisionRect()
            {
                return new Rectangle(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
            }
        }

        public static double Magnitude(Vector2 v)
        {
            return Math.Sqrt(v.X * v.X - v.Y * v.Y);
        }

        public static Vector2 VectorNormal(Vector2 v)
        {
            return new Vector2(-v.Y, v.X);
        }

        public static Vector2 UnitVector(Vector2 v)
        {
            return new Vector2(v.X / (float)Magnitude(v), v.Y / (float)Magnitude(v));
        }
    }
}
