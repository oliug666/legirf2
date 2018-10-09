using N3PR_WPFclient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace N3PR_WPFclient.Core
{
    public static class GlobalCommands
    {
        /// <summary>
        /// Execute with an exception as a parameter to have the exception displayed for the user.
        /// </summary>
        ///
        //Todo implement a register/unregister way instead. Look at PRISM composite command.
        public static DelegateCommand ShowError;
    }
}
