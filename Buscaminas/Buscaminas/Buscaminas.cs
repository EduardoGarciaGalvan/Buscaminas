using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Buscaminas
{
    public partial class Buscaminasmp : Form
    {
        public Buscaminasmp()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.domainUpDown1.Text = Minas.ToString();
            this.domainUpDown2.Text = DimensionXY.ToString();
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void Start_Click(object sender, EventArgs e)
        {
            Game game = new Game(DimensionXY, Minas);
            game.Show();
        }
    }
}
