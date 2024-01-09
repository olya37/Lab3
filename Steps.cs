using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc_LAB2_Dolganova
{
    public class Steps
    {
        public List<double> Results { get; set; }  

        public Steps(List<double> results) 
        {
            Results = results; 
        }

        public Steps() 
        {
            
        }

        public void Show()
        {
            int count = 0;
            foreach (double step in Results)
            {
                Console.Write("[#" + count + " ]"+step+", ");
                count++;
            }
        }





    }
}
