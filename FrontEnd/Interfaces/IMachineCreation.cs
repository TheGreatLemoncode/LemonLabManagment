using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Interfaces
{
    /// <summary>
    /// Interface that is used by the creation windows. Give access to the GetProperties method
    /// </summary>
    public interface IMachineCreation
    {
        /// <summary>
        /// Transform the informations about the machine currently being create into a 
        /// [string, string] dictionary.
        /// </summary>
        /// <returns>A string, string dictionary that contains all the
        /// properties of the machine being created</returns>
        public Dictionary<string, string> GetProperties();
    }
}
