using System.Collections.Generic; //подключение библиотеки классов, определяющих типизированные коллекции
using System.Text; //содержит классы, представляющие кодировки ASCII и Юникод, абстрактные 
//базовые классы для преобразования блоков символов в блоки байтов и обратно и класс поддержки, 
//управляющий объектами String и форматирующий такие объекты без создания промежуточных экземпляров String.
using System.IO; //содержит типы, позволяющие осуществлять чтение и запись в файлы и потоки данных

namespace ControlIndependentWork.TestInformation //пространство имен
{
    class Directions //объявление класса направлений
    {
        //метод для получения списка направлений
        public static List<DirectionName> AddDirections()
        {
            //счиываем массив строк из файла
            string[] lines = File.ReadAllLines("Directions.txt", Encoding.Default);
            //объявление списка наименований направления
            List<DirectionName> directionNames = new List<DirectionName>(); 
            foreach(string line in lines) //пребираем массив строк
            {
                string[] directionName = line.Split(' '); //разбираем строку
                //добавляем объект наименование направления
                directionNames.Add(new DirectionName(directionName[0], directionName [1]));
            }
            return directionNames; //возращаем список направлений
        } 
    }
}
