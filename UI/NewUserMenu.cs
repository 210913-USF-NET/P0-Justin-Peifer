using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI
{
    public class NewUserMenu : IMenu
    {
        public void Start()
        {
            new StartMenu().Start(); 
            
        }
    }
}