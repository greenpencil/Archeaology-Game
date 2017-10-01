using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archeaology_Game.Menu;
using Microsoft.Xna.Framework.Input;

namespace Archeaology_Game.Dig
{
    class GameDig
    {
        Site site;
        Texture2D inventory;
        private Texture2D digCursor;
        private Texture2D digArea;
        ArcheaologyGame game;
        List<Artefact> artefacts;
        int totalDays;
        private List<DigArea> digAreas;
        private int maxDigAreas;
        Rectangle mouseBox;
        MouseState oldMouseState = Mouse.GetState();
        private Button nextDayButton;
        private SpriteFont font;
        private Texture2D personIcon;
        private DigArea selectedArea;
        private List<Button> dayButtons;
        private Texture2D button1;
        private Texture2D button2;
        private Texture2D button3;
        private Texture2D button4;
        private Texture2D flag;
        private List<Point> previouslyDug;



        public GameDig(ArcheaologyGame game)
        {
            site = new Site();
            this.game = game;

            artefacts = new List<Artefact>();
            mouseBox = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 10, 10);
            
            Random r = new Random();
            artefacts.Add(new Artefact("Pottery Fragment", "it used to be a pot", r.Next(6)));
            artefacts.Add(new Artefact("Spear Tip", "made of bronze", r.Next(6)));
            artefacts.Add(new Artefact("Pot", "contained liquid", r.Next(6)));
            artefacts.Add(new Artefact("Text Fragment", "undeciphered", r.Next(6)));
            maxDigAreas = 400;
            totalDays = 20;
            digAreas = new List<DigArea>();
            nextDayButton = new Button(advanceDay, game.Width - 150, game.Height - 36);
            
            dayButtons = new List<Button>();
            previouslyDug = new List<Point>();
        }

        private void changeDays(int days)
        {
            selectedArea.Days = days;
            selectedArea.State = 2;
        }

        private void changeDayButton1()
        {
            changeDays(1);
        }
        
        private void changeDayButton2()
        {
            changeDays(2);
        }
        
        private void changeDayButton3()
        {
            changeDays(3);
        }
        
        public void Initialize()
        {
            Random random = new Random();

            foreach (Artefact artefact in artefacts)
            {
                bool correctLocation = false;
                while (!correctLocation)
                {
                    int x = random.Next(game.Width - 50);
                    int y = random.Next(game.Height - 100);
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
            artefacts[0].Texture = content.Load<Texture2D>("artefacts/full-pot-11");
            artefacts[1].Texture = content.Load<Texture2D>("artefacts/spear-tip-1");
            artefacts[2].Texture = content.Load<Texture2D>("artefacts/full-pot-2");
            artefacts[3].Texture = content.Load<Texture2D>("artefacts/text-fragment-1");
            nextDayButton.Texture = content.Load<Texture2D>("ui/next-day");
            personIcon = content.Load<Texture2D>("ui/person");
            font = content.Load<SpriteFont>("fonts/mainFont");
            button1 = content.Load<Texture2D>("ui/1-day");
            button2 = content.Load<Texture2D>("ui/2-day");
            button3 = content.Load<Texture2D>("ui/3-day");
            button4 = content.Load<Texture2D>("ui/4-day");
            
            digCursor = content.Load<Texture2D>("ui/dig-here");
            flag = content.Load<Texture2D>("ui/flag");
            digArea = content.Load<Texture2D>("ui/aoe");

        }

        public void UnloadContent()
        {

        }

        public void Update(GameTime gameTime)
        {
            //collect artefacts on the surface
            mouseBox.X = Mouse.GetState().X;
            mouseBox.Y = Mouse.GetState().Y;
            MouseState mouseState = Mouse.GetState();

            if (oldMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
            {
                bool isPickingUpArtefact = false;
                bool isMakingDigSite = false;
                bool isClickingButton = false;
                
                List<Artefact> artefactsToRemove = new List<Artefact>();
                foreach (Artefact artefact in artefacts)
                {
                    if (mouseBox.Intersects(artefact.getBoundingBox()))
                    {
                        isPickingUpArtefact = true;
                        if (artefact.Depth == 0)
                        {
                            game.player.Inventory.Add(artefact);
                            artefactsToRemove.Add(artefact);
                        }
                    }
                }

                if (nextDayButton.BoundingBox.Intersects(mouseBox))
                {
                    isClickingButton = true;
                }
                
                if (isPickingUpArtefact)
                {
                    int lastX = 0;
                    foreach (Artefact artefact in game.player.Inventory)
                    {
                        artefact.Drawable = true;
                        artefact.PosX = 50 + lastX;
                        lastX = artefact.PosX;
                        artefact.PosY = game.Height - 63;
                    }

                    foreach (Artefact artefact in artefactsToRemove)
                    {
                        artefacts.Remove(artefact);
                    }
                }
                else if (isClickingButton)
                {
                    nextDayButton.Pressed();
                }
                else
                {
                    // if we don't hit an artefact we will need to create a dig site
                    bool isDigAreaUnComplete = false;

                    foreach (DigArea digArea in digAreas)
                    {
                        if (digArea.State == 1)
                        {
                            isDigAreaUnComplete = true;
                        }
                    }
                    if (!isDigAreaUnComplete)
                    {
                        if (digAreas.Count < maxDigAreas)
                        {
                            Button day1 = new Button(changeDayButton1, mouseState.X - button1.Width, mouseState.Y + digCursor.Height);
                            
                            Button day2 = new Button(changeDayButton2, mouseState.X, mouseState.Y + digCursor.Height);
                            
                            Button day3 = new Button(changeDayButton3, mouseState.X + button1.Width, mouseState.Y + digCursor.Height);
                            
                            day1.Texture = button1;
                            day2.Texture = button2;
                            day3.Texture = button3;
                            
                            dayButtons.Add(day1);
                            dayButtons.Add(day2);
                            dayButtons.Add(day3);
                            
                            DigArea da = new DigArea(mouseState.X - (digArea.Width / 2),
                                mouseState.Y - (digArea.Height / 2), 3);
                            selectedArea = da;
                            digAreas.Add(da);
                        }
                    }
                    else
                    {
                        bool buttonWasPressed = false;
                        foreach (Button button in dayButtons)
                        {
                            if (button.BoundingBox.Intersects(mouseBox))
                            {
                                button.Pressed();
                                buttonWasPressed = true;
                            }
                        }
                        
                        if(buttonWasPressed) dayButtons.Clear();
                    }
                }
            }

            if (oldMouseState.RightButton == ButtonState.Pressed && mouseState.RightButton == ButtonState.Released)
            {
                List<DigArea> digAreaToRemove = new List<DigArea>();
                foreach (DigArea digArea in digAreas)
                {
                    if (digArea.Area.Intersects(mouseBox) && digArea.State == 1)
                    {
                        digAreaToRemove.Add(digArea);
                        dayButtons.Clear();
                    }
                }

                foreach (DigArea digArea in digAreaToRemove)
                {
                    digAreas.Remove(digArea);
                }
            }

            oldMouseState = mouseState;
        }

        public void advanceDay()
        {
            //dig area uncomplete
            bool isUncompleteDig = false;
            foreach (DigArea digArea in digAreas)
            {
                if (digArea.State == 1) isUncompleteDig = true;
            }
            if (!isUncompleteDig)
            {
                List<DigArea> digAreasToRemove = new List<DigArea>();

                foreach (DigArea digArea in digAreas)
                {

                    digArea.State = 3;

                    if (digArea.DaysActive > digArea.Days)
                    {
                        previouslyDug.Add(new Point(digArea.PosX, digArea.PosY));
                        digAreasToRemove.Add(digArea);
                    }
                }

                foreach (DigArea digArea in digAreasToRemove)
                {
                    digAreas.Remove(digArea);
                }
                foreach (Artefact artefact in artefacts)
                {
                    foreach (DigArea digArea in digAreas)
                    {
                        if (digArea.DaysActive > 0)
                        {
                            if (digArea.Area.Intersects(artefact.getBoundingBox()))
                            {
                                if (artefact.Depth > 0)
                                {
                                    artefact.Depth = artefact.Depth - 1;
                                }
                            }
                        }
                    }
                }
                foreach (DigArea digArea in digAreas)
                {
                    digArea.DaysActive = digArea.DaysActive + 1;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            site.Draw(spriteBatch);
            spriteBatch.Draw(inventory, new Rectangle(0, game.Height-74, inventory.Width, inventory.Height), null, Color.White);

            if (game.player != null)
            {
                spriteBatch.Draw(game.player.PortraitSmall, new Rectangle(game.Width-75, game.Height - 100, game.player.PortraitSmall.Width, game.player.PortraitSmall.Height), null, Color.White);
            }

            foreach (Artefact artefact in artefacts)
            {
                artefact.Draw(spriteBatch);
            }

            foreach (DigArea digArea in digAreas)
            {
                
                Color color = Color.White;

                if (digArea.State == 1)
                {
                    
                    color = Color.White;
                    spriteBatch.Draw(digCursor,
                        new Rectangle(digArea.PosX + (this.digArea.Width / 2) - (digCursor.Width / 2),
                            digArea.PosY + (this.digArea.Height / 2) - (digCursor.Height / 2), digCursor.Width,
                            digCursor.Height), null, color);
                }
                else if (digArea.State == 2)
                {
                    color = Color.Green;
                    spriteBatch.Draw(personIcon,
                        new Rectangle(digArea.PosX + (this.digArea.Width / 2) - (personIcon.Width / 2),
                            digArea.PosY + (this.digArea.Height / 2) - (personIcon.Height / 2), personIcon.Width,
                            personIcon.Height), null, color);
                    spriteBatch.DrawString(font, digArea.DaysActive + "/" +  (digArea.Days+1), new Vector2(digArea.PosX + (this.digArea.Width / 2) - 10,
                        digArea.PosY + (this.digArea.Height / 2) + (personIcon.Height/2)), Color.Black);

                } else if (digArea.State == 3)
                {
                    color = Color.Blue;
                    spriteBatch.Draw(personIcon,
                        new Rectangle(digArea.PosX + (this.digArea.Width / 2) - (personIcon.Width / 2),
                            digArea.PosY + (this.digArea.Height / 2) - (personIcon.Height / 2), personIcon.Width,
                            personIcon.Height), null, color);
                    spriteBatch.DrawString(font, digArea.DaysActive + "/" +  (digArea.Days+1), new Vector2(digArea.PosX + (this.digArea.Width / 2) - 10,
                        digArea.PosY + (this.digArea.Height / 2) + (personIcon.Height/2)), Color.Black);
                    
                }

                spriteBatch.Draw(this.digArea, new Rectangle(digArea.PosX, digArea.PosY, this.digArea.Width, this.digArea.Height), null, color * 0.2f);
            }

            foreach (Button button in dayButtons)
            {
                button.Draw(spriteBatch);
            }
            
            foreach (Artefact artefact in game.player.Inventory)
            {
                artefact.Draw(spriteBatch);
            }

            foreach (Point p in previouslyDug)
            {
                spriteBatch.Draw(flag,
                    new Rectangle(p.X + (digArea.Width/2) - (flag.Width/2),
                        p.Y+ (digArea.Height/2) - (flag.Height/2), flag.Width,
                        flag.Height), null, Color.White);
                
                spriteBatch.Draw(this.digArea, new Rectangle(p.X, p.Y, this.digArea.Width, this.digArea.Height), null, Color.DarkGray * 0.1f);

            }
            
            nextDayButton.Draw(spriteBatch);
        }
    }
}
