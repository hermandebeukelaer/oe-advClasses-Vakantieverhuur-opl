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
            UpdateResidences(residences.AllResidences);
        }

        private void UpdateResidences(List<Residence> residences)
        {
            lstResidences.ItemsSource = residences;
        }

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateResidences();
        }

        private void CmbKindOfResidence_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbKindOfResidence.SelectedItem != null)
            {
                ComboBoxItem selectedKind = (ComboBoxItem) cmbKindOfResidence.SelectedItem;
                switch (selectedKind.Content)
                {
                    case "Vakantiehuisjes":
                        UpdateResidences(residences.GetAllVacationHouses());
                        break;
                    case "Caravans":
                        UpdateResidences(residences.GetAllCaravans());
                        break;
                    default:
                        UpdateResidences();
                        break;
                }
            }
        }
    }
}
