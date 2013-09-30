using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNAUtility {
    class TextBox {

        /// <summary>
        /// テキスト
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// TextBox の位置
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// TextBox のバックテクスチャ
        /// </summary>
        public Texture2D Background;

        /// <summary>
        /// 余白
        /// </summary>
        public int Margin { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Text">描画するテキスト</param>
        /// <param name="Position">位置</param>
        /// <param name="Background">バックテクスチャ</param>
        public TextBox(string Text, Vector2 Position, Texture2D Background) :
            this(Text, Position, Background, 15) { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Text">描画するテキスト</param>
        /// <param name="Position">位置</param>
        /// <param name="Background">バックテクスチャ</param>
        /// <param name="Margin">余白</param>
        public TextBox(string Text, Vector2 Position, Texture2D Background, int Margin) {
            this.Text = Text;
            this.Position = Position;
            this.Background = Background;
            this.Margin = Margin;
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Color color) {
            var messageSize = font.MeasureString(Text);

            Rectangle back = new Rectangle(-this.Margin, -this.Margin, (int)messageSize.X + this.Margin * 2, (int)messageSize.Y + this.Margin * 2);
            back.Offset((int)Position.X, (int)Position.Y);

            spriteBatch.Draw(Background, back, color);
            spriteBatch.DrawString(font, Text, this.Position, color);
        }

    }
}
