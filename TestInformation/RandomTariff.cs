using System; //подключение общей библиотеки классов
using System.Collections.Generic; //подключение библиотеки классов, определяющих типизированные коллекции
using System.Linq; //пространство имен System.Linq содержит классы и интерфейсы, которые поддерживают запросы, использующие LINQ

namespace ControlIndependentWork.TestInformation //пространство имен
{
    class RandomTariff //объявление класса рандомный тариф
    {
        private Random rand = new Random(); //объект рандома
        private List<int> listKeys; //список ключей
        private DirectionName departureFrom;
        private DirectionName flightTo;

        public RandomTariff()
        {
            //получаем список ключей направлений
            listKeys = AirportTicketOffice.ListOfDirections.Keys.ToList();
        }

        //метод получения рандомного направления "откуда - куда"
        private void randomDepatureFromAndFlightTo()
        {
            if (rand.Next(0, 2) == 0) //если 0 с нашего аэропорта в иной
            {
                //получение наименования нашего аэропорта
                departureFrom = AirportTicketOffice.AiroportTicketOfficeName;
                //получение рандомного направления 
                flightTo = AirportTicketOffice.ListOfDirections[listKeys[rand.Next(0, listKeys.Count)]];                                
            }
            else //если 1 от иного аэропорта к нашему
            {
                //получение рандомного направления
                departureFrom = AirportTicketOffice.ListOfDirections[listKeys[rand.Next(0, listKeys.Count)]];
                //получение наименования нашего аэропорта
                flightTo = AirportTicketOffice.AiroportTicketOfficeName;
            }
        }

        private DateTime randomDate() //метод для рнадомной даты вылета
        {
            DateTime start = DateTime.Today; //начальная дата
            return start.AddDays(rand.Next(0, 90)); //вернуть рандомную дату вылета
        }

        private decimal randomCost() //метод для рандомной стоимости билета
        {
            return rand.Next(200, 501); //вернуть рандомную стоимость
        }

        public Tariff Next() //метод для получения следующего рандомного тарифа 
        {
            randomDepatureFromAndFlightTo(); //получения рандомного направления "откуда - куда"
            //возвращает рандомный тариф
            return new Tariff(departureFrom, flightTo, randomDate(), randomCost());
        }

    }
}
