using System; //подключение общей библиотеки классов
using System.Collections.Generic; //подключение библиотеки классов, определяющих типизированные коллекции
using ControlIndependentWork.Form; //пространство имен содержит метод формы вывода на экран объекта из списка

namespace ControlIndependentWork.ScriptsForChangingInformation //пространство имен
{
    //объявление класса сценарий удаления тарифа, наследуемого от сценария удаления
    class RemoveTariffScript : DeletsScript
    {     
        public RemoveTariffScript() //конструктор по умолчанию
        {
            title = "Выберите тариф"; //присваивание полю название сценария
            result = string.Format("Тариф удален"); //присваивание полю сообщение результата
            objects = new List<IToShortString>(); //инициализация пустого списка
            //перебор списка тарифов              
            foreach (KeyValuePair<int, Tariff> tariff in AirportTicketOffice.ListOfTariffs)
            {
                objects.Add(tariff.Value); //добавление объектов в список
            }
            
        }

        protected override void action() //реализация метода действия
        {
            Console.Clear(); //очистка консоли
            //удаление выбраного тарифа
            AirportTicketOffice.RemoveTariff(objects[position - 1] as Tariff);
        }
    }
}
