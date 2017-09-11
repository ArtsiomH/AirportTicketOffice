using System; //подключение общей библиотеки классов
using System.Collections.Generic; //подключение библиотеки классов, определяющих типизированные коллекции
using System.Linq; //пространство имен System.Linq содержит классы и интерфейсы, которые поддерживают запросы, использующие LINQ
using ControlIndependentWork.Passengers; //пространсво имен содержит класс пассажира
using ControlIndependentWork.MyEvent; //пространство имен содержит событие обработки нажатия
using ControlIndependentWork.Form; //пространство имен содержит метод формы вывода на экран объекта из списка
using ControlIndependentWork.MyException; //пространство имен содержит классы ошибок

namespace ControlIndependentWork.ScriptsForChangingInformation //пространство имен
{
    //объявление класса сценарий добавления авиабилета, реализующий интерфейс IHandlerPosition
    //и наследуемый от класса сценарий 
    class AddAirTicketScript : Script, IHandlerPosition
    {
        //список тарифов
        protected List<Tariff> tariffs = AirportTicketOffice.ListOfTariffs.Values.ToList();
        //список пассажиров
        protected List<Passenger> passengers = AirportTicketOffice.ListOfPassengers.Values.ToList();       
        protected Passenger passenger; //поля пассажир
        protected Tariff tariff; //поле тариф
        protected int position = 1; //поле позиции
        protected int type = 1; //поля типа
        private AirTicket airTicket = new AirTicket(); //поле авиабилет

        public bool ListIsEmpty //свойство список является пустым
        {
            get //аксессор получения результата проверки списка 
            {
                //возвращает true, если список пустой
                //возвращает false, если список не пуст
                return listCheck();
            }
        }

        public AddAirTicketScript(string title) //параметричемкий конструктор
        {
            this.title = title; //присваивание полю название сценария          
            text.Add("Code of the country"); //добавлении поля текста
            text.Add("Passport ID"); //добавлении поля текста
            text.Add("Personal number"); //добавлении поля текста
            text.Add("Surname"); //добавлении поля текста
            text.Add("Name"); //добавлении поля текста
            text.Add("Date of birth"); //добавлении поля текста
            text.Add("City of departure"); //добавлении поля текста
            text.Add("City of arrival"); //добавлении поля текста
            text.Add("Departure date"); //добавлении поля текста
            text.Add("Cost of flight"); //добавлении поля текста
            text.Add("Cost of flight for a passenger"); //добавлении поля текста
            calculation(); //метод для расчета позиций текста и ввода данных 
        }

        protected virtual bool listCheck() //метод проверки списка
        {
            //если один из списков пуст
            if (tariffs.Count == 0 || passengers.Count == 0)
            {
                Console.Clear(); //очистка консоли
                Console.SetCursorPosition(19, 15); //установка позиции курсора
                //вывод сообщения на консоль
                Console.WriteLine("It is necessary to add tariffs or passengers");
                Console.ReadKey(); //ожидание нажатия клавиши пользователем
                return true; //список пуст
            }
            return false; //список не пуст
        }

        public virtual bool Start() //метод запуска сценария ввода данных пользователем
        {           
            Console.Clear(); //очистка консоли
            if (type == 1) //первый тип для выбора пассажира
            {
                //вывод на консоль описание объекта пассажира
                FormForList.Show("Select a passenger", passengers, position);
                return true; //для продолжения общего цикла
            }
            if (type == 2) //второй тип для выбора тарифа
            {
                //вывод на консоль описание объекта тариф
                FormForList.Show("Select a tariff", tariffs, position);
                return true; //для продолжения общего цикла
            }
            while (true) //бесконечный цикл
            {               
                try
                {
                    //по выбраным данным создается объект авиабилета
                    airTicket = new AirTicket(tariff, passenger);
                    //добавление в коллекцию авиабилета
                    AirportTicketOffice.AddAirTicket(airTicket);
                }
                //если отрицательное значение стоимости при учете скидки пассажира
                catch (NegativeCostException e) 
                {
                    //выводит сообщение об ошибке и прерывает цикл
                    return errorMessage(e.Message, "Air ticket not registered");
                }
                //если экземпляр уже существует в коллекции
                catch(InstanceExistsException e)
                {
                    //выводит сообщение об ошибке и прерывает цикл
                    return errorMessage(e.Message, "Air ticket not registered");                   
                }
                openForm(); //выводит на консоль форму для авиабилета
                //выводит на консоль информацию о авиабилете
                printInfo(passenger, tariff, airTicket); 
                Console.ReadKey(); //ожидает нажитие клавиши пользователем
                return false; //прерывает цикл
            }
        }

        //метод для вывода на консоль информации об авиабилете
        protected void printInfo(Passenger passenger, Tariff tariff, AirTicket airTicket)
        {
            Console.SetCursorPosition(leftInput, topInput); //установка позиции курсора
            Console.WriteLine(passenger.CodeOfTheContry); //вывод на консоль кода страны
            Console.SetCursorPosition(leftInput, topInput + 2); //установка позиции курсора
            Console.WriteLine(passenger.PassportID); //вывод на консоль номера паспорта
            Console.SetCursorPosition(leftInput, topInput + 4); //установка позиции курсора
            Console.WriteLine(passenger.PersonalNumber); //вывод на консоль личного номера
            Console.SetCursorPosition(leftInput, topInput + 6); //установка позиции курсора
            Console.WriteLine(passenger.Surname); //вывод на консоль фамилии
            Console.SetCursorPosition(leftInput, topInput + 8); //установка позиции курсора
            Console.WriteLine(passenger.Name); //вывод на консоль имени
            Console.SetCursorPosition(leftInput, topInput + 10); //установка позиции курсора
            Console.WriteLine(passenger.DateOfBirth.ToShortDateString()); //вывод на консоль даты рождения
            Console.SetCursorPosition(leftInput, topInput + 12); //установка позиции курсора
            Console.WriteLine(tariff.DepartureFrom.FullDirectionName); //вывод на консоль города вылета
            Console.SetCursorPosition(leftInput, topInput + 14); //установка позиции курсора
            Console.WriteLine(tariff.FlightTo.FullDirectionName); //вывод на консоль города прилета
            Console.SetCursorPosition(leftInput, topInput + 16); //установка позиции курсора
            Console.WriteLine(tariff.Date.ToShortDateString()); //вывод на консоль даты вылета 
            Console.SetCursorPosition(leftInput, topInput + 18); //установка позиции курсора
            Console.WriteLine("{0} cu", tariff.Cost); //вывод на консоль полной стоимости авиабилета
            Console.SetCursorPosition(leftInput, topInput + 20); //установка позиции курсора
            Console.WriteLine("{0} cu", airTicket.DiscountedPrice); //вывод на консоль стоимости для пасажира
        }     

        //метод для обработки нажатия клавиши
        public virtual void HandlerPosition(object sender, PositionEventArgs args) 
        {
            if (args.Obj.GetType() == GetType()) //если сообщение было для этого класса
            {
                if (args.Key == ConsoleKey.UpArrow) //если была нажата клавиша вверх
                {
                    if (type == 1) //для списка пассажиров
                    {
                        //если не начало списка, то переход на предыдущию позицию
                        if (position > 1) position--;
                        //если начало списка, то переход на последнию позицию списка 
                        else position = passengers.Count; 
                    }
                    else if (type == 2) //для списка тарифов
                    {
                        //если не начало списка, то переход на предыдущию позицию
                        if (position > 1) position--;
                        //если начало списка, то переход на последнию позицию списка 
                        else position = tariffs.Count; 
                    }
                }
                else if (args.Key == ConsoleKey.DownArrow) //если была нажата клавиша вниз
                {

                    if (type == 1) //для списка пассажиров
                    {
                        //если не конец списка, то переход на следующию позицию
                        if (position < passengers.Count) position++;
                        //если конец списка, то переход на первую позицию списка
                        else position = 1;
                    }
                    else if (type == 2) //для списка тарифов
                    {
                        //если не конец списка, то переход на следующию позицию
                        if (position < tariffs.Count) position++;
                        //если конец списка, то переход на первую позицию списка
                        else position = 1;
                    }
                }
                else if (args.Key == ConsoleKey.Enter) //если была нажата клавиша ввода
                {
                    if (type == 1) //для списка пассажиров
                    {
                        //присваивание пассажира из списка по позиции
                        passenger = passengers[position - 1];
                        type = 2; //переходим на список тарифов
                        position = 1; //обновляем позицию
                        return; //повторение сценария для выбора тарифа
                    }
                    else if (type == 2) //для списка тарифов
                    {
                        //присваивание тарифа из списка по позиции
                        tariff = tariffs[position - 1];
                        type = 0; //закончить выбор объектов для авиабилета
                        return; //продолжение сценария
                    }
                }
            }
        }
    }
}
