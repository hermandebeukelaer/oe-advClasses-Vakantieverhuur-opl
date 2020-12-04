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

        public Rental Rental { get; private set; }

        public WinRental(Residence residence, Rentals rentals, Tenants tenants)
        {
            this.residence = residence;
            this.rentals = rentals;
            this.tenants = tenants;
            InitializeComponent();
        }

        public WinRental(Rental rental, Rentals rentals, Tenants tenants) : this(rental.HolidayResidence, rentals, tenants)
        {
            Rental = rental;
            FillRentalDetails();
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

        private void FillRentalDetails()
        {

            cmbTenant.SelectedItem = Rental.HolidayTenant;
            dtpDateStart.SelectedDate = Rental.DateStart;
            dtpDateEnd.SelectedDate = Rental.DateEnd;
            txtPaid.Text = Rental.Paid.ToString("0.00");
            InferAdditionalRentalDetails();

        }

        private void InferAdditionalRentalDetails()
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

            if (rentals.IsOverbooking(rental, Rental))
            {
                lblOverbooking.Content = "OVERBOEKING!";
                ShowError(lblOverbooking);
            }

            lblNumberOfOvernightStays.Content = rental.CalculateNumberOfNights();

            decimal totalPrice = rental.CalculateTotalPrice();
            lblTotalToPay.Content = totalPrice.ToString("0.00");

            try
            {
                decimal paid = ParsePaid(totalPrice);
                decimal toPay = totalPrice - paid;
                lblToBePaid.Content = toPay.ToString("0.00");
                ShowOk(lblToBePaid);
                ShowOk(txtPaid);
            }
            catch
            {
                lblToBePaid.Content = "???";
                ShowError(lblToBePaid);
                ShowError(txtPaid);
            }
        }

        private decimal ParsePaid(decimal totalPrice)
        {
            decimal paid = decimal.Parse(txtPaid.Text);
            if (paid < 0)
            {
                throw new ArgumentOutOfRangeException(null, "Betaald bedrag kan niet kleiner dan nul zijn.");
            }
            if(paid > totalPrice)
            {
                throw new ArgumentOutOfRangeException(null, "Te veel betaald.");
            }
            return paid;
        }

        private void ShowError(Control control)
        {
            control.Foreground = Brushes.Red;
            control.BorderBrush = Brushes.Red;
        }

        private void ShowOk(Control control)
        {
            control.Foreground = Brushes.Black;
            control.BorderBrush = Brushes.Black;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillTenants();
            ShowResidenceInfo();
        }

        private void DtpDateStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            InferAdditionalRentalDetails();
            ShowOk(dtpDateStart);
        }

        private void DtpDateEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            InferAdditionalRentalDetails();
            ShowOk(dtpDateEnd);
        }

        private void TxtPaid_TextChanged(object sender, TextChangedEventArgs e)
        {
            InferAdditionalRentalDetails();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            // error handling
            bool errors = false;

            if(lblOverbooking.Content.ToString() != "")
            {
                // do not save double booked rentals!
                errors = true;
            }

            if(dtpDateStart.SelectedDate == null)
            {
                errors = true;
                ShowError(dtpDateStart);
            }
            else
            {
                ShowOk(dtpDateStart);
            }

            if (dtpDateEnd.SelectedDate == null)
            {
                errors = true;
                ShowError(dtpDateEnd);
            }
            else
            {
                ShowOk(dtpDateEnd);
            }

            decimal paid = 0m;
            decimal toPay = 0m;
            try
            {
                decimal totalPrice = decimal.Parse(lblTotalToPay.Content.ToString());
                paid = ParsePaid(totalPrice);
                toPay = totalPrice - paid;
                ShowOk(txtPaid);
            }
            catch
            {
                errors = true;
                ShowError(txtPaid);
            }

            if (!errors)
            {

                if(Rental == null)
                {
                    // creating new rental
                    Rental = new Rental();
                }

                Rental.HolidayTenant = (Tenant)cmbTenant.SelectedItem;

                Rental.DateStart = dtpDateStart.SelectedDate.Value;
                Rental.DateEnd = dtpDateEnd.SelectedDate.Value;

                Rental.HolidayResidence = residence;

                Rental.IsDepositPaid = chkDepositPaid.IsChecked == true;

                Rental.Paid = paid;
                Rental.ToPay = toPay;

                Close();
            }

        }
    }
}
