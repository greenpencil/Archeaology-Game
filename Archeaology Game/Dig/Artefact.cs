using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archeaology_Game.Dig
{
    public class Artefact
    {
        private Texture2D texture;
        private String name;
        private String description;
        private Rectangle boundingBox;
        private int posX;
        private int posY;
        private int depth;
        private bool drawable;

        public Artefact(String name, String description)
        {
            this.name = name;
            this.description = description;
            drawable = true;
            Random r = new Random();
            Depth = r.Next(6);
        }

        public Texture2D Texture {
            get => texture;
            set {
                texture = value;
                posX = posX - (texture.Width / 2);
                posY = posY - (texture.Height / 2);
                boundingBox = new Rectangle(posX, posY, texture.Width, texture.Height);
            }
        }

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }


        public int PosY
        {
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

        public int Depth { get => depth; set => depth = value; }

        public Rectangle getBoundingBox()
        {
            return boundingBox;
        }

        public String getPos()
        {
            return posX + ", " + posY;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (drawable || Depth == 0)
            {
                spriteBatch.Draw(texture, boundingBox, null, Color.White);
            }

        }
    }
}
