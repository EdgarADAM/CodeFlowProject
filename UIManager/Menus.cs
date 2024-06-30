using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIManager
{
    internal class Menus
    {
        public void PrincipalMenu()
        {
            Console.WriteLine("What you'd like to do?");
            Console.WriteLine("1.- Registry");
        }

        public void UserTypeMenu()
        {
            Console.WriteLine("What type of user are you?");
            Console.WriteLine("1.- Doctor");
            Console.WriteLine("2.- Pacient");
        }
    }
}
