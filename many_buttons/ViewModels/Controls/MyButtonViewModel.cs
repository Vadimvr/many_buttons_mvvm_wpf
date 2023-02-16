

using many_buttons.ViewModels.Base;
using System.Windows.Input;

namespace many_buttons.ViewModels.Controls
{
    internal class MyButtonViewModel : ViewModel
    {

        private ICommand command;
        private string displayText;

        public ICommand Command
        {
            get { return command; }
            set { Set(ref command, value); }
        }

        public string DisplayText
        {
            get { return displayText; }
            set { Set(ref displayText, value); }
        }
    }
}