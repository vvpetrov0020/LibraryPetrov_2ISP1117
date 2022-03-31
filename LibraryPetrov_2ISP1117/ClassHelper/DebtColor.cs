using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryPetrov_2ISP1117.EF;
using LibraryPetrov_2ISP1117.ClassHelper;

namespace LibraryPetrov_2ISP1117.EF
{
    public partial class Issue
    {
        public string GetDebt { get => $"Задолженность: {LessReturn}"; }

        public string GetColor

        {
            get
            {
                if (LessReturn > 0)
                {
                    return "#ff0000";
                }
                else
                {
                    return "#ffffff";
                }
            }
        }

    }
}
