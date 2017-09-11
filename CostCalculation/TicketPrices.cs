using System; //подключение общей библиотеки классов
using System.Linq; //Пространство имен System.Linq содержит классы и интерфейсы, которые поддерживают запросы, использующие LINQ

namespace ControlIndependentWork.CostCalculation //пространство имен
{
    class TicketPrices //объявление класса стоимости всех билетов
    {
        public static void Cost() //метод для расчета стоимости билетов с учетом скидки
        {
            Console.Clear(); //очистка консоли
            //расчет суммы всех билетов
            decimal sum = AirportTicketOffice.ListJourneyTickets.Sum(x => x.DiscountedPrice);
            //создание форматированной строки вывода на консоль           
            string str = string.Format("The cost of all tickets including discounts: {0} cu", sum);          
            Console.SetCursorPosition((Console.BufferWidth - str.Length) / 2, 15); //установка положения курсора
            Console.WriteLine(str); //вывод строки на консоль
            Console.ReadKey(); //ожидание нажатия клавиши пользователем
        }
    }
}
