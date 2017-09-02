using System; //подключение общей библиотеки классов
using System.Collections.Generic; //подключение библиотеки классов, определяющих типизированные коллекции
using System.Linq; //пространство имен System.Linq содержит классы и интерфейсы, которые поддерживают запросы, использующие LINQ
using ControlIndependentWork.MyEvent; //пространство имен содержит событие обработки нажатия
using ControlIndependentWork.MyException; //пространство имен содержит классы ошибок
using ControlIndependentWork.Form; //пространство имен содержит метод формы вывода на экран объекта из списка

namespace ControlIndependentWork.ScriptsForChangingInformation //пространство имен
{
    //объявление класса сценарий добавления тарифа, реализующий интерфейс IHandlerPosition
    //и наследуемый от класса сценарий 
    class AddTariffScript : Script, IHandlerPosition
    {
        private Tariff tariff = new Tariff();  //поле тариф
        private List<DirectionName> directions; //список направлений
        private int position = 1; //пле позиции
        private int type = 1; //поле типа

        public bool ListIsEmpty //свойство список является пустым
        {
            get //аксессор получения результата проверки списка 
            {
                if (directions.Count == 0) //если списков пуст
                {
                    Console.Clear(); //очистка консоли
                    Console.SetCursorPosition(28, 15); //установка позиции курсора
                    Console.WriteLine("Список направлений пуст."); //вывод сообщения на консоль
                    Console.ReadKey(); //ожидание нажатия клавиши пользователем
                    return true; //список пуст
                }
                return false; //список не пуст
            }
        }

        public AddTariffScript() //конструктор по умолчанию
        {   
            //получение списка наименований направления    
            directions = AirportTicketOffice.ListOfDirections.Values.ToList();
            //если направления есть, то добавляем наше направление
            if (directions.Count != 0) directions.Add(AirportTicketOffice.AiroportTicketOfficeName);
            title = "Ввод данных об тарифе"; //присваивание полю название сценария
            text.Add("Город вылета"); //добавлении поля текста
            text.Add("Город прилета"); //добавлении поля текста
            text.Add("Введите дату отправления"); //добавлении поля текста
            text.Add("Введите стоимость полета"); //добавлении поля текста
            calculation(); //метод для расчета позиций текста и ввода данных            
        }

        public bool Start() //метод запуска сценария ввода данных пассижира
        {
            Console.Clear(); //очистка консоли
            if (type == 1) //первый тип для выбора направления откуда 
            {
                //вывод на консоль описание объекта направления
                FormForList.Show("Выберите пунк отправления", directions, position);
                return true; //для продолжения общего цикла
            }
            if (type == 2) //второй тип для выбора направления куда
            {
                //вывод на консоль описание объекта направления
                FormForList.Show("Выберите пункт прибытия", directions, position);
                return true; //для продолжения общего цикла
            }           
            while (true) //бесконечный цикл
            {
                try
                {
                    Console.CursorVisible = true; //включение видимости курсора                    
                    openForm(); //открытие формы для заполнения данных тарифа
                    Console.SetCursorPosition(leftInput, topInput); //установка позиции курсора
                    //выввод на консоль наименнование выбранного напрвления откуда
                    Console.WriteLine(tariff.DepartureFrom.FullDirectionName);
                    Console.SetCursorPosition(leftInput, topInput + 2); //установка позиции курсора
                    //выввод на консоль наименнование выбранного напрвления куда
                    Console.WriteLine(tariff.FlightTo.FullDirectionName);
                    Console.SetCursorPosition(leftInput, topInput + 4); //установка позиции курсора
                    if (tariff.Date.Year == 1) //если стандартное значение даты 
                    {
                        //ввод даты вылета пользователем
                        tariff.Date = DateTime.Parse(Console.ReadLine());
                    }
                    else //если не стандартное значение даты
                    {
                        //вывод на консоль даты вылета
                        Console.WriteLine(tariff.Date.ToShortDateString());
                    }
                    Console.SetCursorPosition(leftInput, topInput + 6); //установка позиции курсора
                    tariff.Cost = decimal.Parse(Console.ReadLine()); //ввод стоимости тарифа
                    Console.CursorVisible = false; //отключение видимости курсора
                    AirportTicketOffice.AddTariff(tariff); //добавление тарифа в список
                    return false; //выход из общего цикла
                }
                catch (NegativeCostException e) //если введена отрицательная стоимость
                {
                    errorMessage(e.Message); //сообщение ошибки
                }
                catch (FormatException) //если непаравильный формат ввода
                {
                    errorMessage("Неверно ввод данных."); //сообщение ошибки
                }                         
            }
        }

        //метод для обработки нажатия клавиши
        public void HandlerPosition(object sender, PositionEventArgs args)
        {
            if (args.Obj.GetType() == GetType()) //если сообщение было для этого класса
            {
                if (args.Key == ConsoleKey.UpArrow) //если была нажата клавиша вверх
                {
                    //если не начало списка, то переход на предыдущию позицию
                    if (position > 1) position--;
                    //если начало списка, то переход на последнию позицию списка 
                    else position = directions.Count; 
                }
                else if (args.Key == ConsoleKey.DownArrow) //если была нажата клавиша вниз
                {
                    //если не конец списка, то переход на следующию позицию
                    if (position < directions.Count) position++;
                    //если конец списка, то переход на первую позицию списка
                    else position = 1;
                }
                else if (args.Key == ConsoleKey.Enter) //если была нажата клавиша ввода
                {
                    if (type == 1) //для направления откуда
                    {                        
                        tariff.DepartureFrom = directions[position - 1]; //присваивание полю тарифа город вылета
                        //если было выбрана направление нашего аэропорта                         
                        if (tariff.DepartureFrom == AirportTicketOffice.AiroportTicketOfficeName)
                        {                            
                            directions.RemoveAt(position - 1); //то исключаем его из списка
                        }
                        else //если выбран стороний аэропорт
                        {
                            //оставляем только название кассы аэропорта
                            directions.RemoveAll(x => x.ShortDirectionName != AirportTicketOffice.AiroportTicketOfficeName.ShortDirectionName);
                        }
                        type = 2; //присваиваем тип 
                        position = 1; //оновляем позицию
                        return; //продолжение сценария
                    }
                    if(type == 2) //для направления куда
                    {                        
                        tariff.FlightTo = directions[position - 1]; //присваиваем полю тарифа город прилета
                        type = 0; //для вывода на консоль полученных данных и продолжения сценария
                    }
                }
            }
        }
    }
}
