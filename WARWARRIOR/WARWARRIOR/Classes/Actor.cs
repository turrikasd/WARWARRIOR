﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WARWARRIOR
{
    class Actor
    {
        public static List<Actor> actors;

        public Texture2D texture;
        public Texture2D shieldTexture;
        public Vector2 position;
        public float angle;
        public float velocity;
        public float HP;
        public Color color;

        public Vector2 Origin
        {
            get
            {
                return new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2);
            }
        }

        public Vector2 RelOrigin
        {
            get
            {
                return new Vector2(texture.Width / 2, texture.Height / 2);
            }
        }

        public Vector2 RelShieldOrigin
        {
            get
            {
                return new Vector2(shieldTexture.Width / 2, shieldTexture.Height / 2);
            }
        }

        static Actor()
        {
            actors = new List<Actor>();
        }

        public Actor()
        {
            actors.Add(this);
            HP = 100;
            color = Color.White;

            if (!(this is Particle))
                shieldTexture = Game1.contentRef.Load<Texture2D>(@"Textures/Shield");
        }

        public virtual void Move(float amount)
        {
            velocity += amount;
        }

        public virtual void Turn(float amount)
        {
            angle += amount;
        }

        public virtual void ClampMove()
        {
            if (velocity > 1.0f)
                velocity -= 0.25f;
            else if (velocity < -1.0f)
                velocity += 0.25f;
            else
                velocity = 0;
        }

        protected void NormalizeVelocity()
        {
            if (velocity > 5.0f)
                velocity = 5.0f;
            else if (velocity < -5.0f)
                velocity = -5.0f;
        }

        protected virtual void CalculateMovement()
        {
            bool doesCollide = false;

            Vector2 newPos = position + new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * velocity;
            Vector2 newPosX = position + new Vector2((float)Math.Cos(angle), 0.0f) * velocity;
            Vector2 newPosY = position + new Vector2(0.0f, (float)Math.Sin(angle)) * velocity;

            CheckFinalCollision(newPos, newPosX, newPosY);
        }

        protected virtual void CheckFinalCollision(Vector2 newPos, Vector2 newPosX, Vector2 newPosY)
        {
            if (newPos.X > 0 + texture.Width / 2 && newPos.X < Game1.Width - texture.Width / 2 &&
                newPos.Y > 0 + texture.Height / 2 && newPos.Y < Game1.Height - texture.Height / 2)
                position += new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * velocity;

            else if (newPosX.X > 0 + texture.Width / 2 && newPosX.X < Game1.Width - texture.Width / 2 &&
                     newPosX.Y > 0 + texture.Height / 2 && newPosX.Y < Game1.Height - texture.Height / 2)
                position += new Vector2((float)Math.Cos(angle), 0.0f) * velocity;

            else if (newPosY.X > 0 + texture.Width / 2 && newPosY.X < Game1.Width - texture.Width / 2 &&
                     newPosY.Y > 0 + texture.Height / 2 && newPosY.Y < Game1.Height - texture.Height / 2)
                position += new Vector2(0.0f, (float)Math.Sin(angle)) * velocity;
        }

        protected virtual void CheckIfDead()
        {
            if (HP <= 0)
                actors.Remove(this);
        }

        public virtual void Update()
        {
            CheckIfDead();

            if (!(this is Particle))
            {
                NormalizeVelocity();
            }

            CalculateMovement();
        }

        public virtual void Fire()
        {
            new Projectile(this);
        }

        public virtual void Draw(SpriteBatch SB)
        {
            SB.Draw(texture, position, null, color, angle + 1.6f, RelOrigin, 1.0f, SpriteEffects.None, 0.5f);

            if (!(this is Particle) && HP > 25)
            {
                SB.Draw(shieldTexture, position, null, color, angle + 1.6f, RelShieldOrigin, HP / 75.0f, SpriteEffects.None, 0.6f);
            }
        }
    }
}
