using many_buttons.Infrastructure.Commands;
using many_buttons.Services.Interfaces;
using many_buttons.ViewModels.Base;
using many_buttons.ViewModels.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace many_buttons.ViewModels
{
    internal class ButtonPageViewModel : ViewModel
    {
        private readonly IUserDialog _UserDialog;
        private readonly IDataService _DataService;

        private string _Status;

        public string Status { get => _Status; set => Set(ref _Status, value); }
        public ButtonPageViewModel(IUserDialog UserDialog, IDataService DataService)
        {
            _UserDialog = UserDialog;
            _DataService = DataService;
            Status = "OK";
            AddNewButtonCommand = new LambdaCommand(OnAddNewButtonCommandExecuted, CanAddNewButtonCommandExecute);
            DeletedButtonsCommand = new LambdaCommand(OnDeletedButtonsCommandExecuted, CanDeletedButtonsCommandExecute);
            RemoveButtonSelfCommand = new LambdaCommand(OnRemoveButtonSelfCommandExecuted, CanRemoveButtonSelfCommandExecute);
        }


        public ICommand RemoveButtonSelfCommand { get; }

        private bool CanRemoveButtonSelfCommandExecute(object p) => true;
        private void OnRemoveButtonSelfCommandExecuted(object p)
        {
            if(p is MyButtonViewModel )
            {
                ButtonsList.Remove(p as MyButtonViewModel);
            }
        }


        public ICommand AddNewButtonCommand { get; }

        int i = 0;
        private bool CanAddNewButtonCommandExecute(object p) => true;
        private void OnAddNewButtonCommandExecuted(object p)
        {
            ButtonsList.Add(new MyButtonViewModel()
            {
                DisplayText = $"Foo {i++}",
                Command = RemoveButtonSelfCommand
            });;
            Status = i.ToString();
        }

        public ICommand DeletedButtonsCommand { get; }
        private bool CanDeletedButtonsCommandExecute(object p) => ButtonsList.Count>0;
        private void OnDeletedButtonsCommandExecuted(object p)
        {
            var j = i - 1;
            ButtonsList.Remove(ButtonsList[ButtonsList.Count -1]);
        }


        private ObservableCollection<MyButtonViewModel> _ButtonsList = new ObservableCollection<MyButtonViewModel>();

        public ObservableCollection<MyButtonViewModel> ButtonsList { get => _ButtonsList; set => Set(ref _ButtonsList, value); }
    }
}
