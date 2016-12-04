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
using SqlConferenceManagementSystemGenerator.Models;

namespace SqlConferenceManagementSystemGenerator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly GeneratorDataModel generatorData;

        List<ConferenceModel> conferences;
        List<ClientModel> clients;
        List<WorkshopLeaderModel> workshopLeaders;

        public MainWindow()
        {
            InitializeComponent();
            generatorData = new GeneratorDataModel();
            clients = new List<ClientModel>();
            workshopLeaders = new List<WorkshopLeaderModel>();
            ProgressBarGrid.Visibility = Visibility.Collapsed;
        }

        private void ChooseDirectoryClicked(object sender, RoutedEventArgs e)
        {

            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
                DirectoryButton.Content = dialog.SelectedPath;
        }

        private async void GenerateQueries(object sender, RoutedEventArgs e)
        {
            int ConferencesAmount;
            int LastClientId;
            int LastParticipantId;
            int LastConferenceId;
            int LastWorkshopId;
            int LastConferenceReservationId;
            int LastWorkshopReservationId;
            int LastWorkshopLeaderId;
            string ChoosenPath = DirectoryButton.Content.ToString();
            if (!(Int32.TryParse(ConferencesAmountBox.Text, out ConferencesAmount)
                && ConferencesAmount > 0
                && Int32.TryParse(LastClientBox.Text, out LastClientId)
                && Int32.TryParse(LastConferenceBox.Text, out LastConferenceId)
                && Int32.TryParse(LastWorkshopBox.Text, out LastWorkshopId)
                && Int32.TryParse(LastParicipantBox.Text, out LastParticipantId)
                && Int32.TryParse(LastWorkshopReservationBox.Text, out LastWorkshopReservationId)
                && Int32.TryParse(LastConferenceReservationBox.Text, out LastConferenceReservationId)
                && Int32.TryParse(LastWorkshopLeaderBox.Text, out LastWorkshopLeaderId)
                && FirstConferenceDate.SelectedDate.HasValue
                && Directory.Exists(ChoosenPath)))
                {
                    MessageBox.Show("Poprawnie wypełnij wszystkie pola!", "Błąd walidacji danych");
                    return;
                }
            ProgressBarLabel.Content = "Trwa generowanie danych";
            ProgressBarGrid.Visibility = Visibility.Visible;
            var _addressProvider = new AddressProvider(generatorData.Cities, generatorData.Streets);
            var _conferenceProvider = new ConferenceProvider(LastConferenceId, LastWorkshopId);
            var _participantProvider = new ParticipantProvider(LastParticipantId, generatorData.FirstNames, generatorData.LastNames, generatorData.Emails, _addressProvider);
            var _clientProvider = new ClientProvider(LastClientId, generatorData.CompanyNames, _addressProvider, generatorData.FirstNames, generatorData.LastNames, generatorData.Emails);
            var _workshopLeaderProvider = new WorkshopLeaderProvider(generatorData.FirstNames, generatorData.LastNames, generatorData.CompanyNames, LastWorkshopLeaderId, generatorData.Emails);

            conferences = _conferenceProvider.InitConferencesList(ConferencesAmount, FirstConferenceDate.SelectedDate.Value, _addressProvider);
            int neededClientsAmount = ConferencesAmount * 10 < 300 ? 300 : ConferencesAmount * 10;
            for (int i = 0; i < neededClientsAmount; i++)
                clients.Add(_clientProvider.CreateClient());

            var _conferenceReservationProvider = new ConferenceReservationsProvider(LastConferenceReservationId, clients, _participantProvider);
            var _workshopReservationProvider = new WorkshopReservationsProvider(LastWorkshopReservationId, _participantProvider, clients);
            await Task.Run(() =>
            {
                foreach (var con in conferences)
                    foreach (var conday in con.Days)
                        foreach (var work in conday.Workshops)
                            workshopLeaders.Add(_workshopLeaderProvider.CreateLeader());

                foreach (var conf in conferences)
                    for (int i = 0; i < conf.ConferenceLength; i++)
                        conf.Days[i].Reservations = _conferenceReservationProvider.CreateConferenceDayReservations(conf.Days[i].ParticipantsNumber, conf, i + 1);

                foreach (var conf in conferences)
                    foreach (var day in conf.Days)
                        foreach (var workshop in day.Workshops)
                            workshop.Reservations = _workshopReservationProvider.CreateWorkshopReservations(workshop, day);
            });
            ProgressBarGrid.Visibility = Visibility.Collapsed;
            MessageBox.Show("Pomyślnie wygenerowano dane!");
            Application.Current.Shutdown();
        }
    }
}
