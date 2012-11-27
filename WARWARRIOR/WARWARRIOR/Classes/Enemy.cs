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
        public Enemy() : base()
        {
            Random rnd = new Random();
            texture = Game1.contentRef.Load<Texture2D>(@"Textures/Enemy");
            position = new Vector2(rnd.Next(50, 700), rnd.Next(50, 420));
        }

        protected void TurnTowardsPlayer()
        {
            
        }

        public override void Update()
        {
            TurnTowardsPlayer();

            base.Update();
        }
    }
}
