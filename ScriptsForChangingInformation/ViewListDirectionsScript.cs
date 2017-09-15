using System;
using System.Collections.Generic;
using System.Linq;
using ControlIndependentWork.MyEvent;

namespace ControlIndependentWork.ScriptsForChangingInformation
{
    class ViewListDirectionsScript : Script, IHandlerPosition
    {
        private string info;
        protected int position = 1; //поле позиции
        protected List<DirectionName> directions = AirportTicketOffice.ListOfDirections.Values.ToList(); //список пассажиров
        private bool notReturn = true;


        public bool ListIsEmpty //свойство список является пустым
        {
            get //аксессор получения результата проверки списка
            {
                if (directions.Count == 0) //если список объектов пуст
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

        public ViewListDirectionsScript() //конструктор
        {
            title = "Direction"; //присваивание полю название сценария
            text.Add("Full name for the direction"); //добавлении поля текста
            text.Add("Short name for the direction"); //добавлении поля текста
            if (directions.Count != 0) directions.Add(AirportTicketOffice.AiroportTicketOfficeName);
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
                info = string.Format("{0} of {1}", position, directions.Count);
                Console.SetCursorPosition((Console.BufferWidth - info.Length) / 2, 20); //установка позиции курсора
                Console.WriteLine(info); //вывод на консоль строки информации
                Console.SetCursorPosition(31, 22); //установка позиции курсора
                Console.WriteLine("press enter to exit");
                return notReturn;
            }
            return notReturn;
        }

        protected void printInfo()
        {
            Console.SetCursorPosition(leftInput, topInput); //установка позиции курсора
            Console.WriteLine(directions[position - 1].FullDirectionName); //вывод на консоль полного названия
            Console.SetCursorPosition(leftInput, topInput + 2); //установка позиции курсора
            Console.WriteLine(directions[position - 1].ShortDirectionName); //вывод на консоль короткого названия
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
                    else position = directions.Count;
                }
                else if (args.Key == ConsoleKey.RightArrow) //если была нажата клавиша вниз
                {
                    //если не конец списка, то переход на следующию позицию
                    if (position < directions.Count) position++;
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
