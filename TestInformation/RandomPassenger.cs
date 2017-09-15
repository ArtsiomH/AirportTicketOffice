using System; //подключение общей библиотеки классов
using System.Collections.Generic; //подключение библиотеки классов, определяющих типизированные коллекции
using System.IO; //содержит типы, позволяющие осуществлять чтение и запись в файлы и потоки данных
using System.Text; //содержит классы, представляющие кодировки ASCII и Юникод, абстрактные 
//базовые классы для преобразования блоков символов в блоки байтов и обратно и класс поддержки, 
//управляющий объектами String и форматирующий такие объекты без создания промежуточных экземпляров String.
using ControlIndependentWork.Passengers; //пространсво имен содержит класс пассажира

namespace ControlIndependentWork.TestInformation //пространство имен
{
    class RandomPassenger //объявление класса рандомный пассажир
    {
        private Random rand = new Random(); //объект рандома
        private List<string> listOfSurname = new List<string>(); //список фамилий
        private List<string> listOfMaleNames = new List<string>(); //список мужских имен
        private List<string> listOfFemaleNames = new List<string>(); //список женских имен
        private List<string> listOfRegions = new List<string>(); //список регионов
        private Passenger passenger; //поле пассажир   

        public RandomPassenger() //конструктор
        {
            //считывание с файла список фамилий 
            listOfSurname.AddRange(File.ReadAllLines("surnames.txt", Encoding.UTF7));
            //считывание с файла список муских имен
            listOfMaleNames.AddRange(File.ReadAllLines("male names.txt", Encoding.UTF7));
            //считывание с файла список женских имен
            listOfFemaleNames.AddRange(File.ReadAllLines("female names.txt", Encoding.UTF7));
            //считывание с файла список регионов
            listOfRegions.AddRange(File.ReadAllLines("region.txt", Encoding.UTF7));         
        }

        //метод для получения рандомной даты рождения
        private DateTime randomDateTime() 
        {
            DateTime start = new DateTime(1980, 1, 1); //начальная дата
            //промежуток времени         
            TimeSpan days = (DateTime.Today - start) - (new TimeSpan(92, 0, 0));
            return start.AddDays(rand.Next(0, days.Days)); //возвращение рандомной даты рождения
        }

        private string randomSurname() //метод для получения рандомной фамилии
        {
            return listOfSurname[rand.Next(0, listOfSurname.Count)]; //возвращает рандомную фамилию
        }

        private string randomName() //метод для получения рандомного имени
        {
            if (rand.Next(0, 2) == 0) //если 0
            {
                return listOfMaleNames[rand.Next(0, listOfMaleNames.Count)]; //возвращает мужское имя
            }
            passenger.Surname = passenger.Surname + 'a'; //склоняем фамилию
            return listOfFemaleNames[rand.Next(0, listOfFemaleNames.Count)]; //возвращает женское имя      
        }

        private string randomPassportID() //метод получения рандомного номера паспорта
        {
            string region = listOfRegions[rand.Next(0, listOfRegions.Count)]; //рандомный регион
            //возвращаем рандомный номер паспорта
            return region + rand.Next(1000000, 9999999).ToString(); 
        }

        private string randomPersonalNumber() //метод для получения рандомного личного номера
        {
            StringBuilder number = new StringBuilder(); //объект строки
            for (int i = 0; i < 14; i++) //цикл заполнения
            {
                if (i == 7 || i == 11 || i == 12) //позиции для символов
                {
                    //рандомный символ
                    number.Append((char)rand.Next(65, 91));
                    continue; //переход на следующию интерацию
                }
                number.Append(rand.Next(0, 10)); //рандомная цифра    
            }
            return number.ToString(); //возвращаем личный номер
        }

        public Passenger Next() //метод для получения следующего рандомного пассажира
        {
            if (rand.Next(0, 2) == 0) //если 0 пассажир с процентной скидкой
            {
                //присваиваем нового пассажира с процентной скидкой
                passenger = new PassengerWithPercentDiscount(rand.Next(5, 11));
            }
            //присваиваем нового пассажира с фиксированной скидкой
            else passenger = new PassengerWithFixedDiscount(rand.Next(50, 101));
            passenger.CodeOfTheContry = "BLR"; //код страны
            passenger.DateOfBirth = randomDateTime(); //дата рождения
            passenger.Surname = randomSurname(); //фамилия
            passenger.Name = randomName(); //имя
            passenger.PassportID = randomPassportID(); //номер пасспорта
            passenger.PersonalNumber = randomPersonalNumber(); //личный номер
            return passenger; //возвращаем рандомного пассажира           
        }
    }
}
