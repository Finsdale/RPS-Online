using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControllerInput;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Input;
using Microsoft.Xna.Framework;

namespace RPS_Online
{
  internal interface IGameState
  {
    public void Update(InputHandler input, GameTime gameTime);
    public void Draw(SpriteBatch spriteBatch, GameTime gameTime);
  }
}
