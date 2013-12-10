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
        }

        public virtual void init(ContentManager content) { }

        public virtual void update(GameTime pGameTime) { }

        public virtual void draw(SpriteBatch pSpriteBatch) { }
    }

    

}
