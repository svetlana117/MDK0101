using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public class dll
    {
        public static double Age(List<DateTime> dateTime)
        {
            int countUser = dateTime.Count;
            double summa = 0;
            foreach (DateTime dt in dateTime)
            {
                summa += ((DateTime.Now - dt).TotalDays)/365;
            }

            summa = Math.Round(summa / countUser, 2);
            return summa;
        }
        public static List<string> SerchName(List<string> name, string cname)
        {
            name = name.Where(x => x.Contains(cname)).ToList();
            return name;
        }
    }
}
