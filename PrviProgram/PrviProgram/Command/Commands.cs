using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PrviProgram.Command
{
   
        public class Commands : ICommand
        {
            Action<object> executeMethod;
            Func<object, bool> canexecuteMethod;
            public event EventHandler CanExecuteChanged;
            public Commands(Action<object> executeMethod, Func<object, bool> canexecuteMethod)
            {
                this.executeMethod = executeMethod;
                this.canexecuteMethod = canexecuteMethod;
            }
            public bool CanExecute(object parameter)
            {
                return true;
            }

            //logicpart
            public void Execute(object parameter)
            {
                executeMethod(parameter);
            }
        }
}
