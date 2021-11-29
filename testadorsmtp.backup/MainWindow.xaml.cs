using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestadorSmtp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TxtDestinatario.Focus();
        }

        private void ButtonTestar_Click(object sender, RoutedEventArgs e)
        {
            ButtonTestar.IsEnabled = false;
            TxtMensagem.Text = "Enviando...";
            MailMessage message = new MailMessage();
            SmtpClient smtp;
            message.To.Add(new MailAddress(TxtDestinatario.Text));
            message.From = new MailAddress(TxtUsuario.Text);
            message.Subject = "Teste de escoblu.net";
            message.Body = "Teste";
            smtp = new SmtpClient(TxtServidor.Text, int.Parse( TxtPorta.Text));
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            if ((bool)ChkAutenticar.IsChecked)
            {
                smtp.Credentials = new System.Net.NetworkCredential(TxtUsuario.Text, TxtSenha.Text);
            }
            if ((bool)ChkSSL.IsChecked)
            {
                smtp.EnableSsl = true;
            }
            smtp.Timeout = 10000;
            try
            {
                smtp.Send(message);
                TxtMensagem.Text = "Sucesso!";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
                TxtMensagem.Text = "Falhou!";
            }
            smtp.Dispose();
            message.Dispose();
            ButtonTestar.IsEnabled = true;
        }

        
    }
}
