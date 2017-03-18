using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MVVMModalDialogDemo.DataService;
using System.Windows.Input;
using System;
using MVVMModalDialogDemo.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

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

        public ObservableCollection<PersonListModel> People { get; } = new ObservableCollection<PersonListModel>();

        private PersonListModel _SelectedPersonListModel;

        public PersonListModel SelectedPersonListModel
        {
            get
            {
                return _SelectedPersonListModel;
            }
            set
            {
                if (_SelectedPersonListModel != value)
                {
                    _SelectedPersonListModel = value;
                    RaisePropertyChanged();
                }
            }
        }

        #region Private Methods


        private void DoNewPerson()
        {
            throw new NotImplementedException();
        }

        private void DoPersonProperties()
        {
            throw new NotImplementedException();
        }

        private async Task Init()
        {
            var people = await _DataService.GetPeople();
            foreach(var person in people)
            {
                this.People.Add(new PersonListModel(person));
            }
        }

        #endregion

        #region Commands

        private ICommand _AddPersonCommand;

        public ICommand AddPersonCommand
        {
            get
            {
                if (_AddPersonCommand == null)
                {
                    _AddPersonCommand = new RelayCommand(AddPersonCommandExecute);
                }
                return _AddPersonCommand;
            }
        }

        private void AddPersonCommandExecute()
        {
            DoNewPerson();
        }

        private ICommand _EditPersonCommand;

        public ICommand EditPersonCommand
        {
            get
            {
                if (_EditPersonCommand == null)
                {
                    _EditPersonCommand = new RelayCommand(EditPersonCommandExecute, EditPersonCommandCanExecute);
                }
                return _EditPersonCommand;
            }
        }

        private bool EditPersonCommandCanExecute()
        {
            return _SelectedPersonListModel != null;
        }

        private void EditPersonCommandExecute()
        {
            DoPersonProperties();
        }

        private ICommand _ExitCommand;

        public ICommand ExitCommand
        {
            get
            {
                if (_ExitCommand == null)
                {
                    _ExitCommand = new RelayCommand(ExitCommandExecute);
                }
                return _ExitCommand;
            }
        }

        private void ExitCommandExecute()
        {
            App.Current.Shutdown(0);
        }

        private ICommand _InitCommand;

        public ICommand InitCommand
        {
            get
            {
                if (_InitCommand == null)
                {
                    _InitCommand = new RelayCommand(InitCommandExecute);

                }
                return _InitCommand;
            }
        }

        private async void InitCommandExecute()
        {
            await Init();
        }

        #endregion
    }
}