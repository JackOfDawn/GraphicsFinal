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
            mCourts,
            mControls;
        Vector2
            SCREEN_POS = new Vector2(496, 130);

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

        enum SHOW
        {
            MENU = 0,
            CONTROLS,
            CREDITS
        };

        private SHOW
            show;
        float 
            showTime;
        const float 
            MAX_SHOW_TIME = 3000;
        
        

        
        public MainMenuScreen(EventHandler TheScreenEvent, GraphicsDevice graphics)
            : base(TheScreenEvent, graphics)
        {
            mBG = new Background();
            mCursor = new Sprite();
            mSelectionLocations = new Vector2[7];
            mSelectionLocations[0] = new Vector2(515, 145);
            mSelectionLocations[1] = new Vector2(515, 165);
            mSelectionLocations[2] = new Vector2(515, 185);
            mSelectionLocations[3] = new Vector2(515, 205);
            mSelectionLocations[4] = new Vector2(515, 225);
            mSelectionLocations[5] = new Vector2(515, 245);
            mSelectionLocations[6] = new Vector2(515, 265);
            //mSelectionLocations[7] = new Vector2(515, 285);
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
            mControls = content.Load<Texture2D>("FPO/Controls");

            mSelection = 0;
            mCursorLocation = new Vector2(mSelectionLocations[mSelection].X - 18, mSelectionLocations[mSelection].Y);

            showTime = MAX_SHOW_TIME;
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
                    show = SHOW.CONTROLS;
                    break;

                case (int)MenuSelections.Language:
                    if (mCurrentLanguage == Languages.English)
                        mCurrentLanguage = Languages.German;
                    else
                        mCurrentLanguage = Languages.English;
                    break;

                case (int)MenuSelections.Credits:
                    show = SHOW.CREDITS;
                    break;

                case (int)MenuSelections.Quit:
                    isOver = true;
                    break;
            }
        }

        public override void update(GameTime pGameTime)
        {
            
            mBG.update(pGameTime);
            

            switch(show)
            {
                case SHOW.MENU:
                    mMenuManager.update(pGameTime);
                    mCursor.updatePos(mCursorLocation);
                    break;

                case SHOW.CONTROLS:
                    showTime = showTime - pGameTime.ElapsedGameTime.Milliseconds;
                    if (showTime < 0)
                    {
                        show = SHOW.MENU;
                        showTime = MAX_SHOW_TIME;
                    }
                    break;
                case SHOW.CREDITS:

                    showTime = showTime - pGameTime.ElapsedGameTime.Milliseconds;
                    if (showTime < 0)
                    {
                        show = SHOW.MENU;
                        showTime = MAX_SHOW_TIME;
                    }
                    break;
            }
           
            
        }

        public override void draw(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Begin();
            mBG.draw(pSpriteBatch);
            pSpriteBatch.Draw(mCourts, Vector2.Zero, Color.White);

            //Main Menu
            switch (show)
            {
                case SHOW.MENU:
                    pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].menu_Play, mSelectionLocations[0], Color.White);
                    pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].menu_Controls, mSelectionLocations[1], Color.White);
                    pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].menu_Language, mSelectionLocations[2], Color.White);
                    pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].menu_Credits, mSelectionLocations[3], Color.White);
                    pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].menu_Quit, mSelectionLocations[4], Color.White);
                    mCursor.drawZeroOrigin(pSpriteBatch, SpriteEffects.None);
                    break;

                     
                case SHOW.CONTROLS:
                    pSpriteBatch.Draw(mControls, SCREEN_POS, Color.White);
                    break;


                case SHOW.CREDITS:
                    pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].credits_Programmer, mSelectionLocations[0], Color.White);
                    pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].credits_ProgrammerName, mSelectionLocations[1], Color.White);
                    pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].credits_Artist, mSelectionLocations[3], Color.White);
                    pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].credits_ArtistName, mSelectionLocations[4], Color.White);
                    break;
            }

            //Credits


            //Controls


            pSpriteBatch.End();
        }
    }
}
