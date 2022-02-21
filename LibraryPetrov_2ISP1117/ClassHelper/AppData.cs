using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryPetrov_2ISP1117.EF;

namespace LibraryPetrov_2ISP1117.ClassHelper
{
    class AppData
    {
        public static EF.Entities Context { get; } = new EF.Entities();
    }
}
