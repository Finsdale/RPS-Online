using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPS_Online
{
  public sealed class TextureCollection
  {
    private static readonly TextureCollection Collection = new();
    public SpriteFont Text { get; private set; }
    public Texture2D Texture { get; private set; }

    private TextureCollection() { }
    public static TextureCollection Instance { get { return Collection; } }
    public void LoadContent(ContentManager content)
    {
      Text = content.Load<SpriteFont>("Text");
      Texture = content.Load<Texture2D>("FakeImage");
    }
  }
}
