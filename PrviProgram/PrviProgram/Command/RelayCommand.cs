using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PrviProgram.Command
{
    public class RelayCommand : ICommand
    {
        Action<object> executeAction;
        Func<object, bool> canExecute;
        bool canExecuteCache;
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
        public RelayCommand(Action<object> execute) : this(execute, null,false) { }
        public RelayCommand(Action<object> executeMethod, Func<object, bool> canexecuteMethod, bool canExecuteCache)
        {
            this.executeAction = executeMethod;
            this.canExecute = canexecuteMethod;
            this.canExecuteCache = canExecuteCache;
        }
        public bool CanExecute(object parameter)
        {
            if(canExecute==null)
            {
                return true;
            }
            else
            {
                return canExecute(parameter);
            }
        }

        //logicpart
        public void Execute(object parameter)
        {
            executeAction(parameter);
        }
    }
}