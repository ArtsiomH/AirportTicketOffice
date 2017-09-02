using System; //подключение общей библиотеки классов

namespace ControlIndependentWork.MyException //пространство имен
{
    //класс ошибки "неверный номер паспорта"
    class InvalidPassportIDException : ApplicationException
    {
        //переопределенное свойство сообщения
        public override string Message 
        {
            get //аксессор получения сообщения
            {
                //возвращаем сообщение ошибки
                return "неверно введен номер паспорта.";
            }
        }
    }
}
