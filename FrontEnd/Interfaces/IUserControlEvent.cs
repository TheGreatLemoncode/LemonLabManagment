using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Interfaces
{
    /// <summary>
    /// Interface that is used by the controls that allow the mainwindow to 
    /// navigate between them. Give access to the ControlUsed EventHandler
    /// </summary>
    internal interface IUserControlEvent
    {
        /// <summary>
        /// Static event that is raised when a control is completly used
        /// </summary>
        internal abstract static event EventHandler ControlUsed;
    }
}
