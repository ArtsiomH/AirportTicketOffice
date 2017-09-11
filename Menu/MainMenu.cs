namespace ControlIndependentWork.Menu //пространство имен
{
    //объявления класса главного меню наследуемого, от абстрактного класса меню
    sealed class MainMenu : Menu 
    {
        public MainMenu() //конструктор
        {
            topTitle = 9; //установка позиции от верхнего края для названия меню
            top = 12; //установка начальной позиции от верхнего края для пунктов меню
            menuHeading = "Main Menu"; //название меню
            MenuItems.Add("           Change list of directions           "); //пункт меню
            MenuItems.Add("           Change list of passengers           "); //пункт меню
            MenuItems.Add("            Change list of tariffs             "); //пункт меню
            MenuItems.Add("       Register a ticket for a passenger       "); //пункт меню
            MenuItems.Add("    Calculate the cost of passenger tickets    "); //пункт меню
            MenuItems.Add("    Calculate the cost of all tickets sold     "); //пункт меню
            MenuItems.Add("        Increase the cost of the flight        "); //пункт меню
            MenuItems.Add("                 Add test data                 "); //пункт меню
            MenuItems.Add("                     Exit                      "); //пункт меню
            calculate();
        }
    }
}
