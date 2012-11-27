using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WARWARRIOR
{
    class Enemy : Actor
    {
        private float amplitude;
        private double lastFire;

        public Enemy()
        {
            Random rnd = new Random();
            texture = Game1.contentRef.Load<Texture2D>(@"Textures/Enemy");
            position = new Vector2(rnd.Next(50, 700), rnd.Next(50, 420));
            amplitude = (float)rnd.NextDouble();
        }

        protected void TurnAndMoveTowardsPlayer()
        {
            Random rnd = new Random();

            float pxRes = position.X - Player.player.position.X;
            float pyRes = position.Y - Player.player.position.Y;


            if (pxRes > 0.0f)
                angle = (float)(Math.Atan(pyRes / pxRes) + Math.PI);
            else
                angle = (float)(Math.Atan(pyRes / pxRes) + (2 * Math.PI));

            angle += (float)Math.Sin(Game1.totalSeconds) * amplitude;

            velocity = (float)Math.Sqrt(pxRes * pxRes + pyRes * pyRes) / 200;
        }

        protected void ShouldFire()
        {
            if (lastFire < Game1.totalSeconds)
            {
                Random timeSeed = new Random();
                Random rnd = new Random(timeSeed.Next(0, 101));

                if (rnd.Next(0, 101) < 3)
                {
                    Fire();
                    lastFire = Game1.totalSeconds + 0.5f;
                }
            }
        }

        public override void Update()
        {
            TurnAndMoveTowardsPlayer();
            ShouldFire();

            base.Update();
        }
    }
}
