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
using Pra.Vakantieverhuur.CORE.Services;
using Pra.Vakantieverhuur.CORE.Entities;

namespace Pra.Vakantieverhuur.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly Residences residences = new Residences();
        private readonly Rentals rentals = new Rentals();
        private readonly Tenants tenants = new Tenants();

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Methods

        private void UpdateResidences()
        {
            if (cmbKindOfResidence.SelectedItem != null)
            {
                ComboBoxItem selectedKind = (ComboBoxItem)cmbKindOfResidence.SelectedItem;
                switch (selectedKind.Content)
                {
                    case "Vakantiehuisjes":
                        lstResidences.ItemsSource = residences.GetAllVacationHouses();
                        break;
                    case "Caravans":
                        lstResidences.ItemsSource = residences.GetAllCaravans();
                        break;
                    default:
                        lstResidences.ItemsSource = residences.AllResidences;
                        break;
                }
            }
            else
            {
                lstResidences.ItemsSource = residences.AllResidences;
            }
            lstResidences.Items.Refresh();
        }

        private void UpdateRentals()
        {
            dgrRentals.Items.Clear();

            Residence selectedResidence = (Residence)lstResidences.SelectedItem;
            if(selectedResidence != null)
            {
                foreach (Rental rental in rentals.AllRentals)
                {
                    if(rental.HolidayResidence == selectedResidence)
                    {
                        dgrRentals.Items.Add(rental);
                    }
                }
            }
        }

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateResidences();
        }

        private void CmbKindOfResidence_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateResidences();
        }

        private void BtnResidenceDelete_Click(object sender, RoutedEventArgs e)
        {
            if(lstResidences.SelectedItem != null)
            {
                Residence selected = (Residence)lstResidences.SelectedItem;
                residences.Remove(selected);
                UpdateResidences();
            }
        }

        private void BtnResidenceEdit_Click(object sender, RoutedEventArgs e)
        {
            if(lstResidences.SelectedItem != null)
            {
                Residence residence = (Residence)lstResidences.SelectedItem;
                Window residenceWindow = new WinResidences(residence);
                residenceWindow.ShowDialog();
                UpdateResidences();
            }
        }

        private void BtnResidenceNew_Click(object sender, RoutedEventArgs e)
        {
            WinResidences residenceWindow = new WinResidences();
            residenceWindow.ShowDialog();
            if(residenceWindow.Residence != null)
            {
                residences.Add(residenceWindow.Residence);
            }
            UpdateResidences();
        }

        private void BtnNewRental_Click(object sender, RoutedEventArgs e)
        {
            if(lstResidences.SelectedItem != null)
            {
                Residence residence = (Residence)lstResidences.SelectedItem;

                if(!residence.IsRentable)
                {
                    MessageBox.Show($"Verblijf {residence} momenteel niet verhuurbaar...");
                    return;
                }

                WinRental rentalWindow = new WinRental(residence, rentals, tenants);
                rentalWindow.ShowDialog();
                if(rentalWindow.Rental != null)
                {
                    rentals.Add(rentalWindow.Rental);
                }
                UpdateRentals();
            }
        }

        private void BtnRentalDelete_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = (Button)sender;
            Rental rental = (Rental)deleteButton.DataContext;

            MessageBoxResult result = MessageBox.Show(
                "Ben je zeker dat je deze huurovereenkomst wil verwijderen?", "Huurovereenkomst verwijderen?",
                MessageBoxButton.YesNo, MessageBoxImage.Question
            );

            if(result == MessageBoxResult.Yes)
            {
                rentals.Remove(rental);
                UpdateRentals();
            }
        }

        private void BtnRentalEdit_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = (Button)sender;
            Rental rental = (Rental)deleteButton.DataContext;

            WinRental rentalWindow = new WinRental(rental, rentals, tenants);
            rentalWindow.ShowDialog();
            UpdateRentals();

        }

        private void LstResidences_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateRentals();
        }
    }
}
