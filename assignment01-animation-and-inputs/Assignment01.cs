using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace assignment01_animation_and_inputs;

public class Assignment01 : Game
{
    private GraphicsDeviceManager _graphics;
    private KeyboardState _kbStateOld;
    private SpriteBatch _spriteBatch;
    private Texture2D _background, _bushes, _slime, _skeleton;
    private CelAnimationSequence _slimeSequence;
    private CelAnimationPlayer _slimePlayer;
    private CelAnimationSequenceMultiRow _skeletonUp, _skeletonDown, _skeletonLeft, _skeletonRight;
    private CelAnimationPlayerMultiRow _skeletonPlayerUp, _skeletonPlayerDown, _skeletonPlayerLeft, _skeletonPlayerRight;
    private float _skeletonX, _skeletonY, _skeletonSpeed = 2;
    private int _skeletonDirection = 2;
    private int _backgroundHeight = 378, _backgroundWidth = 735;

    public Assignment01()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferHeight = _backgroundHeight;
        _graphics.PreferredBackBufferWidth = _backgroundWidth;
        _graphics.ApplyChanges();
        _skeletonX = _backgroundWidth / 2;
        _skeletonY = _backgroundHeight / 2;

        _kbStateOld = Keyboard.GetState();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _background = Content.Load<Texture2D>("Forrest");
        _bushes = Content.Load<Texture2D>("ForrestBushes");
        _slime = Content.Load<Texture2D>("SlimeSprite");
        _skeleton = Content.Load<Texture2D>("Skeleton");

        _slimeSequence = new CelAnimationSequence(_slime, 160, 1 / 10f);
        _slimePlayer = new CelAnimationPlayer();
        #region Skeleton Sequencing and Animation
        _skeletonUp = new CelAnimationSequenceMultiRow(_skeleton, 64, 64, 1 / 10f, 0);
        _skeletonDown = new CelAnimationSequenceMultiRow(_skeleton, 64, 64, 1 / 10f, 2);
        _skeletonLeft = new CelAnimationSequenceMultiRow(_skeleton, 64, 64, 1 / 10f, 1);
        _skeletonRight = new CelAnimationSequenceMultiRow(_skeleton, 64, 64, 1 / 10f, 3);

        _skeletonPlayerUp = new CelAnimationPlayerMultiRow();
        _skeletonPlayerDown = new CelAnimationPlayerMultiRow();
        _skeletonPlayerLeft = new CelAnimationPlayerMultiRow();
        _skeletonPlayerRight = new CelAnimationPlayerMultiRow();


        _skeletonPlayerUp.Play(_skeletonUp);
        _skeletonPlayerDown.Play(_skeletonDown);
        _skeletonPlayerLeft.Play(_skeletonLeft);
        _skeletonPlayerRight.Play(_skeletonRight);
        #endregion
        _slimePlayer.Play(_slimeSequence);
    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardState kbCurrentState = Keyboard.GetState();

        _slimePlayer.Update(gameTime);

        base.Update(gameTime);
        #region Skeleton Movement
        if (kbCurrentState.IsKeyDown(Keys.Down))
        {
            _skeletonPlayerDown.Update(gameTime); // just to keep the skeleton on its last frame while idling.
            _skeletonY += _skeletonSpeed;
            _skeletonDirection = 2;
        }
        if (kbCurrentState.IsKeyDown(Keys.Up))
        {
            _skeletonY -= _skeletonSpeed;
            _skeletonPlayerUp.Update(gameTime);
            _skeletonDirection = 0;
        }
        if (kbCurrentState.IsKeyDown(Keys.Right))
        {
            _skeletonX += _skeletonSpeed;
            _skeletonPlayerRight.Update(gameTime);
            _skeletonDirection = 3;
        }
        if (kbCurrentState.IsKeyDown(Keys.Left))
        {
            _skeletonX -= _skeletonSpeed;
            _skeletonPlayerLeft.Update(gameTime);
            _skeletonDirection = 1;
        }
        #endregion

        _kbStateOld = kbCurrentState;
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
        _slimePlayer.Draw(_spriteBatch, new Vector2(_backgroundWidth / 2, _backgroundHeight / 2), SpriteEffects.None);
        #region Skeleton Draw
        if (_skeletonDirection == 0)
            _skeletonPlayerUp.Draw(_spriteBatch, new Vector2(_skeletonX,_skeletonY), SpriteEffects.None);
        else if (_skeletonDirection == 1)
            _skeletonPlayerLeft.Draw(_spriteBatch,new Vector2(_skeletonX,_skeletonY), SpriteEffects.None);
        else if (_skeletonDirection == 2)
            _skeletonPlayerDown.Draw(_spriteBatch, new Vector2(_skeletonX,_skeletonY), SpriteEffects.None);
        else if (_skeletonDirection == 3)
            _skeletonPlayerRight.Draw(_spriteBatch, new Vector2(_skeletonX,_skeletonY), SpriteEffects.None);
        #endregion
        _spriteBatch.Draw(_bushes, Vector2.Zero, Color.White);
        _spriteBatch.End();


        base.Draw(gameTime);
    }
    
}
