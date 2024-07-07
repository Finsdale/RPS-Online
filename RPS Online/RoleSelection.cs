using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLEM.Input;
using MLEM.Ui.Elements;
using MLEM.Ui;

namespace RPS_Online
{
  internal class RoleSelection : IGameState
  {
    GameStateMachine GameStateMachine;
    GameData gameData;
    readonly TextureCollection TC = TextureCollection.Instance;
    Rectangle HostRectangle = new Rectangle(40, 200, 200, 200), ClientRectangle = new Rectangle(260, 200, 200, 200);
    TextField IPTextField = new TextField(Anchor.AutoLeft, new Vector2(1, 10), TextField.OnlyNumbers);
    public RoleSelection(GameStateMachine gameStateMachine)
    {
      this.GameStateMachine = gameStateMachine;
      this.gameData = gameStateMachine.gameData;
    }

    public void Update(InputHandler input, GameTime gameTime)
    {
      IPTextField.Update(gameTime);
      input.InvertPressBehavior = true;
      if (input.IsPressed(MLEM.Input.MouseButton.Left)) {
        //if (input.MInput.IsButtonReleased(ControllerInput.MouseButton.Left)) {
        Rectangle mousePosition = new(input.MousePosition, Point.Zero);
        if (HostRectangle.Intersects(mousePosition)) {
          gameData.Host();
          GameStateMachine.Pop();
          GameStateMachine.Push(GameStateMachine.waitingForPlayers);
        }
        else if(ClientRectangle.Intersects(mousePosition)) {
          gameData.Client();
          GameStateMachine.Pop();
          GameStateMachine.Push(GameStateMachine.selectionRequest);
        }
      }
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
      spriteBatch.DrawString(TC.Text, "Host or Client?", new Vector2(300, 20), Color.Black);
      //IPTextField.Draw(gameTime, spriteBatch, 1, new MLEM.Graphics.SpriteBatchContext());
      spriteBatch.Draw(TC.Texture, HostRectangle, Color.Black);
      spriteBatch.Draw(TC.Texture, ClientRectangle, Color.Black);
      spriteBatch.DrawString(TC.Text, "Host", new Vector2(60, 220), Color.White);
      spriteBatch.DrawString(TC.Text, "Client", new Vector2(280, 220), Color.White);
    }
  }
}
