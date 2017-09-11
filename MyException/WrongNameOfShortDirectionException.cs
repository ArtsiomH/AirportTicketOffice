using System; //подключение общей библиотеки классов

namespace ControlIndependentWork.MyException //пространство имен
{
    //класс ошибки "неверное короткое название направления"
    class WrongNameOfShortDirectionException : ApplicationException
    {
        //переопределенное свойство сообщения
        public override string Message
        {
            get //аксессор получения сообщения
            {
                //возвращаем сообщение ошибки
                return "Record must consist of maximum of three characters in international format.";
            }
        }
    }
}
