using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archeaology_Game.Menu
{
     class Button
    {
        private Texture2D texture;

        private Texture2D hoverTexture;
        private String text;
        private Rectangle boundingBox;
        private int posX;
        private int posY;
        private bool hover;
        private Action pressed;



        public Button(Action pressed, int posX, int posY)
        {
            this.pressed = pressed;
            this.posX = posX;
            this.posY = posY;
            hover = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!hover)
            {
                spriteBatch.Draw(texture, boundingBox, null, Color.White);
            } else
            {
                spriteBatch.Draw(hoverTexture, boundingBox, null, Color.White);
            }
        }

        
        public string Text { get => text; set => text = value; }

        public Texture2D Texture {
            get => texture;
            set {
                texture = value;
                posX = posX - (texture.Width / 2);
                posY = posY - (texture.Height / 2);
                boundingBox = new Rectangle(posX, posY, texture.Width, texture.Height);
            }
        }
        
        public Texture2D HoverTexture { get => hoverTexture; set => hoverTexture = value; }
        public Rectangle BoundingBox { get => boundingBox; set => boundingBox = value; }
        public bool Hover { get => hover; set => hover = value; }
        public Action Pressed { get => pressed; set => pressed = value; }

        public String getPos()
        {
            return posX + ", " + posY;
        }
    }
}
