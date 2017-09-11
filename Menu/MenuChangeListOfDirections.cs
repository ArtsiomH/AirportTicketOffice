namespace ControlIndependentWork.Menu //пространство имен
{
    //объявления класса меню для списка направлений, наследуемого от абстрактного класса меню
    sealed class MenuChangeListOfDirections : Menu
    {
        public MenuChangeListOfDirections() //конструктор
        {
            topTitle = 9; //установка позиции от верхнего края для названия меню
            top = 12; //установка начальной позиции от верхнего края для пунктов меню
            menuHeading = "Change List Of Directions"; //название меню
            MenuItems.Add("  Add direction        "); //пункт меню
            MenuItems.Add("  Delete direction     "); //пункт меню
            MenuItems.Add("  Return to main menu  "); //пункт меню
            calculate();
        }
    }
}
