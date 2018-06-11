namespace Buscaminas
{
    partial class Game
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game));
            this.gameGrid = new Buscaminas.Game.TileGrid();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gameGrid
            // 
            this.gameGrid.Location = new System.Drawing.Point(10, 50);
            this.gameGrid.Name = "gameGrid";
            this.gameGrid.Size = new System.Drawing.Size(260, 241);
            this.gameGrid.TabIndex = 0;
            // 
            // btnNewGame
            // 
            this.btnNewGame.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnNewGame.Location = new System.Drawing.Point(111, 12);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(75, 23);
            this.btnNewGame.TabIndex = 1;
            this.btnNewGame.Text = "Start";
            this.btnNewGame.UseVisualStyleBackColor = true;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 303);
            this.Controls.Add(this.btnNewGame);
            this.Controls.Add(this.gameGrid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(600, 650);
            this.MinimumSize = new System.Drawing.Size(300, 350);
            this.Name = "Game";
            this.Text = "Juego";
            this.Load += new System.EventHandler(this.Juego_Load);
            this.ResumeLayout(false);

        }

        private TileGrid gameGrid;


        #endregion

        private System.Windows.Forms.Button btnNewGame;
    }
}