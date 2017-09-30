using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archeaology_Game.Dig
{
    class GameDig
    {
        Site site;
        Player player;
        Texture2D inventory;
        ArcheaologyGame game;
        List<Artefact> artefacts;
        int totalDays;

        public GameDig(ArcheaologyGame game)
        {
            site = new Site();
            player = game.player;
            this.game = game;

            artefacts = new List<Artefact>();

            artefacts.Add(new Artefact("Pottery Fragment", "it used to be a pot"));
            artefacts.Add(new Artefact("Spear Tip", "made of bronze"));
            artefacts.Add(new Artefact("Pot", "contained liquid"));
            artefacts.Add(new Artefact("Text Fragment", "undeciphered"));

            totalDays = 20;

        }

        public void Initialize()
        {
            Random random = new Random();

            foreach(Artefact artefact in artefacts)
            {
                bool correctLocation = false;
                while (!correctLocation)
                {
                    int x = random.Next(game.Width - 50);
                    int y = random.Next(game.Height - 50);
                    artefact.PosX = x;
                    artefact.PosY = y;

                    foreach (Artefact artefact2 in artefacts)
                    {
                        if (!artefact.getBoundingBox().Intersects(artefact2.getBoundingBox()))
                        {
                            correctLocation = true;
                        } else if(artefact.Depth > artefact2.Depth || artefact2.Depth > artefact.Depth)
                        {
                            correctLocation = true;
                        }
                        else
                        {
                            correctLocation = false;
                        }
                }
                }
            }
        }

        public void LoadContent(ContentManager content)
        {
            site.Texture = content.Load<Texture2D>("digs/1");
            inventory = content.Load<Texture2D>("ui/inventory");
            artefacts[0].Texture = content.Load<Texture2D>("artefacts/pottery-fragment-1");
            artefacts[1].Texture = content.Load<Texture2D>("artefacts/spear-tip-1");
            artefacts[2].Texture = content.Load<Texture2D>("artefacts/full-pot-1");
            artefacts[3].Texture = content.Load<Texture2D>("artefacts/text-fragment-1");
        }

        public void UnloadContent()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            site.Draw(spriteBatch);
            spriteBatch.Draw(inventory, new Rectangle(0, game.Height-74, inventory.Width, inventory.Height), null, Color.White);

            foreach(Artefact artefact in artefacts)
            {
                artefact.Draw(spriteBatch);
            }
        }
    }
}
