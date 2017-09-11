using System; //подключение общей библиотеки классов
using System.Collections.Generic; //подключение библиотеки классов, определяющих типизированные коллекции
using ControlIndependentWork.Passengers; //пространсво имен содержит класс пассажира
using ControlIndependentWork.Form; //пространство имен содержит метод формы вывода на экран объекта из списка

namespace ControlIndependentWork.ScriptsForChangingInformation //пространство имен
{
    //объявление класса сценарий удаления пассажира, наследуемого от сценария удаления
    class RemovePassengerScript : DeletsScript
    {
        public RemovePassengerScript() //конструктор по умолчанию
        {
            title = "Select a passenger"; //присваивание полю название сценария
            result = string.Format("Passenger was deleted"); //присваивание полю сообщение результата
            objects = new List<IToShortString>(); //инициализация пустого списка
            //перебор списка пассажиров           
            foreach (KeyValuePair<int, Passenger> passenger in AirportTicketOffice.ListOfPassengers)
            {
                objects.Add(passenger.Value); //добавление объектов в список 
            }
        }

        protected override void action() //реализация метода действия
        {
            Console.Clear(); //очистка консоли
            //удаление выбраного пассажира
            AirportTicketOffice.RemovePassenger(objects[position - 1] as Passenger);
        }
    }
}
