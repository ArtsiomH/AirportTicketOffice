namespace ControlIndependentWork.Menu //пространство имен
{
    //объявления класса меню для выбора типа пассажира, наследуемого от абстрактного класса меню
    class MenuPassengerSelection : Menu
    {
        public MenuPassengerSelection() //конструктор
        {
            topTitle = 9; //установка позиции от верхнего края для названия меню
            top = 12; //установка начальной позиции от верхнего края для пунктов меню
            menuHeading = "Select type of passenger discount"; //название меню
            MenuItems.Add("   Percent discount   "); //пункт меню
            MenuItems.Add("    Fixed discount    "); //пункт меню
            calculate();
        }
    }
}
