using System; //подключение общей библиотеки классов
using System.Text.RegularExpressions; //пространство имен содержит класс регулярных выражений
using ControlIndependentWork.MyException; //пространство имен содержит классы ошибок
using ControlIndependentWork.Form; //пространство имен содержит интерфейс IToShortString

namespace ControlIndependentWork.Passengers //пространство имен
{
    //объявление абстрактного класса, реализующий интерфейс IToShortString, для хранения 
    //паспортных данных пассажира
    abstract class Passenger : IToShortString
    {
        private string passportID; //поле номера паспорта
        private string personalNumber; //поле личного номера

        public string CodeOfTheContry { get; set; } //свойство кода страны

        public string Surname { get; set; } //свойство фамилии

        public string Name { get; set; } //свойство имени

        public DateTime DateOfBirth { get; set; } //свойство даты рождения

        public string PassportID //свойство для поля номера паспорта
        {
            get { return passportID; } //возвращает значение поля
            set //аксессор установки значения поля номер паспорта
            {
                //создание объекта регулярного выражения
                Regex mask = new Regex(@"[A-z]{2}[0-9]{7}");
                if ( !mask.IsMatch(value) ) //проверка формата строки
                {
                    //если не подходит формат, то генерируется ошибка
                    //"неверный номер паспорта" 
                    throw new InvalidPassportIDException();
                }
                passportID = value; //если проверка успешна, то присваеваем значение
            }
        }

        public string PersonalNumber //свойство для поля личного номера
        {
            get { return personalNumber; } //возвращает значение поля
            set //аксессор установки значения поля личный номер
            {
                //создание объекта регулярного выражения
                Regex mask = new Regex(@"[0-9]{7}[A-z][0-9]{3}[A-z]{2}[0-9]");
                if (!mask.IsMatch(value)) //проверка формата строки
                {
                    //если не подходит формат, то генерируется ошибка
                    //"неверный личный номер" 
                    throw new InvalidPersonNumberException();
                }
                personalNumber = value; //если проверка успешна, то присваеваем значение
            }
        }

        public Passenger() //конструктор по умолчанию
        { }

        //параметрический конструктор
        public Passenger(string codeOfTheCountry, string passportID, string personalNumber, string surname, string name, DateTime dateOfBirth)
        {
            CodeOfTheContry = codeOfTheCountry; //присваивание полю код страны
            PassportID = passportID; //присваивание поле номер паспорта
            PersonalNumber = personalNumber; //присваивание полю личный номер
            Surname = surname; //присваивание полю фамилии
            Name = name; //присваивание полю имени
            DateOfBirth = dateOfBirth; //присваивание полю даты рождения 
        }

        //реализация интерфейса IToShortString
        public string ToShortString()
        {
            //возвращает форматированную строку
            return string.Format("Passport ID: {0}  Surname: {1}  Name: {2}", PassportID, Surname, Name);
        }

        //переопределенный метод ToString
        public override string ToString()
        {
            //возвращает строку со значениями всех полей
            return CodeOfTheContry + ' ' + PassportID + ' ' + PersonalNumber + ' ' + Surname + ' ' + Name + ' ' + DateOfBirth.ToShortDateString();
        }
    }
}
