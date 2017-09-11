using System; //подключение общей библиотеки классов
using System.Collections.Generic; //подключение библиотеки классов, определяющих типизированные коллекции
using ControlIndependentWork.Form; //пространство имен содержит метод формы вывода на экран объекта из списка


namespace ControlIndependentWork.ScriptsForChangingInformation //пространство имен
{
    //объявление класса сценарий удаления направления, наследуемого от сценария удаления
    class RemoveDirectionScript : DeletsScript 
    {
        public RemoveDirectionScript() //конструктор по умолчанию
        {
            title = "Choose direction"; //присваивание полю название сценария
            result = string.Format("Direction deleted"); //присваивание полю сообщение результата
            objects = new List<IToShortString>(); //инициализация пустого списка
            //перебор списка направлений            
            foreach (KeyValuePair<int, DirectionName> direction in AirportTicketOffice.ListOfDirections)
            {
                objects.Add(direction.Value); //добавление объектов в список 
            }
        }

        protected override void action() //реализация метода действия
        {
            Console.Clear(); //очистка консоли
            //удаление выбраного направления
            AirportTicketOffice.RemoveDirection(objects[position - 1] as DirectionName);
        }
    }
}
