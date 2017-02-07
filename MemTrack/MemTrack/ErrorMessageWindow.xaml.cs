using System;
using System.Windows;

namespace MemTrack
{
    /// <summary>
    ///     Interaction logic for ErrorMessageWindow.xaml
    /// </summary>
    public partial class ErrorMessageWindow
    {
        public ErrorMessageWindow(Exception e)
        {
            InitializeComponent();
            txtBlockErrorMessage.Text = e.Message + "\n" + e.HelpLink;
            ShowDialog();
        }

        public ErrorMessageWindow(string message)
        {
            InitializeComponent();
            txtBlockErrorMessage.Text = message;
            ShowDialog();
        }

        private void btnOkay_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}