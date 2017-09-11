using System; //подключение общей библиотеки классов
using System.Collections.Generic;  //подключение библиотеки классов, определяющих типизированные коллекции
using ControlIndependentWork.MyEvent;  //пространство имен содержит событие обработки нажатия

namespace ControlIndependentWork.Menu //пространство имен
{
    //объявление абстрактного класса меню реализующего интерфей IHandlerPosition
    abstract class Menu : IHandlerPosition
    {
        protected string menuHeading; //поля для заголовка меню
        protected List<string> MenuItems = new List<string>(); //список пунктов меню
        protected int leftTitle; //позиция от левого края для названия меню
        protected int topTitle; //позиция от верхнего края для названия меню
        protected int left; // позиция от левого края для пунктов меню
        protected int top; //начальная позиция от верхнего края для пунктов меню

        public int Position { get; set; } //свойство позиции      

        public Menu() //конструктор
        {
            Position = 1; //начальное значение позиции
        }

        protected void calculate()
        {
            leftTitle = (Console.BufferWidth - menuHeading.Length) / 2;
            left = (Console.BufferWidth - MenuItems[0].Length) / 2;
        }

        //метод для обработки нажатия клавиши
        public void HandlerPosition(object sender, PositionEventArgs args)
        {
            if (args.Obj.GetType() == GetType()) //если сообщение было для этого класса
            {
                if (args.Key == ConsoleKey.UpArrow) //если была нажата клавиша вверх
                {
                    if (Position > 1) Position--; //если не начало списка, то переход на предыдущию позицию
                    else Position = MenuItems.Count; //если начало списка, то переход на последнию позицию списка
                }
                else if (args.Key == ConsoleKey.DownArrow) //если была нажата клавиша вниз
                {
                    if (Position < MenuItems.Count) Position++; //если не конец списка, то переход на следующию позицию
                    else Position = 1; //если конец списка, то переход на первую позицию списка
                }
            }
        }

        protected void ChangeColor(string str) //метод замены цвета для текста
        {
            //меняем цвет фона на белый цвет
            Console.BackgroundColor = ConsoleColor.White;
            //меняем цвет текста на черный цвет
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(str); //выводим текст сообщения
            Console.ResetColor(); //возвращаем стандартные цвета
        }

        protected void ShowMenuTitle() //метод для отображения названия меню
        {
            Console.Clear(); //очистка консоли
            Console.SetCursorPosition(leftTitle, topTitle); //установка позиции курсора         
            Console.WriteLine(menuHeading); //вывод текста названия меню
            Console.SetCursorPosition(leftTitle, topTitle + 1); //установка позиции курсора
            Console.Write(new string('-', menuHeading.Length)); //подчеркивание под названием меню
        }

        public virtual void ShowMenu() //отображение меню на консоле
        {
            ShowMenuTitle(); //отображение названия меню
            for (int i = 0; i < MenuItems.Count; i++) //пребор по списку пунктов меню
            {
                Console.SetCursorPosition(left, top + i); //установка позиции курсора
                //если позиция в классе соответствует пунку, то меняем цвет пункта
                if (Position == i + 1) ChangeColor(MenuItems[i]);
                else Console.WriteLine(MenuItems[i]); //если нет просто выводим текст пункта
            }
        }
    }
}
