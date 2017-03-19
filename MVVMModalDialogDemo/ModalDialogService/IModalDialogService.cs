using System;
using System.Windows;
using MVVMModalDialogDemo.Data;
using MVVMModalDialogDemo.DataService;

namespace MVVMModalDialogDemo.ModalDialogService
{
    public interface IModalDialogService
    {
        bool PersonProperties(Person person, IDataService dataService);
        void ShowError(Exception ex);
        void ShowMessage(string message, string caption, MessageBoxImage image);
    }
}