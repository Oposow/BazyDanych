using System;
using System.Collections.Generic;
using System.Linq;
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
using System.IO;

namespace SqlConferenceManagementSystemGenerator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChooseDirectoryClicked(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
                DirectoryButton.Content = dialog.SelectedPath;
        }

        private void GenerateQueries(object sender, RoutedEventArgs e)
        {
            int LastClientId;
            int LastParticipantId;
            int LastConferenceId;
            int LastWorkshopId;
            int LastConferenceReservationId;
            int LastWorkshopReservationId;
            int LastWorkshopLeaderId;
            string ChoosenPath = DirectoryButton.Content.ToString();

            if (!(Int32.TryParse(LastClientBox.Text, out LastClientId)
                && Int32.TryParse(LastConferenceBox.Text, out LastConferenceId)
                && Int32.TryParse(LastWorkshopBox.Text, out LastWorkshopId)
                && Int32.TryParse(LastParicipantBox.Text, out LastParticipantId)
                && Int32.TryParse(LastWorkshopReservationBox.Text, out LastWorkshopReservationId)
                && Int32.TryParse(LastConferenceReservationBox.Text, out LastConferenceReservationId)
                && Int32.TryParse(LastWorkshopLeaderBox.Text, out LastWorkshopLeaderId)
                && Directory.Exists(ChoosenPath)))
                {
                    MessageBox.Show("Poprawnie wypełnij wszystkie pola!", "Błąd walidacji danych");
                    return;
                }

        }
    }
}
