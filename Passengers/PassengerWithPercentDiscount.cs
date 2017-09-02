using System; //подключение общей библиотеки классов

namespace ControlIndependentWork.Passengers //пространство имен
{
    //объявление класса пассажира с процентной скидкой, наследуемого от класса пассажира
    class PassengerWithPercentDiscount : Passenger
    {
        public decimal PercentDiscount { get; set; } //свойство для поля процентной скидки

        public PassengerWithPercentDiscount() //конструктор по умолчанию
        { }

        //параметрический конструктор
        public PassengerWithPercentDiscount(decimal percentDiscount)
        {
            PercentDiscount = percentDiscount; //присваивание полю процентной скидки
        }

        //параметрический конструктор, наследуемый от базового
        public PassengerWithPercentDiscount(string codeOfTheCountry, string passportID, string personalNumber, string surname, string name, DateTime dateOfBirth, decimal percentDiscount) 
                                            : base(codeOfTheCountry, passportID, personalNumber, surname, name, dateOfBirth)
        {
            PercentDiscount = percentDiscount; //присваивание полю фпроцентной скидки
        }

        //переопределенный метод ToString()
        public override string ToString()
        {
            //возвращает строку со значениями всех полей
            return base.ToString() + ' ' + PercentDiscount.ToString();
        }
    }
}
