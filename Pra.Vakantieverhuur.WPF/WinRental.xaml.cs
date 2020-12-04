using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Pra.Vakantieverhuur.CORE.Entities;
using Pra.Vakantieverhuur.CORE.Services;

namespace Pra.Vakantieverhuur.WPF
{
    /// <summary>
    /// Interaction logic for WinRental.xaml
    /// </summary>
    public partial class WinRental : Window
    {

        private readonly Tenants tenants;
        private readonly Rentals rentals;
        private readonly Residence residence;

        public WinRental(Residence residence, Rentals rentals, Tenants tenants)
        {
            this.residence = residence;
            this.rentals = rentals;
            this.tenants = tenants;
            InitializeComponent();
        }

        private void ShowResidenceInfo()
        {
            txtResidenceName.Text = residence.ResidenceName;
            txtStreetAndNumber.Text = residence.StreetAndNumber;
            txtPostalCode.Text = residence.PostalCode;
            txtTown.Text = residence.Town;

            txtBasePrice.Text = residence.BasePrice.ToString("0.00");
            txtReducedPrice.Text = residence.ReducedPrice.ToString("0.00");
            txtDaysForReduction.Text = residence.DaysForReduction.ToString();
            txtGuarantee.Text = residence.Deposit.ToString("0.00");

            txtMaxPersons.Text = residence.MaxPersons.ToString();

            chkMicrowave.IsChecked = residence.Microwave;
            chkTV.IsChecked = residence.TV;

            chkWashingMachine.Visibility = chkDishwasher.Visibility = chkWoodStove.Visibility = chkPrivateSanitaryBlock.Visibility = Visibility.Hidden;

            if(residence is Caravan caravan)
            {
                chkPrivateSanitaryBlock.IsChecked = caravan.PrivateSanitaryBlock;
                chkPrivateSanitaryBlock.Visibility = Visibility.Visible;
            }

            if(residence is VacationHouse house)
            {
                chkDishwasher.IsChecked = house.DishWasher;
                chkWashingMachine.IsChecked = house.WashingMachine;
                chkWoodStove.IsChecked = house.WoodStove;
                chkWashingMachine.Visibility = chkDishwasher.Visibility = chkWoodStove.Visibility = Visibility.Visible;
            }

        }

        private void FillTenants()
        {
            cmbTenant.ItemsSource = tenants.AllTenants;
            cmbTenant.SelectedIndex = 0;
        }

        private void UpdateRentalDetails()
        {
            DateTime? start = dtpDateStart.SelectedDate;
            DateTime? end = dtpDateEnd.SelectedDate;

            if(start == null || end == null)
            {
                lblOverbooking.Content = "";

                lblNumberOfOvernightStays.Content = "";
                lblTotalToPay.Content = "";
                lblToBePaid.Content = "";
                return;
            }

            Rental rental = new Rental
            {
                DateStart = start.Value,
                DateEnd = end.Value,
                HolidayResidence = residence
            };

            if (rentals.IsOverbooking(rental))
            {
                lblOverbooking.Content = "OVERBOEKING!";
            }

            lblNumberOfOvernightStays.Content = rental.CalculateNumberOfNights();

            decimal totalPrice = rental.CalculateTotalPrice();
            lblTotalToPay.Content = totalPrice.ToString("0.00");

            try
            {
                decimal paid = decimal.Parse(txtPaid.Text);
                if(paid < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                decimal toPay = totalPrice - paid;
                if(toPay >= 0)
                {
                    lblToBePaid.Content = toPay.ToString("0.00");
                    lblToBePaid.Foreground = Brushes.Black;
                }
                else
                {
                    lblToBePaid.Content = "Te veel betaald!";
                    lblToBePaid.Foreground = Brushes.Red;
                }
            }
            catch
            {
                lblToBePaid.Content = "Vul een geldige waarde in bij \"Reeds betaald\"";
                lblToBePaid.Foreground = Brushes.Red;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillTenants();
            ShowResidenceInfo();
        }

        private void DtpDateStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateRentalDetails();
        }

        private void DtpDateEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateRentalDetails();
        }

        private void TxtPaid_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateRentalDetails();
        }
    }
}
