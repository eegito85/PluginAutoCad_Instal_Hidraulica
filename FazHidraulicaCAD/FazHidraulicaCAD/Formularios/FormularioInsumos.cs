using FazHidraulicaCAD.Funcoes;
using Microsoft.WindowsAPICodePack.Shell;
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
    public partial class FormularioInsumos : Form
    {
        LeituraPastasArquivos folders = new LeituraPastasArquivos();
        ComandosCAD cad = new ComandosCAD();
        List<string> listaPastasCategorias = new List<string>();
        List<string> listaBlocosPorCategoria = new List<string>();

        public FormularioInsumos()
        {
            InitializeComponent();
        }

        private void FormularioInsumos_Load(object sender, EventArgs e)
        {
            listaPastasCategorias = folders.LerPastasCategorias();
            CarregarCategorias(listaPastasCategorias);
        }

        private void ComboBoxInsumosH_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string categoria = ComboBoxInsumosH.SelectedItem.ToString();
                listaBlocosPorCategoria = folders.LerBlocosPorCategoria(categoria);
                CarregarBlocos(listaBlocosPorCategoria);
            }
            catch { }
        }

        private void ListBoxInsumosH_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show("Entrou aqui");
                string nomeCategoria = ComboBoxInsumosH.SelectedItem.ToString();
                string nomeBloco = ListBoxInsumosH.SelectedItem.ToString();
                string caminho = folders.RetornaCaminhoCompletoArquivo(nomeCategoria, nomeBloco);
                GerarThumbnailDoArquivo(caminho);
            }
            catch { }
        }

        private void BtInserirInsumosH_Click(object sender, EventArgs e)
        {
            try
            {
                string nomeCategoria = ComboBoxInsumosH.SelectedItem.ToString();
                string nomeBloco = ListBoxInsumosH.SelectedItem.ToString();
                string caminho = folders.RetornaCaminhoCompletoArquivo(nomeCategoria, nomeBloco);
                this.Hide();
                cad.InserirBlocoNoDesenho(caminho);
                this.Show();
            }
            catch { }
        }


        private void CarregarCategorias(List<string> lista)
        {
            ComboBoxInsumosH.Items.Clear();
            if (lista != null)
            {
                foreach (string categoria in lista)
                {
                    ComboBoxInsumosH.Items.Add(categoria);
                }
            }
        }

        private void CarregarBlocos(List<string> lista)
        {
            ListBoxInsumosH.Items.Clear();
            foreach (string bloco in lista)
            {
                ListBoxInsumosH.Items.Add(bloco);
            }
        }

        private void GerarThumbnailDoArquivo(string caminhoArquivo)
        {
            ShellFile shellFile = ShellFile.FromFilePath(caminhoArquivo);
            ShellThumbnail thumbnail = shellFile.Thumbnail;
            Image image = thumbnail.LargeBitmap;
            PictureBoxHidraulica.Image = image;
            PictureBoxHidraulica.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public bool ThumbnailCallback()
        {
            return false;
        }





    }
}
