using MVVMModalDialogDemo.Data;
using MVVMModalDialogDemo.DataService;
using MVVMModalDialogDemo.Dialogs;
using MVVMModalDialogDemo.ViewModel;
using System;
using System.Windows;

namespace MVVMModalDialogDemo.ModalDialogService
{
    public class ModalDialogService : IModalDialogService
    {
        public bool PersonProperties(Person person, IDataService dataService)
        {
            PersonPropertiesDialog dlg = new PersonPropertiesDialog();
            dlg.Owner = App.Current.MainWindow;
            dlg.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            dlg.DataContext = new PersonViewModel(dataService, person);

            return dlg.ShowDialog() ?? false;
        }

        public void ShowMessage(string message, string caption, MessageBoxImage image)
        {
            MessageBox.Show(App.Current.MainWindow, message, caption, MessageBoxButton.OK, image);
        }

        public void ShowError(Exception ex)
        {
            this.ShowMessage(ex.Message, "Error", MessageBoxImage.Error);
        }
    }
}
