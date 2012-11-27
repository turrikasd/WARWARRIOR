using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WARWARRIOR
{
    class Particle : Actor
    {
        protected override void CalculateMovement()
        {
            Vector2 newPos = position + new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * velocity;

            if (newPos.X < 0 + texture.Width / 2 || newPos.X > Game1.Width - texture.Width / 2 ||
                newPos.Y < 0 + texture.Height / 2 || newPos.Y > Game1.Height - texture.Height / 2)
                actors.Remove(this);

            base.CalculateMovement();
        }
    }
}
