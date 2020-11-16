using System;
using System.Collections.Generic;

namespace RayCastingEng
{
    class Player
    {
        private readonly double rotationSpeed = 0.1;
        private readonly double moveSpeed = 1.0;
        public static readonly double FOV = Math.PI/3;
        public Player(double x, double y, 
                      double depht = 16.0, double angle = 0)
        {
            Depht = depht;
            X = x;
            Y = y;
            POV = angle;
        }
        public double Depht { get; private set; }
        public double X { get; private set; }
        public double Y { get; private set; }
        public double POV { get; private set; }

        /// <summary>
        /// Change person direction.
        /// </summary>
        /// <param name="powerDir"> If dir more than 0 then change person direction will occur clockwise.
        /// If dir lover then 0 it will be counterclockwise.
        /// Else do not rotation.
        /// </param>
        public void Rotation(int powerDir)
        {
            POV += powerDir * rotationSpeed;
        }
        public double GetDeegreePOV()
        {
            return (POV * 180 / Math.PI) % 360;
        }
        /// <summary>
        /// Change person possition.
        /// </summary>
        /// <param name="powerX"> If dir more than 0 then change person direction will occur clockwise.
        /// If dir lover then 0 it will be counterclockwise.
        /// Else do not rotation.
        /// </param>
        /// <param name="powerY"> If dir more than 0 then change person direction will occur clockwise.
        /// If dir lover then 0 it will be counterclockwise.
        /// Else do not rotation.
        /// </param>
        public void Move(double powerX, double powerY)
        {
            X += powerX * moveSpeed;
            Y += powerY * moveSpeed;
        }
        public void Move(int powerSpeed)
        {
            X += Math.Sin(POV) * moveSpeed * powerSpeed;
            Y += Math.Cos(POV) * moveSpeed * powerSpeed;
        }
    }
}