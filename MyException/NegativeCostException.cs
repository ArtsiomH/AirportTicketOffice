using System; //подключение общей библиотеки классов

namespace ControlIndependentWork.MyException //пространство имен
{
    //класс ошибки "отрицательная стоимость номер"
    class NegativeCostException : ApplicationException
    {
        //переопределенное свойство сообщения
        public override string Message
        {
            get //аксессор получения сообщения
            {
                //возвращаем сообщение ошибки
                return "Не верна указана стоимость.";
            }
        }
    }
}
