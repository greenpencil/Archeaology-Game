using Archeaology_Game.Dig;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archeaology_Game
{
    public class Player
    {
        List<Artefact> inventory;
        Texture2D portrait;
        Texture2D portraitSmall;
        String name;
        int days;

        public Player(string name)
        {
            this.name = name;
            inventory = new List<Artefact>();
        }

        public string Name { get => name; set => name = value; }
        public int Days { get => days; set => days = value; }
        public Texture2D Portrait { get => portrait; set => portrait = value; }
        public Texture2D PortraitSmall { get => portraitSmall; set => portraitSmall = value; }
        internal List<Artefact> Inventory { get => inventory; set => inventory = value; }

        public void AddArtefact(Artefact artefact)
        {
            inventory.Add(artefact);
        }
    }
}
