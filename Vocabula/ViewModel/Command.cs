using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Vocabula.ViewModel
{
    public class Command : ICommand
    {
        public Command(Func<object, bool> CanExecute, Action<object> Execute)
        {
            _canExecute = CanExecute;
            _execute = Execute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            return _canExecute(parameter);
        }

        public virtual void Execute(object parameter)
        {
            _execute(parameter);
        }

        /// <summary>
        /// Forces the re-evaluation of the CanExecute function.
        /// NOTE: Can only be called from the UI thread.
        /// </summary>
        public void RaiseCanExecuteChangedEvent()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        protected Action<object> _execute;
        private Func<object, bool> _canExecute;
    }
}
