using System; //подключение общей библиотеки классов
using System.Collections.Generic; //подключение библиотеки классов, определяющих типизированные коллекции
using System.Linq; //пространство имен System.Linq содержит классы и интерфейсы, которые поддерживают запросы, использующие LINQ
using ControlIndependentWork.MyEvent; //пространство имен содержит событие обработки нажатия

namespace ControlIndependentWork.ScriptsForChangingInformation //пространство имен
{
    //объявление класса сценария измение в билете, наследумого от сценария добавления авиабилета 
    class ChangeInTicketPriceScript : AddAirTicketScript
    {
        //получение списка билетов
        List<AirTicket> airTickets = AirportTicketOffice.ListJourneyTickets.ToList();
        private string info; //поле сообщения информации о позиции билета в списке
        decimal cost; //поля для добавления стоимости билета

        //параметрический конструктор наследуемый от базового
        public ChangeInTicketPriceScript(string title) : base(title)
        {  }

        protected override bool listCheck() //переопределенный метод проверки списка
        {
            if (airTickets.Count == 0) //если списк билетов пуст
            {
                Console.Clear(); //очистка консоли
                Console.SetCursorPosition(23, 15); //установка позиции курсора
                Console.WriteLine("The list of air tickets is empty."); //вывод сообщения на консоль
                Console.ReadKey(); //ожидание нажатия клавиши пользователем
                return true; //список пуст
            }
            return false; //список не пуст
        }

        //метод запуска сценария выбора билета пользователем для изменния стоимости
        public override bool Start()
        {
            Console.Clear(); //очистка консоли
            if (type == 1) //тип для выбора билета из списка
            {
                //получаем пассажира из списка по текущей по билету
                passenger = AirportTicketOffice.ListOfPassengers[airTickets[position - 1].PersonalNumber];
                //получаем тариф из списка по текущему билету
                tariff = AirportTicketOffice.ListOfTariffs[airTickets[position - 1].TariffID];
                Console.SetCursorPosition(leftText - 4, 15); //установка позиции курсора
                Console.WriteLine((char)0x25c4); //печать символа из Unicode
                Console.SetCursorPosition(leftInput + 21, 15); //установка позиции курсора
                Console.WriteLine((char)0x25ba); //печать символа из Unicode
                openForm(); //открытие формы для билета
                //вывод на консоль данных об билете
                printInfo(passenger, tariff, airTickets[position - 1]);
                //присваивание строки информации о текущей позиции билета из списка
                info = string.Format("{0} of {1}", position, airTickets.Count);
                Console.SetCursorPosition((Console.BufferWidth - info.Length) / 2, 28); //установка позиции курсора
                Console.WriteLine(info); //вывод на консоль строки информации
                return true; //для продолжения общего цикла
            }
            Console.CursorVisible = true; //включение видимости курсора
            Console.SetCursorPosition(12, 15); //установка позиции курсора
            //вывод на консоль сообщения
            Console.Write("Enter how much you want to increase the cost of the flight: ");
            try
            {
                //ввод пользователем добавочной стоимости для билета
                cost = decimal.Parse(Console.ReadLine());
                if (cost < 0) throw new FormatException();             
            }
            catch (FormatException) //если неверный ввод
            {
                //вывод сообщения и прерывание общего цикла
                return errorMessage("Invalid input.");
            }
            airTickets[position - 1] += cost; //добавление стоимости для билета
            Console.CursorVisible = false; //отключение видимости курсора
            title = "New ticket"; //изменение названия заголовка
            leftTitle = 34; //изменение позиции для заголовка от левого края
            Console.Clear(); //очистка консоли
            openForm(); //открытие формы для билета
            //вывод на печать информации об измененном билете
            printInfo(passenger, tariff, airTickets[position - 1]); 
            Console.ReadKey(); //ожидание нажатия клавиши пользователем
            return false; //выход из цикла
        }

        //метод для обработки нажатия клавиши
        public override void HandlerPosition(object sender, PositionEventArgs args)
        {
            if (args.Obj.GetType() == GetType()) //если сообщение было для этого класса
            {
                if (args.Key == ConsoleKey.LeftArrow) //если была нажата клавиша влево
                {
                    //если не начало списка, то переход на предыдущию позицию
                    if (position > 1) position--;
                    //если начало списка, то переход на последнию позицию списка 
                    else position = airTickets.Count;
                }
                else if (args.Key == ConsoleKey.RightArrow) //если была нажата клавиша вправо
                {
                    //если не конец списка, то переход на следующию позицию
                    if (position < airTickets.Count) position++;
                    //если конец списка, то переход на первую позицию списка
                    else position = 1;

                }
                else if (args.Key == ConsoleKey.Enter) //если была нажата клавиша ввода
                {
                    type = 0; //для изменения стоимости и распечатки измененного билета                 
                }
            }
        }
    }
}
