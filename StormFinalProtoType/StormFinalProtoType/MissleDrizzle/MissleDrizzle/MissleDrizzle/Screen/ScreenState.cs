using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MissileDrizzle.Manager;
using MDDataLibrary;
using Microsoft.Xna.Framework.Content;

namespace MissileDrizzle.Screen
{
    enum Languages
    {
        English = 0,
        German
    };

    class ScreenState
    {
        protected EventHandler
            eScreenEvent;
        protected GraphicsDevice
            mGraphics;
        public static Menu[]
            mMainLanguage;
        static protected Languages
            mCurrentLanguage;
        static protected SpriteFont
            mMainFont;
        static protected PlayerIndex 
            mMenuPlayer = PlayerIndex.One;
        static protected bool
            isOver;

        static protected Vector2[]
            mSelectionLocations,
            mControlLocations;

        protected Vector2 
            SCREEN_POS = new Vector2(496, 130);

        static protected int
            mSelection;

        static protected Sprite
            mCursor;

        static protected Vector2
            mCursorLocation;

        protected const float
            MAX_CONTROL_TIME = 2000;
        protected static Texture2D
            mControls;

        public ScreenState(EventHandler TheScreenEvent, GraphicsDevice pGraphics)
        {
            eScreenEvent = TheScreenEvent;
            mGraphics = pGraphics;
            mCurrentLanguage = Languages.English;
        }

        public void loadLanguages(ContentManager content)
        {
            mMainLanguage = content.Load<Menu[]>("Languages");
            mMainFont = content.Load<SpriteFont>("MenuFont");
            isOver = false;
        }

        public void loadMenu()
        {
            mSelectionLocations = new Vector2[7];
            mSelectionLocations[0] = new Vector2(515, 145);
            mSelectionLocations[1] = new Vector2(515, 165);
            mSelectionLocations[2] = new Vector2(515, 185);
            mSelectionLocations[3] = new Vector2(515, 205);
            mSelectionLocations[4] = new Vector2(515, 225);
            mSelectionLocations[5] = new Vector2(515, 245);
            mSelectionLocations[6] = new Vector2(515, 265);

            mControlLocations = new Vector2[5];
            mControlLocations[0] = new Vector2(560, 145);
            mControlLocations[1] = new Vector2(560, 171);
            mControlLocations[2] = new Vector2(560, 195);
            mControlLocations[3] = new Vector2(560, 225);
            mControlLocations[4] = new Vector2(560, 255);
        }

        public virtual void init(ContentManager content) { }

        public virtual void update(GameTime pGameTime) { }

        public virtual void draw(SpriteBatch pSpriteBatch) { }

        public bool returnIsOver()
        {
            return isOver;
        }


    }

    

}
