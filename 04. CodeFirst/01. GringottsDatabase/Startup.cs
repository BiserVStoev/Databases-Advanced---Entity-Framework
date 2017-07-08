namespace _01.GringottsDatabase
{
    using System.Data.Entity.Validation;
    using System;
    using Models;

    public class Startup
    {
        public static void Main()
        {
            GringottsContext context = new GringottsContext();

            try
            {
                WizardDeposits wizardDeposit = new WizardDeposits()
                {
                    FirstName = "Albus",
                    LastName = "Dumbledore",
                    Age = 150,
                    MagicWandCreator = "Antioch Peverell",
                    MagicWandSize = 15,
                    DepositStartDate = new DateTime(2016, 10, 20),
                    DepositExpirationDate = new DateTime(2020, 10, 20),
                    DepositAmount = 20000.24m,
                    DepositCharge = 0.2m,
                    IsDepositExpired = false
                };

                context.WizardDeposits.Add(wizardDeposit);
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult dbEntityValidationResult in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError dbValidationError in dbEntityValidationResult.ValidationErrors)
                    {
                        Console.WriteLine(dbValidationError.ErrorMessage);
                    }
                }
            }
        }
    }
}
