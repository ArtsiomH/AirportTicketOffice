using System; //подключение общей библиотеки классов

namespace ControlIndependentWork.TestInformation //пространство имен
{
    //объявление класса добавление тестовой информации
    static class AddingTestInform
    {
        static AddingTestInform() //статический конструктор
        {
            //перебор наименований полученных из метода 
            foreach (DirectionName directionName in Directions.AddDirections())
            {
                AirportTicketOffice.AddDirection(directionName); //добавление в список
            }
        }

        //метод добавления пассажиров, тарифов и билетов в списки
        public static void Add(int passengers, int tariffs, int airTickets)
        {
            //создание объекта рандомного пассажира           
            RandomPassenger passenger = new RandomPassenger();
            for (int i = 0; i < passengers; i++) //кол-во пассажир
            {
                //добавление пассажира в список
                AirportTicketOffice.AddPassenger(passenger.Next());            
            }
            //создание объекта рандомного тарифа
            RandomTariff randomTariff = new RandomTariff();
            for (int i = 0; i < tariffs; i++) //кол-во тарифов
            {
                //добавление тарифа в список
                AirportTicketOffice.AddTariff(randomTariff.Next());
            }
            //создание объекта рандомного билета
            RandomAirTicket randomAirTicket = new RandomAirTicket();
            for (int i = 0; i < airTickets; i++) //кол-во билетов
            {
                try
                {
                    //добавление билета
                    AirportTicketOffice.AddAirTicket(randomAirTicket.Next());
                }
                catch { } //если билет существует, то пропустить добавление
            }
            Console.Clear(); //очистка консоли
            Console.SetCursorPosition(27, 15); //установка позиции курсора
            Console.WriteLine("Тестовые данные добавлены."); //сообщение о выполнении
            Console.ReadKey(); //ожидание нажатия клавиши пользователем     
        }
    }
}
