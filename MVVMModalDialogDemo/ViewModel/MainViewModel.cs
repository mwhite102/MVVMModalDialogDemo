using GalaSoft.MvvmLight;
using MVVMModalDialogDemo.DataService;

namespace MVVMModalDialogDemo.ViewModel
{
    
    public class MainViewModel : ViewModelBase
    {
        private IDataService _DataService;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _DataService = dataService;
        }
    }
}