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
    public RoleSelection(GameStateMachine gameStateMachine)
    {
      this.GameStateMachine = gameStateMachine;
      this.gameData = gameStateMachine.gameData;
    }

    public void Update(NewInput input)
    {

    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.DrawString(TC.Text, "Host or Client?", new Vector2(300, 20), Color.Black);
      spriteBatch.Draw(TC.Texture, new Rectangle(40, 200, 200, 200), Color.Black);
      spriteBatch.Draw(TC.Texture, new Rectangle(260, 200, 200, 200), Color.Black);
    }
  }
}
