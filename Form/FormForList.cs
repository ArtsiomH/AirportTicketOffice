using System; //подключение общей библиотеки классов
using System.Collections.Generic; //подключение библиотеки классов, определяющих типизированные коллекции

namespace ControlIndependentWork.Form //пространство имен
{
    class FormForList //объявление класса формы отображения данных объекта из списка
    {
        //обобщенный метод для отображения на консоль информации об объекте, который релиазовал интерфейс IToShortString  
        public static void Show<T>(string title, List<T> list, int position) where T : IToShortString
        {
            //расчет позиции текстовой информации об объекте
            int leftText = (Console.BufferWidth - list[position - 1].ToShortString().Length) / 2;
            //расчет позиции текстовой информации о списке (номер элемента из списка)
            int leftInfo = (Console.BufferWidth - (position.ToString().Length + 4 + list.Count.ToString().Length)) / 2;
            Console.Clear(); //очистка консоли
            Console.SetCursorPosition((Console.BufferWidth - title.Length) / 2, 10); //установка позиции курсора 
            Console.Write(title); //печать названия списка
            Console.SetCursorPosition(39, 12); //установка позиции курсора 
            Console.Write((char)0x25b2); //печать символа из Unicode
            Console.SetCursorPosition(leftText, 14); //установка позиции курсора 
            Console.Write(list[position - 1].ToShortString());
            Console.SetCursorPosition(39, 16); //установка позиции курсора 
            Console.Write((char)0x25bc); //печать символа из Unicode
            Console.SetCursorPosition(leftInfo, 18); //установка позиции курсора 
            Console.WriteLine("{0} of {1}", position, list.Count); //печат информации о списке
        }
    }
}
