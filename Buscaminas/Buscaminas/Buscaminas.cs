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
            this.domainUpDown2.Text = DimensionXY.ToString(); ;
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            numeros = domainUpDown1.Text;
            Minas = Convert.ToInt16(numeros);
            if(!(Minas>min && Minas < minasmax))
            {

            }
        }

        private void domainUpDown2_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
