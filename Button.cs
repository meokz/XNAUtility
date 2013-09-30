using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNAUtility {
    class Button {
        /// <summary>
        /// �W���̃e�N�X�`��
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// �I���}�E�X���̃e�N�X�`��
        /// </summary>
        public Texture2D TextureOn { get; set; }

        /// <summary>
        /// �N���b�N���̃e�N�X�`��
        /// </summary>
        public Texture2D TextureClick { get; set; }

        Vector2 position = Vector2.Zero;
        /// <summary>
        /// Button �̈ʒu
        /// </summary>
        public Vector2 Position {
            get { return position; }
            set {
                if(position != value && VectorChage != null) VectorChage(this, EventArgs.Empty);
                position = value;
            }
        }

        /// <summary>
        /// Button �̕�
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Button �̍���
        /// </summary>
        public int Height { get; set; }

        float scale = 1.0f;
        /// <summary>
        /// Button �̑傫��
        /// </summary>
        public float Scale {
            get { return scale; }
            set {
                if(scale != value && ScaleChange != null) ScaleChange(this, EventArgs.Empty);
                scale = value;
            }
        }

        bool isOnMouse = false;
        /// <summary>
        /// Button �̏�Ƀ}�E�X������Ă��邩
        /// </summary>
        public bool IsOnMouse {
            get { return isOnMouse; }
            set {
                if(!isOnMouse && value && MouseEnter != null) MouseEnter(this, EventArgs.Empty);
                if(isOnMouse && !value && MouseLeave != null) MouseLeave(this, EventArgs.Empty);
                isOnMouse = value;
            }
        }

        bool isClicked = false;
        /// <summary>
        /// Button ���N���b�N���ꂽ��
        /// </summary>
        public bool IsClicked {
            get { return isClicked; }
            set {
                if(isClicked && !value && this.IsOnMouse && MouseClick != null) MouseClick(this, EventArgs.Empty);
                isClicked = value;
            }
        }

        bool isActive = true;
        /// <summary>
        /// Button ���A�N�e�B�u��
        /// false�Ȃ�Update()���X�L�b�v
        /// </summary>
        public bool IsActive {
            get { return isActive; }
            set {
                isActive = true;
            }
        }

        bool isVisible = true;
        /// <summary>
        /// Button �����ł��邩
        /// false�Ȃ�Draw()���X�L�b�v
        /// </summary>
        public bool IsVisible {
            get { return isVisible; }
            set {
                isVisible = value;
            }
        }

        /// <summary>
        /// �傫�����ύX���ꂽ���ɔ�������C�x���g
        /// </summary>
        public event EventHandler ScaleChange;

        /// <summary>
        /// �I���}�E�X���ɔ�������C�x���g
        /// </summary>
        public event EventHandler MouseEnter;

        /// <summary>
        /// �}�E�X���{�^�����痣�ꂽ���ɔ�������C�x���g
        /// </summary>
        public event EventHandler MouseLeave;

        /// <summary>
        /// �N���b�N���ꂽ�Ƃ��ɔ�������C�x���g
        /// </summary>
        public event EventHandler MouseClick;

        public event EventHandler ButtonSelect;

        public event EventHandler ButtonCancel;


        public event EventHandler VectorChage;


        public Button(Texture2D Texture) : this(Texture, Vector2.Zero) { }

        public Button(Texture2D Texture, Vector2 Position)
            : this(Texture, Texture, Texture, Position) {
        }

        public Button(Texture2D Texture, Texture2D TextureOn, Texture2D TextureClick)
            : this(Texture, TextureOn, TextureClick, Vector2.Zero) { }

        public Button(Texture2D Texture, Texture2D TextureOn, Texture2D TextureClick, Vector2 Position) {
            this.Texture = Texture;
            this.TextureOn = TextureOn;
            this.TextureClick = TextureClick;
            this.Position = Position;
            this.Width = this.Texture.Width;
            this.Height = this.Texture.Height;
        }

        /// <summary>
        /// �X�V����
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public virtual void Update(GameTime gameTime) {
            if(!this.IsActive) return;

            // �}�E�X�̍��W���擾
            var mouseState = Mouse.GetState();

            // �����蔻��
            if(mouseState.X >= Position.X && mouseState.X - 10 <= Position.X + this.Width
                && mouseState.Y >= Position.Y && mouseState.Y - 10 <= Position.Y + this.Height) {
                this.IsOnMouse = true;
                this.Scale = 1.05f;
            } else {
                this.IsOnMouse = false;
                this.Scale = 1.0f;
            }

            // �N���b�N����
            if(mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && this.IsOnMouse)
                this.IsClicked = true;
            else if(mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
                this.IsClicked = false;
        }

        /// <summary>
        /// �`�揈��
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        public virtual void Draw(SpriteBatch spriteBatch) {
            this.Draw(spriteBatch, Color.White);
        }

        /// <summary>
        /// �`�揈��
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="color">Color</param>
        public virtual void Draw(SpriteBatch spriteBatch, Color color) {
            if(!this.IsVisible) return;

            if(IsClicked) {
                // �N���b�N����Ă����Ԃł���΁ATextureClick�����̑傫����0.99�{�ŕ`��
                this.Scale = 0.99f;
                spriteBatch.Draw(this.TextureClick, Position, null, color,
                    0.0f, Vector2.Zero, this.Scale, SpriteEffects.None, 0);
            } else if(IsOnMouse) {
                // �I���}�E�X�̏�Ԃł���΁ATextureOn�����̑傫����1.05�{�ŕ`��
                float x = Position.X - ((float)Width * (this.Scale - 1.0f) / 2.0f);
                float y = Position.Y - ((float)Height * (this.Scale - 1.0f) / 2.0f);
                spriteBatch.Draw(this.TextureOn, new Vector2(x, y), null, color,
                    0.0f, Vector2.Zero, this.Scale, SpriteEffects.None, 0);
            } else {
                // �ʏ�`��
                spriteBatch.Draw(this.Texture, this.Position, color);
            }
        }

    }
}