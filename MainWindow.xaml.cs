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
using SqlConferenceManagementSystemGenerator.Providers;

namespace SqlConferenceManagementSystemGenerator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> FirstNames = new List<string>();
        public List<string> CompanyNames = new List<string>();
        public List<string> LastNames = new List<string>();
        public List<string> CitiesNames = new List<string>();
        public List<string> StreetNames = new List<string>();
        public AddressProvider _addressProvider;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                var currentDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                if (File.Exists(System.IO.Path.Combine(currentDirectory, "FirstNames.txt")))
                {
                    var tmp = File.ReadLines(System.IO.Path.Combine(currentDirectory, "FirstNames.txt")).ToList();
                    tmp.ForEach(x => FirstNames.Add(new StringBuilder().Append(x.First()).Append(x.Substring(1).ToLower()).ToString()));
                }
                else
                    throw new FileNotFoundException();
                if (File.Exists(System.IO.Path.Combine(currentDirectory, "LastNames.txt")))
                {
                    var tmp = File.ReadLines(System.IO.Path.Combine(currentDirectory, "LastNames.txt")).ToList();
                    tmp.ForEach(x => LastNames.Add(new StringBuilder().Append(x.First()).Append(x.Substring(1).ToLower()).ToString()));
                }
                else
                    throw new FileNotFoundException();
                if (File.Exists(System.IO.Path.Combine(currentDirectory, "CompanyNames.txt")))
                    CompanyNames = File.ReadLines(System.IO.Path.Combine(currentDirectory, "CompanyNames.txt")).ToList();
                else
                    throw new FileNotFoundException();
                if (File.Exists(System.IO.Path.Combine(currentDirectory, "CitiesNames.txt")))
                    CompanyNames = File.ReadLines(System.IO.Path.Combine(currentDirectory, "CitiesNames.txt")).ToList();
                else
                    throw new FileNotFoundException();
                if (File.Exists(System.IO.Path.Combine(currentDirectory, "StreetNames.txt")))
                    CompanyNames = File.ReadLines(System.IO.Path.Combine(currentDirectory, "StreetNames.txt")).ToList();
                else
                    throw new FileNotFoundException();

                _addressProvider = new AddressProvider(CitiesNames, StreetNames);
            }
            catch(FileNotFoundException)
            {
                MessageBox.Show("Brak pliku(ów) z danymi");
                Application.Current.Shutdown();
            }
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
            int ConferencesAmount;
            int LastClientId;
            int LastParticipantId;
            int LastConferenceId;
            int LastWorkshopId;
            int LastConferenceReservationId;
            int LastWorkshopReservationId;
            int LastWorkshopLeaderId;
            int ParticipantsAmount;
            string ChoosenPath = DirectoryButton.Content.ToString();

            if (!(Int32.TryParse(ConferencesAmountBox.Text, out ConferencesAmount)
                && Int32.TryParse(LastClientBox.Text, out LastClientId)
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
