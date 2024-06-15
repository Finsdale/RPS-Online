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
  internal class SelectionRequest : IGameState
  {
    GameStateMachine gameStateMachine;
    GameData gameData;
    readonly TextureCollection TC = TextureCollection.Instance;
    Rectangle RockButton = new Rectangle(40, 200, 200, 200);
    Rectangle PaperButton = new Rectangle(260, 200, 200, 200);
    Rectangle ScissorButton = new Rectangle(480, 200, 200, 200);
    public SelectionRequest(GameStateMachine gameStateMachine)
    {
      this.gameStateMachine = gameStateMachine;
      this.gameData = gameStateMachine.gameData;
    }
    public void Update(NewInput input, InputHandler otherInput)
    {
      gameData.Update();
      if(gameData.selectionMade == false) {
        if (otherInput.try
          input.MInput.IsButtonReleased(ControllerInput.MouseButton.Left)) {
          Rectangle mousePosition = new Rectangle(input.MInput.MousePosition(), Point.Zero);
          if (RockButton.Intersects(mousePosition)) {
            gameData.selectionMade = true;
            gameData.SendSelection("R");
          } else if (PaperButton.Intersects(mousePosition)) {
            gameData.selectionMade = true;
            gameData.SendSelection("P");
          } else if (ScissorButton.Intersects(mousePosition)) {
            gameData.selectionMade = true;
            gameData.SendSelection("S");
          }
        }
      }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
      if(gameData.selectionMade == false) {
        spriteBatch.DrawString(TC.Text, "Make your selection", new Vector2(20, 20), Color.Black);
        spriteBatch.Draw(TC.Texture, RockButton, Color.Black);
        spriteBatch.Draw(TC.Texture, PaperButton, Color.Black);
        spriteBatch.Draw(TC.Texture, ScissorButton, Color.Black);
        spriteBatch.DrawString(TC.Text, "Rock", new Vector2(60, 220), Color.White);
        spriteBatch.DrawString(TC.Text, "Paper", new Vector2(280, 220), Color.White);
        spriteBatch.DrawString(TC.Text, "Scissors", new Vector2(500, 220), Color.White);
      }
      else {
        spriteBatch.DrawString(TC.Text, "Waiting for your opponent", new Vector2(20, 20), Color.Black);
      }

      if(gameData.result?.Length > 0) {
        string message = "";
        switch(gameData.result) {
          case "0":
            message = "It's a tie!";
            break;
            case "1":
            message = gameData.isHost ? "You Win!" : "Player 1 Wins!";
            break;
            case "2":
            message = gameData.isClient ? "You Win!" : "Player 2 Wins!";
            break;
        }
        spriteBatch.DrawString(TC.Text, message, new Vector2(20,40), Color.Black);
      }
    }
  }
}
