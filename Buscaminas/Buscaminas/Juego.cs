using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Buscaminas.Properties;


namespace Buscaminas
{
    public partial class Juego : Form
    {
        public Juego()
        {
            InitializeComponent();
            this.Juego_Load(null, null);
        }

        private void Juego_Load(object sender, EventArgs e)
        {
            int x, y, mines;
            x = y = 20;
            mines = 10;
            this.gameGrid.loadGrid(new Size (x,y), mines);
            this.MaximumSize = this.MinimumSize = new Size(this.gameGrid.Width + 50, this.gameGrid.Height + 100);

        }

        private class TileGrid:Panel
        {
            private Size gridSize;
            private int mines;
            private int flags;


            private void Tile_MouseDown(object sender, MouseEventArgs e )
            {
                Tile cell = (Tile) sender;

            }

            internal void loadGrid (Size gridSize, int mines)
            {
                this.Controls.Clear();
                this.gridSize = gridSize;
                this.mines = this.flags = mines;
                this.Size = new Size(gridSize.Width * Tile.LENGTH, gridSize.Height * Tile.LENGTH);
                for (int c = 0; c < gridSize.Width; c++)
                {
                    for (int z = 0; z < gridSize.Height; z++)
                    {
                        Tile cell = new Tile(c,z);
                        cell.MouseDown += Tile_MouseDown;
                        this.Controls.Add(cell);

                    }
                }
            }

            private class Tile:PictureBox
            {
                internal const int LENGTH = 25;

                internal Point GridPosition
                {
                    get;
                }

                internal bool Opened
                {
                    get;
                    private set;
                }

                internal bool Flagged
                {
                    get;
                    private set;
                }


                internal Tile(int x, int y)
                {
                    // It is comes into its own when used to build strings with reference to other values. What previously had to be written as:
                    this.Name = $"Tile_{x}_{y}";
                    this.Location = new Point(x * LENGTH, y * LENGTH);
                    this.GridPosition = new Point(x, y);
                    this.Size = new Size(LENGTH, LENGTH);
                    this.Image = Resources.Tile;
                    this.SizeMode = PictureBoxSizeMode.Zoom;

                }
            }

        }


    }
}
