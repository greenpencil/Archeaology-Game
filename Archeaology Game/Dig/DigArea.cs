using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archeaology_Game.Dig
{
    class DigArea
    {
        public int PosX { get; }
        public int PosY { get; }
        private int state;

        public int State
        {
            get => state;
            set => state = value;
        }

        public int Days
        {
            get => days;
            set => days = value;
        }


        int days;
        public int DaysActive { get; set; }
        public Rectangle Area { get; }

        public string Report { get; }

        public DigArea(int posX, int posY, int days)
        {
            this.PosX = posX;
            this.PosY = posY;
            this.days = days;
            Area = new Rectangle(posX, posY, 150, 150);
            state = 1;
            DaysActive = 0;
            Report = "We have just chosen this location but not assigned a number of days to dig";
        }
        
    }
}
