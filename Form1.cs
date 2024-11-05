using System;
using System.Drawing;
using System.Windows.Forms;

namespace kolmNurg
{
    public partial class KolmNurgForm : Form
    {
        Button btn; // Кнопка для вычислений
        PictureBox pb; // Картинка треугольника
        ListView listView1; // Список для отображения результатов
        TextBox txtA, txtB, txtC; // Переменные для текстовых полей ввода сторон треугольника

        public KolmNurgForm()
        {
            this.Size = new Size(800, 600); // Размер формы
            this.Text = "Kolmnurga töö"; // Заголовок формы

            // Инициализация и настройка кнопки
            btn = new Button();
            btn.Text = "Вычислить треугольник"; // Текст кнопки
            btn.BackColor = Color.FromArgb(255, 255, 192); // Цвет фона
            btn.Font = new Font("Arial", 20); // Шрифт и размер текста
            btn.Cursor = Cursors.Hand; // Курсор при наведении
            btn.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192); // Цвет границы
            btn.FlatAppearance.BorderSize = 10; // Толщина границы
            btn.FlatStyle = FlatStyle.Flat; // Стиль кнопки
            btn.Size = new Size(180, 60); // Размер кнопки
            btn.Location = new Point(600, 20); // Положение кнопки
            btn.Click += Run_button_Click; // Подписка на событие клика
            Controls.Add(btn); // Добавление кнопки на форму

            // Инициализация и настройка PictureBox
            pb = new PictureBox();
            pb.Size = new Size(400, 200); // Размер PictureBox
            pb.Location = new Point(this.ClientSize.Width - 420, this.ClientSize.Height - 220); // Положение PictureBox в правом нижнем углу
            pb.SizeMode = PictureBoxSizeMode.Zoom; // Режим отображения изображения
            pb.Image = Image.FromFile("AngryCat.png"); // Загрузка изображения треугольника
            Controls.Add(pb); // Добавление PictureBox на форму

            // Создание текстовых полей с соответствующими метками
            CreateLabelAndTextBox("Сторона A:", 20, 340); // Текстовое поле для стороны A
            CreateLabelAndTextBox("Сторона B:", 20, 380); // Текстовое поле для стороны B
            CreateLabelAndTextBox("Сторона C:", 20, 420); // Текстовое поле для стороны C

            // Инициализация и настройка ListView для отображения результатов
            listView1 = new ListView();
            listView1.Size = new Size(300, 200); // Размер ListView (уменьшен)
            listView1.Location = new Point(20, 20); // Положение ListView в верхнем левом углу
            listView1.View = View.Details; // Установка режима отображения в виде деталей
            listView1.FullRowSelect = true; // Разрешить выбор полной строки
            listView1.GridLines = true; // Показать линии сетки

            // Определение колонок для ListView
            listView1.Columns.Add("Свойство", 150, HorizontalAlignment.Left); // Первая колонка
            listView1.Columns.Add("Значение", 150, HorizontalAlignment.Left); // Вторая колонка

            Controls.Add(listView1); // Добавление ListView на форму
        }

        // Метод для создания метки и текстового поля
        private void CreateLabelAndTextBox(string labelText, int x, int y)
        {
            // Создание метки
            Label label = new Label();
            label.Text = labelText; // Установка текста метки
            label.Location = new Point(x, y); // Положение метки
            label.Size = new Size(100, 30); // Размер метки
            Controls.Add(label); // Добавление метки на форму

            // Создание текстового поля
            TextBox textBox = new TextBox();
            textBox.Location = new Point(x + 110, y); // Положение текстового поля рядом с меткой
            textBox.Size = new Size(100, 30); // Размер текстового поля (уменьшен)

            // Установка текста подсказки
            textBox.Text = "Введите значение"; // Текст по умолчанию
            textBox.ForeColor = Color.Gray; // Цвет текста для подсказки

            // Обработчик события для потери фокуса
            textBox.Enter += (sender, e) =>
            {
                if (textBox.Text == "Введите значение") // Если текст равен подсказке
                {
                    textBox.Text = string.Empty; // Очистить текст
                    textBox.ForeColor = Color.Black; // Изменить цвет текста на черный
                }
            };

            // Обработчик события для получения фокуса
            textBox.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text)) // Если текст пустой
                {
                    textBox.Text = "Введите значение"; // Восстановить текст подсказки
                    textBox.ForeColor = Color.Gray; // Изменить цвет текста на серый
                }
            };

            Controls.Add(textBox); // Добавление текстового поля на форму

            // Сохранение ссылки на текстовое поле для дальнейшего использования
            if (labelText == "Сторона A:")
                txtA = textBox;
            else if (labelText == "Сторона B:")
                txtB = textBox;
            else if (labelText == "Сторона C:")
                txtC = textBox;
        }

        private void Run_button_Click(object sender, EventArgs e)
        {
            double a, b, c;

            // Попытка разобрать введенные значения из текстовых полей
            if (double.TryParse(txtA.Text, out a) &&
                double.TryParse(txtB.Text, out b) &&
                double.TryParse(txtC.Text, out c))
            {
                Triangle triangle = new Triangle(a, b, c); // Создание экземпляра треугольника

                // Очистка предыдущих элементов в ListView
                listView1.Items.Clear();

                // Добавление свойств в ListView
                listView1.Items.Add(new ListViewItem(new[] { "Сторона a", triangle.outputA() }));
                listView1.Items.Add(new ListViewItem(new[] { "Сторона b", triangle.outputB() }));
                listView1.Items.Add(new ListViewItem(new[] { "Сторона c", triangle.outputC() }));
                listView1.Items.Add(new ListViewItem(new[] { "Периметр", triangle.Perimeter().ToString() }));
                listView1.Items.Add(new ListViewItem(new[] { "Площадь", triangle.Surface().ToString() }));
                listView1.Items.Add(new ListViewItem(new[] { "Существует?", triangle.ExistTriangle ? "Существует" : "Не существует" }));
                listView1.Items.Add(new ListViewItem(new[] { "Тип треугольника", triangle.TriangleType }));
            }
            else
            {
                // Показ сообщения об ошибке, если ввод некорректный
                MessageBox.Show("Введите корректные значения для сторон треугольника.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KolmNurgForm_Load(object sender, EventArgs e)
        {
            // Дополнительная логика загрузки может быть добавлена здесь
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Логика для другой кнопки, если необходимо
        }
    }
}