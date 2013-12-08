using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MissileDrizzle.Screen
{
    class SplashScreen : ScreenState
    {
        Texture2D
            mBackGround;
        private double
            mTimeElapsed;
        private const double
            MAX_TIME = 3000; //MILLISECONDS
        private SpriteFont Test;

        public SplashScreen(EventHandler TheScreenEvent, GraphicsDevice graphics)
            : base(TheScreenEvent, graphics)
        {
            
        }

        public override void init(ContentManager content)
        {
            mBackGround = content.Load<Texture2D>("FPO/SplashScreen");
            mTimeElapsed = MAX_TIME;
            Test = content.Load<SpriteFont>("SpriteFont1");
            loadLanguages(content);
            mCurrentLanguage = Languages.German;
            //Rectangle sourceRect = new Rectangle(0, 0, tempTexture.Width, tempTexture.Height);
            //mBackGround.createSprite(tempTexture, sourceRect);
        }

        public override void update(GameTime pGameTime)
        {
            mTimeElapsed -= pGameTime.ElapsedGameTime.Milliseconds;

            if (mTimeElapsed <= 0)
            {
                eScreenEvent.Invoke(this, new EventArgs());
                mTimeElapsed = MAX_TIME;
                return;
            }
        }

        public override void draw(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Begin();
            pSpriteBatch.Draw(mBackGround, Vector2.Zero, Color.White);
            pSpriteBatch.DrawString(Test, mMainLanguage[(int)mCurrentLanguage].pause_Controls, Vector2.Zero, Color.White);
            pSpriteBatch.End();
        }
        
    }
}
