using GalaSoft.MvvmLight.Messaging;
using MVVMModalDialogDemo.ViewModel;
using System;
using System.Windows;

namespace MVVMModalDialogDemo.Dialogs
{
    /// <summary>
    /// Interaction logic for PersonPropertiesDialog.xaml
    /// </summary>
    public partial class PersonPropertiesDialog : Window
    {
        public PersonPropertiesDialog()
        {
            InitializeComponent();

            RegisterForMessages();
        }

        private void RegisterForMessages()
        {
            // Register to get the SaveResult Message
            Messenger.Default.Register<bool>(this, "SaveResult", result =>
            {
                if (result)
                {
                    this.DialogResult = result;
                }
            });

            // Register to get the SaveError Message
            Messenger.Default.Register<string>(this, "SaveError", msg =>
            {
                MessageBox.Show(this, msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            });
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            // Unregister messages
            Messenger.Default.Unregister<bool>(this, "SaveResult");
            Messenger.Default.Unregister<string>(this, "SaveError");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PersonViewModel vm = DataContext as PersonViewModel;
            if (vm != null)
            {
                if (vm.IsModified)
                {
                    if (MessageBox.Show(
                        this,
                        $"Person has been modified.{Environment.NewLine}Are you sure you wish to cancel?",
                        "Confirm Cancel",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        this.DialogResult = false;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            };
        }
    }
}
