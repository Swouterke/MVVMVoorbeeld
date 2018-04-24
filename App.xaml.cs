using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MVVMVoorbeeld
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Model.TekstMetOpmaak mijnTekst = new Model.TekstMetOpmaak("Tik hier uw tekst...", false, false);
            ViewModel.TekstMetOpmaakVM vm = new ViewModel.TekstMetOpmaakVM(mijnTekst);
            View.TextBoxView mijnTextBoxView = new View.TextBoxView();

            mijnTextBoxView.Closing += vm.OnWindowClosing;
            mijnTextBoxView.DataContext = vm;
            mijnTextBoxView.Show();
        }
    }
}
