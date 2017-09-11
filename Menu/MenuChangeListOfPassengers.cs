namespace ControlIndependentWork.Menu //пространство имен
{
    //объявления класса меню для списка пассажиров, наследуемого от абстрактного класса меню
    sealed class MenuChangeListOfPassengers : Menu
    {
        public MenuChangeListOfPassengers() //конструктор
        {
            topTitle = 9; //установка позиции от верхнего края для названия меню
            top = 12; //установка начальной позиции от верхнего края для пунктов меню
            menuHeading = "Change List Of Passengers"; //название меню
            MenuItems.Add("  Add passenger        "); //пункт меню
            MenuItems.Add("  Delete passenger     "); //пункт меню
            MenuItems.Add("  Return to main menu  "); //пункт меню
            calculate();
        }
    }
}
