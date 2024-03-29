﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace WARWARRIOR
{
    static class InputManager
    {
        static List<Keys> keysDown;

        static InputManager()
        {
            keysDown = new List<Keys>();
        }

        public static void HandleInput(GAMESTATE gameState)
        {
            for (int i = keysDown.Count() - 1; i >= 0; i--)
            {
                if (Keyboard.GetState().IsKeyUp(keysDown[i]))
                    keysDown.Remove(keysDown[i]);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !keysDown.Contains(Keys.Enter))
            {
                if (gameState == GAMESTATE.PLAYING)
                    Game1.gameState = GAMESTATE.PAUSED;
                else
                    Game1.gameState = GAMESTATE.PLAYING;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !keysDown.Contains(Keys.Space))
                Player.player.Fire();

            foreach (Keys k in Keyboard.GetState().GetPressedKeys())
            {
                keysDown.Add(k);
            }

            if (gameState == GAMESTATE.PLAYING)
                if (!Playing())
                    Game1.gameState = GAMESTATE.START_SCREEN;
        }

        static private bool Playing()
        {
            if (keysDown.Contains(Keys.Up))
                Player.player.Move(0.25f);
            else if (keysDown.Contains(Keys.Down))
                Player.player.Move(-0.25f);
            else
                Player.player.ClampMove();

            if (keysDown.Contains(Keys.Right))
                Player.player.Turn(0.075f);
            else if (keysDown.Contains(Keys.Left))
                Player.player.Turn(-0.075f);


            for (int i = Actor.actors.Count() - 1; i >= 0; i--)
            {
                if (Game1.gameState == GAMESTATE.START_SCREEN)
                    return false;

                if (i < Actor.actors.Count())
                    Actor.actors[i].Update();
            }

            return true;
        }
    }
}
