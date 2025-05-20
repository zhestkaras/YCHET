﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace YCHET.VM
{
    class CommandVM : ICommand
    {
        Action action;
        Func<bool> canExecute;

        public CommandVM(Action action, Func<bool> canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return canExecute();
        }

        public void Execute(object? parameter)
        {
            action();
        }
    }
    /*
         public class CommandVM : ICommand
         {
             Action action;
             Func<bool> canExecute;

             public CommandVM(Action action, Func<bool> canExecute)
             {
                 this.action = action;
                 this.canExecute = canExecute;
             }

             public event EventHandler? CanExecuteChanged
             {
                 add { CommandManager.RequerySuggested += value; }
                 remove { CommandManager.RequerySuggested -= value; }
             }

             public bool CanExecute(object? parameter)
             {
                 return canExecute();
             }

             public void Execute(object? parameter)
             {
                 action();
             }
         }
    */
}

