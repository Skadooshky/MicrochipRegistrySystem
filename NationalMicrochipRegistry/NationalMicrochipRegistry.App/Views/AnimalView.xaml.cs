using System.Windows.Controls;
using NationalMicrochipRegistry.App.ViewModels;

namespace NationalMicrochipRegistry.App.Views
{
    public partial class AnimalView : UserControl
    {
        public AnimalView()
        {
            InitializeComponent();
            DataContext = new AnimalViewModel();
        }
    }
}
