using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FazHidraulicaCAD.Formularios
{
    public partial class FormularioManual : Form
    {
        Funcoes.LeituraPastasArquivos arquivos = new Funcoes.LeituraPastasArquivos();

        public FormularioManual()
        {
            InitializeComponent();
        }

        private void btAbrirPdf_Click(object sender, EventArgs e)
        {
            arquivos.AbrirManual();
        }

        private void btSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
