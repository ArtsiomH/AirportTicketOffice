using System; //подключение общей библиотеки классов
using System.Collections.Generic; //подключение библиотеки классов, определяющих типизированные коллекции
using System.Linq; //пространство имен System.Linq содержит классы и интерфейсы, которые поддерживают запросы, использующие LINQ
using ControlIndependentWork.Passengers; //пространсво имен содержит класс пассажира

namespace ControlIndependentWork.TestInformation //пространство имен
{
    class RandomAirTicket //объявление класса рандомный билет
    {
        private Random rand = new Random(); //объект рандома

        private List<int> passegers; //список пассажиров
        private List<int> tariffs; //список тарифов

        public RandomAirTicket() //конструктор
        {
            //получение списка пассажиров
            passegers = AirportTicketOffice.ListOfPassengers.Keys.ToList();
            //получение списка тарифов
            tariffs = AirportTicketOffice.ListOfTariffs.Keys.ToList();
        }

        public AirTicket Next() //метод для получения следующего рандомного билета
        {
            //олучение рандомного тарифа
            Tariff tariff = AirportTicketOffice.ListOfTariffs[tariffs[rand.Next(0, tariffs.Count)]];
            //получение рандомного пассажира
            Passenger passenger = AirportTicketOffice.ListOfPassengers[passegers[rand.Next(0, passegers.Count)]];
            return new AirTicket(tariff, passenger); //возвращает рандомнй билет
        }
    }
}
