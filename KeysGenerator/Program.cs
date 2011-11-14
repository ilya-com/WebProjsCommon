using System;
using System.Windows.Forms;

namespace KeysGenerator
{
    class Program
    {
    	static void Test1()
        {
            // ключ для SHA1 (128)
            string key1 = KeyGenerator.GenerateKey("WebChat (SHA1)", 128);
            // ключ для AES (64)
            string key2 = KeyGenerator.GenerateKey("WebChat (AES)", 64);
            // вывод в файл полученных ключей
            KeyGenerator.PrintKeysToFile(key1, key2, "SHA1", "AES", "out1.txt");
        }

        static void Test2()
        {
            KeyData key1 = KeyGenerator2.GenerateKey(KeyTypes.SHA1);
            KeyData key2 = KeyGenerator2.GenerateKey(KeyTypes.DES3);
            KeyGenerator2.PrintKeysToFile(key1, key2, "out2.txt");
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Fmain());
            //Test1();
            //Test2();
        }
    }
}
