using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WARWARRIOR
{
    class Projectile : Particle
    {
        private Actor owner;

        public Projectile(Actor owner)
        {
            this.owner = owner;
            texture = Game1.contentRef.Load<Texture2D>(@"Textures/Particle");
            position = owner.position;
            angle = owner.angle;
            velocity = 5.0f;
        }

        protected override void CalculateMovement()
        {
            Vector2 newPos = position + new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * velocity;

            if (newPos.X < 0 + texture.Width / 2 || newPos.X > 800 - texture.Width / 2 ||
                newPos.Y < 0 + texture.Height / 2 || newPos.Y > 480 - texture.Height / 2)
                actors.Remove(this);

            base.CalculateMovement();
        }

        public override void Update()
        {
            for (int i = actors.Count() - 1; i >= 0; i--)
            {
                if (this.owner != actors[i] &&
                    !(actors[i] is Particle) &&
                    this.position.X > actors[i].position.X - actors[i].texture.Width / 2 &&
                    this.position.Y > actors[i].position.Y - actors[i].texture.Height / 2 &&
                    this.position.X < actors[i].position.X + actors[i].texture.Width / 2 &&
                    this.position.Y < actors[i].position.Y + actors[i].texture.Height / 2)
                {
                    actors[i].HP -= 25;

                    for (int ii = 0; ii < 25; ii++)
                    {
                        actors.Add(new HPParticle(this, ii, actors[i]));
                    }

                    actors.Remove(this);
                }
            }

            base.Update();
        }
    }
}
