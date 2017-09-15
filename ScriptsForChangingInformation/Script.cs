using System; //подключение общей библиотеки классов
using System.Collections.Generic; //подключение библиотеки классов, определяющих типизированные коллекции
using System.Linq; //пространство имен System.Linq содержит классы и интерфейсы, которые поддерживают запросы, использующие LINQ

namespace ControlIndependentWork.ScriptsForChangingInformation //пространство имен
{
    abstract class Script //объявление абстрактного класса сценария
    {
        protected string title; //поле название сценария
        protected List<string> text = new List<string>(); //список полей текста
        protected int top; //начальная позиция от верхнего края для текста
        protected int leftText; //позиция от левого края для теста
        protected int leftTitle; //позиция от левого края для названия сценария 
        protected int widthText; //ширина текста
        protected int leftInput; //позиция от левого края для ввода данных
        protected int topInput; //начальная позиция от верхнего края для ввода данных 

        //метод для расчета позиций текста и ввода данных
        protected void calculation()
        {
            //расчет начальной позиции от верхнего края для текста
            top = (Console.BufferHeight - (text.Count * 2 + 4)) / 2;
            //расчет позиции от левого края для названия сценария 
            leftTitle = (Console.BufferWidth - title.Length) / 2;
            widthText = text.Max(x => x.Length); //расчет максимальной ширины текста
            //расчет позиции от левого края для теста
            leftText = (Console.BufferWidth - (widthText + 24)) / 2;
            //расчет позиции от левого края для ввода данных
            leftInput = leftText + widthText + 6; 
            topInput = top + 4; //расчет начальной позиции от верхнего края для ввода данных 
        }

        //метод для вывода на консоль формы ввода данных по сценарию
        protected virtual void openForm()
        {
            int top = this.top; //присваивание начальной позиции от верхнего края для текста
            Console.SetCursorPosition(leftTitle, top++); //установка позиции курсора
            Console.Write(title); //вывод на консоль название сценария
            Console.SetCursorPosition(leftTitle, top); //установка позиции курсора
            Console.WriteLine(new string('-', title.Length)); //печать горизонтальной черты под названием 
            top = top + 2; //увеличиваем позицию от верхнего края на два
            for (int i = 0; i < text.Count; i++)
            {
                Console.SetCursorPosition(leftText, top++); //установка позиции курсора
                Console.Write(" {0} ", new string('-', widthText + 2)); //печать горизонтальной черты над тестом
                Console.Write(" {0} ", new string('-', 18)); //печать горизонтальной черты над вводом данных
                Console.SetCursorPosition(leftText, top++); //установка позиции курсора
                //вывод на консоль текст сообщения
                Console.Write("| {0}{1} |", text[i], new string(' ', widthText - text[i].Length));
                Console.Write("| {0} |", new string(' ', 16)); //пространство для ввода данных
                if (i == text.Count - 1) //если последняя строка
                {
                    Console.SetCursorPosition(leftText, top); //установка позиции курсора
                    Console.Write(" {0} ", new string('-', widthText + 2)); //печать горизонтальной черты под тестом
                    Console.Write(" {0} ", new string('-', 18)); //печать горизонтальной черты под вводом данных
                }
            }
        }

        //метод для вывода на консоль сообщения ошибки           
        protected virtual bool errorMessage(string error)
        {
            Console.Clear(); //очистка консоли
            Console.CursorVisible = false; //отключение видимости курсора
            Console.SetCursorPosition((80 - error.Length) / 2, 15); //установка позиции курсора
            Console.WriteLine(error); //вывод на консоль сообщения об ошибке
            Console.ReadKey(); //ожидание нажатия клавиши пользователем
            return false; //прерывает цикл
        }
       
        //пререгрузка метода вывода на консоль сообщения об ошибке 
        protected virtual bool errorMessage(string error, string actionMessage)
        {
            Console.Clear(); //очистка консоли
            Console.CursorVisible = false; //отключение видимости курсора
            Console.SetCursorPosition((80 - error.Length) / 2, 14); //установка позиции курсора
            Console.WriteLine(error); //вывод на консоль сообщения об ошибке
            Console.SetCursorPosition((80 - actionMessage.Length) / 2, 16); //установка позиции курсора
            Console.WriteLine(actionMessage); //вывод на консоль сообщения действия
            Console.ReadKey(); //ожидание нажатия клавиши пользователем
            return false; //прерывает цикл
        }


    }
}
