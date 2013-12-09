using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MissileDrizzle.Parallax;


namespace MissileDrizzle.Screen
{
    class MainMenuScreen : ScreenState
    {
        Background
            mBG;
        Texture2D
            mCourts;
        Sprite
            mCursor;
        
        public MainMenuScreen(EventHandler TheScreenEvent, GraphicsDevice graphics)
            : base(TheScreenEvent, graphics)
        {
            mBG = new Background();
            mCursor = new Sprite();
        }

        public override void init(ContentManager content)
        {
            Texture2D temp;
            temp = content.Load<Texture2D>("FPO/ball");
            

            mBG.init(content);
            mCursor.createSprite(temp, new Rectangle(0,0,temp.Width, temp.Height));
            mCourts = content.Load<Texture2D>("FPO/MenuScreen");
        }

        public override void update(GameTime pGameTime)
        {
            mBG.update(pGameTime);
            
           
            
        }

        public override void draw(SpriteBatch pSpriteBatch)
        {
            
            pSpriteBatch.Begin();
            mBG.draw(pSpriteBatch);
            pSpriteBatch.Draw(mCourts, Vector2.Zero, Color.White);
            mCursor.drawOrigin(pSpriteBatch, SpriteEffects.None);
            pSpriteBatch.End();
        }
    }
}
