using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Лаба2_ПО
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_CLOSE = 0xF060;

        static void Main(string[] args)
        {
            ConsoleKeyInfo key;
            string path = "";
            string fileName = "null";
            Process proc = new Process();
            int value = 0;
            int t = 0;
            FileStream fnn;
            Console.WriteLine("Нажмите клавишу Esc для выхода из программы или любую клавишу для начала работы");
            Console.WriteLine("s - пуск проводника, A - ввод пути к папке, Q - создание файла в этой папке,\n" +
                "g - удаление файла, p - закрытие проводника, h - помощь, u - указать имя файла, w - открыть папку");
            do
            {
                key = Console.ReadKey();
                value = (int)key.KeyChar;
                Console.WriteLine("\nКод числа в десятичной система счисления: " + value.ToString());
                string s = Convert.ToString(value, 16);
                t = Convert.ToInt32(s);
                Console.WriteLine("Код числа в 16-тиричной системе счисления: " + t.ToString());


                if ((key.Modifiers & ConsoleModifiers.Shift) != 0)
                {
                    switch (t)
                    {
                        case 41:
                            Console.WriteLine(t);
                            Console.WriteLine("Укажите путь к папке");
                            path = Console.ReadLine();
                            break;

                        case 51:
                            if (path != "" && fileName != "null")
                            {
                                Console.WriteLine("Если такой уже есть, то он будет перезаписан");
                                fnn = File.Create(fileName);
                                fnn.Close();
                                //FileInfo fn = new FileInfo(fileName);
                                //DeleteFile(fileName);
                                //fn.Create();
                            }
                            else
                                Console.WriteLine("Сначала введите путь к папке и имя файла");
                            break;

                        default:
                            Console.WriteLine("Попробуйте Shift + A или Shift + Q");
                            break;
                    }
                }
                else
                {
                    switch (t)
                    {
                        case 73:
                            if (path != "")
                            {
                                Process.Start(path);
                            }
                            break;
                        case 68:
                            Console.WriteLine("Нажмите клавишу Esc для выхода из программы или любую клавишу для начала работы");
                            Console.WriteLine("s - пуск проводника, A - ввод пути к папке, Q - создание файла в этой папке,\n" +
                                "g - удаление файла, p - закрытие проводника, h - помощь, u - указать имя файла");
                            break;
                        case 49:
                            if (path != "")
                            {
                                Process.Start(path);
                            }
                            else
                                Console.WriteLine("Сначала укажите путь");
                            break;

                        case 67:
                            if (fileName != "null")
                                File.Delete(fileName);

                            else
                                Console.WriteLine("Сначала укажите имя и путь к файлу");
                            break;

                        case 70:
                            int sender = FindWindow("CabinetWClass", path.Split(Path.DirectorySeparatorChar).Last());
                            if (sender > 0)
                            {      
                                SendMessage(sender, WM_SYSCOMMAND, SC_CLOSE, 0);
                            }
                            else Console.WriteLine("Не удается найти указаный путь, попробуйте еще раз");
                            break;
                            //////////////uuuuuuu
                        case 75:
                            if (path != "")
                            {
                                Console.WriteLine("Укажите имя файла, используя только цифры и латинский алфавит");
                                fileName = path + "/" + Console.ReadLine() + ".txt";
                            }
                            else
                                Console.WriteLine("Сначала укажите папку");
                            break;
                        default:
                            break;
                    }
                }

            } while (key.Key != ConsoleKey.Escape);

            Console.ReadKey();
        }
    }
}
