using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archeaology_Game.Dig
{
    class DigArea
    {
        int posX;
        int posY;
        int days;

        public DigArea(int posX, int posY, int days)
        {
            this.posX = posX;
            this.posY = posY;
            this.days = days;
        }
    }
}
