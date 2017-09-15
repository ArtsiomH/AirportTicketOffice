using System;
using System.Collections.Generic;
using System.Linq;
using ControlIndependentWork.MyEvent;

namespace ControlIndependentWork.ScriptsForChangingInformation
{
    class ViewListTariffsScript : Script, IHandlerPosition
    {
        private string info;
        protected int position = 1; //поле позиции
        protected List<Tariff> tariffs = AirportTicketOffice.ListOfTariffs.Values.ToList(); //список пассажиров
        private bool notReturn = true;


        public bool ListIsEmpty //свойство список является пустым
        {
            get //аксессор получения результата проверки списка
            {
                if (tariffs.Count == 0) //если список объектов пуст
                {
                    Console.Clear(); //очистка консоли
                    Console.SetCursorPosition(30, 15); //установка позиции курсора
                    Console.WriteLine("The list is empty."); //вывод сообщения на консоль
                    Console.ReadKey(); //ожидание нажатия клавиши пользователем
                    return true; //список пуст
                }
                return false; //список не пуст
            }
        }

        public ViewListTariffsScript() //конструктор
        {
            title = "Tariff"; //присваивание полю название сценария
            text.Add("City of departure"); //добавлении поля текста
            text.Add("City of arrival"); //добавлении поля текста
            text.Add("Departure date"); //добавлении поля текста
            text.Add("Cost of the flight"); //добавлении поля текста
            calculation(); //метод для расчета позиций текста и ввода данных 
        }

        public bool Start()
        {
            if (notReturn)
            {
                Console.Clear();
                Console.SetCursorPosition(leftText - 4, 16); //установка позиции курсора
                Console.WriteLine((char)0x25c4); //печать символа из Unicode
                Console.SetCursorPosition(leftInput + 21, 16); //установка позиции курсора
                Console.WriteLine((char)0x25ba); //печать символа из Unicode               
                openForm();
                printInfo();
                info = string.Format("{0} of {1}", position, tariffs.Count);
                Console.SetCursorPosition((Console.BufferWidth - info.Length) / 2, 22); //установка позиции курсора
                Console.WriteLine(info); //вывод на консоль строки информации
                Console.SetCursorPosition(31, 24); //установка позиции курсора
                Console.WriteLine("press enter to exit");
                return notReturn;
            }
            return notReturn;
        }

        protected void printInfo()
        {
            Console.SetCursorPosition(leftInput, topInput); //установка позиции курсора
            Console.WriteLine(tariffs[position - 1].DepartureFrom); //вывод на консоль города вылета
            Console.SetCursorPosition(leftInput, topInput + 2); //установка позиции курсора
            Console.WriteLine(tariffs[position - 1].FlightTo); //вывод на консоль города прилета
            Console.SetCursorPosition(leftInput, topInput + 4); //установка позиции курсора
            Console.WriteLine(tariffs[position - 1].Date.ToShortDateString()); //вывод на консоль даты вылета
            Console.SetCursorPosition(leftInput, topInput + 6); //установка позиции курсора
            Console.WriteLine("{0} cu", tariffs[position - 1].Cost); //вывод на консоль стоимости тарифа
        }

        //метод для обработки нажатия клавиши
        public virtual void HandlerPosition(object sender, PositionEventArgs args)
        {
            if (args.Obj.GetType() == GetType()) //если сообщение было для этого класса
            {
                if (args.Key == ConsoleKey.LeftArrow) //если была нажата клавиша вверх
                {
                    //если не начало списка, то переход на предыдущию позицию
                    if (position > 1) position--;
                    //если начало списка, то переход на последнию позицию списка 
                    else position = tariffs.Count;
                }
                else if (args.Key == ConsoleKey.RightArrow) //если была нажата клавиша вниз
                {
                    //если не конец списка, то переход на следующию позицию
                    if (position < tariffs.Count) position++;
                    //если конец списка, то переход на первую позицию списка
                    else position = 1;
                }
                else if (args.Key == ConsoleKey.Enter) //если была нажата клавиша ввода
                {
                    notReturn = false;
                }
            }
        }
    }
}
