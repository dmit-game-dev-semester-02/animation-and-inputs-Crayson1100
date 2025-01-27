using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace assignment01_animation_and_inputs;
    
/// <summary>
/// Controls playback of a CelAnimationSequenceMultiRow.
/// </summary>
public class CelAnimationPlayerMultiRow
{
    private CelAnimationSequenceMultiRow CelAnimationSequenceMultiRow;
    private int celIndexX;
    private float celTimeElapsed;
    private Rectangle celSourceRectangle;

    /// <summary>
    /// Begins or continues playback of a CelAnimationSequenceMultiRow.
    /// </summary>
    public void Play(CelAnimationSequenceMultiRow CelAnimationSequenceMultiRow)
    {
        if (CelAnimationSequenceMultiRow == null)
        {
            throw new Exception("CelAnimationPlayerMultiRow.PlayAnimation received null CelAnimationSequenceMultiRow");
        }
        // If this animation is already running, do not restart it...
        if (CelAnimationSequenceMultiRow != this.CelAnimationSequenceMultiRow)
        {
            this.CelAnimationSequenceMultiRow = CelAnimationSequenceMultiRow;
            //situation ONE: One animation, multi rows
            celIndexX = 0;
            celTimeElapsed = 0.0f;

            celSourceRectangle.X = 0;
            celSourceRectangle.Y = CelAnimationSequenceMultiRow.RowToAnimate * CelAnimationSequenceMultiRow.CelHeight; //adjust this based on row to animate
            celSourceRectangle.Width = this.CelAnimationSequenceMultiRow.CelWidth;
            celSourceRectangle.Height = this.CelAnimationSequenceMultiRow.CelHeight;
        }
    }

    /// <summary>
    /// Update the state of the CelAnimationPlayerMultiRow.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    public void Update(GameTime gameTime)
    {
        if (CelAnimationSequenceMultiRow != null)
        {
            celTimeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (celTimeElapsed >= CelAnimationSequenceMultiRow.CelTime)
            {
                celTimeElapsed -= CelAnimationSequenceMultiRow.CelTime;

                // Advance the frame index looping as appropriate...
                celIndexX = (celIndexX + 1) % CelAnimationSequenceMultiRow.CelColumnCount;
                //celIndexY = (celIndexY + 1) % CelAnimationSequenceMultiRow.CelRowCount;
                

                celSourceRectangle.X = celIndexX * celSourceRectangle.Width;
            }
        }
    }

    /// <summary>
    /// Draws the current cel of the animation.
    /// </summary>
    public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
    {
        if (CelAnimationSequenceMultiRow != null)
        {
            spriteBatch.Draw(CelAnimationSequenceMultiRow.Texture, position, celSourceRectangle, Color.White, 0.0f, Vector2.Zero, 1.0f, spriteEffects, 0.0f);
        }
    }
}

