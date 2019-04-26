using Autodesk.AutoCAD.Runtime;
using Autodesk.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

[assembly: CommandClass(typeof(FazHidraulicaCAD.Comandos))]
[assembly: ExtensionApplication(typeof(FazHidraulicaCAD.CmdPrincipal))]
namespace FazHidraulicaCAD
{
    public class CmdPrincipal : IExtensionApplication
    {
        public void Initialize()
        {
            RibbonControl ribbon = ComponentManager.Ribbon;
            if (ribbon != null)
            {
                RibbonTab rtab = ribbon.FindTab("FazHidraulica");
                if (rtab != null)
                {
                    ribbon.Tabs.Remove(rtab);
                }
                rtab = new RibbonTab();
                rtab.Title = "FazHidraulica";
                rtab.Id = "FazHidraulica";
                //Add the Tab
                ribbon.Tabs.Add(rtab);
                AddContent(rtab);
            }
        }

        static void AddContent(RibbonTab rtab)
        {
            rtab.Panels.Add(AdicionarPainelComandos());
            rtab.Panels.Add(AdicionarPainelAjuda());
        }

        static RibbonPanel AdicionarPainelComandos()
        {
            RibbonButton rbI = new RibbonButton();
            RibbonButton rbQ = new RibbonButton();
            RibbonPanelSource rps = new RibbonPanelSource();
            rps.Title = "Comandos";
            RibbonPanel rp = new RibbonPanel();
            rp.Source = rps;

            //--->>>> BOTÃO INSUMOS
            rbI.Name = "Adicionar os insumos da parte hidráulica";
            rbI.Orientation = System.Windows.Controls.Orientation.Vertical;
            Uri uriImage = new Uri(@"C:\Program Files\FazHidraulicaCAD\Código\Imagens\ribbon_insumos_hidraulica.png");
            BitmapImage bmpE = new BitmapImage(uriImage);
            rbI.LargeImage = bmpE;
            rbI.Image = bmpE;
            rbI.Size = RibbonItemSize.Large;
            rbI.ShowImage = true;
            rbI.ShowText = true;
            rbI.Text = "Insumos";
            rbI.CommandParameter = "InsumosHidraulica ";
            rbI.CommandHandler = new AdskCommandHandler();

            //--->>>> BOTÃO QUANTITATIVO
            rbQ.Name = "Adicionar os quantitativos da parte hidráulica";
            rbQ.Orientation = System.Windows.Controls.Orientation.Vertical;
            Uri uriImageH = new Uri(@"C:\Program Files\FazHidraulicaCAD\Código\Imagens\ribbon_quantitativo_hidraulico.png");
            BitmapImage bmpH = new BitmapImage(uriImageH);
            rbQ.LargeImage = bmpH;
            rbQ.Image = bmpH;
            rbQ.ShowImage = true;
            rbQ.Size = RibbonItemSize.Large;
            rbQ.ShowText = true;
            rbQ.Text = "Quantitativo";
            rbQ.CommandParameter = "QuantitativoHidraulica ";
            rbQ.CommandHandler = new AdskCommandHandler();

            //Add the Button to the Tab
            rps.Items.Add(rbI);
            rps.Items.Add(rbQ);

            return rp;
        }

        static RibbonPanel AdicionarPainelAjuda()
        {
            RibbonButton rbM = new RibbonButton();
            RibbonButton rbS = new RibbonButton();
            RibbonPanelSource rps = new RibbonPanelSource();
            rps.Title = "Ajuda";
            RibbonPanel rp = new RibbonPanel();
            rp.Source = rps;

            //--->>>> BOTÃO MANUAL
            rbM.Name = "Instruções sobre o plugin";
            rbM.Orientation = System.Windows.Controls.Orientation.Vertical;
            Uri uriImage = new Uri(@"C:\Program Files\FazHidraulicaCAD\Código\Imagens\ribbon_manual_hidraulica.png");
            BitmapImage bmpE = new BitmapImage(uriImage);
            rbM.LargeImage = bmpE;
            rbM.Image = bmpE;
            rbM.Size = RibbonItemSize.Large;
            rbM.ShowImage = true;
            rbM.ShowText = true;
            rbM.Text = "Manual";
            rbM.CommandParameter = "Guide ";
            rbM.CommandHandler = new AdskCommandHandler();

            //--->>>> BOTÃO SOBRE
            rbS.Name = "Informações sobre o desenvolvimento do plugin";
            rbS.Orientation = System.Windows.Controls.Orientation.Vertical;
            Uri uriImageH = new Uri(@"C:\Program Files\FazHidraulicaCAD\Código\Imagens\ribbon_info_hidraulica.png");
            BitmapImage bmpH = new BitmapImage(uriImageH);
            rbS.LargeImage = bmpH;
            rbS.Image = bmpH;
            rbS.ShowImage = true;
            rbS.Size = RibbonItemSize.Large;
            rbS.ShowText = true;
            rbS.Text = "Sobre";
            rbS.CommandParameter = "About ";
            rbS.CommandHandler = new AdskCommandHandler();

            //Add the Button to the Tab
            rps.Items.Add(rbM);
            rps.Items.Add(rbS);

            return rp;
        }

        public void Terminate()
        {
            throw new NotImplementedException();
        }

    }

    public class Comandos
    {
        [CommandMethod("InsumosHidraulica")]
        static public void InsumosEletrica()
        {
            Formularios.FormularioInsumos frmInsumos = new Formularios.FormularioInsumos();
            frmInsumos.Show();
        }

        [CommandMethod("QuantitativoHidraulica")]
        static public void QuantitativoEletrica()
        {
            Formularios.FormularioQuantitativo frmQuantitativo = new Formularios.FormularioQuantitativo();
            frmQuantitativo.Show();
            //MessageBox.Show("Aqui será exibido o quantitativo", "Olá", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        [CommandMethod("Guide")]
        static public void Guide()
        {
            Formularios.FormularioManual frmManual = new Formularios.FormularioManual();
            frmManual.Show();
            //MessageBox.Show("Aqui será exibido o manual de instruções", "Olá", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        [CommandMethod("About")]
        static public void About()
        {
            Formularios.FormularioSobre frmS = new Formularios.FormularioSobre();
            frmS.Show();
            //MessageBox.Show("Aqui será exibido dados do plugin", "Olá", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    public class AdskCommandHandler : System.Windows.Input.ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }
        //Num serve pra nada...
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
        public void Execute(object parameter)
        {
            //is from a Ribbon Button?
            RibbonButton ribBtn = parameter as RibbonButton;
            if (ribBtn != null)
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.SendStringToExecute((String)ribBtn.CommandParameter, true, false, true);
            //is from s Ribbon Textbox?
            RibbonTextBox ribTxt = parameter as RibbonTextBox;
            if (ribTxt != null)
                MessageBox.Show(ribTxt.TextValue);
        }

    }
}
