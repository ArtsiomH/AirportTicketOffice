using System; //подключение общей библиотеки классов
using ControlIndependentWork.Passengers; //пространсво имен содержит класс пассажира
using ControlIndependentWork.MyException; //пространство имен содержит классы ошибок

namespace ControlIndependentWork.ScriptsForChangingInformation //пространство имен
{
    //объявление класса сценарий добавления пасажира, наследуемый от класса сценарий
    class AddPassengerScript : Script
    {
        private Passenger passenger; //поле пассажир      

        public AddPassengerScript(Passenger passenger) //параметрический конструктор
        {
            this.passenger = passenger; //присваивание полю пассажир
            title = "Ввод паспортных данных пассажира"; //присваивание полю название сценария
            text.Add("Введите код страны"); //добавлении поля текста
            text.Add("Введите номер паспорта"); //добавлении поля текста
            text.Add("Введите личный номер"); //добавлении поля текста
            text.Add("Введите фамилию"); //добавлении поля текста
            text.Add("Введите имя"); //добавлении поля текста
            text.Add("Введите дату рождения"); //добавлении поля текста
            //если пассажир относится к классу пассажир с процентной скидкой
            if (passenger is PassengerWithPercentDiscount) text.Add("Введите скидку в процентах");
            //если пассадир относится к классу пассажир с фиксированной скидкой
            else text.Add("Введите фиксированную скидку");
            calculation(); //метод для расчета позиций текста и ввода данных
        }

        public void StartScript() //метод запуска сценария ввода данных пассижира
        {            
            while (true) //бесконечный цикл
            {
                Console.Clear(); //очистка консоли
                try
                {
                    Console.CursorVisible = true; //включение видимости курсора
                    openForm(); //открытие формы для заполнения данных пассажира
                    addCodeOfContry(); //метод добавления кода страны
                    addPassportID(); //метод добавления номера паспорта
                    addPersonalNumber(); //метод добавления личного номера
                    addSurname(); //метод добавления фамилии
                    addName(); //метод добавления имени
                    addDateOfBirth(); //метод добавления даты рождения
                    addDiscount(); //метод добавления скидки
                    Console.CursorVisible = false; //отключение видимости курсора
                    AirportTicketOffice.AddPassenger(passenger);
                    return;
                }
                catch(InvalidPassportIDException e) //если неверно введен номер паспорта
                {
                    //сообщение ошибки
                    errorMessage(e.Message, "Пример формата для номера паспорта: MC1475542");
                }
                catch (InvalidPersonNumberException e) //если неверно введен личный номер
                {
                    //сообщение ошибки
                    errorMessage(e.Message, "Пример формата для линого номера: 4110495G024PB5");
                }
                catch (FormatException) //если неверный формат ввода
                {
                    //сообщение ошибки
                    errorMessage("Неверно ввод данных.");
                }                                 
            }                       
        }

        private void addCodeOfContry() //метод добавления кода страны
        {
            Console.SetCursorPosition(leftInput, topInput); //установка позиции курсора
            if (passenger.CodeOfTheContry == null) //если данных нет
            {           
                passenger.CodeOfTheContry = Console.ReadLine(); //ввод кода страны
            }
            else //если данные есть
            {
                Console.WriteLine(passenger.CodeOfTheContry); //вывод на консоль коды страны
            }
        }

        private void addPassportID()
        {
            Console.SetCursorPosition(leftInput, topInput + 2); //установка позиции курсора
            if (passenger.PassportID == null) //если данных нет
            {                
                passenger.PassportID = Console.ReadLine(); //ввод номера паспорта
            }
            else //если данные есть
            {
                Console.WriteLine(passenger.PassportID); //вывод на консоль номера паспорта
            }
        }

        private void addPersonalNumber()
        {
            Console.SetCursorPosition(leftInput, topInput + 4); //установка позиции курсора
            if (passenger.PersonalNumber == null) //если данных нет
            {             
                passenger.PersonalNumber = Console.ReadLine(); //ввод личного номера
            }
            else //если данные есть
            {
                Console.WriteLine(passenger.PersonalNumber); //вывод на консоль личного номера
            }
        }

        private void addSurname()
        {
            Console.SetCursorPosition(leftInput, topInput + 6); //установка позиции курсора
            if (passenger.Surname == null) //если данных нет
            {
                passenger.Surname = Console.ReadLine(); //ввод фамилии
            }
            else //если данные есть
            {
                Console.WriteLine(passenger.Surname); //вывод на консоль фамилии
            }
        }

        private void addName()
        {
            Console.SetCursorPosition(leftInput, topInput + 8); //установка позиции курсора
            if (passenger.Name == null) //если данных нет
            {
                passenger.Name = Console.ReadLine(); //ввод ввод имени
            }
            else //если данные есть
            {
                Console.WriteLine(passenger.Name); //вывод на консоль имени
            }
        }

        private void addDateOfBirth()
        {
            Console.SetCursorPosition(leftInput, topInput + 10); //установка позиции курсора
            if (passenger.DateOfBirth.Year == 1) //если стандартное значение для даты
            { 
                passenger.DateOfBirth = DateTime.Parse(Console.ReadLine()); //ввод даты рождения
            }
            else //если не стандартное значение для даты
            {
                Console.WriteLine(passenger.DateOfBirth.ToShortDateString()); //вывод на консоль даты рождения
            }
        }

        private void addDiscount()
        {
            Console.SetCursorPosition(leftInput, topInput + 12); //установка позиции курсора
            if (passenger is PassengerWithPercentDiscount) //если пассажир с процентной скидкой
            {
                //приведение к типу пассажир с проценстной скидкой
                PassengerWithPercentDiscount passengerWhitPercentDiscount = passenger as PassengerWithPercentDiscount;
                passengerWhitPercentDiscount.PercentDiscount = int.Parse(Console.ReadLine()); //ввод
            }
            else //если пассажир с фиксированной скидкой
            {
                //приведение к типу пассажир с фиксированной скидкой
                PassengerWithFixedDiscount passengerWithFixedDoscount = passenger as PassengerWithFixedDiscount;
                passengerWithFixedDoscount.FixedDiscount = int.Parse(Console.ReadLine()); //ввод
            }
        }
    }
}
