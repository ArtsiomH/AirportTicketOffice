using System;
using System.Collections.Generic;
using System.Linq;
using ControlIndependentWork.MyEvent;
using ControlIndependentWork.Form;
using ControlIndependentWork.Passengers;
using System.Text;

namespace ControlIndependentWork.ScriptsForChangingInformation
{
    class ViewListPassengerFlightScript : ViewListPassengersScript, IHandlerPosition
    {
        private List<Tariff> tariffs = AirportTicketOffice.ListOfTariffs.Values.ToList();
        int type = 1;

        public bool ListTariffsIsEmpty //свойство список является пустым
        {
            get //аксессор получения результата проверки списка
            {
                if (tariffs.Count == 0) //если список объектов пуст
                {
                    Console.Clear(); //очистка консоли
                    Console.SetCursorPosition(25, 15); //установка позиции курсора
                    Console.WriteLine("The list of tariffs is empty."); //вывод сообщения на консоль
                    Console.ReadKey(); //ожидание нажатия клавиши пользователем
                    return true; //список пуст
                }
                return false; //список не пуст
            }
        }

        public bool ListPassengersIsEmpty //свойство список является пустым
        {
            get //аксессор получения результата проверки списка
            {
                if (passengers.Count == 0) //если список объектов пуст
                {
                    Console.Clear(); //очистка консоли
                    Console.SetCursorPosition(20, 15); //установка позиции курсора
                    Console.WriteLine("No registered passengers on the flight"); //вывод сообщения на консоль
                    Console.ReadKey(); //ожидание нажатия клавиши пользователем
                    return true; //список пуст
                }
                return false; //список не пуст
            }
        }

        public ViewListPassengerFlightScript()
        { }

        public override bool Start()
        {
            if (type == 1)
            {
                FormForList.Show("Select a tariff", tariffs, position);
                return true;
            }
            else if (type == 0) return false;
            return base.Start();
        }

        public override void HandlerPosition(object sender, PositionEventArgs args)
        {
            if (args.Obj.GetType() == GetType()) //если сообщение было для этого класса
            {
                if (args.Key == ConsoleKey.UpArrow) //если была нажата клавиша вверх
                {
                    if (type == 1)
                    {
                        //если не начало списка, то переход на предыдущию позицию
                        if (position > 1) position--;
                        //если начало списка, то переход на последнию позицию списка 
                        else position = tariffs.Count;
                    }
                }
                else if (args.Key == ConsoleKey.DownArrow) //если была нажата клавиша вниз
                {
                    if (type == 1)
                    {
                        //если не конец списка, то переход на следующию позицию
                        if (position < tariffs.Count) position++;
                        //если конец списка, то переход на первую позицию списка
                        else position = 1;
                    }
                }
                else if ((args.Key == ConsoleKey.RightArrow  || args.Key == ConsoleKey.LeftArrow) && type == 2)
                { base.HandlerPosition(sender, args); }
                else if (args.Key == ConsoleKey.Enter) //если была нажата клавиша ввода
                {
                    if (type == 1)
                    {
                        passengers = new List<Passenger>();
                        foreach (AirTicket airTicket in AirportTicketOffice.ListJourneyTickets.Where(x => x.TariffID == tariffs[position - 1].TariffNumber))
                        {
                            passengers.Add(AirportTicketOffice.ListOfPassengers.First(x => x.Key == airTicket.PersonalNumber).Value);
                        }
                        if(ListPassengersIsEmpty)
                        {
                            type = 0;
                            notReturn = false;
                            return;
                        }
                        type = 2;
                        position = 1;
                        return;
                    }
                    notReturn = false;
                }
            }
        }

    }
}
