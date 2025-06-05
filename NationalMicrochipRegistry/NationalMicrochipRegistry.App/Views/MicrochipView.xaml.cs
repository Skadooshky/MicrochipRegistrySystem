using System.Windows.Controls;
using NationalMicrochipRegistry.App.ViewModels;

namespace NationalMicrochipRegistry.App.Views
{
    public partial class MicrochipView : UserControl
    {
        public MicrochipView()
        {
            InitializeComponent();
            DataContext = new MicrochipViewModel();
        }
    }
}
