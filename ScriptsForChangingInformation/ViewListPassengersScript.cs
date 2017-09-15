using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlIndependentWork.MyEvent;
using ControlIndependentWork.Passengers;

namespace ControlIndependentWork.ScriptsForChangingInformation
{
    class ViewListPassengersScript : Script, IHandlerPosition
    {
        private string fixedDiscount = "Fixed discount"; //поле для пассажира с фиксированной скидкой
        private string percentDiscount = "Percent discount"; //поле для пассажира с процентной скидкой
        private string info;
        protected int position = 1; //поле позиции
        protected List<Passenger> passengers; //список пассажиров
        protected bool notReturn = true;
        

        public bool ListIsEmpty //свойство список является пустым
        {
            get //аксессор получения результата проверки списка
            {
                passengers = AirportTicketOffice.ListOfPassengers.Values.ToList();
                if (passengers.Count == 0) //если список объектов пуст
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

        public ViewListPassengersScript() //конструктор
        {          
            title = "Passenger"; //присваивание полю название сценария
            text.Add("Code of the country"); //добавлении поля текста
            text.Add("Passport ID"); //добавлении поля текста
            text.Add("Personal number"); //добавлении поля текста
            text.Add("Surname"); //добавлении поля текста
            text.Add("Name"); //добавлении поля текста
            text.Add("Date of birth"); //добавлении поля текста
            text.Add("Percent discount"); //добавлении поля текста                        
            calculation(); //метод для расчета позиций текста и ввода данных 
        }

        public virtual bool Start()
        {
            if(notReturn)
            {
                Console.Clear();
                Console.SetCursorPosition(leftText - 4, 15); //установка позиции курсора
                Console.WriteLine((char)0x25c4); //печать символа из Unicode
                Console.SetCursorPosition(leftInput + 21, 15); //установка позиции курсора
                Console.WriteLine((char)0x25ba); //печать символа из Unicode
                if (passengers[position - 1] is PassengerWithFixedDiscount) text[text.Count - 1] = fixedDiscount;
                else text[text.Count - 1] = percentDiscount;
                openForm();
                printInfo();
                info = string.Format("{0} of {1}", position, passengers.Count);
                Console.SetCursorPosition((Console.BufferWidth - info.Length) / 2, 25); //установка позиции курсора
                Console.WriteLine(info); //вывод на консоль строки информации
                Console.SetCursorPosition(31, 27); //установка позиции курсора
                Console.WriteLine("press enter to exit");
                return notReturn;
            }
            return notReturn;
        }

        protected void printInfo()
        {
            Console.SetCursorPosition(leftInput, topInput); //установка позиции курсора
            Console.WriteLine(passengers[position - 1].CodeOfTheContry); //вывод на консоль кода страны
            Console.SetCursorPosition(leftInput, topInput + 2); //установка позиции курсора
            Console.WriteLine(passengers[position - 1].PassportID); //вывод на консоль номера паспорта
            Console.SetCursorPosition(leftInput, topInput + 4); //установка позиции курсора
            Console.WriteLine(passengers[position - 1].PersonalNumber); //вывод на консоль личного номера
            Console.SetCursorPosition(leftInput, topInput + 6); //установка позиции курсора
            Console.WriteLine(passengers[position - 1].Surname); //вывод на консоль фамилии
            Console.SetCursorPosition(leftInput, topInput + 8); //установка позиции курсора
            Console.WriteLine(passengers[position - 1].Name); //вывод на консоль имени
            Console.SetCursorPosition(leftInput, topInput + 10); //установка позиции курсора
            Console.WriteLine(passengers[position - 1].DateOfBirth.ToShortDateString()); //вывод на консоль даты рождения
            Console.SetCursorPosition(leftInput, topInput + 12); //установка позиции курсора
            if (passengers[position - 1] is PassengerWithFixedDiscount)
            {
                //вывод на консоль фиксированной скидки
                Console.WriteLine(((PassengerWithFixedDiscount)passengers[position - 1]).FixedDiscount);
            }
            else
            {
                //вывод на консоль процентной скидки скидки
                Console.WriteLine(((PassengerWithPercentDiscount)passengers[position - 1]).PercentDiscount);
            }
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
                    else position = passengers.Count;
                }
                else if (args.Key == ConsoleKey.RightArrow) //если была нажата клавиша вниз
                {
                    //если не конец списка, то переход на следующию позицию
                    if (position < passengers.Count) position++;
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
