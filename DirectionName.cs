using System.Text.RegularExpressions; //пространство имен содержит класс регулярных выражений
using ControlIndependentWork.MyException; //пространство имен содержит классы ошибок
using ControlIndependentWork.Form; //пространство имен содержит интерфейс IToShortString

namespace ControlIndependentWork //пространство имен
{
    //объявление класса, реализующий интерфейс IToShortString, для хранения 
    //информации о наименовании направления
    class DirectionName : IToShortString
    {
        private string shortDirectionName; //поле короткого наименования
        public string FullDirectionName { get; set; } //свойство полного наименования

        public string ShortDirectionName //свойство для поля короткого наименования
        {
            get { return shortDirectionName; } //получить поле короткого наименования
            set //установить значение для поля короткого наименования
            {
                Regex eng = new Regex(@"^[A-z]{1,3}$"); //создание объекта регулярного выражения
                if (!eng.IsMatch(value)) //проверка формата строки
                {
                    //если не подходит формат, то генерируется ошибка
                    //"неверное короткое наименование направления"
                    throw new WrongNameOfShortDirectionException();
                }
                shortDirectionName = value; //если проверка успешна, то присваеваем значение
            }
        }

        public DirectionName() //конструктор по умолчанию
        { }
        
        //параметрический конструктор
        public DirectionName(string shortDirectionName, string fullDirectionName)
        {
            ShortDirectionName = shortDirectionName; //присваивание полю короткое наименование
            FullDirectionName = fullDirectionName; //присваивание полю полное наименование
        }
      
        public string ToShortString() //реализация интерфейся IToShortString
        {
            return FullDirectionName; //возвращает строку полного наименования
        }

        public override string ToString() //переопределенный метод ToString
        {
            //возвращает информацию о полях
            return ShortDirectionName + ' ' + FullDirectionName;
        }

        //переопределенный метод получения хэш кода
        public override int GetHashCode()
        {
            //возвращает хэш код
            return ShortDirectionName.GetHashCode();
        }
    }
}
