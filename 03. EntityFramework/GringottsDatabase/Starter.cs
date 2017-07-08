namespace GringottsDatabase
{
    using System;
    using System.Linq;

    public class Starter
    {
        public static void Main()
        {
            var context = new GringottsContext();
            var uniqueWizardFirstLetters = context.WizzardDeposits
                .Where(w => w.DepositGroup == "Troll Chest")
                .Select(w => w.FirstName.Substring(0, 1))
                .Distinct()
                .ToArray();

            foreach (var letter in uniqueWizardFirstLetters)
            {
                Console.WriteLine(letter);
            }
        }
    }
}
