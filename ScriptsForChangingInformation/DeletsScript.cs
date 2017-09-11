using System; //подключение общей библиотеки классов
using System.Collections.Generic; //подключение библиотеки классов, определяющих типизированные коллекции
using ControlIndependentWork.Form; //пространство имен содержит метод формы вывода на экран объекта из списка
using ControlIndependentWork.MyEvent; //пространство имен содержит событие обработки нажатия

namespace ControlIndependentWork.ScriptsForChangingInformation //пространство имен
{
    //абстрактный класс сценарий для удаления, реализующий интерфейс IHandlerPosition 
    abstract class DeletsScript : IHandlerPosition
    {
        protected int position = 1; //поля позиция
        protected List<IToShortString> objects; //поля списка объектов интерфейса
        bool print = false; //поля для разрешения вывода на консоль
        protected string title; //поле названия сценария
        protected string result; //поле сообщение результата

        public bool ListIsEmpty //свойство список является пустым
        {
            get //аксессор получения результата проверки списка
            {
                if (objects.Count == 0) //если список объектов пуст
                {
                    Console.Clear(); //очистка консоли
                    Console.SetCursorPosition(34, 15); //установка позиции курсора
                    Console.WriteLine("The list is empty."); //вывод сообщения на консоль
                    Console.ReadKey(); //ожидание нажатия клавиши пользователем
                    return true; //список пуст
                }
                return false; //список не пуст
            }
        }

        public bool Start() //метод запуска сценария для выбора объекта для удаления
        {
            Console.Clear(); //очистка консоли
            if (print)
            {
                //установка позиции курсора
                Console.SetCursorPosition((Console.BufferWidth - result.Length) / 2, 15);
                Console.WriteLine(result); //вывод сообщения результата действия
                Console.ReadKey(); //ожидние нажатия клавиши пользователем
                return false; //выход из общего цикла
            }
            FormForList.Show(title, objects, position); //вывод на консоль описание объекта 
            return true; //продолжение общего цикла
        }

        protected abstract void action(); //абстрактный метод действия

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
                    else position = objects.Count;
                }
                else if (args.Key == ConsoleKey.DownArrow) //если была нажата клавиша вниз
                {
                    //если не конец списка, то переход на следующию позицию
                    if (position < objects.Count) position++;
                    //если конец списка, то переход на первую позицию списка
                    else position = 1;
                }
                else if (args.Key == ConsoleKey.Enter) //если была нажата клавиша ввода
                {
                    action(); //выполнение действия
                    print = true; //для печати результата действия
                }
            }
        }

    }
}
