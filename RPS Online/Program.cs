
using Microsoft.Xna.Framework;
using MLEM.Misc;

public static class Program
{
  public static void Main()
  {
    MlemPlatform.Current = new MlemPlatform.DesktopGl<TextInputEventArgs>((w, c) => w.TextInput += c);
    using var game = new RPS_Online.Game1();
    game.Run();
  }
}
