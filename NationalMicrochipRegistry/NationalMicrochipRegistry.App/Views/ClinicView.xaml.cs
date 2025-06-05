using System.Windows.Controls;
using NationalMicrochipRegistry.App.ViewModels;

namespace NationalMicrochipRegistry.App.Views
{
    public partial class ClinicView : UserControl
    {
        public ClinicView()
        {
            InitializeComponent();
            DataContext = new ClinicViewModel();
        }
    }
}
