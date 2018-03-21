﻿using System;
using System.Linq;
using System.Windows.Input;

namespace NESS.VoucherManagement.Utils.MVVM
{
	public class RelayCommand : ICommand
	{
		private readonly Func<object, bool> canExecute;

		private readonly Action<object> execute;

		public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
		{
			this.execute = execute;
			this.canExecute = canExecute;
		}

		public event EventHandler CanExecuteChanged { add => CommandManager.RequerySuggested += value; remove => CommandManager.RequerySuggested -= value; }

		public bool CanExecute(object parameter) => canExecute == null || canExecute(parameter);

		public void Execute(object parameter)
		{
			execute(parameter);
		}
	}
}