using ControllerInput;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPS_Online
{
  internal class RoleSelection : IGameState
  {
    GameStateMachine GameStateMachine;
    GameData gameData;
    readonly TextureCollection TC = TextureCollection.Instance;
    Rectangle HostRectangle = new Rectangle(40, 200, 200, 200), ClientRectangle = new Rectangle(260, 200, 200, 200);
    public RoleSelection(GameStateMachine gameStateMachine)
    {
      this.GameStateMachine = gameStateMachine;
      this.gameData = gameStateMachine.gameData;
    }

    public void Update(NewInput input)
    {
      if (input.MInput.IsButtonReleased(MouseButton.Left)) {
        if (HostRectangle.Intersects(new Rectangle(input.MInput.MousePosition(), Point.Zero))) {
          gameData.Host();
          GameStateMachine.Pop();
          GameStateMachine.Push(GameStateMachine.waitingForPlayers);
        }
        else if(ClientRectangle.Intersects(new Rectangle(input.MInput.MousePosition(), Point.Zero))) {

        }
      }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.DrawString(TC.Text, "Host or Client?", new Vector2(300, 20), Color.Black);
      spriteBatch.Draw(TC.Texture, HostRectangle, Color.Black);
      spriteBatch.Draw(TC.Texture, ClientRectangle, Color.Black);
      spriteBatch.DrawString(TC.Text, "Host", new Vector2(60, 220), Color.White);
      spriteBatch.DrawString(TC.Text, "Client", new Vector2(280, 220), Color.White);
    }
  }
}
