using System; //подключение общей библиотеки классов

namespace ControlIndependentWork.Passengers //пространство имен
{
    //объявление класса пассажира с фиксированной скидкой, наследуемого от класса пассажир
    class PassengerWithFixedDiscount : Passenger
    {
        public decimal FixedDiscount { get; set; } //свойство для поля фиксированная скидка

        public PassengerWithFixedDiscount() //конструктор по умолчанию
        { }

        //параметрический конструктор
        public PassengerWithFixedDiscount(decimal fixedDiscount)
        {
            FixedDiscount = fixedDiscount; //присваивание полю фиксированная скидка
        }

        //параметрический конструктор, наследуемый от базового
        public PassengerWithFixedDiscount(string codeOfTheCountry, string passportID, string personalNumber, string surname, string name, DateTime dateOfBirth, decimal fixedDiscount) 
                                          : base(codeOfTheCountry, passportID, personalNumber, surname, name, dateOfBirth)
        {
            FixedDiscount = fixedDiscount; //присваивание полю фиксированная скидка
        }
        
        //переопределенный метод ToString()
        public override string ToString()
        {
            //возвращает строку со значениями всех полей
            return base.ToString() + ' ' + FixedDiscount.ToString();
        }
    }
}
