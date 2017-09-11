using System; //подключение общей библиотеки классов
using System.Collections.Generic; //подключение библиотеки классов, определяющих типизированные коллекции
using System.Linq; //пространство имен System.Linq содержит классы и интерфейсы, которые поддерживают запросы, использующие LINQ 
using ControlIndependentWork.Form; //пространство имен содержит метод формы вывода на экран объекта из списка
using ControlIndependentWork.MyEvent; //пространство имен содержит событие обработки нажатия
using ControlIndependentWork.Passengers; //пространсво имен содержит класс пассажира

namespace ControlIndependentWork.CostCalculation //пространство имен
{
    //объявление класса стоимость билетов пассажира реализующий интерфей IHandlerPosition
    class CostPassengerTickets : IHandlerPosition 
    {
        private List<Passenger> passengers; //список пассажиров
        private int position = 1; //поле позиции
        bool print = false; //булевская переменная для разрешения вывода результата
        decimal sum = 0; //переменная для суммы стоимости билетов

        public bool ListIsEmpty //свойство список является пустым
        {
            get //аксессор получения результата проверки списка
            {
                if (passengers.Count == 0) //если список пассажиров пуст
                {
                    Console.Clear(); //очистка консоли
                    Console.SetCursorPosition(28, 15); //установка позиции курсора
                    Console.WriteLine("The passenger list is empty."); //вывод сообщения на консоль
                    Console.ReadKey(); //ожидание нажатия клавиши пользователем
                    return true; //список пуст
                }
                return false; //список не пуст
            }
        }

        public CostPassengerTickets() //конструктор
        {
            passengers = AirportTicketOffice.ListOfPassengers.Values.ToList(); //заполняеем список пассажирами
        }

        public bool Cost() //метод вывода на консоль стоимости для пассажира
        {
            Console.Clear(); //очистка консоли
            if (print) //если можно выводить информацию на консоль о стоимости
            {
                //создается форматированная строка сообщения
                string str = string.Format("The cost of tickets bought by the passenger was: {0} cu", sum);
                //установка позиции курсора
                Console.SetCursorPosition((Console.BufferWidth - str.Length) / 2, 15);
                Console.WriteLine(str); //вывод строки на консоль
                Console.ReadKey(); //ожинание нажатия клавиши пользователем
                return false; //возвращет false для выход из цикла
            }
            FormForList.Show("Select a passenger", passengers, position); //вывод на экран информации о пассажире        
            return true; //возвращает true для повторения операции просмотра нового пассажира
        }

        public void HandlerPosition(object sender, PositionEventArgs args) //метод для обработки нажатия клавиши
        {
            if (args.Obj.GetType() == GetType()) //если сообщение было для этого класса
            {
                if (args.Key == ConsoleKey.UpArrow) //если была нажата клавиша вверх
                {
                    if (position > 1) position--; //если не начало списка, то переход на предыдущию позицию
                    else position = passengers.Count; //если начало списка, то переход на последнию позицию списка
                }
                else if (args.Key == ConsoleKey.DownArrow) //если была нажата клавиша вниз
                {
                    if (position < passengers.Count) position++; //если не конец списка, то переход на следующию позицию
                    else position = 1; //если конец списка, то переход на первую позицию списка
                }
                else if (args.Key == ConsoleKey.Enter) //если была нажата клавиша ввода
                {
                    //поиск ключа в списке пассажиров                  
                    int id = passengers[position - 1].PersonalNumber.GetHashCode();
                    //поиск билетов по ключу билетов купленных пассажиром
                    IEnumerable<AirTicket> tickets = AirportTicketOffice.ListJourneyTickets.Where(x => x.PersonalNumber == id);
                    //рассчет суммы купленных билетов не учитывая скидку
                    sum = tickets.Sum(x => sum + AirportTicketOffice.ListOfTariffs.First(y => y.Key == x.TariffID).Value.Cost);                    
                    print = true; //утановка значения true для вывода на консоль результата
                }
            }
        }
    }
}
