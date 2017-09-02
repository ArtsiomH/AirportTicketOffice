using ControlIndependentWork.Passengers; //пространсво имен содержит класс пассажира
using ControlIndependentWork.MyException; //пространство имен содержит классы ошибок

namespace ControlIndependentWork //пространство имен
{
    class AirTicket //объявление класса авиабилет
    {
        private decimal discountedPrice; //поле стоимость со скидкой

        public int TariffID { get; set; } //свойство номер тарифа

        public int PersonalNumber { get; set; } //свойство личный номер

        public decimal DiscountedPrice //свойство поля скидки
        {
            get { return discountedPrice; } //получить значение поля стоимость со скидкой
            set //установить скидку
            {
                if (value < 0) //если значение отицательное
                {
                    //выбрасываем исключение "отрицательная стоимость"
                    throw new NegativeCostException();
                }
                discountedPrice = value; //присваиваем значение полю стоиости со скидкой 
            }
        }


        public AirTicket() //конструктор по умолчанию
        { }
       
        public AirTicket(Tariff tariff, Passenger passenger) //параметрический конструктор
        {            
            TariffID = tariff.TariffNumber; //присваивание полю номер тарифа            
            PersonalNumber = passenger.PersonalNumber.GetHashCode(); //присваивание полю личный номер
            if (passenger is PassengerWithFixedDiscount) //если пассажир с фиксированной скидкой
            {
                //рассчитываем стоимость и присваиваем полю стоимость со скидкой
                DiscountedPrice = tariff.Cost - ((PassengerWithFixedDiscount)passenger).FixedDiscount;
            }
            else if (passenger is PassengerWithPercentDiscount) //если пассажир с процентной скидкой
            {
                //рассчитываем стоимость и присваиваем полю стоимость со скидкой
                DiscountedPrice = tariff.Cost * ((100 - ((PassengerWithPercentDiscount)passenger).PercentDiscount) / 100);
            }
        }

        //прегруженный бинарный оператор для увеличения стоимости авиабилета
        public static AirTicket operator +(AirTicket ticket, decimal addCost)
        {
            //увеличиваем стоимость авиабилета
            ticket.DiscountedPrice += addCost;
            return ticket; //возвращаем авиабилет
        }
        
        //переопределенный метод ToString
        public override string ToString()
        {
            //возвращаем строку со всеми параметрами
            return TariffID.ToString() + PersonalNumber.ToString() + DiscountedPrice;
        }

        //метод для получение короткой записи
        public string ToShortString()
        {
            //возвращаем короткую запись об объекте
            return TariffID.ToString() + PersonalNumber.ToString();
        }

        //переопределенный метод получения хэш кода
        public override int GetHashCode()
        {
            //возвращаем хэш код
            return ToShortString().GetHashCode();
        }
    }
}
