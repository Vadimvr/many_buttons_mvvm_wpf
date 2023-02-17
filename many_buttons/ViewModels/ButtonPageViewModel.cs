using Accessibility;
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
        }

        #region RemoveButtonSelfCommand - Удаление кнопки из которой происходит вызов команды 
        private LambdaCommand _RemoveButtonSelfCommand;

        public ICommand RemoveButtonSelfCommand => _RemoveButtonSelfCommand ??=
            new LambdaCommand(OnRemoveButtonSelfCommandExecuted, CanRemoveButtonSelfCommandExecute);

        private bool CanRemoveButtonSelfCommandExecute(object p) => true;
        private void OnRemoveButtonSelfCommandExecuted(object p)
        {
            if (p is MyButtonViewModel)
            {
                ButtonsList.Remove(p as MyButtonViewModel);
            }
        }
        #endregion

        #region AddNewButtonCommand - добавление новой кнопки 
        private LambdaCommand _AddNewButtonCommand;
        public ICommand AddNewButtonCommand => _AddNewButtonCommand ??=
            new LambdaCommand(OnAddNewButtonCommandExecuted, CanAddNewButtonCommandExecute);
        private bool CanAddNewButtonCommandExecute(object p) => true;
        int i = 0; // счетчик
        private void OnAddNewButtonCommandExecuted(object p)
        {
            ButtonsList.Add(new MyButtonViewModel()
            {
                DisplayText = $"Foo {i++}",
                Command = RemoveButtonSelfCommand
            }); ;
            Status = i.ToString();
        }
        #endregion

        #region DeletedButtonsCommand  - Удаление последней кнопки 
        private LambdaCommand _DeletedButtonsCommand;

        public ICommand DeletedButtonsCommand => _DeletedButtonsCommand ??=
            new LambdaCommand(OnDeletedButtonsCommandExecuted, CanDeletedButtonsCommandExecute);

        private bool CanDeletedButtonsCommandExecute(object p) => ButtonsList.Count> 0;

        private void OnDeletedButtonsCommandExecuted(object p)
        {

            ButtonsList.Remove(ButtonsList[ButtonsList.Count - 1]);
        }
        #endregion


        private ObservableCollection<MyButtonViewModel> _ButtonsList = new ObservableCollection<MyButtonViewModel>();

        public ObservableCollection<MyButtonViewModel> ButtonsList { get => _ButtonsList; set => Set(ref _ButtonsList, value); }
    }
}
