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
        Vector2[]
            mSelectionLocations;
        Vector2
            mCursorLocation;
        int
            mSelection;

        
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
            

            mBG.init(content);
            mCursor.createSprite(temp, new Rectangle(0,0,temp.Width, temp.Height));
            mCourts = content.Load<Texture2D>("FPO/MenuScreen");

            mSelection = 0;
            mCursorLocation = new Vector2(mSelectionLocations[mSelection].X - 18, mSelectionLocations[mSelection].Y);
        }

        public override void update(GameTime pGameTime)
        {
            mBG.update(pGameTime);
            mCursor.updatePos(mCursorLocation);
           
            
        }

        public override void draw(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Begin();
            mBG.draw(pSpriteBatch);
            pSpriteBatch.Draw(mCourts, Vector2.Zero, Color.White);

            pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].menu_Play, mSelectionLocations[0], Color.White);
            pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].menu_Controls, mSelectionLocations[1], Color.White);
            pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].menu_Language, mSelectionLocations[2], Color.White);
            pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].menu_Credits, mSelectionLocations[3], Color.White);
            pSpriteBatch.DrawString(mMainFont, mMainLanguage[(int)mCurrentLanguage].menu_Quit, mSelectionLocations[4], Color.White);

            mCursor.drawZeroOrigin(pSpriteBatch, SpriteEffects.None);
            pSpriteBatch.End();
        }
    }
}
