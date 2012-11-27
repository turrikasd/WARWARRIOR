using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WARWARRIOR
{
    class ExplosionParticle : HPParticle
    {
        public ExplosionParticle(Projectile projectile, int num, Actor caster) : base(projectile, num, caster) 
        {
            Random timeSeed = new Random();
            Random rnd = new Random(num + timeSeed.Next(0, 101));
            texture = Game1.contentRef.Load<Texture2D>(@"Textures/ExplosionParticle");
            velocity = (float)rnd.NextDouble();
            this.spawnTime = (int)Game1.totalMillis + 2500;
        }

        public override void Update()
        {
            for (int i = actors.Count() - 1; i >= 0; i--)
            {
                  if (!(actors[i] is Particle) &&
                      actors[i] != caster &&
                      Utils.GetDistance(actors[i].position, this.position) < 32 * actors[i].HP / 75.0f)
                {
                    Random rnd = new Random(i);

                    this.angle = Utils.GetDirection(actors[i].position, this.position);
                    this.velocity = actors[i].velocity;
                }
            }

            base.Update();
        }
    }
}
