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
                return "Запись должна состоять максимум из трех символов в международном формате.";
            }
        }
    }
}
