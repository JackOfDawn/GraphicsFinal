using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MissileDrizzle.Manager
{
    public enum Direction
    {
        up = 0,
        down,
        left,
        right

    }

    class InputManager
    {

        PlayerIndex thisPlayer;

        //Defines Signatures for events
        public delegate void buttonPressDelegate();
        public delegate void directionPressDelegate(Vector2 value);
        public delegate void menuDirectionPressDelegate(bool[] direction);

        //Defining events
        public event buttonPressDelegate event_actionPressed;
        public event buttonPressDelegate event_backPressed;
        public event buttonPressDelegate event_startPressed;
        public event directionPressDelegate event_directionPressed;
        public event directionPressDelegate event_directionLeftStickPressed;
        public event directionPressDelegate event_directionRightStickPressed;
        public event menuDirectionPressDelegate event_menuDirectionPressed;

        KeyboardState lastKeyBoardState;
        GamePadState lastGamePadState; 

        float keyBoardSensitivity = 2.0f;
        float dPadSensitivity = 0.2f;
        const float DEAD_ZONE = 0.25f;

        bool actionPressed = false;
        bool backPressed = false;
        bool startPressed = false;

        Vector2 directionPressed = Vector2.Zero;
        Vector2 directionPressedLeftStick = Vector2.Zero; 
        Vector2 directionPressedRightStick = Vector2.Zero;

        //Keys Down;
        bool backKeyDown;
        bool actionKeyDown;

        //Buttons down
        bool aButtonDown;
        bool bButtonDown;



        bool[] menuDirectionPressed; 

        public InputManager(PlayerIndex pPlayerIndex)
        {
            menuDirectionPressed = new bool[4] { false, false, false, false };
            actionPressed = false;
            backPressed = false;
            startPressed = false;

            //Keys
            backKeyDown = false;
            actionKeyDown = false;

            //Buttons
            aButtonDown = true;
            bButtonDown = false;

            thisPlayer = pPlayerIndex;
        }

        public void update(GameTime pGameTime)
        {
            backPressed = false;
            startPressed = false;
            actionPressed = false;
            directionPressed = Vector2.Zero;

            for(int i = 0; i < 4; i++)
            {
                menuDirectionPressed[i] = false;
            }

            
            #region Keyboard

            //Debounced input
            #region PLAYER_ONE
            if (thisPlayer == PlayerIndex.One)
            {
                if (lastKeyBoardState != Keyboard.GetState())
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.OemComma) && !backKeyDown) // JumpKey
                    {
                        backPressed = true;
                        backKeyDown = true;
                    }
                    else if (!Keyboard.GetState().IsKeyDown(Keys.OemComma) && backKeyDown)
                    {
                        backKeyDown = false;
                    }



                    if (Keyboard.GetState().IsKeyDown(Keys.OemQuestion) && !actionKeyDown) //ShootKey
                    {
                        actionPressed = true;
                        actionKeyDown = true;
                    }
                    else if (!Keyboard.GetState().IsKeyDown(Keys.OemQuestion) && actionKeyDown)
                    {
                        actionKeyDown = false;
                    }


                    //MENU DIRECTIONS
                    if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    {
                        menuDirectionPressed[(int)Direction.up] = true;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    {
                        menuDirectionPressed[(int)Direction.down] = true;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    {
                        menuDirectionPressed[(int)Direction.right] = true;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    {
                        menuDirectionPressed[(int)Direction.left] = true;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        startPressed = true;
                    }
                }

                //Constant Keyboard input

                //ArrowKeys
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    directionPressed.X -= keyBoardSensitivity;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    directionPressed.X += keyBoardSensitivity;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    directionPressed.Y -= keyBoardSensitivity;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    directionPressed.Y += keyBoardSensitivity;
                }

                lastKeyBoardState = Keyboard.GetState();
            #endregion
            #region PLAYER_TWO
            }
            else if (thisPlayer == PlayerIndex.Two)
            {
                if (lastKeyBoardState != Keyboard.GetState())
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.F) && !backKeyDown) // JumpKey
                    {
                        backPressed = true;
                        backKeyDown = true;
                    }
                    else if (!Keyboard.GetState().IsKeyDown(Keys.F) && backKeyDown)
                    {
                        backKeyDown = false;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.G) && !actionKeyDown) //ShootKey
                    {
                        actionPressed = true;
                        actionKeyDown = true;
                    }
                    else if (!Keyboard.GetState().IsKeyDown(Keys.G) && actionKeyDown)
                    {
                        actionKeyDown = false;
                    }


                    //MENU DIRECTIONS
                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        menuDirectionPressed[(int)Direction.up] = true;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.S))
                    {
                        menuDirectionPressed[(int)Direction.down] = true;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        menuDirectionPressed[(int)Direction.right] = true;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        menuDirectionPressed[(int)Direction.left] = true;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        startPressed = true;
                    }
                }

                //Constant Keyboard input

                //ArrowKeys
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    directionPressed.X -= keyBoardSensitivity;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    directionPressed.X += keyBoardSensitivity;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    directionPressed.Y -= keyBoardSensitivity;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    directionPressed.Y += keyBoardSensitivity;
                }

                lastKeyBoardState = Keyboard.GetState();
            }
            #endregion
            #endregion

            #region Gampad Input

            //Debounced
            if (GamePad.GetState(thisPlayer) != lastGamePadState)
            {
                if (GamePad.GetState(thisPlayer).Buttons.B == ButtonState.Pressed && !bButtonDown) //jump?
                {
                    backPressed = true;
                    bButtonDown = true;
                }
                else if (GamePad.GetState(thisPlayer).Buttons.B != ButtonState.Pressed && bButtonDown)
                {
                    bButtonDown = false;
                }

                if (GamePad.GetState(thisPlayer).Buttons.A == ButtonState.Pressed && !aButtonDown)
                {
                    actionPressed = true;
                    aButtonDown = true;
                }
                else if (!(GamePad.GetState(thisPlayer).Buttons.A == ButtonState.Pressed) && aButtonDown)
                {
                    aButtonDown = false;
                }

                if (GamePad.GetState(thisPlayer).Buttons.Start == ButtonState.Pressed)
                {
                    startPressed = true;
                }

                //Directions
                if (GamePad.GetState(thisPlayer).DPad.Up == ButtonState.Pressed)
                {
                    menuDirectionPressed[(int)Direction.up] = true;
                }
                if (GamePad.GetState(thisPlayer).DPad.Down == ButtonState.Pressed)
                {
                    menuDirectionPressed[(int)Direction.down] = true;
                }
                if (GamePad.GetState(thisPlayer).DPad.Right == ButtonState.Pressed)
                {
                    menuDirectionPressed[(int)Direction.right] = true;
                }
                if (GamePad.GetState(thisPlayer).DPad.Down == ButtonState.Pressed)
                {
                    menuDirectionPressed[(int)Direction.down] = true;
                }

            }

            //Constant
            if (GamePad.GetState(thisPlayer).DPad.Left == ButtonState.Pressed)
                directionPressed.X -= dPadSensitivity;
            if (GamePad.GetState(thisPlayer).DPad.Right == ButtonState.Pressed)
                directionPressed.X += dPadSensitivity;
            if (GamePad.GetState(thisPlayer).DPad.Up == ButtonState.Pressed)
                directionPressed.Y -= dPadSensitivity;
            if (GamePad.GetState(thisPlayer).DPad.Down == ButtonState.Pressed)
                directionPressed.Y += dPadSensitivity;

            //Thumbsticks
            directionPressedLeftStick += GamePad.GetState(thisPlayer).ThumbSticks.Left;
            if (directionPressedLeftStick.X < DEAD_ZONE && directionPressedLeftStick.X > -DEAD_ZONE)
                directionPressedLeftStick.X = 0.0f;
            if (directionPressedLeftStick.Y < DEAD_ZONE && directionPressedLeftStick.Y > -DEAD_ZONE)
                directionPressedLeftStick.Y = 0.0f;
            //directionPressedLeftStick.Y = 0.0f;

            directionPressedRightStick += GamePad.GetState(thisPlayer).ThumbSticks.Right;
            if (directionPressedRightStick.Length() < DEAD_ZONE)
                directionPressedRightStick = Vector2.Zero;

            lastGamePadState = GamePad.GetState(thisPlayer);
            #endregion

            #region InputEvents

            //Call Events
            if (backPressed && event_backPressed != null)
                event_backPressed();

            if (actionPressed && event_actionPressed != null)
                event_actionPressed();

            if (startPressed && event_startPressed != null)
                event_startPressed();

            if ((menuDirectionPressed[(int)Direction.down] || menuDirectionPressed[(int)Direction.up]
                || menuDirectionPressed[(int)Direction.left] || menuDirectionPressed[(int)Direction.right]) && event_menuDirectionPressed != null)
            {
                event_menuDirectionPressed(menuDirectionPressed);
            }

            if (directionPressed != Vector2.Zero && event_directionPressed != null)
            {
                event_directionPressed(directionPressed);
            }

            if (directionPressedLeftStick != Vector2.Zero && event_directionLeftStickPressed != null)
            {
                event_directionLeftStickPressed(directionPressedLeftStick);
            }

            if (directionPressedLeftStick != Vector2.Zero && event_directionRightStickPressed != null)
            {
                event_directionRightStickPressed(directionPressedRightStick);
            }

            directionPressed = Vector2.Zero;
            directionPressedLeftStick = Vector2.Zero;
            directionPressedRightStick = Vector2.Zero;


            #endregion
        }
    }
}
