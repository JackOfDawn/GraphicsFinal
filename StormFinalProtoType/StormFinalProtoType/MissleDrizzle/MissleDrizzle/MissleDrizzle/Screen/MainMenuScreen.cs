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
    enum MENU_SELECTIONS
    {
        PLAY = 0,
        CONTROLS,
        LANGUAGE,
        CREDITS,
        QUIT
    };

    

    class MainMenuScreen : ScreenState
    {
        Background
            mBG;
        Texture2D
            mCourts;
            
        

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
            showTime,
            coolDown = 500;
        const float
            MAX_CREDIT_TIME = 1500;
        
        

        
        public MainMenuScreen(EventHandler TheScreenEvent, GraphicsDevice graphics)
            : base(TheScreenEvent, graphics)
        {
            mBG = new Background();
            mCursor = new Sprite();
            
        }

        public override void init(ContentManager content)
        {
            Texture2D temp;
            temp = content.Load<Texture2D>("FPO/Cursor");

            mMenuManager = new InputManager(mMenuPlayer);

            coolDown = 500;

            addListeners();

            mBG.init(content);
            mCursor.createSprite(temp, new Rectangle(0,0,temp.Width, temp.Height));
            mCourts = content.Load<Texture2D>("FPO/MenuScreen");
            mControls = content.Load<Texture2D>("FPO/Controls");

            mSelection = 0;
            mCursorLocation = new Vector2(mSelectionLocations[mSelection].X - 18, mSelectionLocations[mSelection].Y);

            showTime = MAX_CONTROL_TIME;
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
                case (int)MENU_SELECTIONS.PLAY:
                    mMenuManager.event_menuDirectionPressed -= new InputManager.menuDirectionPressDelegate(menuNavigation);
                    mMenuManager.event_actionPressed -= new InputManager.buttonPressDelegate(menuAction);
                    eScreenEvent.Invoke(this, new EventArgs());
                    break;

                case (int)MENU_SELECTIONS.CONTROLS:
                    show = SHOW.CONTROLS;
                    showTime = MAX_CONTROL_TIME;
                    break;

                case (int)MENU_SELECTIONS.LANGUAGE:
                    if (mCurrentLanguage == Languages.English)
                        mCurrentLanguage = Languages.German;
                    else
                        mCurrentLanguage = Languages.English;
                    break;

                case (int)MENU_SELECTIONS.CREDITS:
                    show = SHOW.CREDITS;
                    showTime = MAX_CREDIT_TIME;
                    break;

                case (int)MENU_SELECTIONS.QUIT:
                    isOver = true;
                    break;
            }
        }

        public override void update(GameTime pGameTime)
        {
            
            mBG.update(pGameTime);

            if (coolDown > 0)
            {
                coolDown = coolDown - pGameTime.ElapsedGameTime.Milliseconds;
            }
            else
            {
                switch (show)
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
                            //showTime = MAX_CONTROL_TIME;
                        }
                        break;
                    case SHOW.CREDITS:

                        showTime = showTime - pGameTime.ElapsedGameTime.Milliseconds;
                        if (showTime < 0)
                        {
                            show = SHOW.MENU;
                            //showTime = MAX_CONTROL_TIME;
                        }
                        break;
                }
            }
           
            
        }

        public override void draw(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Begin();
            mBG.draw(pSpriteBatch);
            pSpriteBatch.Draw(mCourts, Vector2.Zero, Color.White);

           
            switch (show)
            {
                //Main Menu
                case SHOW.MENU:
                    pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].menu_Play, mSelectionLocations[0], Color.White);
                    pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].menu_Controls, mSelectionLocations[1], Color.White);
                    pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].menu_Language, mSelectionLocations[2], Color.White);
                    pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].menu_Credits, mSelectionLocations[3], Color.White);
                    pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].menu_Quit, mSelectionLocations[4], Color.White);
                    mCursor.drawZeroOrigin(pSpriteBatch, SpriteEffects.None);
                    break;

                //Controls
                case SHOW.CONTROLS:
                    pSpriteBatch.Draw(mControls, SCREEN_POS, Color.White);
                    pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].controls_Stick, mControlLocations[0], Color.White);
                    pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].controls_Start, mControlLocations[1], Color.White);
                    pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].controls_Select, mControlLocations[2], Color.White);
                    pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].controls_A, mControlLocations[3], Color.White);
                    pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].controls_B, mControlLocations[4], Color.White);
                    break;

                //Credits
                case SHOW.CREDITS:
                    pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].credits_Programmer, mSelectionLocations[0], Color.White);
                    pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].credits_ProgrammerName, mSelectionLocations[1], Color.White);
                    pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].credits_Artist, mSelectionLocations[3], Color.White);
                    pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].credits_ArtistName, mSelectionLocations[4], Color.White);
                    break;
            }

            pSpriteBatch.End();
        }
    }
}
