using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackList
{
	struct BL
	{
		public string Name;
		public string PhoneNumber;
		//констркутор BL
		public BL(string name, string pnumber)
		{
			this.Name = name;
			this.PhoneNumber = pnumber;
		}
		//вывод списка BL на экран
		public void Display()
		{

			Console.WriteLine("{0} {1}\t", this.Name.PadLeft(25), this.PhoneNumber.PadRight(25));
			Console.WriteLine(" ");
		}
		public void EnterDate()
		{
			Console.WriteLine("____________________________________________________________________________");
			Console.WriteLine("введите имя");
			Name = Console.ReadLine();
			Console.WriteLine("____________________________________________________________________________");
			Console.WriteLine("введите тел.номер");
			PhoneNumber = Console.ReadLine();
		}
		//функция чтения из файл blacklist
		public void ReadFile()
		{
			FileStream file1 = new FileStream(@"D:\C#\blacklist.txt", FileMode.OpenOrCreate); 
			StreamReader reader = new StreamReader(file1, Encoding.UTF8); 
			Console.WriteLine(reader.ReadToEnd()); 
			reader.Close();
		}
		//функция записи в файл blacklist
		public void WriteFile(List<BL> list)
		{
			FileStream file1 = new FileStream(@"D:\C#\blacklist.txt", FileMode.Create);
			StreamWriter writer = new StreamWriter(file1, Encoding.UTF8); 
			foreach (var item in list)
			{
				writer.WriteLine("{0} {1}\t", item.Name.PadLeft(25), item.PhoneNumber.PadRight(25) );
			}
			writer.Close(); 
		}
	}
	class Program
	{
		static void Main(string[] args)
		{

			//создаем объект blacklist используя структуру BL
			BL blacklist = new BL();
			//создаем список BL используя структуру BL
			var list = new List<BL>();
			do {
			if (!File.Exists("D:\\C#\\blacklist.txt"))
			{ 
			//заполняем массивы ListName и ListFemale значениями
			string[] ListName = { "Михаил", "Алексей", "Дмитрий", "Константин", "Евгений", "Олег", "Сергей", "Андрей", "Антон", "Иван" };
			string[] ListFemale = { "Иванов", "Петров", "Сидоров", "Орлов", "Быстров", "Зайцев", "Скворцов", "Дятлов", "Дубов", "Березин" };
			//создаем объект rnd
			Random rnd = new Random();
			//заполняем список случайными значениями имен и телефонных номеров
			for (int i = 0; i < 15; i++)
			{
				int rndN = rnd.Next(ListName.Length);//генерация псевдослучайного числа		
				int rndF = rnd.Next(ListFemale.Length);//генерация псевдослучайного числа
			    //формирование случайного имени вида "Name Female"	
				string NameFemale = ListName[rndN] + " " + ListFemale[rndF];
				int RND1 = rnd.Next(0, 9);
				int RND2 = rnd.Next(0, 9);
				int RND3 = rnd.Next(0, 9);
				int RND4 = rnd.Next(0, 9);
				int RND5 = rnd.Next(0, 9);
				int RND6 = rnd.Next(0, 9);
				int RND7 = rnd.Next(0, 9);
				//генерация случайного телефонного номера в формате +7-9**-***-**-**
				string pnumber = "+7" + "-" + "9" + RND1 + RND2 + "-" + RND3 + RND4 + "-" + RND6 + RND7;
				//записываем сгенерированные значения имени и тел.номера в создпнный ранее список BL
				list.Add(new BL(NameFemale, pnumber));
			}
				//запись списка в файл blacklist
				blacklist.WriteFile(list);
				//чтение из файл blacklist
				blacklist.ReadFile();
			};
			Console.WriteLine("____________________________________________________________________________");
			Console.WriteLine("доступны следущие операции:");
			Console.WriteLine(" ");
			Console.WriteLine("поиск по списку введеных значений - search");
			Console.WriteLine("добавить в список введенные значения - add");
			Console.WriteLine("удалить из списка введеные значения - delete");
			Console.WriteLine("удалить из списка значения по номеру строки из списка - delete on index");
			Console.WriteLine("отредактировать введенные значение - edit");
			Console.WriteLine("отредактировать значение по номеру строки из списка - edit on index");
			Console.WriteLine("просмотр списка телефонных номеров - display");
			Console.WriteLine(" ");
			var comand = Console.ReadLine();
			switch (comand)
			{
				case "search":
					Console.WriteLine("Введите имя и номер телефона, которые необходимо найти");
					blacklist.EnterDate();
					Console.WriteLine(" ");
					int indS = list.IndexOf(new BL(blacklist.Name, blacklist.PhoneNumber));
					if (indS != 0)
					{
						Console.WriteLine(" ");
						Console.WriteLine("Строка под номером - {0}", indS + 1);
						Console.WriteLine(" ");
					}
					else
					{
						Console.WriteLine(" ");
						Console.WriteLine("Данной записи не существует в списке");
						Console.WriteLine(" ");
					};
					break;
				case "add":
					Console.WriteLine("Введите имя и номер телефона, которые необходимо добавить");
					blacklist.EnterDate();
					Console.WriteLine(" ");
					list.Add(new BL(blacklist.Name, blacklist.PhoneNumber));
					Console.WriteLine(" ");
					blacklist.Display();
					break;
				case "delete":
					Console.WriteLine("Введите имя и номер телефона, которые необходимо удалить");
					blacklist.EnterDate();
					Console.WriteLine(" ");
					list.Remove(new BL(blacklist.Name, blacklist.PhoneNumber));
					Console.WriteLine(" ");
					break;
				case "delete on index":
					Console.WriteLine("Введите номер строки которую необходимо удалить");
					string doni = Console.ReadLine(); var d  = 1;
					Console.WriteLine(" ");
					if (int.TryParse(doni, out d)) { int indDoni = int.Parse(doni); list.RemoveAt(indDoni); }					 
					Console.WriteLine(" ");
					break;
				case "edit":
					Console.WriteLine("Введите имя и номер телефона, которые необходимо заменить");
					blacklist.EnterDate();
					Console.WriteLine(" ");
					int indE = list.IndexOf(new BL(blacklist.Name, blacklist.PhoneNumber));
					Console.WriteLine("Введите новые значение имени и номер телефона");
					list.Insert(indE, blacklist);
					Console.WriteLine(" ");
					break;
				case "edit on index":
					Console.WriteLine("Введите номер строки которую необходимо удалить");
					string eoni = Console.ReadLine(); var e = 1;
					Console.WriteLine(" ");
					if (int.TryParse(eoni, out e)) { int indEoni = int.Parse(eoni); list.Insert(indEoni, blacklist); ; }
					Console.WriteLine(" ");
					break;
				case "display":
					//чтение из файл blacklist
					blacklist.ReadFile();
					break;
				default:
					Console.WriteLine(" ");
					Console.WriteLine("Ошибка ввода! Команда указана не верно!");
					Console.WriteLine(" ");
					break;
			}
			//запись списка в файл blacklist
			blacklist.WriteFile(list);
			//чтение из файл blacklist
			blacklist.ReadFile();
			Console.WriteLine("Введите <exit> чтобы закончить работу с программой или любую другую клавишу для продолжения");
			} while (Console.ReadLine() != "exit");
			Console.WriteLine("___________________________________________________");
			Console.WriteLine("До новых встреч");
			Console.ReadKey();}		
	}
}
	

