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
            position = owner.position;
            angle = owner.angle;
            velocity = 10.0f;

            if (owner is Player)
                texture = Game1.contentRef.Load<Texture2D>(@"Textures/PlayerFire");
            else
                texture = Game1.contentRef.Load<Texture2D>(@"Textures/EnemyFire");
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

                    if (actors[i].HP + 25 > 25)
                    {
                        for (int ii = 0; ii < 25; ii++)
                        {
                            new HPParticle(this, ii, actors[i]);
                        }
                    }
                    else
                    {
                        for (int ii = 0; ii < 500; ii++)
                        {
                            new ExplosionParticle(this, ii, actors[i]);
                        }
                    }
                    

                    actors.Remove(this);
                    break;
                }
            }

            base.Update();
        }
    }
}
