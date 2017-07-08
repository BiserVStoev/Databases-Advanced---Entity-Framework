using System;
using MassDefect.Data;

namespace MassDefect.ConsoleClient
{
    public class Startup
    {
        public static void Main()
        {
            var context = new MassDefectContext();
            try
            {
                context.Database.Initialize(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
