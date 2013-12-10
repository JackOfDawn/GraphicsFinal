using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MissileDrizzle.Actors;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MissileDrizzle.Manager;
using MissileDrizzle.Particles;
using MissileDrizzle.Parallax;


namespace MissileDrizzle.Screen
{

    class GameScreen : ScreenState
    {
        //Camera system
        Camera 
            mCam;

        //Inputmanagers for the players
        InputManager
            mInputManagerPOne,
            mInputManagerPTwo;

        //Other managers
        ParticleManager
            mParticleManager;
        BallManager
            mBallManager;

        //Player list
        public List<Player>
            mPlayers { get; private set; }

        //Game Layers
        float[]
            mZoomLevels;
        const float
            FIELD_WIDTH = 1000.0f,
            LAYER1_MAX = 0.0f,
            LAYER2_MAX = 0.3f,
            LAYER3_MAX = 0.5f;

        //Stands
        Sprite
            mBackground;

        //Clouds
        Background
            mBG;
        
        //Gameplay Area
        Sprite
            mCourt;

        //ForeGround

        //float count = 1.0f;

        //SpriteFont FONT;

        public GameScreen(EventHandler TheScreenEvent, GraphicsDevice pGraphics)
            : base(TheScreenEvent, pGraphics)
        {
            mPlayers = new List<Player>();
            mPlayers.Add(new Player(true));
            mPlayers.Add(new Player(false));

            mBG = new Background();
            mBackground = new Sprite();
            mCourt = new Sprite();
            mInputManagerPOne = new InputManager(PlayerIndex.One);
            mInputManagerPTwo = new InputManager(PlayerIndex.Two);
            mParticleManager = new ParticleManager();
            mBallManager = new BallManager(mPlayers);
            mZoomLevels = new float[3];

            
        }

        public override void init(ContentManager content)
        {
            for (int i = 0; i < 3; i++)
            {
                mZoomLevels[i] = 1.0f;
            }

            mBG.init(content);
            mCam = new Camera();
            foreach (Player p in mPlayers)
            {
                p.init(content);
            }
            mPlayers[(int)PlayerIndex.One].mPosition = new Vector2(300, 0);
            mPlayers[(int)PlayerIndex.Two].mPosition = new Vector2(980, 0);

            mParticleManager.loadContent(content);
            mBallManager.loadBall(content);

            //FONT = content.Load<SpriteFont>("SpriteFont1");

            Texture2D tmpTexture = content.Load<Texture2D>("FPO/BackGround");
            mBackground.createSprite(tmpTexture, new Rectangle(0, 0, 1280, 720));
            //mBackground.updateScale(2.0f);
            tmpTexture = content.Load<Texture2D>("FPO/PlayGround");
            mCourt.createSprite(tmpTexture, new Rectangle(0, 0, 1280, 720));
            
            attachListeners();
           
        }

        public void attachListeners()
        {
            //Player one
            mInputManagerPOne.event_directionLeftStickPressed += new InputManager.directionPressDelegate(mPlayers[(int)PlayerIndex.One].playerMovedHandler);
            mInputManagerPOne.event_directionPressed += new InputManager.directionPressDelegate(mPlayers[(int)PlayerIndex.One].playerMovedHandler);
            mInputManagerPOne.event_directionPressed += new InputManager.directionPressDelegate(mPlayers[(int)PlayerIndex.One].mCannon.cannonRotationHandler);
            mInputManagerPOne.event_directionLeftStickPressed += new InputManager.directionPressDelegate(mPlayers[(int)PlayerIndex.One].mCannon.cannonRotationHandler);
            mInputManagerPOne.event_actionPressed += new InputManager.buttonPressDelegate(mBallManager.playerOneShotHandler);
            mInputManagerPOne.event_backPressed += new InputManager.buttonPressDelegate(mPlayers[(int)PlayerIndex.One].playerJumpHandler);

            //Player Two
            mInputManagerPTwo.event_directionLeftStickPressed += new InputManager.directionPressDelegate(mPlayers[(int)PlayerIndex.Two].playerMovedHandler);
            mInputManagerPTwo.event_directionLeftStickPressed += new InputManager.directionPressDelegate(mPlayers[(int)PlayerIndex.Two].mCannon.cannonRotationHandler);
            mInputManagerPTwo.event_directionPressed += new InputManager.directionPressDelegate(mPlayers[(int)PlayerIndex.Two].playerMovedHandler);
            mInputManagerPTwo.event_directionPressed += new InputManager.directionPressDelegate(mPlayers[(int)PlayerIndex.Two].mCannon.cannonRotationHandler);
            mInputManagerPTwo.event_actionPressed += new InputManager.buttonPressDelegate(mBallManager.playerTwoShotHandler);
            mInputManagerPTwo.event_backPressed += new InputManager.buttonPressDelegate(mPlayers[(int)PlayerIndex.Two].playerJumpHandler);
            //mInputManagerPTwo.event_directionPressed += new InputManager.directionPressDelegate(mPlayerTwo.mCannon.cannonRotationHandler);

            
            
        }

        public override void  update(GameTime pGameTime)
        {
            calculateCameraZoom();

            if (Keyboard.GetState().IsKeyDown(Keys.B) == true)
            {
                mParticleManager.addEffect(EffectType.explosion, new Vector2(500, 500));
            } 
            

            mInputManagerPOne.update(pGameTime);
            mInputManagerPTwo.update(pGameTime);

            foreach (Player p in mPlayers)
            {
                p.update(pGameTime);
            }

            
            //OtherSystems
            mParticleManager.update(pGameTime);
            mBallManager.update(pGameTime);
            mBG.update(pGameTime);
           
            //mCam.Zoom = count;
            //mCam.Pos = new Vector2(1, 5);
            
        }

        private void calculateCameraZoom()
        {
            float distance = Math.Abs(mPlayers[0].mPosition.X - mPlayers[1].mPosition.X);
            float midpoint = (mPlayers[0].mPosition.X + mPlayers[1].mPosition.X) / 2;
            float offset =  midpoint - 640;
            float percentage = 1 - (distance / FIELD_WIDTH);

            mZoomLevels[0] = 1.0f + (LAYER1_MAX * percentage);
            mZoomLevels[1] = 1.0f + (LAYER2_MAX * percentage);
            mZoomLevels[2] = 1.0f + (LAYER3_MAX * percentage);

            mCam.ZoomOffset = new Vector2(offset, 300);
            
        }

        public override void draw(SpriteBatch pSpriteBatch)
        {

            //Background Layer
            mCam.Zoom = mZoomLevels[0];
            pSpriteBatch.Begin(SpriteSortMode.Immediate,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                mCam.getTransformation(Vector2.Zero));
            mBG.draw(pSpriteBatch);
            pSpriteBatch.End();


            //Mid Layer
            mCam.Zoom = mZoomLevels[1];
            pSpriteBatch.Begin(SpriteSortMode.Immediate,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                mCam.getTransformation(Vector2.Zero));
            mBackground.drawZeroOrigin(pSpriteBatch, SpriteEffects.None);
           
            pSpriteBatch.End();


            //Court Level
            mCam.Zoom = mZoomLevels[2];
            pSpriteBatch.Begin(SpriteSortMode.Immediate,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                mCam.getTransformation(Vector2.Zero));
            mCourt.drawZeroOrigin(pSpriteBatch, SpriteEffects.None);

            mBallManager.draw(pSpriteBatch);


            foreach (Player p in mPlayers)
            {
                p.draw(pSpriteBatch);
            }

            mParticleManager.draw(pSpriteBatch);

            /*
            string tmpString = "MAX PARTICLE COUNT " + mParticleManager.ParticleCount;
            pSpriteBatch.DrawString(FONT, tmpString, new Vector2(0,0), Color.Black);

            tmpString = "ALIVE PARTICLE COUNT " + mParticleManager.ParticleCountAlive;
            pSpriteBatch.DrawString(FONT, tmpString, new Vector2(0, 50), Color.Black);
            */
            pSpriteBatch.End();

            
        }

    }
}
