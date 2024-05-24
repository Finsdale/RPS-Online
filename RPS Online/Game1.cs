using ControllerInput;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPS_Online
{
  public class Game1 : Game
  {
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    RenderTarget2D _renderTarget;
    GameStateMachine _gameStateMachine = new();
    NewInput _input = new NewInput(0, false, true);
    TextureCollection TC;

    public Game1()
    {
      _graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      IsMouseVisible = true;
      TC = TextureCollection.Instance;
    }

    protected override void Initialize()
    {
      // TODO: Add your initialization logic here
      _graphics.PreferredBackBufferWidth = 800;
      _graphics.PreferredBackBufferHeight = 480;
      _graphics.ApplyChanges();
      _renderTarget = new RenderTarget2D(GraphicsDevice, 800, 480, false, GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
      _input.SetMouseScale(new Vector2(800, 480), new Vector2(800, 480));

      base.Initialize();
    }

    protected override void LoadContent()
    {
      _spriteBatch = new SpriteBatch(GraphicsDevice);
      TC.LoadContent(Content);
      // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

      // TODO: Add your update logic here
      _input.Update();
      _gameStateMachine.Update(_input);

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.SetRenderTarget(_renderTarget);
      GraphicsDevice.Clear(Color.CornflowerBlue);
      _spriteBatch.Begin();
      // TODO: Add your drawing code here
      _gameStateMachine.Draw(_spriteBatch);
      _spriteBatch.End();
      GraphicsDevice.SetRenderTarget(null);
      GraphicsDevice.Clear(Color.CornflowerBlue);
      _spriteBatch.Begin();
      _spriteBatch.Draw(_renderTarget, new Rectangle(0,0, 800, 480), Color.White);
      _spriteBatch.End();
      base.Draw(gameTime);
    }
  }
}
