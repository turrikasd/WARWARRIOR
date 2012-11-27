using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WARWARRIOR
{
    class Player : Actor
    {
        public static Player player;

        public Player() : base()
        {
            texture = Game1.contentRef.Load<Texture2D>(@"Textures/Player");
            position = new Vector2(250, 250);
            player = this;
        }

        protected override void CheckIfDead()
        {
            if (HP <= 0)
            {
                Game1.ResetGame();
            }
        }
    }
}
