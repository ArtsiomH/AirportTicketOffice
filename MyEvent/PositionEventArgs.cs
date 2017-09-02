using System; //подключение общей библиотеки классов

namespace ControlIndependentWork.MyEvent //пространство имен
{
    //объявления класса параметров события наследуемого, от класса EventArgs
    class PositionEventArgs : EventArgs
    {
        //свойство для хранения информации об нажатой клавиши
        public ConsoleKey Key { get; set; } 

        //свойство для хранения информации об объете, которому послано событие
        public IHandlerPosition Obj { get; set; }

        public PositionEventArgs(IHandlerPosition obj, ConsoleKey key) //конструктор
        {
            Obj = obj; //присваевание параметра для объекта события
            Key = key; //присваевание параметра для объекта события         
        }
    }
}
