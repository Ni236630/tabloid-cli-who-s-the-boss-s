﻿using System;

namespace TabloidCLI.UserInterfaceManagers
{
    public class MainMenuManager : IUserInterfaceManager
    {
        private const string CONNECTION_STRING = 
            @"Data Source=localhost\SQLEXPRESS;Database=TabloidCLI;Integrated Security=True";

        public IUserInterfaceManager Execute()
        {

            //we will print out a pleasant greeting here
            string managementQtn = "Hello there! These are a list of managements you can work with. You can add, list tags, edit, or remove one of the posts under one of these managements. Just select one you want to work with and let's get started!";
            Console.WriteLine($"{managementQtn}");

            Console.WriteLine();
            Console.WriteLine("Main Menu");

            Console.WriteLine(" 1) Journal Management");
            Console.WriteLine(" 2) Blog Management");
            Console.WriteLine(" 3) Author Management");
            Console.WriteLine(" 4) Post Management");
            Console.WriteLine(" 5) Tag Management");
            Console.WriteLine(" 6) Search by Tag");
            Console.WriteLine(" 0) Exit");

            Console.Write("> ");


            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": throw new NotImplementedException();
                case "2": return new BlogManager(this, CONNECTION_STRING);

                case "3": return new AuthorManager(this, CONNECTION_STRING);
                case "4": throw new NotImplementedException();
                case "5": return new TagManager(this, CONNECTION_STRING);
                case "6": return new SearchManager(this, CONNECTION_STRING);
                case "0":
                    Console.WriteLine("Good bye");
                    return null;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }
    }
}
