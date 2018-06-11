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
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();
          //  this.Juego_Load(null, null);
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
            private static readonly Random random = new Random();
            private static readonly HashSet<Tile> gridSearchBlacklist = new HashSet<Tile>();
            private Size gridSize;
            private int mines;
            private int flags;
            private Tile this[Point point] => (Tile)this.Controls[$"Tile_{point.X}_{point.Y}"];

            private bool createdMines;

            private void Tile_MouseDown(object sender, MouseEventArgs e )
            {
                Tile cell = (Tile) sender;
                if(!cell.Opened)
                {
                    switch (e.Button)
                    {
                        case MouseButtons.Left when !cell.Flagged:
                            if(!this.createdMines)
                            {
                                this.CreateMines(cell);
                            }
                            if (cell.Mined)
                            {
                                this.DisableTiles(true);
                            }
                            else
                            {
                                cell.TestAdjecentTiles();
                                gridSearchBlacklist.Clear();
                            }
                            break;
                        case MouseButtons.Right when this.flags > 0:
                            if(cell.Flagged)
                            {
                                cell.Flagged = false;
                                this.flags++;
                            }
                            else
                            {
                                cell.Flagged = true;
                                this.flags--;
                            }

                            break;

                    }
                }
                this.CheckWin();

            }

            internal void loadGrid (Size gridSize, int mines)
            {
                this.createdMines = false;
                this.Controls.Clear();
                this.gridSize = gridSize;
                this.mines = this.flags = mines;
                this.Size = new Size(gridSize.Width * Tile.LENGTH, gridSize.Height * Tile.LENGTH);
                for (int x = 0; x < gridSize.Width; x++)
                {
                    for (int y = 0; y < gridSize.Height; y++)
                    {
                        Tile cell = new Tile(x,y);
                        cell.MouseDown += Tile_MouseDown;
                        this.Controls.Add(cell);

                    }
                }

                foreach (Tile cell in this.Controls)
                {
                    cell.setAdjecentTiles();
                }
            }

            private void CreateMines(Tile safeTile)
            {
                int safeTileCount = safeTile.AdjecentTiles.Length + 1;
                Point[] usedPosition = new Point[this.mines + safeTileCount];
                usedPosition[0] = safeTile.GridPosition;
                for ( int c = 1; c < safeTileCount; c++ )
                {
                    usedPosition[c] = safeTile.AdjecentTiles[c - 1].GridPosition;
                }
                for (int c = safeTileCount; c < usedPosition.Length; c++)
                {
                    Point point = new Point(random.Next(this.gridSize.Width), random.Next(this.gridSize.Height));
                    if (!usedPosition.Contains(point))
                    {
                        this[point].Mine();
                        usedPosition[c] = point;
                    }
                    else
                    {
                        c--;
                    }
                    this.createdMines = true;
                }
            }

            private void DisableTiles(bool gameOver)
            {
                foreach (Tile cell in this.Controls)
                {
                    cell.MouseDown -= this.Tile_MouseDown;
                    if(gameOver)
                    {
                        cell.Image = !cell.Opened && cell.Mined && !cell.Flagged ? Resources.unnamed : cell.Flagged && !cell.Mined ? Resources.FFlag : cell.Image;  
                    }
                }
            }

            private void CheckWin()
            {
                if(this.flags != 0 && this.Controls.OfType<Tile>().Count(cell => cell.Opened || cell.Flagged) != this.gridSize.Width * this.gridSize.Height)
                {
                    return;
                }
                MessageBox.Show("You Win!");
                this.DisableTiles(false);
            }

            private class Tile:PictureBox
            {
                internal const int LENGTH = 25;
                private static readonly int[][] adjecentCoords =
                {
                    new [] {-1,-1}, new [] {0,-1}, new [] {1,-1},
                    new [] {1,0},new [] {1,1},new [] {0,1},
                    new [] {-1,1},new [] {-1,0}
                };



                internal Tile(int x, int y)
                {
                    //-When used to build strings with reference to other values. What previously had to be written as:
                    this.Name = $"Tile_{x}_{y}";
                    this.Location = new Point(x * LENGTH, y * LENGTH);
                    this.GridPosition = new Point(x, y);
                    this.Size = new Size(LENGTH, LENGTH);
                    this.Image = Resources.Tile;
                    this.SizeMode = PictureBoxSizeMode.Zoom;

                }

                private bool flagged;

                internal Tile[] AdjecentTiles
                {
                    get;
                    private set;
                }
                internal Point GridPosition
                {
                    get;
                }
                internal bool Opened
                {
                    get;
                    private set;
                }
                internal bool Mined
                {
                    get;
                    private set;
                }
                internal bool Flagged
                {
                    get => this.flagged;
                    set
                    {
                        this.flagged = value;
                        this.Image = value ? Resources.Flag : Resources.Tile;


                    }
                }

                private int adjecentMines => this.AdjecentTiles.Count(cell => cell.Mined);

                internal void setAdjecentTiles()
                {
                    TileGrid GameGrid = (TileGrid)this.Parent;
                    List<Tile> adjecentTiles = new List<Tile>(8);
                    foreach (int [] adjecentCoord in adjecentCoords)
                    {
                        Tile cell = GameGrid[new Point(this.GridPosition.X + adjecentCoord[0], this.GridPosition.Y + adjecentCoord[1])];
                        if (cell != null)
                        {
                            adjecentTiles.Add(cell);
                        }
                    }
                    this.AdjecentTiles = adjecentTiles.ToArray();
                }

                internal void TestAdjecentTiles()
                {
                    if(this.flagged || gridSearchBlacklist.Contains(this))
                    {
                        return;
                    }
                    gridSearchBlacklist.Add(this);
                    if(this.adjecentMines == 0)
                    {
                        foreach(Tile cell in this.AdjecentTiles)
                        {
                            cell.TestAdjecentTiles();
                        }
                    }
                    this.Open();
                }

                internal void Mine()
                {
                    this.Mined = true;
                    //this.Image = Resources.unnamed;
                }

                private void Open()
                {
                    this.Opened = true;
                    this.Image = (Image)Resources.ResourceManager.GetObject($"ETile_{this.adjecentMines}");
                }

            }

        }


    }
}
