using System.Collections.Generic; //подключение общей библиотеки классов
using System.Linq; //пространство имен System.Linq содержит классы и интерфейсы, которые поддерживают запросы, использующие LINQ
using ControlIndependentWork.Passengers; //пространсво имен содержит класс пассажира
using ControlIndependentWork.MyException; //пространство имен содержит классы ошибок

namespace ControlIndependentWork //пространство имен
{
    class AirportTicketOffice //объявление класса касса аэропорта
    {
        private static readonly NameAirport airoportTicketOfficeName;  //поле наименования кассы аэропорта  
        private static Dictionary<int, DirectionName> listOfDirections; //коллекция направлений      
        private static Dictionary<int, Tariff> listOfTariffs; //коллекция тарифов
        private static Dictionary<int, Passenger> listOfPassengers; //коллекция пассажиров
        private static HashSet<AirTicket> listJourneyTickets; //коллекция авиабилетов
       
        public static NameAirport AiroportTicketOfficeName //свойство поля наименовния аэропорта
        {
            get { return airoportTicketOfficeName; } //получить поле наименование аэропорта
        }
      
        public static Dictionary<int, DirectionName> ListOfDirections //свойство коллекции направлений
        {
            get { return listOfDirections; } //получить список направлений        
        }
        
        public static Dictionary<int, Tariff> ListOfTariffs //свойство коллекции тарифов
        {
            get { return listOfTariffs; } //получить список тарифов
        }
      
        public static Dictionary<int, Passenger> ListOfPassengers //свойство коллекции пассажиров
        {
            get { return listOfPassengers; } //получить коллекцию пассажиров
        }
       
        public static HashSet<AirTicket> ListJourneyTickets //свойство коллекции авиабилетов
        {
            get { return listJourneyTickets; } //получить коллекцияю авиабилетов
        }
        
        static AirportTicketOffice() //статический конструктор
        {           
            airoportTicketOfficeName = new NameAirport("MSQ", "Minsk"); //присваивание названия кассы аэропорта         
            listOfDirections = new Dictionary<int, DirectionName>(); //инициализация пустой коллекции направлений                   
            listOfTariffs = new Dictionary<int, Tariff>(); //инициализация пустоой коллекции тарифов
            listOfPassengers = new Dictionary<int, Passenger>(); //инициализация пустой коллекции пассажиров         
            listJourneyTickets = new HashSet<AirTicket>(); //инициализация пустой коллекции билетов
        }
       
        public static void AddDirection(DirectionName directionName) //метод для добавления напрвления в коллекцию
        {                      
            listOfDirections.Add(directionName.GetHashCode(), directionName); //добавление напрвления в коллекцию
        }
      
        public static void AddTariff(Tariff tariff) //метод для добавления тарифа в коллекцию
        {
            listOfTariffs.Add(tariff.TariffNumber, tariff); //добавление тарифа в коллекцию
        }
       
        public static void AddPassenger(Passenger passenger) //метод для добавления пассажира в коллекцию
        {
            
            listOfPassengers.Add(passenger.PersonalNumber.GetHashCode(), passenger); //добавление пассажира в коллекцию
        }
        
        public static void AddAirTicket(AirTicket airTicket) //метод для добавления авиабилета в коллекцию
        {   
            //если в коллекции уже существует авиабилет
            if (listJourneyTickets.Any(x => x.PersonalNumber == airTicket.PersonalNumber && x.TariffID == airTicket.TariffID))
            {
                throw new InstanceExistsException(); //выбрасывает исключение "экземпляр существует в коллекции"                
            }
            listJourneyTickets.Add(airTicket); //добавление авивабилета в коллекцию
        }

        public static void RemoveDirection(DirectionName direction) //метод удаления направления
        {
            //находим в коллекции названий ключ объекта
            int directionID = listOfDirections.First(x => x.Key == direction.ShortDirectionName.GetHashCode()).Key;
            //находим все тарифы где встречается ключ направления
            List<Tariff> tariffs = listOfTariffs.Values.Where(x => x.FlightTo.GetHashCode() == directionID ||
                                                              x.DepartureFrom.GetHashCode() == directionID).ToList();
            foreach (Tariff tariff in tariffs) //пребираем полученный список тарифов
            {
                RemoveTariff(tariff); //удаляем тариф
            }
            listOfDirections.Remove(directionID); //удаляем направление
        }

        public static void RemovePassenger(Passenger passenger) //метод удаления пссажира
        {
            //находи личный номер пассажира
            int iD = listOfPassengers.First(x => x.Key == passenger.PersonalNumber.GetHashCode()).Key;           
            listJourneyTickets.RemoveWhere(x => x.PersonalNumber == iD); //удаляем все билеты, включающие этого пасажира
            listOfPassengers.Remove(iD); //удаляем пассажира
        }

        public static void RemoveTariff(Tariff tariff) //метод для удаления тарифа
        {
            //удалем все билеты, включающие этот тариф
            listJourneyTickets.RemoveWhere(x => x.TariffID == tariff.TariffNumber);
            listOfTariffs.Remove(tariff.TariffNumber); //удалаем тариф
        }           
    }
}
