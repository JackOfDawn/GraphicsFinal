using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MissileDrizzle.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;


namespace MissileDrizzle.Manager
{
    public enum SCREEN_STATES
    {
        SPLASH_SCREEN = 0,
        MENU_SCREEN,
        GAME_SCREEN,
        PAUSE_SCREEN
    } 

    class StateManager
    {
        InputManager mInputManagerPlayerOne;

        //Defining Signatures
        //public delegate void changeScreenDelegate(SCREEN_STATES newState);

        //Defining Event
        //public event changeScreenDelegate event_ChangeScreen;
        int
            screenHeight = 720,
            screenWidth = 1280;
        public bool
            isOver { get; private set; }

        //Screens
        public SCREEN_STATES
            mCurrentState,
            mNextState;
        ScreenState
            mCurrentScreen;
        SplashScreen
            mSplashScreen;
        GameScreen
            mGameScreen;
        MainMenuScreen
            mMenuScreen;

        //Buffers and things
        ContentManager
            mContent;
        GraphicsDevice
            mGraphics;
        RenderTargetBinding[]
            tempBinding;
        RenderTarget2D
            tempRenderTargetOne,
            tempRenderTargetTwo;

        //effects
        Effect
            noiseEffect,
            SepiaEffect;
        EffectParameter
            noiseFilter,
            blackLine;
        int 
            noise;
        Texture2D[] TempNoise;
        Random rand;


        float
            timer1,
            timer2;
        const float
            MAX_TIME_ONE = 100,
            MAX_TIME_TWO = 100;

        public bool
            changeScreen { get; private set; }

        public StateManager(ContentManager content, GraphicsDevice pGraphics)
        {

            ///centerCoordParam = blurEffect.Parameters["fCenter"];

            mCurrentState = mNextState = SCREEN_STATES.SPLASH_SCREEN;
            changeScreen = false;
            mContent = content;
            mGraphics = pGraphics;

            mInputManagerPlayerOne = new InputManager(PlayerIndex.One);
            //intantiate screens
            
            mSplashScreen = new SplashScreen(new EventHandler(SplashScreenEvent), pGraphics);
            mGameScreen = new GameScreen(new EventHandler(GameScreenEvent), pGraphics);
            mMenuScreen = new MainMenuScreen(new EventHandler(MenuScreenEvent), pGraphics);


            //mCurrentScreen = mGameScreen
            
            tempRenderTargetOne = new RenderTarget2D(mGraphics, screenWidth, screenHeight);
            tempRenderTargetTwo = new RenderTarget2D(mGraphics, screenWidth, screenHeight);
            tempBinding = mGraphics.GetRenderTargets();
            mGraphics.SetRenderTarget(tempRenderTargetOne);

            //effects
            noiseEffect = content.Load<Effect>("Noise");
            SepiaEffect = content.Load<Effect>("Sepia");

            noiseFilter = noiseEffect.Parameters["noisefilter"];
            blackLine = noiseEffect.Parameters["blkLine"];

            TempNoise = new Texture2D[3];
            TempNoise[0] = content.Load<Texture2D>("NOISE/noise1");
            TempNoise[1] = content.Load<Texture2D>("NOISE/noise2");
            TempNoise[2] = content.Load<Texture2D>("NOISE/noise3");
            rand = new Random();
            noise = 0;
            

        }

        public void initScreens()
        {
            mSplashScreen.init(mContent);
            //mGameScreen.init(mContent);
            mMenuScreen.init(mContent);
            
        }

        public void update(GameTime pGameTime)
        {

            switch(mCurrentState)
            {
                case SCREEN_STATES.SPLASH_SCREEN:
                    mCurrentScreen = mSplashScreen;
                    break;
                case SCREEN_STATES.GAME_SCREEN:
                    mCurrentScreen = mGameScreen;
                    break;
                case SCREEN_STATES.MENU_SCREEN:
                    mCurrentScreen = mMenuScreen;
                    break;
            }

            mCurrentScreen.update(pGameTime);
            isOver = mCurrentScreen.returnIsOver();


            timer1 = timer1 - pGameTime.ElapsedGameTime.Milliseconds;
            timer2 = timer2 - pGameTime.ElapsedGameTime.Milliseconds;
            if (timer1 < 0)
            {
                noise = (noise + 1) % 3;
                noiseFilter.SetValue(TempNoise[noise]);
                timer1 = MAX_TIME_ONE;
            }
            blackLine.SetValue(rand.Next(0, 100) / 100.0f);
            
            
        }

        public void draw(SpriteBatch pSpriteBatch)
        {


            //Set to first back buffer
            mGraphics.SetRenderTarget(tempRenderTargetOne);
           
            mCurrentScreen.draw(pSpriteBatch);

            //Gather buffers?
            mGraphics.SetRenderTargets(tempBinding);

            //Set to second back buffer
            mGraphics.SetRenderTarget(tempRenderTargetTwo);

            pSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            //Apply first Effect
            if (mCurrentState != SCREEN_STATES.GAME_SCREEN)
                noiseEffect.CurrentTechnique.Passes[0].Apply();
            //Draw first buffer Texture to second buffer
            pSpriteBatch.Draw(tempRenderTargetOne, Vector2.Zero, Color.White);
            pSpriteBatch.End();

            //Gather it up?
            mGraphics.SetRenderTargets(tempBinding);


            pSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            //Apply second Effect

            if(mCurrentState != SCREEN_STATES.GAME_SCREEN)
                SepiaEffect.CurrentTechnique.Passes[0].Apply();

            //Draw Second buffer to first buffer
            pSpriteBatch.Draw(tempRenderTargetTwo, Vector2.Zero, Color.White);  //WHY U NO WORK?
            pSpriteBatch.End();

            //MAGIC!
            
        }
        
        //Screen Events

        public void SplashScreenEvent( object obj, EventArgs e)
        {
            mCurrentState = SCREEN_STATES.MENU_SCREEN;
        }

        public void MenuScreenEvent(object obj, EventArgs e)
        {
            mGameScreen.init(mContent);
            mCurrentState = SCREEN_STATES.GAME_SCREEN;
        }

        public void GameScreenEvent(object obj, EventArgs e)
        {
            mMenuScreen.init(mContent);
            mCurrentState = SCREEN_STATES.MENU_SCREEN;
        }

    }
}
