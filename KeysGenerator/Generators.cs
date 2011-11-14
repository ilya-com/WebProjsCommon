using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace KeysGenerator
{
    /// <summary>
    /// Типы ключей
    /// </summary>
    public enum KeyTypes
    {
        SHA1, AES, DES3
    }

    /// <summary>
    /// Описание ключа и его параметров
    /// </summary>
    public struct KeyData
    {
        public string KeyValue { get; set; } // собственно сам ключ
        public KeyTypes Type { get; set; }   // тип ключа
        public int Length { get; set; }      // длина ключа
    }

    /// <summary>
    /// Класс для генерации ключей (machineKey) для веб сайтов на Web Farm
    /// </summary>
    public class KeyGenerator
    {
        /// <summary>
        /// Генерация случайного ключа для приложения
        /// </summary>
        /// <param name="appName">Название приложения</param>
        /// <param name="keyLen">Длина ключа (2*кол-во байт)</param>
        /// <returns>Возвращает стоку с ключом</returns>
        public static string GenerateKey(string appName, int keyLen)
        {
            int len = keyLen;
            byte[] buff = new byte[len / 2];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buff);
            StringBuilder sb = new StringBuilder(len);
            for (int i = 0; i < buff.Length; i++)
                sb.Append(string.Format("{0:X2}", buff[i]));
            Console.WriteLine(String.Format("Key for {0} application is {1}", appName, sb));
            return sb.ToString();
        }

        /// <summary>
        /// Печать ключей в файл
        /// </summary>
        /// <param name="valKey">Ключ validationKey</param>
        /// <param name="desKey">Ключ decryptionKey</param>
        /// <param name="valAlg">Адгоритм для validation</param>
        /// <param name="desAlg">Алгоритм для decryption</param>
        /// <param name="fname">Файл для вывода результатов</param>
        /// <remarks>Генерируется файл с форматированной строкой для конфигурации веб-приложения machineKey</remarks>
        public static void PrintKeysToFile(string valKey, string desKey, string valAlg, string desAlg, string fname)
        {
            StreamWriter file = new StreamWriter(fname, false, Encoding.UTF8);
            file.WriteLine(String.Format("<machineKey validationKey=\"{0}\" decryptionKey=\"{1}\" validation=\"{2}\" decryption=\"{3}\" />", valKey, desKey, valAlg, desAlg));
            file.Close();
        }
    }

    /// <summary>
    /// Класс для генерации ключей (machineKey) для веб сайтов на Web Farm
    /// </summary>
    public class KeyGenerator2
    {
        /// <summary>
        /// Генерация случайного ключа для приложения
        /// </summary>
        /// <param name="type">Тип ключа</param>
        /// <returns>Возвращает данные ключа и сам ключ</returns>
        public static KeyData GenerateKey(KeyTypes type)
        {
            int len = GetKeyLenForType(type);
            byte[] buff = new byte[len / 2];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buff);
            StringBuilder sb = new StringBuilder(len);
            for (int i = 0; i < buff.Length; i++)
                sb.Append(string.Format("{0:X2}", buff[i]));
            KeyData res = new KeyData();
            res.KeyValue = sb.ToString();
            res.Length = len;
            res.Type = type;
            return res;
        }

        /// <summary>
        /// Печать ключей в файл
        /// </summary>
        /// <param name="fname">Файл для вывода результатов</param>
        /// <remarks>Генерируется файл с форматированной строкой для конфигурации веб-приложения machineKey</remarks>
        public static void PrintKeysToFile(KeyData valKey, KeyData desKey, string fname)
        {
            StreamWriter file = new StreamWriter(fname, false, Encoding.UTF8);
            file.WriteLine(String.Format("<machineKey validationKey=\"{0}\" decryptionKey=\"{1}\" validation=\"{2}\" decryption=\"{3}\" />",
                valKey.KeyValue, desKey.KeyValue, GetAlgorithmName(valKey.Type), GetAlgorithmName(desKey.Type)));
            file.Close();
        }

        #region Вспомогательные функции
        /// <summary>
        /// Получение длины ключа для типа ключа
        /// </summary>
        /// <param name="type">Тип ключа</param>
        /// <returns>Возвращает длину ключа</returns>
        private static int GetKeyLenForType(KeyTypes type)
        {
            int res = 0;
            switch (type)
            {
                case KeyTypes.SHA1:
                    res = 128;
                    break;
                case KeyTypes.AES:
                    res = 64;
                    break;
                case KeyTypes.DES3:
                    res = 48;
                    break;
            }
            return res;
        }

        /// <summary>
        /// Получение строки с названием уку
        /// </summary>
        /// <param name="type">Тип ключа (алгоритм)</param>
        /// <returns>Возвращает строку с названием алгоритма для файла конфигурации</returns>
        private static string GetAlgorithmName(KeyTypes type)
        {
            string str = "";
            switch (type)
            {
                case KeyTypes.AES:
                    str = type.ToString();
                    break;
                case KeyTypes.SHA1:
                    str = type.ToString();
                    break;
                case KeyTypes.DES3:
                    str = "3DES";
                    break;
            }
            return str;
        } 
        #endregion
    }
}