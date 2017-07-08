namespace SoftUniDatabase
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using SoftUniDatabase.Models;

    public class Starter
    {
        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            SoftUniContext context = new SoftUniContext();

            /** 03. Employees full information
            var employees = context.Employees;
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary}");
            }
            **/

            /** 04. Employees with Salary over 50 000
            var employeeNames = context.Employees.
                Where(e => e.Salary > 50000).
                Select(e => e.FirstName);
            foreach (var employeeName in employeeNames)
            {
                Console.WriteLine(employeeName);
            }
            **/

            /** 05. Employees from Seattle
            var employeesFromSeattle =
                context.Employees
                    .Where(employee => employee.Department.Name == "Research and Development")
                    .OrderBy(employee => employee.Salary)
                    .ThenByDescending(employee => employee.FirstName)
                    .ToArray();
            foreach (var employee in employeesFromSeattle)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName} " +
                                  $"from {employee.Department.Name} - ${employee.Salary:F2}");
            }
            **/

            /** 06. Adding New Address and Updating Employee
            var address = new Address()
            {
                AddressText = "Vitoshka 15",
                TownID = 4
            };

            context.Addresses.Add(address);

            var employee = context.Employees.FirstOrDefault(e => e.LastName == "Nakov");
            employee.Address = address;
            context.SaveChanges();

            var employeeAddresses = context.Employees
                .OrderByDescending(e => e.AddressID)
                .Take(10)
                .Select(e => e.Address.AddressText);
            foreach (var employeeAddress in employeeAddresses)
            {
                Console.WriteLine(employeeAddress);
            }
            **/

            /** 07. Delete Project by Id
            var project = context.Projects.Find(2);

            var employeesWithGivenProject = project.Employees;
            foreach (var employee in employeesWithGivenProject)
            {
                employee.Projects.Remove(project);
            }

            context.Projects.Remove(project);
            context.SaveChanges();

            var top10Projects = context.Projects.Take(10).Select(p => p.Name);
            foreach (var proj in top10Projects)
            {
                Console.WriteLine(proj);
            }
            **/

            /** 08. Find Employees in Period
            var employees = context.Employees
                .Where(e => e.Projects.Count(p => p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003) > 0).Take(30);
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.Manager.FirstName}");
                foreach (var project in employee.Projects)
                {
                    Console.WriteLine($"--{project.Name} {project.StartDate:M/d/yyyy h:mm:ss tt} {project.EndDate:M/d/yyyy h:mm:ss tt}");
                }
            }
            **/

            /* 09. Addresses by town name 
            var addresses = context.Addresses
                .OrderByDescending(a => a.Employees.Count)
                .ThenBy(a => a.Town.Name)
                .Take(10);
            foreach (var address in addresses)
            {
                Console.WriteLine($"{address.AddressText}, {address.Town.Name} - {address.Employees.Count} employees");
            }
            */

            /* 10. Employee with Id 147 sorted by project names
            var employee = context.Employees.Find(147);
            var projects = employee.Projects.OrderBy(p => p.Name);
            Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.JobTitle}");
            foreach (var project in projects)
            {
                Console.WriteLine($"{project.Name}");
            }
            */

            /* 11. Departments with more than 5 employees
            var departments = context.Departments
                .Where(d => d.Employees.Count > 5)
                .OrderBy(d => d.Employees.Count);
            foreach (var department in departments)
            {
                var manager = context.Employees.Find(department.ManagerID);
                Console.WriteLine($"{department.Name} {manager.FirstName}");
                foreach (var employee in department.Employees)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.JobTitle}");
                }
            }
            */

            /* 12. Native SQL Query
            context.Employees.Count();
            var timer = new Stopwatch();
            timer.Start();
            PrintNamesWithLinq(context);
            timer.Stop();
            Console.WriteLine($"Native: {timer.Elapsed}");

            timer.Restart();
            PrintNamesWithNativeQuery(context);
            timer.Stop();
            Console.WriteLine($"Linq: {timer.Elapsed}");
            */

            /* Latest 10 Projects
            var projects = context.Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .OrderBy(p => p.Name);

            foreach (var project in projects)
            {
                Console.WriteLine($"{project.Name} {project.Description} {project.StartDate:M/d/yyyy h:mm:ss tt} {project.EndDate:M/d/yyyy h:mm:ss tt}");
            }
            */

            /* 16. Increase Salaries
            var employees = context.Employees
                .Where(e => e.Department.Name == "Engineering"
                            || e.Department.Name == "Tool Design"
                            || e.Department.Name == "Marketing"
                            || e.Department.Name == "Information Services");
            foreach (var employee in employees)
            {
                employee.Salary += employee.Salary * 0.12m;
            }

            context.SaveChanges();

            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:f6})");
            }
            */
            
            /* 17. Remove Towns
            var townName = Console.ReadLine();
            Town town = context.Towns.FirstOrDefault(t => t.Name == townName);
            Address[] addresses = town.Addresses.ToArray();
            foreach (var address in addresses)
            {
                var employees = address.Employees;
                foreach (var employee in employees)
                {
                    employee.AddressID = null;
                }

            }

            context.Addresses.RemoveRange(addresses);
            context.Towns.Remove(town);
            context.SaveChanges();

            var pluralOrSingle = DeterminePluralOrSingle(addresses.Length);
            Console.WriteLine($"{addresses.Length} addresses in {town.Name} {pluralOrSingle} deleted");
            */

            /* 18. Find Employees by First Name Starting with 'SA'
            var employees = context.Employees.Where(e => e.FirstName.StartsWith("SA"));
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary})");
            }
            */
        }

        private static string DeterminePluralOrSingle(int length)
        {
            string single = "was";
            string plural = "were";
            if (length != 1)
            {
                return single;
            }

            return plural;
        }

        private static void PrintNamesWithLinq(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(e => e.Projects.Count(p => p.StartDate.Year == 2002) > 0)
                .Select(e => e.FirstName);
            foreach (var employee in employees)
            {
            }
        }

        private static void PrintNamesWithNativeQuery(SoftUniContext context)
        {
            var query = "SELECT e.FirstName FROM Employees AS e " +
                        "JOIN EmployeesProjects AS emp " +
                        "ON emp.EmployeeId = e.EmployeeId " +
                        "JOIN Projects AS p " +
                        "ON emp.ProjectId = p.ProjectId " +
                        "WHERE YEAR(p.StartDate) = 2002 " +
                        "GROUP BY e.FirstName";
            var employees = context.Database.SqlQuery<string>(query);
            foreach (var employee in employees)
            {
            }
        }
    }
}
