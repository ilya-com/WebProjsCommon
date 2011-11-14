using System;
using System.Windows.Forms;

namespace KeysGenerator
{
    public partial class Fmain : Form
    {
        KeyData key1, key2; // ключи

        public Fmain()
        {
            InitializeComponent();
            saveButton.Visible = false;
            InitLists();
        }

        /// <summary>
        /// Инициалиазция списков с алгоритмами
        /// </summary>
        private void InitLists()
        {
            comboBox1.Items.AddRange(Enum.GetNames(typeof(KeyTypes)));
            comboBox2.Items.AddRange(Enum.GetNames(typeof(KeyTypes)));
        }

        /// <summary>
        /// Кнопка Выход
        /// </summary>
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Кнопка Сгенерировать
        /// </summary>
        private void generateButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0 || comboBox2.SelectedIndex < 0)
            {
                MessageBox.Show("Не выбраны алгоритмы для генерации ключей!", "Предупредение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            key1 = KeyGenerator2.GenerateKey((KeyTypes)Enum.Parse(typeof(KeyTypes), String.Format("{0}", comboBox1.SelectedIndex)));
            key2 = KeyGenerator2.GenerateKey((KeyTypes)Enum.Parse(typeof(KeyTypes), String.Format("{0}", comboBox2.SelectedIndex)));
            textBox1.Text = key1.KeyValue;
            textBox2.Text = key2.KeyValue;
            saveButton.Visible = true;
        }

        /// <summary>
        /// Кнопка Сохранить
        /// </summary>
        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dg = new SaveFileDialog();
            dg.Title = "Введите название файла для сохранения ключей";
            dg.Filter = "Текстовые файлы|*.txt|XML-файлы|*.xml";
            dg.ShowDialog();
            if (dg.FileName != "")
            {
                KeyGenerator2.PrintKeysToFile(key1, key2, dg.FileName);
                MessageBox.Show("Ключи успешно сохранены в файле!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
