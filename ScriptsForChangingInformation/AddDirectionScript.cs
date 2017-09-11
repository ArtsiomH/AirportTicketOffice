using System; //подключение общей библиотеки классов
using ControlIndependentWork.MyException; //пространство имен содержит классы ошибок

namespace ControlIndependentWork.ScriptsForChangingInformation //пространство имен
{
    //объявление класса сценарий добавления направления, наследуемый от класса сценарий
    class AddDirectionScript : Script
    {
        private DirectionName direction = new DirectionName(); //поле направление

        public AddDirectionScript() //конструктор
        {
            title = "Entering direction data"; //присваивание полю название сценария
            text.Add("Enter a full name for the direction"); //добавлении поля текста
            text.Add("Enter a short name for the direction"); //добавлении поля текста
            calculation(); //метод для расчета позиций текста и ввода данных 
        }

        public void Start() //метод запуска сценария ввода данных направления
        {
            while (true) //бесконечный цикл
            {
                Console.Clear(); //очистка консоли
                Console.CursorVisible = true; //включение видимости курсора
                openForm(); //выводит на консоль форму для направления
                try
                {                  
                    Console.SetCursorPosition(leftInput, topInput); //установка позиции курсора
                    if (direction.FullDirectionName == null) //если данных нет
                    {
                        //ввод полного наименования направления
                        direction.FullDirectionName = Console.ReadLine();
                    }
                    else //если данные есть
                    {
                        //вывод на консоль полного наименования направления
                        Console.WriteLine(direction.FullDirectionName);
                    }
                    Console.SetCursorPosition(leftInput, topInput + 2); //установка позиции курсора
                    if (direction.ShortDirectionName == null) //если данных нет
                    {
                        //ввод данных короткого наименования направления                      
                        direction.ShortDirectionName = Console.ReadLine();
                    }
                    else //если данные есть
                    {
                        //вывод на консоль короткого наименования направления
                        Console.WriteLine(direction.ShortDirectionName);
                    }
                    Console.CursorVisible = false; //выключение видимости курсора
                    AirportTicketOffice.AddDirection(direction);  //добавление направления в список
                    return; //возврат
                }
                //если введено неверное короткое название направления
                catch (WrongNameOfShortDirectionException e) 
                {
                    //вывод сообщения на консоль
                    errorMessage(e.Message, "Example format for a short name: MNQ");                  
                }
                catch (ArgumentException e) //если уже существует экземпляр
                {
                    errorMessage(e.Message); //вывод сообщения на консоль
                    return; //возврат
                }
            }
        }
    }
}
