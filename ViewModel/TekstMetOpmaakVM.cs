using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.ComponentModel;

namespace MVVMVoorbeeld.ViewModel
{
    public class TekstMetOpmaakVM : ViewModelBase
    {
        private Model.TekstMetOpmaak opgemaakteTekst;
        public TekstMetOpmaakVM (Model.TekstMetOpmaak deTekst)
        {
            opgemaakteTekst = deTekst;
        }
        public string Inhoud
        {
            get { return opgemaakteTekst.Inhoud; }
            set { opgemaakteTekst.Inhoud = value;
                RaisePropertyChanged("Inhoud");
            }
        }
        public Boolean Schuin
        {
            get { return opgemaakteTekst.Schuin; }
            set { opgemaakteTekst.Schuin = value;
                RaisePropertyChanged("Schuin");
            }
        }
        public Boolean Vet
        {
            get { return opgemaakteTekst.Vet; }
            set { opgemaakteTekst.Vet = value;
                RaisePropertyChanged("Vet");}
        }
        public RelayCommand NieuwCommand
        {
            get { return new RelayCommand(NieuweTextBox); }
        }
        private void NieuweTextBox()
        {
            Inhoud = string.Empty;
            Vet = false;
            Schuin = false;
        }
        public RelayCommand OpslaanCommand
        {
            get { return new RelayCommand(OpslaanBestand); }
        }
        private void OpslaanBestand()
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = "tekstbox";
                dlg.DefaultExt = ".box";
                dlg.Filter = "Textbox documents |*.box";

                if (dlg.ShowDialog() == true)
                {
                    using (StreamWriter bestand = new StreamWriter(dlg.FileName))
                    {
                        bestand.WriteLine(Inhoud);
                        bestand.WriteLine(Vet.ToString());
                        bestand.WriteLine(Schuin.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Opslaan mislukt: " + ex.Message);
            }
        }
        public RelayCommand OpenCommand
        {
            get { return new RelayCommand(OpenBestand); }
        }
        private void OpenBestand()
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.FileName = "";
                dlg.DefaultExt = ".box";
                dlg.Filter = "Textbox documents |*.box";

                if (dlg.ShowDialog() == true)
                {
                    using (StreamReader bestand = new StreamReader(dlg.FileName))
                    {
                        Inhoud = bestand.ReadLine();
                        Vet = Convert.ToBoolean(bestand.ReadLine());
                        Schuin = Convert.ToBoolean(bestand.ReadLine());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Openen mislukt: " + ex.Message);
            }
        }
        public RelayCommand AfsluitenCommand
        {
            get { return new RelayCommand(AfsluitenApp); }
        }
        private void AfsluitenApp()
        {
            Application.Current.MainWindow.Close();
        }
        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show("Afsluiten", "Wilt u het programma sluiten?",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                e.Cancel = true;
        }
    }
}
