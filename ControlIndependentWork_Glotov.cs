using System; //подключение общей библиотеки классов
using ControlIndependentWork.Passengers; //пространсво имен содержит класс пассажира
using ControlIndependentWork.TestInformation; //пространство имен содержит классы добавления тестовой инвформации
using ControlIndependentWork.Menu; //пространство имен содержит классы меню
using ControlIndependentWork.ScriptsForChangingInformation; //пространство имен содержит классы сценариев изменения информации
using ControlIndependentWork.MyEvent; //пространство имен содержит событие обработки нажатия
using ControlIndependentWork.CostCalculation; //пространство имен содержит классы для расчета стоимости

namespace ControlIndependentWork //пространство имен
{
    class ControlIndependentWork_Glotov //объявление класса
    {
        static void Main(string[] args) //точка входа в программу
        {
            Console.SetWindowSize(80, 30); //задаем размер окна консоли
            Console.SetBufferSize(80, 30); //задаем размер буфера консоли
            Console.CursorVisible = false; //отключаем видимость курсора
            bool notReturn = true; //булевская переменная для прерывания циклов
            MainMenu mainMenu = new MainMenu(); //объект главного меню          
            PositionEvent positionEvent = new PositionEvent(); //событие нажатия клавиши
            positionEvent.Position += mainMenu.HandlerPosition; //подписка главного меню на событие            
            ConsoleKeyInfo keyInfo; //объект хранящий информацию о нажатой клавиши 
            while (true) //бесконечный цикл
            {
                mainMenu.ShowMenu(); //отображение главного меню
                keyInfo = Console.ReadKey(); //ожидание нажатия клавиши пользователем
                positionEvent.OnPosition(mainMenu, keyInfo.Key); //запуск события
                notReturn = true; //для зацикливания
                if (keyInfo.Key == ConsoleKey.Enter) //если пользователь нажал Enter
                {
                    
                    switch (mainMenu.Position) //переход по позиции
                    {
                        case 1: //если позиция равна единицы
                            //создается объект меню наименования направлений
                            MenuChangeListOfDirections menuDirection = new MenuChangeListOfDirections();
                            //подписка на событие нажатия клавиши
                            positionEvent.Position += menuDirection.HandlerPosition;                                                      
                            do
                            {
                                menuDirection.ShowMenu(); //отображение меню для направления                              
                                keyInfo = Console.ReadKey(); //ожидание нажатия пользователем клавиши                                
                                positionEvent.OnPosition(menuDirection, keyInfo.Key); //запуск события
                                if (keyInfo.Key == ConsoleKey.Enter) //если пользователь нажал Enter
                                {
                                    switch (menuDirection.Position) //переход по позиции
                                    {
                                        case 1: //если позиция равна единицы
                                            //создание объекта сценария добавления направления
                                            AddDirectionScript direction = new AddDirectionScript();
                                            direction.Start(); //запуск сценария добавления
                                            break; //выход
                                        case 2: //если позиция равна двойке
                                            //создание объекта сценария удаления направления
                                            RemoveDirectionScript removeDirection = new RemoveDirectionScript();
                                            if (removeDirection.ListIsEmpty) break; //выход если пустой список
                                            //подписка сценария на событие
                                            positionEvent.Position += removeDirection.HandlerPosition;
                                            removeDirection.Start(); //запуск сценария
                                            do
                                            {
                                                //ожидание нажатия клавиши пользователем
                                                keyInfo = Console.ReadKey();
                                                //запуск события 
                                                positionEvent.OnPosition(removeDirection, keyInfo.Key);
                                                notReturn = removeDirection.Start(); //продолжение выполнения сценария 
                                            } while (notReturn); //если сценарий закончился выход
                                            //отписка от события
                                            positionEvent.Position -= removeDirection.HandlerPosition;
                                            notReturn = true; //для продолжения цикла
                                            break; //выход
                                        case 3: //если позиция равна тройке
                                            notReturn = false; //для прерывания цикла
                                            //отписка от события нажатия клавиши
                                            positionEvent.Position -= menuDirection.HandlerPosition;
                                            break; //выход
                                    }
                                }
                            } while (notReturn); //цикл
                            break; //выход

                        case 2: //если позиция равна двойке
                            //создается объект меню для пассажиров
                            MenuChangeListOfPassengers menuPassenger = new MenuChangeListOfPassengers();
                            //подписка на событие нажатия клавиши
                            positionEvent.Position += menuPassenger.HandlerPosition;
                            do
                            {
                                menuPassenger.ShowMenu(); //отображение меню
                                keyInfo = Console.ReadKey(); //ожидание нажатия пользователем клавиши
                                positionEvent.OnPosition(menuPassenger, keyInfo.Key); //запуск события
                                if (keyInfo.Key == ConsoleKey.Enter) //если пользователь нажал Enter
                                {
                                    switch (menuPassenger.Position) //переход по позиции
                                    {
                                        case 1: //если позиция равна единицы
                                            //создается объект меню выбора пассажира
                                            MenuPassengerSelection passengerSelection = new MenuPassengerSelection();
                                            //подписка на событие нажатия клавиши
                                            positionEvent.Position += passengerSelection.HandlerPosition;
                                            do
                                            {
                                                passengerSelection.ShowMenu(); //отображение меню                                              
                                                keyInfo = Console.ReadKey(); //ожидание нажатия пользователем клавиши
                                                positionEvent.OnPosition(passengerSelection, keyInfo.Key);
                                                if (keyInfo.Key == ConsoleKey.Enter) //если пользователь нажал Enter
                                                {
                                                    AddPassengerScript newPassenger; //переменная новый пассажир
                                                    switch (passengerSelection.Position) //переход по позиции
                                                    {
                                                        case 1: //если позиция равна единицы
                                                            //создание объекта сценария добавления новго пассажира с процентной скидкой
                                                            newPassenger = new AddPassengerScript(new PassengerWithPercentDiscount());
                                                            
                                                            break; //выход
                                                        case 2: //если позиция равна двойке
                                                            //создание объекта сценария добавления новго пассажира с фиксированной скидкой
                                                            newPassenger = new AddPassengerScript(new PassengerWithFixedDiscount());
                                                            newPassenger.StartScript(); //запуск сценария
                                                            break; //выход
                                                    }
                                                    notReturn = false; //выход из цикла
                                                }
                                            } while (notReturn); //цикл
                                            //отписка от события нажатия клавиши
                                            positionEvent.Position -= passengerSelection.HandlerPosition;
                                            notReturn = true; //продолжить цикл 
                                            break; //выход
                                        case 2: //если позиция равна двойке
                                            //создание объекта сценария удаления пассажира
                                            RemovePassengerScript removePassenger = new RemovePassengerScript();
                                            if (removePassenger.ListIsEmpty) break; //выход если список пуст
                                            //подписка на событие нажатия клавиши
                                            positionEvent.Position += removePassenger.HandlerPosition;
                                            removePassenger.Start(); //запуск сценария
                                            do
                                            {
                                                keyInfo = Console.ReadKey(); //ожидание нажатия пользователем клавиши
                                                //запуск события нажатия клавиши
                                                positionEvent.OnPosition(removePassenger, keyInfo.Key);
                                                notReturn = removePassenger.Start(); //продолжение выполнения сценария
                                            } while (notReturn); //если сценарий закончился выход
                                            //отписка от события нажатия клавиши
                                            positionEvent.Position -= removePassenger.HandlerPosition;
                                            notReturn = true; //продолжить цикл  
                                            break; //выход
                                        case 3: //если позиция равна тройке
                                            notReturn = false; //выход из цикла
                                            //отписка от события нажатия клавиши
                                            positionEvent.Position -= menuPassenger.HandlerPosition;
                                            break; //выход
                                    }
                                }
                            } while (notReturn); //цикл
                            break; //выход

                        case 3: //если позиция равна тройке
                            //создается объект меню наименования направлений
                            MenuChangeListOfTariffs menuTariff = new MenuChangeListOfTariffs();
                            //подписка на событие нажатия клавиши
                            positionEvent.Position += menuTariff.HandlerPosition;
                            do
                            {
                                menuTariff.ShowMenu(); //отображение меню
                                keyInfo = Console.ReadKey(); //ожидание нажатия пользователем клавиши
                                positionEvent.OnPosition(menuTariff, keyInfo.Key); //запуск события
                                if (keyInfo.Key == ConsoleKey.Enter) //если пользователь нажал Enter
                                {                                    
                                    switch (menuTariff.Position) //переход по позиции
                                    {
                                        case 1: //если позиция равна единицы
                                            //создание объекта сценария добавления тарифа
                                            AddTariffScript tariff = new AddTariffScript();
                                            if (tariff.ListIsEmpty) break; //если список напралений пуст выход                                            
                                            positionEvent.Position += tariff.HandlerPosition; //подписка на событие нажатия клавиши
                                            tariff.Start(); //запуск сценария
                                            do
                                            {                                              
                                                keyInfo = Console.ReadKey(); //ожидание нажатия пользователем клавиши
                                                positionEvent.OnPosition(tariff, keyInfo.Key); //запуск события
                                                notReturn = tariff.Start(); //продолжение сценария
                                            } while (notReturn); //если сценарий закончился выход
                                            //отписка от события нажатия клавиши
                                            positionEvent.Position -= tariff.HandlerPosition;
                                            notReturn = true; //продолжить цикл  
                                            break; //выход
                                        case 2: //если позиция равна двойке
                                            //создание объекта сценария удаления тарифа
                                            RemoveTariffScript removeTariff = new RemoveTariffScript();
                                            if (removeTariff.ListIsEmpty) break; //если список пуст выход
                                            //подписка на событие нажатия клавиши
                                            positionEvent.Position += removeTariff.HandlerPosition;
                                            removeTariff.Start(); //запуск сценария
                                            do
                                            {
                                                keyInfo = Console.ReadKey(); //ожидание нажатия пользователем клавиши
                                                positionEvent.OnPosition(removeTariff, keyInfo.Key); //запуск события
                                                notReturn = removeTariff.Start(); //продолжение сценария
                                            } while (notReturn); //если сценарий закончился выход
                                            //отписка от события нажатия клавиши
                                            positionEvent.Position -= removeTariff.HandlerPosition;
                                            notReturn = true; //продолжить цикл 
                                            break; //выход
                                        case 3: //если позиция равна тройке
                                            notReturn = false; //выход из цикла
                                            //отписка от события нажатия клавиши
                                            positionEvent.Position -= menuTariff.HandlerPosition;
                                            break; //выход
                                    }
                                }
                            } while (notReturn); //цикл
                            break; //выход

                        case 4: //если позиция равна четверке
                            //создание объекта сценария добавления билета
                            AddAirTicketScript ticket = new AddAirTicketScript("Билет зарегистрирован");
                            if (ticket.ListIsEmpty) break; //если список пуст выход
                            //подписка на событие нажатия клавиши
                            positionEvent.Position += ticket.HandlerPosition;
                            ticket.Start(); //запуск сценария
                            do
                            {
                                keyInfo = Console.ReadKey(); //ожидание нажатия пользователем клавиши
                                positionEvent.OnPosition(ticket, keyInfo.Key); //запуск события
                                notReturn = ticket.Start(); //продолжение события
                            } while (notReturn); //если сценарий закончился выход
                            //отписка от события нажатия клавиши
                            positionEvent.Position -= ticket.HandlerPosition;
                            notReturn = true;  //выход из цикла
                            break; //выход

                        case 5:
                            //создание объекта стоимость билетов для пассажира
                            CostPassengerTickets cost = new CostPassengerTickets();
                            if (cost.ListIsEmpty) break; //если список пуст выход
                            //подписка на событие нажатия клавиши
                            positionEvent.Position += cost.HandlerPosition;
                            cost.Cost(); //1) выбор пассажира для расчета
                            do
                            {
                                keyInfo = Console.ReadKey(); //ожидание нажатия пользователем клавиши
                                positionEvent.OnPosition(cost, keyInfo.Key); //запуск события
                                notReturn = cost.Cost(); //2)если пассажир выбран расчет стоимости билета
                            } while (notReturn); //выход после расчета стоимости                            
                            positionEvent.Position -= cost.HandlerPosition; //отписка от события нажатия клавиши
                            notReturn = true; //продолжить цикл
                            break; //выход

                        case 6: //если позиция равна шести
                            TicketPrices.Cost(); //расчет стоимости всех билетов с учетом скидки
                            break; //выход

                        case 7: //если позиция равна семи
                            //создание объекта сценария изменения 
                            ChangeInTicketPriceScript ticketPrice = new ChangeInTicketPriceScript("Выберите билет");
                            if (ticketPrice.ListIsEmpty) break; //если список пуст выход
                            //подписка на событие нажатия клавиши
                            positionEvent.Position += ticketPrice.HandlerPosition;
                            ticketPrice.Start(); //запуск сценария
                            do
                            {
                                keyInfo = Console.ReadKey(); //ожидание нажатия пользователем клавиши
                                positionEvent.OnPosition(ticketPrice, keyInfo.Key); //запуск события
                                notReturn = ticketPrice.Start(); //продолжение сценария
                            } while (notReturn); //если сценарий закончился выход                             
                            positionEvent.Position -= ticketPrice.HandlerPosition; //отписка от события нажатия клавиши
                            notReturn = true; //продолжить цикл
                            break; //выход

                        case 8: //если позиция равна восьми
                            //добавление тестовых данных
                            AddingTestInform.Add(20, 20, 100); //(20 пассажиров, 20 тарифов и 100 билетов) 
                            break; //выход                                                

                        case 9: //если позиция ранв девяти
                            return; //выход
                    }                
                }
            } 
        }
    }
}
