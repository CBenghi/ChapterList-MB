using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SyncView
{
    class Regexes
    {

        /// <summary>
        /// L1P1[042:22.200]
        /// </summary>
        static internal Regex regexUnnamedAbsolutePosition = new Regex(@"L(?<L>\d+)P(?<P>\d+)(?<position>\[.+])"); 

    }
}
