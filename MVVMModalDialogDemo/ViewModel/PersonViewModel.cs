using GalaSoft.MvvmLight.CommandWpf;
using MVVMModalDialogDemo.Data;
using MVVMModalDialogDemo.DataService;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMModalDialogDemo.ViewModel
{
    public class PersonViewModel : ValidatableViewModelBase
    {
        private IDataService _DataService;

        private Person _Person;

        public PersonViewModel(IDataService dataService, Person person)
        {
            _DataService = dataService;
            _Person = person;
            this.PropertyChanged += PersonViewModel_PropertyChanged;
        }

        private void PersonViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IsModified = IsModified || true;
        }

        public bool IsModified { get; set; }

        [Required( ErrorMessage ="Please enter a first name")]
        public string FirstName
        {
            get{ return _Person.FirstName; }
            set
            {
                if (_Person.FirstName != value)
                {
                    _Person.FirstName = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(ErrorMessage = "Please enter a last name")]
        public string LastName
        {
            get { return _Person.LastName; }
            set
            {
                if (_Person.LastName != value)
                {
                    _Person.LastName = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(ErrorMessage = "Please enter a birth date")]
        public DateTime BirthDate
        {
            get { return _Person.BirthDate; }
            set
            {
                if (_Person.BirthDate != value)
                {
                    _Person.BirthDate = value;
                    RaisePropertyChanged();
                }
            }
        }

        #region Private Methods

        private async Task DoSave()
        {
            try
            {
                await _DataService.SaveChanges();
                IsModified = false;
                MessengerInstance.Send(true, "SaveResult");
            }
            catch (Exception ex)
            {
                MessengerInstance.Send(
                    $"Error Saving Person.{Environment.NewLine}{ex.Message}",
                    "SaveError");
            }
        }

        #endregion

        /// <summary>
        /// Determines if the current object is valid
        /// </summary>
        /// <returns>Returns True if valid, false if it is not</returns>
        public bool IsValid()
        {
            bool result = true;
            foreach (var s in ValidatedFields)
            {
                if (!string.IsNullOrEmpty(OnValidate(s)))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// A string array of fields that are validated
        /// </summary>
        string[] ValidatedFields = new string[]
        {
            "FirstName",
            "LastName",
            "BirthDate"
        };

        #region Commands

        private ICommand _SaveCommand;

        public ICommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                {
                    _SaveCommand = new RelayCommand(SaveCommandExecute, SaveCommandCanExecute);
                }
                return _SaveCommand;
            }
        }

        private bool SaveCommandCanExecute()
        {
            return this.IsModified && this.IsValid();
        }

        private async void SaveCommandExecute()
        {
            await DoSave();
        }

        #endregion

    }
}
