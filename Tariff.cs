using System; //подключение общей библиотеки классов
using ControlIndependentWork.MyException; //пространство имен содержит классы ошибок
using ControlIndependentWork.Form; //пространство имен содержит интерфейс IToShortString

namespace ControlIndependentWork //пространство имен
{
    //объявление класса, реализующий интерфейс IToShortString, для хранения 
    //информации о тарифе
    class Tariff : IToShortString
    {
        private static int count = 1; //счетчик

        private decimal cost; //поле стоимости

        public int TariffNumber { get; private set; } //свойство номер тарифа

        public DirectionName DepartureFrom { get; set; } //свойство город вылета
        public DirectionName FlightTo { get; set; } //свойство город прилета

        public DateTime Date { get; set; } //свойство дата вылета

        public decimal Cost //свойство для поля стоимости
        {
            get { return cost; } //возвращает значение поля стоимость
            set //устанавливает значение поля стоимость
            {
                if (value < 0) //если значение меньше нуля
                {
                    //выбрасывает исключение "отрицательная стоимость"
                    throw new NegativeCostException();
                }
                cost = value; //присваивание значения стоимости
            }
        }

        public Tariff() //конструктор
        {
            TariffNumber = count++; //присваиваем номер тарифу и увеличваем счетчик
        }

        //параметрический конструктор
        public Tariff(DirectionName departureFrom, DirectionName flightTo, DateTime date, decimal cost)
        {
            TariffNumber = count++; //присваиваем номер тарифу и увеличваем счетчик
            DepartureFrom = departureFrom; //присваивание полю город вылета
            FlightTo = flightTo; //присваивание полю город прилета
            Date = date; //присваивание полю даты вылета
            Cost = cost; //присваивание полю стоимости
        }

        //реавлизация интерфейса IToShortString
        public string ToShortString()
        {
            //возвращает форматированную строку
            return string.Format("{0} ({1}) - {2} ({3}) Дата вылета: {4} Стоимость: {5} у.е.", DepartureFrom.FullDirectionName, DepartureFrom.ShortDirectionName, 
                                FlightTo.FullDirectionName, FlightTo.ShortDirectionName, Date.ToShortDateString(), Cost.ToString());
        }

        //пререопределенный метод ToString
        public override string ToString()
        {
            //возвращает запись об объекте
            return TariffNumber.ToString() + ' ' + DepartureFrom.ToString() + ' ' + FlightTo.ToString() + ' ' + Date.ToShortDateString() + ' ' + Cost.ToString();
        }
    }
 
}
