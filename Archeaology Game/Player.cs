using Archeaology_Game.Dig;
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
        String name;
        int days;

        public Player(string name)
        {
            this.name = name;
        }

        public string Name { get => name; set => name = value; }
        internal List<Artefact> Inventory { get => inventory; set => inventory = value; }

        public void AddArtefact(Artefact artefact)
        {
            inventory.Add(artefact);
        }
    }
}
