using System;
using System.Diagnostics.Contracts;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Net;
using System.Numerics;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace HelloWorld
{
    class Student
    {
        public string Name { get; set; }
        public double Phone { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }

    class Program
    {
        static List<Student> contacts = new List<Student>();

        static void Main(string[] args)
        {
            Boolean running = true;
            while (running)
            {
                Console.WriteLine("***** Address Book Application In List *****");
                Console.WriteLine("1.AddContact.");
                Console.WriteLine("2.ViewContact.");
                Console.WriteLine("3.EditContact.");
                Console.WriteLine("4.DeleteContact.");
                Console.WriteLine("5.Exits.");
                Console.WriteLine("Chooise The Number :- ");

                int num = Convert.ToInt32(Console.ReadLine());

                switch (num)
                {
                    case 1:
                            AddContact();
                            break;
                    case 2:
                            ViewContact();
                            break;
                    case 3:
                            EditContact();
                            break;
                    case 4:
                            DeleteContact();
                            break;
                    case 5:
                            running = false;
                            Console.WriteLine("\nExits...");
                            break;
                }
            }

            //Add Contacts
            static void AddContact()
            { 
                // name  
                Console.WriteLine("\nEnter The Name :-");
                string name = Console.ReadLine();
                string nameRegex = @"^[A-Za-z\s]+$";
                if (Regex.IsMatch(name, nameRegex))
                {
                    Console.WriteLine("Vaild Name.");
                }
                else
                {
                    Console.WriteLine("Invaild Name.");
                }

                // phone number
                Console.WriteLine("\nEnter The Phone Number (10 Digits):- ");
                double number = Convert.ToDouble(Console.ReadLine());

                string phoneStr = Convert.ToString(number);
                if (phoneStr.Length == 10)
                {
                    string PhoneRegex = @"^\d{10}$";

                    if (Regex.IsMatch(phoneStr, PhoneRegex))
                    {
                        Console.WriteLine("Valid Phone Number.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid Phone Number.");
                    }
                }
                else
                {
                        Console.WriteLine("Invalid input! Enter Only 10 Digit Number.");
                }

                // age 
                Console.WriteLine("\nEnter Your Age (Only 1 to 120 Range):-");
                int age = Convert.ToInt32(Console.ReadLine());

                if (age >= 0 && age <= 120)
                {
                    string ageStr = Convert.ToString(age);
                    string ageRegex = @"^([0-9][0-9]?|1[01][0-9]|120)$";
                    if (Regex.IsMatch(ageStr, ageRegex))
                    {
                        Console.WriteLine("Valid Age.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input! Age must be a numeric value.");
                    }
                }
                else
                {
                    Console.WriteLine(" Age Out Of Range.");
                }

                //email
                Console.WriteLine("\nEnter The Email Address :-");
                string email = Console.ReadLine();

                string emailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                if (Regex.IsMatch(email, emailRegex))
                {
                    Console.WriteLine("Valid Email Address.");
                }
                else
                {
                    Console.WriteLine("Invalid Email Address.");
                }

                //address
                Console.WriteLine("\nEnter The Address (You Can Use Letters, Numbers, Spaces, and ,:./#-) :-");
                string address = Console.ReadLine();

                string addressRegex = @"^[a-zA-Z0-9\s,:./#-]{1,100}$";
                if (Regex.IsMatch(address, addressRegex))
                {
                    Console.WriteLine("Valid Address.\n");
                }
                else
                {
                    Console.WriteLine("Invalid Address.\n");
                }

                //add in list
                Student user = new Student{ Name = name, Phone = number, Age = age, Email = email, Address = address };
                contacts.Add(user);
            }

            //View Contacts
            static void ViewContact()
            {
                if(contacts.Count == 0)
                {
                    Console.WriteLine("\n *---- Contact Is Not Found !!! ----*");
                    return;
                }
                foreach (Student c in contacts)
                {
                    Console.WriteLine("\n *---- Contacts Is Founds ----*");
                    Console.WriteLine($" Name = {c.Name} \n Phone Number = {c.Phone} \n Age = {c.Age} \n Email = {c.Email} \n Address = {c.Address} \n");
                }
            }

            //Edit Contacts
            static void EditContact()
            {
                //enter the edit contact name
                Console.WriteLine("\nEnter The UserName of Contact to Edit :-");
                string name = Console.ReadLine();
                Student c = contacts.Find(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

                if ( c != null )
                {
                    Console.WriteLine("\nIf You Do Not Need To Fill In Any Data Then Press Enter.");

                    // enter the new name 
                    Console.WriteLine("\nEnter The New Name :- ");
                    string newname = Console.ReadLine();
                    if(!string.IsNullOrWhiteSpace(newname)) c.Name = newname;
                    string newnameRegex = @"^[A-Za-z\s]+$";
                    if (Regex.IsMatch(newname, newnameRegex))
                    {
                        Console.WriteLine("Vaild Name.");
                    }
                    else
                    {
                        Console.WriteLine("Invaild Name.");
                    }

                    // enter the new phone number 
                    Console.Write("\nEnter The New Phone Number (10 Digits) :- \n");
                    string phoneInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(phoneInput))
                        if (double.TryParse(phoneInput, out double newnumber))
                        {
                            c.Phone = newnumber;
                        }
                    if (phoneInput.Length == 10)
                    {
                        string newPhoneRegex = @"^\d{10}$";

                        if (Regex.IsMatch(phoneInput, newPhoneRegex))
                        {
                            Console.WriteLine("Valid Phone Number.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid Phone Number.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input! Enter Only 10 Digit Number.");
                    }


                    // enter the new age
                    Console.WriteLine("\nEnter The New Age (Only 1 to 120 Range) :- ");
                    string newage = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newage))
                        if (int.TryParse(newage, out int newAge))
                        {
                            c.Age = newAge;
                        }
                    int newage1 = Convert.ToInt32(newage);
                    if (newage1 >= 0 && newage1 <= 120)
                    {
                        string ageStr = Convert.ToString(newage1);
                        string newageRegex = @"^([0-9][0-9]?|1[01][0-9]|120)$";
                        if (Regex.IsMatch(ageStr, newageRegex))
                        {
                            Console.WriteLine("Valid Age.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid input! Age must be a numeric value.");
                        }
                    }
                    else
                    {
                        Console.WriteLine(" Age Out Of Range.");
                    }



                    // enter the new email
                    Console.WriteLine("\nEnter The New Email Address :- ");
                    string newemail = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newemail)) c.Email = newemail;

                    string newemailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                    if (Regex.IsMatch(newemail, newemailRegex))
                    {
                        Console.WriteLine("Valid Email Address.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid Email Address.");
                    }


                    // enter the new address
                    Console.WriteLine("\nEnter the New Address (You Can Use Letters, Numbers, Spaces, and ,:./#-) :- ");
                    string newaddress = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newaddress)) c.Address = newaddress;

                    string newaddressRegex = @"^[a-zA-Z0-9\s,:./#-]{1,100}$";
                    if (Regex.IsMatch(newaddress, newaddressRegex))
                    {
                        Console.WriteLine("Valid Address.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid Address.");
                    }

                    Console.WriteLine("\nContacts Update Successfully... \n");
                }
                else
                {
                    Console.WriteLine("\nContact Is Not Found !!!\n");
                }
            }

            // Delete Contacts
            static void DeleteContact()
            {
                //enter the delete contact name
                Console.WriteLine("\nEnter The UserName of Contact to Delete :-");
                string name = Console.ReadLine();
                Student c = contacts.Find(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

                if (c != null)
                {
                    contacts.Remove(c);
                    Console.WriteLine("\nContacts Delete Successfully...\n");
                }
                else
                {
                    Console.WriteLine("\nContact Is Not Found !!!\n");
                }
            }
        }
    }
}