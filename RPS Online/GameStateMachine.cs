using ControllerInput;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPS_Online
{
  internal class GameStateMachine
  {
    public List<IGameState> GameStack = new();
    public GameData gameData;
    public RoleSelection roleSelection;
    public WaitingForPlayers waitingForPlayers;
    public SelectionRequest selectionRequest;

    public GameStateMachine()
    {
      gameData = new GameData();
      roleSelection = new RoleSelection(this);
      waitingForPlayers = new WaitingForPlayers(this);
      selectionRequest = new SelectionRequest(this);
      /*
      Here is a list of states that will be required:
      Both:
      RoleSelection
      Host:
      WaitingForPlayers
      SelectionRequest
      SelectionResult
      Client:
      WaitingForHost
      UserSelection

      I will also need to have this data available:
      Players
      PlayerSelection

      */
      Push(roleSelection);
    }

    public void Clear()
    {
      GameStack.Clear();
    }

    public void Push (IGameState state)
    {
      GameStack.Add(state);
    }

    public void Pop()
    {
      GameStack.RemoveAt(GameStack.Count - 1);
    }

    public void Update(NewInput input, InputHandler otherInput)
    {
      if(GameStack.Count > 0) {
        GameStack[^1].Update(input, otherInput);
      }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      for(int i = 0; i < GameStack.Count; i++) {
        GameStack[i].Draw(spriteBatch);
      }
    }
  }
}
