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
    }
}
