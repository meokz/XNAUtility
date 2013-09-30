using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNAUtility {
    class TextBox {

        /// <summary>
        /// �e�L�X�g
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// TextBox �̈ʒu
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// TextBox �̃o�b�N�e�N�X�`��
        /// </summary>
        public Texture2D Background;

        /// <summary>
        /// �]��
        /// </summary>
        public int Margin { get; set; }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="Text">�`�悷��e�L�X�g</param>
        /// <param name="Position">�ʒu</param>
        /// <param name="Background">�o�b�N�e�N�X�`��</param>
        public TextBox(string Text, Vector2 Position, Texture2D Background) :
            this(Text, Position, Background, 15) { }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="Text">�`�悷��e�L�X�g</param>
        /// <param name="Position">�ʒu</param>
        /// <param name="Background">�o�b�N�e�N�X�`��</param>
        /// <param name="Margin">�]��</param>
        public TextBox(string Text, Vector2 Position, Texture2D Background, int Margin) {
            this.Text = Text;
            this.Position = Position;
            this.Background = Background;
            this.Margin = Margin;
        }

        /// <summary>
        /// �`�揈��
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
