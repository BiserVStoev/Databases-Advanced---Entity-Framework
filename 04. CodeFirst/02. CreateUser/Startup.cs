namespace _02.CreateUser
{
    using System;
    using System.Collections.Generic;
    using Models;
    using System.Linq;

    public class Startup
    {
        public static void Main()
        {
            UserContext context = new UserContext();
            context.Database.Initialize(true);

            //11.Get Users by Email Provider
            //Console.WriteLine("Please enter email provider: ");
            //string emailProvider = Console.ReadLine();
            //GetUsersByEmailProvider(context, emailProvider);

            //12.Count Users with Bigger Pictures
            // Console.WriteLine("Please enter number of pixels: ");
            //int numberOfPixels = int.Parse(Console.ReadLine());
            //int countOfBigPictures = GetCountOfBiggerPictures(context, numberOfPixels);
            //Console.WriteLine($"{countOfBigPictures} users have profile pictures wider than {numberOfPixels} pixels");


            //13.Remove Inactive Users
            //Console.WriteLine("Please enter a date: ");
            //string enteredDateString = Console.ReadLine();
            //DateTime enteredDate = DateTime.Parse(enteredDateString);
            //RemoveInactiveUsers(context, enteredDate);

        }

        //private static int GetCountOfBiggerPictures(UserContext context, int numberOfPixels)
        //{
        //    var userWithPictures = context.Users.Where(user => user.ProfilePicture != null).Select(user => new
        //        {
        //            ProfileImage = user.ProfileImage
        //        });
        //    int count = 0;

        //    foreach (var userWithPicture in userWithPictures)
        //    {
        //        if (userWithPicture.ProfileImage.Width > numberOfPixels)
        //        {
        //            count++;
        //        }
        //    }

        //    return count;
        //}

        private static void RemoveInactiveUsers(UserContext context, DateTime logDate)
        {
            List<User> users = context.Users.Where(user => user.LastTimeLoggedIn < logDate && !user.IsDeleted).ToList();
            foreach (User user in users)
            {
                user.IsDeleted = true;
            }

            if (users.Count == 0)
            {
                Console.WriteLine("No users have been deleted");
            }
            else
            {
                Console.WriteLine($"{users.Count} user has been deleted");
            }

            context.SaveChanges();
        }

        private static void GetUsersByEmailProvider(UserContext context, string emailProvider)
        {
            var wantedUsers = context.Users.Where(user => user.Email.EndsWith(emailProvider)).Select(user => new
            {
                Username = user.Username,
                Password = user.Password,
                Age = user.Age
            });

            foreach (var wantedUser in wantedUsers)
            {
                Console.WriteLine($"{wantedUser.Username} {wantedUser.Password} - {wantedUser.Age}");
            }
        }
    }
}
