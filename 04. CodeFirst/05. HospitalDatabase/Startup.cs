using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05.HospitalDatabase
{
    public class Startup
    {
        public static void Main()
        {
            var context = new HospitalContext();
            context.Database.Initialize(true);
        }
    }
}
