using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WARWARRIOR
{
    class HPParticle : Particle
    {
        protected Actor caster;
        protected int spawnTime;

        public HPParticle(Projectile projectile, int num, Actor caster)
        {
            Random rnd = new Random(num);
            this.position = projectile.position;
            this.angle = rnd.Next(0, 361);
            this.velocity = (float)rnd.NextDouble() / 5;
            this.caster = caster;
            this.spawnTime = (int)Game1.totalMillis + 5000;

            this.texture = Game1.contentRef.Load<Texture2D>(@"Textures/ShieldParticle");
        }

        protected void UpdateColor()
        {
            float nextColor = (int)(spawnTime - Game1.totalMillis);

            if (nextColor <= 0.0f)
                actors.Remove(this);
            else
                color = new Color(nextColor / 1000, nextColor / 1000, nextColor / 1000, nextColor / 1000);
        }

        public override void Update()
        {
            if (!(this is ExplosionParticle))
            {
                for (int i = actors.Count() - 1; i >= 0; i--)
                {
                    if (this.caster != actors[i] &&
                        !(actors[i] is Particle) &&
                        this.position.X > actors[i].position.X - actors[i].texture.Width / 2 &&
                        this.position.Y > actors[i].position.Y - actors[i].texture.Height / 2 &&
                        this.position.X < actors[i].position.X + actors[i].texture.Width / 2 &&
                        this.position.Y < actors[i].position.Y + actors[i].texture.Height / 2)
                    {
                        if (actors[i].HP < 100)
                            actors[i].HP += 1;

                        actors.Remove(this);
                    }
                }
            }

            UpdateColor();

            base.Update();
        }
    }
}
