using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archeaology_Game.Dig
{
    class Site
    {
        Texture2D texture;
        Rectangle boundingBox;
        int posX;
        int posY;

        public Texture2D Texture
        {
            get => texture;
            set
            {
                texture = value;
                boundingBox = new Rectangle(0, 0, texture.Width, texture.Height);
            }
        }

        public int PosY {
            get => posY;
            set
            {
                posY = value;
                boundingBox.Y = value;
            }

        }

        public int PosX
        {
            get => posX;
            set
            {
                posX = value;
                boundingBox.X = value;
            }

        }


        public String getPos()
        {
            return posX + ", " + posY;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, boundingBox, null, Color.White);
            
        }
    }
}
