using System; //подключение общей библиотеки классов

namespace ControlIndependentWork.MyException //пространство имен
{
    //класс ошибки "экземпляр уже существует"
    class InstanceExistsException : ApplicationException
    {
        //переопределенное свойство сообщения
        public override string Message 
        {
            get //аксессор получения сообщения 
            {
                //возвращаем сообщение ошибки
                return "Экземпляр уже существует в списке.";
            }
        }
    }
}
