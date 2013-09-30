using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNAUtility {
    class Button {
        /// <summary>
        /// 標準のテクスチャ
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// オンマウス時のテクスチャ
        /// </summary>
        public Texture2D TextureOn { get; set; }

        /// <summary>
        /// クリック時のテクスチャ
        /// </summary>
        public Texture2D TextureClick { get; set; }

        Vector2 position = Vector2.Zero;
        /// <summary>
        /// Button の位置
        /// </summary>
        public Vector2 Position {
            get { return position; }
            set {
                if(position != value && VectorChage != null) VectorChage(this, EventArgs.Empty);
                position = value;
            }
        }

        /// <summary>
        /// Button の幅
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Button の高さ
        /// </summary>
        public int Height { get; set; }

        float scale = 1.0f;
        /// <summary>
        /// Button の大きさ
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
        /// Button の上にマウスが乗っているか
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
        /// Button がクリックされたか
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
        /// Button がアクティブか
        /// falseならUpdate()をスキップ
        /// </summary>
        public bool IsActive {
            get { return isActive; }
            set {
                isActive = true;
            }
        }

        bool isVisible = true;
        /// <summary>
        /// Button が可視であるか
        /// falseならDraw()をスキップ
        /// </summary>
        public bool IsVisible {
            get { return isVisible; }
            set {
                isVisible = value;
            }
        }

        /// <summary>
        /// 大きさが変更された時に発生するイベント
        /// </summary>
        public event EventHandler ScaleChange;

        /// <summary>
        /// オンマウス時に発生するイベント
        /// </summary>
        public event EventHandler MouseEnter;

        /// <summary>
        /// マウスがボタンから離れた時に発生するイベント
        /// </summary>
        public event EventHandler MouseLeave;

        /// <summary>
        /// クリックされたときに発生するイベント
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
        /// 更新処理
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public virtual void Update(GameTime gameTime) {
            if(!this.IsActive) return;

            // マウスの座標を取得
            var mouseState = Mouse.GetState();

            // 当たり判定
            if(mouseState.X >= Position.X && mouseState.X - 10 <= Position.X + this.Width
                && mouseState.Y >= Position.Y && mouseState.Y - 10 <= Position.Y + this.Height) {
                this.IsOnMouse = true;
                this.Scale = 1.05f;
            } else {
                this.IsOnMouse = false;
                this.Scale = 1.0f;
            }

            // クリック判定
            if(mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && this.IsOnMouse)
                this.IsClicked = true;
            else if(mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
                this.IsClicked = false;
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        public virtual void Draw(SpriteBatch spriteBatch) {
            this.Draw(spriteBatch, Color.White);
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="color">Color</param>
        public virtual void Draw(SpriteBatch spriteBatch, Color color) {
            if(!this.IsVisible) return;

            if(IsClicked) {
                // クリックされている状態であれば、TextureClickを元の大きさの0.99倍で描画
                this.Scale = 0.99f;
                spriteBatch.Draw(this.TextureClick, Position, null, color,
                    0.0f, Vector2.Zero, this.Scale, SpriteEffects.None, 0);
            } else if(IsOnMouse) {
                // オンマウスの状態であれば、TextureOnを元の大きさの1.05倍で描画
                float x = Position.X - ((float)Width * (this.Scale - 1.0f) / 2.0f);
                float y = Position.Y - ((float)Height * (this.Scale - 1.0f) / 2.0f);
                spriteBatch.Draw(this.TextureOn, new Vector2(x, y), null, color,
                    0.0f, Vector2.Zero, this.Scale, SpriteEffects.None, 0);
            } else {
                // 通常描画
                spriteBatch.Draw(this.Texture, this.Position, color);
            }
        }

    }
}