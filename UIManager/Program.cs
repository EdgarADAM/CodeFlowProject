// See https://aka.ms/new-console-template for more information;
using UIManager;
using Common.Models;
using UIManager.Business;

Console.WriteLine("Welcome to the Appointment Management Service");

UserBusiness Business = new UserBusiness(); 
Menus FirstMenu = new Menus();

FirstMenu.PrincipalMenu();

string option = Console.ReadLine();


while ( (option != "1") && (option !="2"))
{
    Console.WriteLine("Please select a valid option...");
    option = Console.ReadLine();
    Console.Clear();
    FirstMenu.PrincipalMenu();
}
Console.Clear();
if (option == "1")
{
    FirstMenu.UserTypeMenu();
    string userType = Console.ReadLine(); 
    while ((userType != "1") && (userType != "2"))
    {
        Console.WriteLine("Please select a valid option...");
        userType = Console.ReadLine();
        Console.Clear();
        FirstMenu.UserTypeMenu();
    }
    Console.WriteLine("Please write your name: ");
    string name = Console.ReadLine();
    UserModel newUser = new UserModel();
    newUser.name = name;
    newUser.type = userType;
    Business.UserRegistry(newUser);
    
}
else if (option == "2")
{
    Console.WriteLine("Please select the Doctor you want to make your appointment with");
    string doctorId = Console.ReadLine();
}
