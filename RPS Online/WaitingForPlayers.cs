using ControllerInput;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLEM.Input;

namespace RPS_Online
{
  internal class WaitingForPlayers : IGameState
  {
    GameStateMachine gameStateMachine;
    GameData gameData;
    TextureCollection TC = TextureCollection.Instance;

    public WaitingForPlayers(GameStateMachine gameStateMachine)
    {
      this.gameStateMachine = gameStateMachine;
      this.gameData = gameStateMachine.gameData;
    }
    public void Update(NewInput input, InputHandler otherInput)
    {
      gameData.Update();
      if (gameData.gameActive) {
        gameStateMachine.Pop();
        gameStateMachine.Push(gameStateMachine.selectionRequest);
      }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.DrawString(TC.Text, "Waiting for players", new Vector2(20, 20), Color.Black);
      spriteBatch.DrawString(TC.Text, $"Connected Players: {gameData.playersConnected}", new Vector2(20, 40), Color.Black);
    }
  }
}
