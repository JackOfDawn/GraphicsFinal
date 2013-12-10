using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MissileDrizzle.Parallax;
using MissileDrizzle.Manager;


namespace MissileDrizzle.Screen
{
    enum MenuSelections
    {
        Play = 0,
        Controls,
        Language,
        Credits,
        Quit
    };

    class MainMenuScreen : ScreenState
    {
        Background
            mBG;
        Texture2D
            mCourts;
        Sprite
            mCursor;
        Vector2[]
            mSelectionLocations;
        Vector2
            mCursorLocation;
        int
            mSelection;
        InputManager
            mMenuManager;
        

        
        public MainMenuScreen(EventHandler TheScreenEvent, GraphicsDevice graphics)
            : base(TheScreenEvent, graphics)
        {
            mBG = new Background();
            mCursor = new Sprite();
            mSelectionLocations = new Vector2[5];
            mSelectionLocations[0] = new Vector2(515, 145);
            mSelectionLocations[1] = new Vector2(515, 165);
            mSelectionLocations[2] = new Vector2(515, 185);
            mSelectionLocations[3] = new Vector2(515, 205);
            mSelectionLocations[4] = new Vector2(515, 225);
        }

        public override void init(ContentManager content)
        {
            Texture2D temp;
            temp = content.Load<Texture2D>("FPO/Cursor");

            mMenuManager = new InputManager(mMenuPlayer);

            addListeners();

            mBG.init(content);
            mCursor.createSprite(temp, new Rectangle(0,0,temp.Width, temp.Height));
            mCourts = content.Load<Texture2D>("FPO/MenuScreen");

            mSelection = 0;
            mCursorLocation = new Vector2(mSelectionLocations[mSelection].X - 18, mSelectionLocations[mSelection].Y);
        }

        private void addListeners()
        {
            mMenuManager.event_menuDirectionPressed += new InputManager.menuDirectionPressDelegate(menuNavigation);
            mMenuManager.event_actionPressed += new InputManager.buttonPressDelegate(menuAction);
        }

        private void menuNavigation(bool[] direction)
        {
            if (direction[(int)Direction.up])
            {
                mSelection--;
                if (mSelection < 0)
                    mSelection = 0;
            }
            if (direction[(int)Direction.down])
            {
                mSelection++;
                if (mSelection > 4)
                    mSelection = 4;
            }

            mCursorLocation = new Vector2(mSelectionLocations[mSelection].X - 18, mSelectionLocations[mSelection].Y);
        }

        private void menuAction()
        {
            switch (mSelection)
            {
                case (int)MenuSelections.Play:
                    mMenuManager.event_menuDirectionPressed -= new InputManager.menuDirectionPressDelegate(menuNavigation);
                    mMenuManager.event_actionPressed -= new InputManager.buttonPressDelegate(menuAction);
                    eScreenEvent.Invoke(this, new EventArgs());
                    break;

                case (int)MenuSelections.Controls:
                    break;

                case (int)MenuSelections.Language:
                    if (mCurrentLanguage == Languages.English)
                        mCurrentLanguage = Languages.German;
                    else
                        mCurrentLanguage = Languages.English;
                    break;

                case (int)MenuSelections.Credits:
                    break;

                case (int)MenuSelections.Quit:
                    isOver = true;
                    break;
            }
        }

        public override void update(GameTime pGameTime)
        {
            mMenuManager.update(pGameTime);
            mBG.update(pGameTime);
            mCursor.updatePos(mCursorLocation);
           
            
        }

        public override void draw(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Begin();
            mBG.draw(pSpriteBatch);
            pSpriteBatch.Draw(mCourts, Vector2.Zero, Color.White);

            //Main Menu
            pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].menu_Play, mSelectionLocations[0], Color.White);
            pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].menu_Controls, mSelectionLocations[1], Color.White);
            pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].menu_Language, mSelectionLocations[2], Color.White);
            pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].menu_Credits, mSelectionLocations[3], Color.White);
            pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].menu_Quit, mSelectionLocations[4], Color.White);
            mCursor.drawZeroOrigin(pSpriteBatch, SpriteEffects.None);

            //Credits


            //Controls


            pSpriteBatch.End();
        }
    }
}
