using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using static Archeaology_Game.ArcheaologyGame;

namespace Archeaology_Game.Menu
{
    class GameMenu
    {
        ArcheaologyGame game;
        enum MenuState
        {
            startScreen,
            nameScreen,
            loreScreen
        }
        MenuState menuState;
        Button startButton;
        Button settingButton;
        Rectangle mouseBox;
        List<Button> buttonList;
        Texture2D logo;

        public void StartButtonPressed()
        {
            game._state = GameState.DigState;
            game.player = new Player("Test");
        }
        

        public GameMenu(ArcheaologyGame game)
        {
            this.game = game;
            menuState = MenuState.startScreen;

            Debug.WriteLine("H:" + game.Height + ", W:" + game.Width);
            startButton = new Button(StartButtonPressed, game.Width / 2, game.Height /2 + 50);
            settingButton = new Button(StartButtonPressed, game.Width / 2, (game.Height / 2)+120);

            mouseBox = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 10, 10);
            buttonList = new List<Button>();
            buttonList.Add(startButton);
            buttonList.Add(settingButton);
        }


        public void Initialize()
        {

        }

        public void LoadContent(ContentManager content)
        {
            logo = content.Load<Texture2D>("logo");
            startButton.Texture = content.Load<Texture2D>("start");
            startButton.HoverTexture = content.Load<Texture2D>("start_hover");

            settingButton.Texture = content.Load<Texture2D>("settings");
            settingButton.HoverTexture = content.Load<Texture2D>("settings_hover");
        }

        public void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public void Update(GameTime gameTime)
        {
            mouseBox.X = Mouse.GetState().X;
            mouseBox.Y = Mouse.GetState().Y;

            foreach(Button button in buttonList)
            {
                if(mouseBox.Intersects(button.BoundingBox))
                {
                    button.Hover = true;

                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        button.Pressed();
                    }

                }
                else
                {
                    button.Hover = false;
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(logo, new Rectangle((game.Width / 2) - (logo.Width/2), 20, logo.Width, logo.Height), null, Color.White);

            startButton.Draw(spriteBatch);
            settingButton.Draw(spriteBatch);
        }
        
    }
}
