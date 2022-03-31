using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryPetrov_2ISP1117.ClassHelper
{
    public class LessIssue
    {
        public static double Debt(double bookCost, DateTime startDate)
        {
            int dayCount = (DateTime.Now - startDate).Days;
            double debt = (dayCount - 30) * 0.01 * bookCost;
            if (bookCost <= 0)
            {
                return 0;
            }
            if (debt > 0)
            {
                return debt;
            }
            else
            {
                return 0;
            }
                   
        }
    }
}
