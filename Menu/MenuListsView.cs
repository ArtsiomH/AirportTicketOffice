using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlIndependentWork.Menu
{
    class MenuListsView : Menu
    {
        public MenuListsView() //конструктор
        {
            topTitle = 9; //установка позиции от верхнего края для названия меню
            top = 12; //установка начальной позиции от верхнего края для пунктов меню
            menuHeading = "Lists View"; //название меню
            MenuItems.Add("       List of directions       "); //пункт меню
            MenuItems.Add("       List of passengers       "); //пункт меню
            MenuItems.Add("        List of tariffs         "); //пункт меню
            MenuItems.Add("  List of passengers for flight "); //пункт меню
            MenuItems.Add("       Return to main menu      "); //пункт меню
            calculate();
        }
    }
}
