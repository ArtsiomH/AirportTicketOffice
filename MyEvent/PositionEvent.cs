using System; //подключение общей библиотеки классов

namespace ControlIndependentWork.MyEvent //пространство имен
{
    class PositionEvent //объявление класса
    {
        //событие изменения позиции элемента
        public event EventHandler<PositionEventArgs> Position; 

        //метод для запуска события 
        public void OnPosition(IHandlerPosition obj, ConsoleKey key)
        {
            //создание объекта параметров события
            PositionEventArgs args = new PositionEventArgs(obj, key);
            Position?.Invoke(this, args); //запуск события
        }
    }
}
