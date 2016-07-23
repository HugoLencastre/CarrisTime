using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Email;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CarrisTime
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private String tipo { get; set; }
        private String bus { get; set; }
        private String destino { get; set; }
        private String codigo { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            codigo = "C 00605";
            ChooseTipe();
        }

        #region design

        private void Clean()
        {
            TelaPrincipal.Children.Clear();
        }
      
        private void ChooseTipe()
        {
            TelaPrincipal.Children.Add(makeSettingsButton());
            /*
            Image logo = new Image();
            ImageSource logoimg = new BitmapImage(new Uri("Assets/carris_logo.jpeg"));
            logo.Source = logoimg;
            
            TelaPrincipal.Children.Add(logo);
            */

            ListBox listbox = new ListBox();
            listbox.Margin = new Thickness(5,50,5,0);

            Button electrico = new Button();
            electrico.Content = "Electrico";
            electrico.Click += Electrico;
            electrico.Width = 150;
            electrico.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

            Button autocarro = new Button();
            autocarro.Content = "Autocarro";
            autocarro.Click += Autocarro;
            autocarro.Width = 150;
            autocarro.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

            Button elevador = new Button();
            elevador.Content = "Elevador";
            elevador.Click += Elevador;
            elevador.Width = 150;
            elevador.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

            listbox.Items.Add(electrico);
            listbox.Items.Add(autocarro);
            listbox.Items.Add(elevador);

            TelaPrincipal.Children.Add(listbox);
        }

        private Button makeSettingsButton()
        {
            Button def = new Button();
            def.Name = "Def";
            def.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            def.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            def.VerticalAlignment = VerticalAlignment.Top;
            def.HorizontalAlignment = HorizontalAlignment.Left;
            def.Height = 50;
            def.Width = 50;

            def.Content = new SymbolIcon
            {
                Symbol = Symbol.Setting
            };

            return def;
        }

        #endregion

        private void Electrico(object sender, RoutedEventArgs e)
        {
            tipo = "Electrico";
        }

        private async void Autocarro(object sender, RoutedEventArgs e)
        {
            tipo = "Autocarro";
            await ComposeEmail(codigo);
        }

        private void Elevador(object sender, RoutedEventArgs e)
        {
            tipo = "Elevador";
        }

        #region mail

        private async Task ComposeEmail(string subject)
        {
            var emailMessage = new EmailMessage();
            emailMessage.To.Add(new EmailRecipient("sms@carris.pt"));
            emailMessage.Subject = subject;
            await EmailManager.ShowComposeNewEmailAsync(emailMessage);
        }

        #endregion
    }
}
