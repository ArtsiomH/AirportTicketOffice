namespace ControlIndependentWork.Menu //пространство имен
{
    //объявления класса меню для списка тарифов, наследуемого от абстрактного класса меню
    sealed class MenuChangeListOfTariffs : Menu
    {
        public MenuChangeListOfTariffs() //конструктор
        {
            topTitle = 9; //установка позиции от верхнего края для названия меню
            top = 12; //установка начальной позиции от верхнего края для пунктов меню
            menuHeading = "Change List Of Tariffs"; //название меню
            MenuItems.Add("  Add tariff           "); //пункт меню
            MenuItems.Add("  Delete tariff        "); //пункт меню
            MenuItems.Add("  Return to main menu  "); //пункт меню
            calculate();
        }
    }
}
