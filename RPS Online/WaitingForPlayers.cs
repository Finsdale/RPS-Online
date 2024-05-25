using ControllerInput;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPS_Online
{
  internal class WaitingForPlayers : IGameState
  {
    public WaitingForPlayers(GameStateMachine gameStateMachine) { }
    public void Update(NewInput input) { }
    public void Draw(SpriteBatch spriteBatch) { }
  }
}
