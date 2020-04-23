using System;//имена классов
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 5;
            string cd = @"C:\";//начальный каталог
            while (x > 0)
            {
                string a;//строковая переменная
                a = Console.ReadLine();//ввод строки 
                string[] str = a.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);//разделение строки на части по символу "пробел", с удалением пустых вхождений.
                if (str[0] == "cd")//если первый элемент равен сиди
                {
                    cd = cd + str[1] + @"\";//прибавляем к нач каталогу первый лемент массива
                }
                    else if (str[0] == "mkdir")

                { 
                  DirectoryInfo file = new DirectoryInfo(cd + str[1]);
                    if (File.Exists(cd + str[1]))
                    {
                        Directory.CreateDirectory(cd + str[1]);
                    }
                else
                {
                        Console.WriteLine("file is not found");
                    }
                   }

                    else if (str[0] == "cp")
                    {
                    FileInfo file = new FileInfo(cd + str[1]);
                    if (File.Exists(cd + str[1]))//фукнция для нахождения файла
                    {
                        File.Copy(cd + str[1], cd + str[2]);
                    }
                    else
                    {
                        Console.WriteLine("file is not found");
                    }
                  }
                else if (str[0] == "rm")
                { 
                    FileInfo file = new FileInfo(cd + str[1]);
                if (File.Exists(cd + str[1]))
                {
                       
                 File.Delete(cd + str[1]);
                }
                else
                {
                    Console.WriteLine("file is not found");
                }
            }

                  else if (str[0] == "ls")
                {
                    if (str.Length == 1)// если длина массива стр равна единице, выполняется данный блок кода
                    {
                        ls(cd);//вывод содержимого папки,в кот мы находимся
                    }
                    else if (str.Length == 2)// ls windows
                    {
                        if (str[1] == "-p")
                        {
                            lsp(cd);//вывод содержимого папки,в кот.мы нходимся с ключом p
                        }
                        else if (str[1] == "-a")
                        {
                            lsa(cd);//вывод содержимого папки,в кот.мы нходимся с ключом а
                        }
                        else if (str[1] == "-1")
                        {
                            ls1(cd);//вывод содержимого папки,в кот.мы нходимся с ключом 1
                        }
                        else
                        {
                            ls(cd + str[1]);//str1=windows
                        }
                    }
                    else if (str.Length == 3)
                    {
                        if (str[1] == "-p")
                        {
                            lsp(cd + str[2]);
                        }
                        else if (str[1] == "-a")
                        {
                            lsa(cd + str[2]);
                        }
                        else if (str[1] == "-1")
                        {
                            ls1(cd + str[2]);
                        }
                    }

                }
                else if (str[0] == "exit")
                {
                    x = -2;
                }
            }


        }
        public static void ls1(string str)//static возвращает значеия переменных к исходному состоянию, void - функция,которая ничего не возвращает.
        {
            int f = 0, i = 0;
            string k;
            if (Directory.Exists(str)) //"если директория стр найдена"
            {
                string[] dirs = Directory.GetDirectories(str);//запись имен подкаталогов в массив строк
                foreach (string s in dirs)
                {
                    k = s.Remove(0, str.Length);//удаление пути к файлу
                    Console.WriteLine(k);// вывод на экран имени каталогов
                }
                string[] files = Directory.GetFiles(str);//запись имен файлов в массив строк
                foreach (string s in files)//"для каждого s в files выполнить..."
                {
                    k = s.Remove(0, str.Length);//удаление пути к файлу
                    Console.WriteLine(k);
                }
            }
            else
            {
                Console.WriteLine("directory is not found");//вывод
            }
        }
        public static void lsp(string str)
        {
            int f = 0, i = 0;
            string k;
            if (Directory.Exists(str)) //"если директория стр найдена"
            {
                string[] dirs = Directory.GetDirectories(str);//запись имен подкаталогов в массив строк
                foreach (string s in dirs)
                {
                    k = s.Remove(0, str.Length) + @"\";//удаление пути к файлу
                    if (k.Length > 20)
                    {
                        k = k.Remove(15) + "...";//обрезаем длинное имя файла до десяти символов, и добавляем троеточие.
                    }
                    while (f <= 25)
                    {
                        f = k.Length;
                        k = k + ' ';
                    }
                    Console.Write(k);// вывод на экран имени каталогов
                    i++;
                    if (i == 4)
                    {
                        Console.WriteLine(" ");
                        i = 0;
                    }
                    f = 0;
                }
                string[] files = Directory.GetFiles(str);//запись имен файлов в массив строк
                foreach (string s in files)//"для каждого s в files выполнить..."
                {
                    k = s.Remove(0, str.Length);//удаление пути к файлу
                    if (k.Length > 20)
                    {
                        k = k.Remove(15) + "...";//обрезаем длинное имя файла до десяти символов, и добавляем троеточие.
                    }
                    while (f <= 25)
                    {
                        f = k.Length;
                        k = k + ' ';
                    }
                    Console.Write(k);
                    i++;
                    if (i == 4)
                    {
                        Console.WriteLine(" ");
                        i = 0;
                    }
                    f = 0;
                }
            }
            else
            {
                Console.WriteLine("directory is not found");//вывод
            }
        }//функция для команды ls с ключом -p
        public static void lsa(string str)//функция для вывода в консоль только файлов.
        {
            int f = 0, i = 0;
            string k;
            if (Directory.Exists(str)) //"если директория стр найдена"
            {
                string[] files = Directory.GetFiles(str);//запись имен файлов в массив строк
                foreach (string s in files)//"для каждого s в files выполнить..."
                {
                    k = s.Remove(0, str.Length);//удаление пути к файлу
                    if (k.Length > 20)
                    {
                        k = k.Remove(15) + "...";//обрезаем длинное имя файла до десяти символов, и добавляем троеточие.
                    }
                    while (f <= 25)
                    {
                        f = k.Length;
                        k = k + ' ';
                    }
                    Console.Write(k);
                    i++;
                    if (i == 4)
                    {
                        Console.WriteLine(" ");
                        i = 0;
                    }
                    f = 0;
                }
            }
            else
            {
                Console.WriteLine("directory is not found");//вывод
            }
        }
        public static void ls(string str)
        {
            int f = 0, i = 0;
            string k;
            if (Directory.Exists(str)) //"если директория стр найдена"
            {
                string[] dirs = Directory.GetDirectories(str);//запись имен подкаталогов в массив строк
                foreach (string s in dirs)
                {
                    k = s.Remove(0, str.Length);//удаление пути к файлу
                    if (k.Length > 20)
                    {
                        k = k.Remove(15) + "...";//обрезаем длинное имя файла до десяти символов, и добавляем троеточие.
                    }
                    while (f <= 25)//удлинение имени папки или файла с помощью пробелов для корректного отображения столбиков
                    {
                        f = k.Length;
                        k = k + ' ';
                    }
                    Console.Write(k);// вывод на экран имени каталогов
                    i++;
                    if (i == 4)
                    {
                        Console.WriteLine(" ");//переходим на другую строку
                        i = 0;
                    }
                    f = 0;
                }
                string[] files = Directory.GetFiles(str);//запись имен файлов в массив строк
                foreach (string s in files)//"для каждого s в files выполнить..."
                {
                    k = s.Remove(0, str.Length);//удаление пути к файлу
                    if (k.Length > 20)
                    {
                        k = k.Remove(15) + "...";//обрезаем длинное имя файла до десяти символов, и добавляем троеточие.
                    }
                    while (f <= 25)
                    {
                        f = k.Length;
                        k = k + ' ';
                    }
                    Console.Write(k);
                    i++;
                    if (i == 4)
                    {
                        Console.WriteLine(" ");
                        i = 0;
                    }
                    f = 0;
                }
            }
            else
            {
                Console.WriteLine("directory is not found");//вывод
            }
        }
    }
}