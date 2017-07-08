using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.HotelDatabase
{
    public class Startup
    {
        public static void Main()
        {
            var context = new HotelContext();
            context.Database.Initialize(true);
        }
    }
}
