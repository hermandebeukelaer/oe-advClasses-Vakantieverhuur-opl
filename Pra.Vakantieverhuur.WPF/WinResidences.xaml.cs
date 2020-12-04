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

namespace Pra.Vakantieverhuur.WPF
{
    /// <summary>
    /// Interaction logic for WinResidences.xaml
    /// </summary>
    public partial class WinResidences : Window
    {

        public Residence Residence { get; private set; }

        public WinResidences()
        {
            InitializeComponent();
            lblTitle.Content = "Verblijf aanmaken";
            cmbKindOfResidence.IsEnabled = true;
            ToggleCheckBoxes(false);
        }

        public WinResidences(Residence residence)
        {
            Residence = residence;
            InitializeComponent();
            lblTitle.Content = "Verblijf editeren";
            cmbKindOfResidence.IsEnabled = false;
        }

        private void ShowDetails()
        {
            if(Residence != null)
            {
                cmbKindOfResidence.SelectedIndex = Residence is VacationHouse ? 0 : 1;
                chkRentable.IsChecked = Residence.IsRentable;

                txtHouseName.Text = Residence.ResidenceName;
                txtStreetAndNumber.Text = Residence.StreetAndNumber;
                txtPastalCode.Text = Residence.PostalCode;
                txtTown.Text = Residence.Town;

                txtBasePrice.Text = Residence.BasePrice.ToString("0.00");
                txtReducedPrice.Text = Residence.ReducedPrice.ToString("0.00");
                txtDaysForReduction.Text = Residence.DaysForReduction.ToString();
                txtGuarantee.Text = Residence.Deposit.ToString("0.00");

                txtMaxPersons.Text = Residence.MaxPersons.ToString();

                chkMicrowave.IsChecked = Residence.Microwave;
                chkTV.IsChecked = Residence.TV;

                if (Residence is Caravan caravan)
                {
                    ShowDetails(caravan);
                }

                if (Residence is VacationHouse house)
                {
                    ShowDetails(house);
                }
            }

        }

        private void ShowDetails(Caravan caravan)
        {
            ToggleCheckBoxes(true);
            chkPrivateSanitaryBlock.IsChecked = caravan.PrivateSanitaryBlock;
        }

        private void ShowDetails(VacationHouse house)
        {
            ToggleCheckBoxes(false);
            chkDishwasher.IsChecked = house.DishWasher;
            chkWashingMachine.IsChecked = house.WashingMachine;
            chkWoodStove.IsChecked = house.WoodStove;
        }

        private void ToggleCheckBoxes(bool caravan)
        {
            chkDishwasher.IsEnabled = chkWashingMachine.IsEnabled = chkWoodStove.IsEnabled = chkPrivateSanitaryBlock.IsEnabled = false;
            if (caravan)
            {
                chkPrivateSanitaryBlock.IsEnabled = true;
            }
            else
            {
                chkDishwasher.IsEnabled = chkWashingMachine.IsEnabled = chkWoodStove.IsEnabled = true;
            }
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
            ShowDetails();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            // start with parsing all values (including error checks)
            bool errors = false;

            if (string.IsNullOrWhiteSpace(txtHouseName.Text))
            {
                errors = true;
                ShowError(txtHouseName);
            }

            if (string.IsNullOrWhiteSpace(txtStreetAndNumber.Text))
            {
                errors = true;
                ShowError(txtStreetAndNumber);
            }

            if (string.IsNullOrWhiteSpace(txtPastalCode.Text))
            {
                errors = true;
                ShowError(txtPastalCode);
            }

            if (string.IsNullOrWhiteSpace(txtTown.Text))
            {
                errors = true;
                ShowError(txtTown);
            }

            decimal basePrice = 0m;
            decimal reducedPrice = 0m;
            decimal deposit = 0m;

            try
            {
                basePrice = decimal.Parse(txtBasePrice.Text);
                if (basePrice < 0) throw new ArgumentOutOfRangeException();
            }
            catch
            {
                errors = true;
                ShowError(txtBasePrice);
            }

            try
            {
                reducedPrice = decimal.Parse(txtReducedPrice.Text);
                if (reducedPrice < 0) throw new ArgumentOutOfRangeException();
            }
            catch
            {
                errors = true;
                ShowError(txtReducedPrice);
            }

            try
            {
                deposit = decimal.Parse(txtGuarantee.Text);
                if (deposit < 0) throw new ArgumentOutOfRangeException();
            }
            catch
            {
                errors = true;
                ShowError(txtGuarantee);
            }

            byte numDaysBasePrice = 0;
            try
            {
                numDaysBasePrice = byte.Parse(txtDaysForReduction.Text);
            }
            catch
            {
                errors = true;
                ShowError(txtDaysForReduction);
            }

            int maxPersons = 0;
            try
            {
                maxPersons = int.Parse(txtMaxPersons.Text);
                if (maxPersons < 0) throw new ArgumentOutOfRangeException();
            }
            catch
            {
                errors = true;
                ShowError(txtMaxPersons);
            }

            if (!errors)
            {
                // when adding a new residence, we must create the object first
                if (Residence == null)
                {
                    ComboBoxItem selectedKind = (ComboBoxItem)cmbKindOfResidence.SelectedItem;
                    if (selectedKind.Content.ToString() == "Vakantiehuisjes")
                    {
                        Residence = new VacationHouse();
                    }
                    else
                    {
                        Residence = new Caravan();
                    }
                }

                // set/update all property values
                Residence.ResidenceName = txtHouseName.Text;
                Residence.StreetAndNumber = txtStreetAndNumber.Text;
                Residence.PostalCode = txtPastalCode.Text;
                Residence.Town = txtTown.Text;

                Residence.IsRentable = chkRentable.IsChecked == true;
                Residence.Microwave = chkMicrowave.IsChecked;
                Residence.TV = chkTV.IsChecked;

                Residence.BasePrice = basePrice;
                Residence.ReducedPrice = reducedPrice;
                Residence.DaysForReduction = numDaysBasePrice;
                Residence.Deposit = deposit;
                Residence.MaxPersons = maxPersons;

                if (Residence is Caravan caravan)
                {
                    caravan.PrivateSanitaryBlock = chkPrivateSanitaryBlock.IsChecked;
                }

                if (Residence is VacationHouse house)
                {
                    house.DishWasher = chkDishwasher.IsChecked;
                    house.WashingMachine = chkWashingMachine.IsChecked;
                    house.WoodStove = chkWoodStove.IsChecked;
                }

                Close();
            }
            
        }

        private void CmbKindOfResidence_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(IsLoaded)
            {
                ComboBoxItem selectedKind = (ComboBoxItem)cmbKindOfResidence.SelectedItem;
                if (selectedKind.Content.ToString() == "Caravans")
                {
                    ToggleCheckBoxes(true);
                }
                else
                {
                    ToggleCheckBoxes(false);
                }
            }
            
        }

        private void ResidenceDetails_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowOk((Control)sender);
        }
    }
}
