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

            bool addingNewResidence = (Residence == null);
            if(addingNewResidence)
            {

                ComboBoxItem selectedKind = (ComboBoxItem) cmbKindOfResidence.SelectedItem;
                if(selectedKind.Content.ToString() == "Vakantiehuisjes")
                {
                    Residence = new VacationHouse();
                }
                else
                {
                    Residence = new Caravan();
                }
            }

            try
            {
                Residence.IsRentable = chkRentable.IsChecked == true;

                Residence.ResidenceName = txtHouseName.Text;
                Residence.StreetAndNumber = txtStreetAndNumber.Text;
                Residence.PostalCode = txtPastalCode.Text;
                Residence.Town = txtTown.Text;

                Residence.BasePrice = decimal.Parse(txtBasePrice.Text);
                Residence.ReducedPrice = decimal.Parse(txtReducedPrice.Text);
                Residence.DaysForReduction = byte.Parse(txtDaysForReduction.Text);
                Residence.Deposit = decimal.Parse(txtGuarantee.Text);

                Residence.MaxPersons = int.Parse(txtMaxPersons.Text);

                Residence.Microwave = chkMicrowave.IsChecked;
                Residence.TV = chkTV.IsChecked;

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
            catch
            {
                MessageBox.Show("Something went wrong?!");
                if(addingNewResidence)
                {
                    Residence = null;
                }
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
    }
}
