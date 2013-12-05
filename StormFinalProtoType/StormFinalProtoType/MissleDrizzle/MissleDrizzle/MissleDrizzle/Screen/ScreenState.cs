using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MissileDrizzle.Manager;
using Microsoft.Xna.Framework.Content;

namespace MissileDrizzle.Screen
{


    class ScreenState
    {
        protected EventHandler
            eScreenEvent;
        protected GraphicsDevice
            mGraphics;

        public ScreenState(EventHandler TheScreenEvent, GraphicsDevice pGraphics)
        {
            eScreenEvent = TheScreenEvent;
            mGraphics = pGraphics;
        }

        public virtual void init(ContentManager content) { }

        public virtual void update(GameTime pGameTime) { }

        public virtual void draw(SpriteBatch pSpriteBatch) { }
    }

    

}
