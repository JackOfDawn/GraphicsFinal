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

        public SCREEN_STATES
            mCurrentState,
            mNextState;
        ScreenState
            mCurrentScreen;
        SplashScreen
            mSplashScreen;
        GameScreen
            mGameScreen;

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
            blurEffect,
            SepiaEffect,
            ReflectionEffect;

        public bool
            changeScreen { get; private set; }

        public StateManager(ContentManager content, GraphicsDevice pGraphics)
        {

            ///centerCoordParam = blurEffect.Parameters["fCenter"];

            mCurrentState = mNextState = SCREEN_STATES.GAME_SCREEN;
            changeScreen = false;
            mContent = content;
            mGraphics = pGraphics;

            mInputManagerPlayerOne = new InputManager(PlayerIndex.One);
            //intantiate screens
            
            mSplashScreen = new SplashScreen(new EventHandler(SplashScreenEvent), pGraphics);
            mGameScreen = new GameScreen(new EventHandler(GameScreenEvent), pGraphics);

            //mCurrentScreen = mGameScreen
            
            tempRenderTargetOne = new RenderTarget2D(mGraphics, screenWidth, screenHeight);
            tempRenderTargetTwo = new RenderTarget2D(mGraphics, screenWidth, screenHeight);
            tempBinding = mGraphics.GetRenderTargets();
            mGraphics.SetRenderTarget(tempRenderTargetOne);

            //effects
            blurEffect = content.Load<Effect>("Blur");
            SepiaEffect = content.Load<Effect>("Sepia");
            ReflectionEffect = content.Load<Effect>("Reflection");
            

        }

        public void initScreens()
        {
            mSplashScreen.init(mContent);
            mGameScreen.init(mContent);
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
            }

            mCurrentScreen.update(pGameTime);


            
            
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
            //blurEffect.CurrentTechnique.Passes[0].Apply();
            //Draw first buffer Texture to second buffer
            pSpriteBatch.Draw(tempRenderTargetOne, Vector2.Zero, Color.White);
            pSpriteBatch.End();

            //Gather it up?
            mGraphics.SetRenderTargets(tempBinding);


            pSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            //Apply second Effect
            //SepiaEffect.CurrentTechnique.Passes[0].Apply();

            //Draw Second buffer to first buffer
            pSpriteBatch.Draw(tempRenderTargetTwo, Vector2.Zero, Color.White);  //WHY U NO WORK?
            pSpriteBatch.End();

            //MAGIC!
            
        }
        
        public void SplashScreenEvent( object obj, EventArgs e)
        {
            mCurrentState = SCREEN_STATES.GAME_SCREEN;
        }

        public void GameScreenEvent(object obj, EventArgs e)
        {

        }

    }
}
