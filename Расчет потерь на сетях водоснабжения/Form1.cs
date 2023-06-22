using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Data.SqlTypes;
using System.IO;

namespace Расчет_потерь_на_сетях_водоснабжения
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Start sf = new Start();
            sf.ShowDialog();
        }
        int flag = -1;

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void choiceTypeofDamage(object sender, EventArgs e)
        {
            flag = damageType.SelectedIndex;
        }
        int ind = 0;
        private void AddData_Click(object sender, EventArgs e)
        {
             
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void saveData_Click(object sender, EventArgs e)
        {

            //string s = "Расчет потерь на сетях водоснабжения" + Convert.ToString(damageType.Items[ind]);
            string data = Convert.ToString(DateTime.Now).Replace('.', '_');
            data = data.Replace(':', '-');

            string s = "Расчет потерь на сетях водоснабжения" + data + ".txt";

            string s1 = s.Replace(' ', '_');

            //string s = "Отчет_.txt";
            // string path = @"C:\\ResWT\" + s1;

            string path = @"..\" + s1;

            try
            {
                //FileStream fs = new FileStream(s, FileMode.Create, FileAccess.Write);           // Новый файл
                StreamWriter sw = new StreamWriter(path);                                         // поток на запись

                sw.WriteLine(textBox1.Text);

                sw.Close();
               // fs.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            textBox1.Text = "Файл с отчетом успешно создан: " + s1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void damageType_DoubleClick(object sender, EventArgs e)
        {
            ind = damageType.SelectedIndex;
            double f1 = 0, f2 = 0, f3 = 0, f4 = 0, f5 = 0, f6 = 0, res = 0;

            if (ind == -1)
                MessageBox.Show("Выберите тип повреждения из списка");

            //Ввод данных по тиу повреждения (задание параметров разные для каждого повреждения)
            //_____________________________________________________________________________________________________________________________________________________
            //УТЕЧКИ ВОДЫ ПРИ ПОВРЕЖДЕНИЯХ 

            if (ind == 0)
            {
                choice1 win1 = new choice1();

                // win1.Location = new Point { X = 431, Y = 58 };
                win1.ShowDialog();
                string[] mas = new string[3];
                if (win1.DialogResult == DialogResult.OK)    // После нажатия кнопки ОК на второй форме результаты передаются сюда
                {

                    try
                    {
                        mas = win1.ReturnData();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message, "\n Please, check data format");
                        return;
                    }

                }

                try
                {
                    f3 = Math.Sqrt(Convert.ToDouble(mas[1]));
                    f1 = Convert.ToDouble(mas[2]);
                    f2 = Convert.ToDouble(mas[0]);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("\n Значение Н не может быть нулевым");
                    return;
                }
                try
                {
                    f1 = Convert.ToDouble(mas[2]);
                    f2 = Convert.ToDouble(mas[0]);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("\n Задайти значения всем полям");
                    return;
                }


                res = 9600 * f1 * f2 * f3;


                textBox1.Text = "   Расчет потери воды при повреждении\r\n\r\n\r\n   Входные данные:\r\n" +
                    "\r\n   Wᵢ - площадь живого сечения i-го отверстия (кв. м)\r\n" +
                    "   H   - принимается равным средней величине напора воды в\r\n   трубопроводе на поврежденном участке;"+
                    "при переломах и\r\n   разрывах труб H принимается равным средней глубине\r\n   заложения трубопровода\r\n" +
                    "   t  - продолжительность утечки с момента обнаружения до\r\n   отключения поврежденного участка или заделки отверстия\r\n   трубопровода\r\n\r\n   Формула расчета:\r\n   Wᵧₙᵢ = 9600 × t × Wᵢ × √Hᵢ \r\n" +
                    "\r\n   Результат:\r\n" +
                    "   Wᵧₙᵢ = 9600 × " + f1 + " × " + f2 + " × " + f3 + " = " + res + " м³ ";
            }

            //_____________________________________________________________________________________________________________________________________________________
            //СВИЩЕВЫЕ ПОВРЕЖДЕНИЯ 
            if (ind == 1)
            {
                choice2 win2 = new choice2();

                // win1.Location = new Point { X = 431, Y = 58 };
                win2.ShowDialog();
                string[] mas = new string[2];
                if (win2.DialogResult == DialogResult.OK)    // После нажатия кнопки ОК на второй форме результаты передаются сюда
                {

                    try
                    {
                        mas = win2.ReturnData();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message, "\n Please, check data format");
                    }

                }

                try
                {
                    f2 = Math.Sqrt(Convert.ToDouble(mas[0]));
                    

                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("\n Значение Н не может быть нулевым");
                    return;
                }

                try
                {
                    f1 = Convert.ToDouble(mas[1]);

                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("\n Задайте значения всем полям");
                    return;
                }

                
                res = 1.92 * f1 * f2;


                textBox1.Text = "   Свищевые повреждения\r\n\r\n   Входные данные:\r\n\r\n" +

                    "   H - принимается равным средней величине напора воды\r\n   в трубопроводе на поврежденном участке;"+
                    " при переломах\r\n   и разрывах труб H принимается равным средней глубине\r\n   заложения трубопровода\r\n" +
                    "   t - продолжительность утечки с момента обнаружения до\r\n   отключения поврежденного участка или заделки отверстия\r\n   трубопровода\r\n\r\n   Формула расчета:\r\n   Wᵧₙᵢ = 1.92 × t × √Hᵢ \r\n" +
                    "\r\n\r\n   Результат:\r\n" +
                    "   Wᵧₙᵢ = 1.92 × " + f1 + " × " + f2 + " = " + res + " м³ ";

            }
            //_____________________________________________________________________________________________________________________________________________________
            //ТРЕЩИНЫ 
            if (ind == 2)
            {
                choice3 win3 = new choice3();

                // win1.Location = new Point { X = 431, Y = 58 };
                win3.ShowDialog();
                string[] mas = new string[3];
                if (win3.DialogResult == DialogResult.OK)    // После нажатия кнопки ОК на второй форме результаты передаются сюда
                {

                    try
                    {
                        mas = win3.ReturnData();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message, "\n Please, check data format");
                    }

                }
                try
                {
                    f3 = Math.Sqrt(Convert.ToDouble(mas[0]));
                    
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("\n Значение Н не может быть нулевым");
                    return;
                }

                try
                {
                    f1 = Math.Pow(Convert.ToDouble(mas[1]), 2);
                    f2 = Convert.ToDouble(mas[2]);

                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("\n Задайте значения всем полям");
                    return;
                }


                res = 374.4 * f1 * f2 * f3;


                textBox1.Text = "   Трещины\r\n\r\n   Входные данные:\r\n" +

                    "   H - принимается равным средней величине напора воды\r\n   в трубопроводе на поврежденном участке;"+
                    " при переломах\r\n   и разрывах труб H принимается равным средней глубине\r\n   заложения трубопровода\r\n" +
                    "   d - диаметр трубы\r\n"+
                    "   t - продолжительность утечки с момента обнаружения до\r\n   отключения поврежденного участка или заделки отверстия\r\n   трубопровода\r\n\r\n   Формула расчета:\r\n   Wᵧₙᵢ = 374.4 × d² × t × √Hᵢ\r\n\r\n" +
                    "\r\n   Результат\r\n" +
                    "   Wᵧₙᵢ = 374.4 × " + f1 + " × " + f2 + " × " + f3 + " = " + res + " м³ ";

            }
            //_____________________________________________________________________________________________________________________________________________________
            //ПЕРЕЛОМЫ, РАЗРЫВЫ
            if (ind == 3)
            {
                choice4 win4 = new choice4();

                // win1.Location = new Point { X = 431, Y = 58 };
                win4.ShowDialog();
                string[] mas = new string[3];
                if (win4.DialogResult == DialogResult.OK)    // После нажатия кнопки ОК на второй форме результаты передаются сюда
                {

                    try
                    {
                        mas = win4.ReturnData();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message, "\n Please, check data format");
                    }

                }

                try
                {
                    f3 = Math.Sqrt(Convert.ToDouble(mas[0]));
                    
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("\n Значение Н не может быть нулевым");
                    return;
                }
                try
                {
                    f1 = Math.Pow(Convert.ToDouble(mas[1]), 2);
                    f2 = Convert.ToDouble(mas[2]);

                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("\n Задайте значения всем полям");
                    return;
                }

                res = 5652 * f1 * f2 * f3;


                textBox1.Text = "   Переломы, разрывы\r\n\r\n   Входные данные:\r\n" +

                    "   H - принимается равным средней величине напора воды\r\n   в трубопроводе на поврежденном участке;"+
                    "при переломах\r\n   и разрывах труб H принимается равным средней глубине\r\n   заложения трубопровода\r\n" +
                    "   d - диаметр трубы\r\n   " +
                    "t - продолжительность утечки с момента обнаружения\r\n   доотключения поврежденного участка или заделки отверстия\r\n   трубопровода\r\n \r\n\r\n   Формула расчета:\r\n   Wᵧₙᵢ = 5652 × d² × t × √Hᵢ\r\n" +
                    "\r\n\r\n   Результат:\r\n" +
                    "   Wᵧₙᵢ = 5652 × " + f1 + " × " + f2 + " × " + f3 + " = " + res + " м³ ";

            }

            //_____________________________________________________________________________________________________________________________________________________
            //УТЕЧКИ ЧЕРЕЗ УПЛОТНЕНИЯ СЕТЕВОЙ АРМАТУРЫ
            if (ind == 4)
            {
                choice5 win5 = new choice5();

                // win1.Location = new Point { X = 431, Y = 58 };
                win5.ShowDialog();
                string[] mas = new string[4];
                if (win5.DialogResult == DialogResult.OK)    // После нажатия кнопки ОК на второй форме результаты передаются сюда
                {

                    try
                    {
                        mas = win5.ReturnData();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message, "\n Please, check data format");
                    }

                }

                try
                {
                    f1 = Convert.ToDouble(mas[0]);
                    f2 = Convert.ToDouble(mas[1]);
                    f3 = Convert.ToDouble(mas[2]);
                    f4 = Convert.ToDouble(mas[3]);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("\n Задайте значения всем полям");
                    return;
                }

                
                 
                 res = f1 * f2 * f3 * f4;


                textBox1.Text = "   Утечки через уплотнения сетевой арматуры\r\n\r\n   Входные данные:\r\n\r\n" +

                    "   u - доля арматуры, имеющей утечки в долях единиц \r\n" +
                    "   n - общее количество сетевой арматуры\r\n" +
                    "   q - средний расход при утечке через уплотнения сетевой\r\n   арматуры (куб. м/сутки); " +
                    "При отсутствии\r\n   фактических данных средний расход при утечке\r\n   через уплотнения сетевой арматуры допускается\r\n   принимать равным 4,3 куб. м/сутки на 1 ед. сетевой\r\n   арматуры, равным 0,02;\r\n" +
                    "   z - расчетный период (количество суток).\r\n\r\n   Формула расчета:\r\n   Wᵧₙᵢ = u × n × q × z \r\n" +
                    "\r\n\r\n   Результат:Т\r\n" +
                    "   Wᵧₙᵢ =  " + f1 + " × " + f2 + " × " + f3 + " × " + f4 + " = " + res + " м³ ";

            }
            //_____________________________________________________________________________________________________________________________________________________
            //УТЕЧКИ ЧЕРЕЗ ВОДОРАЗБОРНЫЕ КОЛОНКИ (НА ПРОТОК)
            if (ind == 5)
            {
                choice6 win6 = new choice6();

                // win1.Location = new Point { X = 431, Y = 58 };
                win6.ShowDialog();
                string[] mas = new string[4];
                if (win6.DialogResult == DialogResult.OK)    // После нажатия кнопки ОК на второй форме результаты передаются сюда
                {

                    try
                    {
                        mas = win6.ReturnData();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message, "\n Please, check data format");
                    }

                }
                try
                {
                    f1 = Convert.ToDouble(mas[0]);
                    f2 = Convert.ToDouble(mas[1]);
                    f3 = Convert.ToDouble(mas[2]);
                    f4 = Convert.ToDouble(mas[3]);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("\n Задайте значения всем полям");
                    return;
                }

                
                 
                 res = f1 * f2 * f3 * f4;


                textBox1.Text = "\r\n   Утечки через водоразборные колонки (на проток)\r\n\r\n   Входные данные:\r\n" +
                    "   u - доля водоразборных колонок, имеющих утечки в долях единиц\r\n" +
                    "   n - общее количество водоразборных колонок\r\n" +
                    "   q - средний расход при утечке через водоразборную колонку (куб. м/сутки).\r\n   При отсутствии фактических данных допускается принимать\r\n   q = 0.25 л/сек;\r\n   q = 21.6 м³/сут;\r\n \r\n" +
                    "   z - расчетный период (количество суток).\r\n \r\n\r\n   Формула расчета:\r\n   Wᵧₙᵢ = u × n × q × z\r\n" +
                    "\r\n   Результат:\r\n" +
                    "   Wᵧₙᵢ =  " + f1 + " × " + f2 + " × " + f3 + " × " + f4 + " = " + res + " м³ ";

            }
            //_____________________________________________________________________________________________________________________________________________________
            //УТЕЧКИ НА ВОДОРАЗБОРНЫХ КОЛОНКАХ (ПРИ ВКЛ/ВЫКЛ)
            if (ind == 6)
            {
                choice7 win7 = new choice7();

                // win1.Location = new Point { X = 431, Y = 58 };
                win7.ShowDialog();
                string[] mas = new string[6];
                if (win7.DialogResult == DialogResult.OK)    // После нажатия кнопки ОК на второй форме результаты передаются сюда
                {

                    try
                    {
                        mas = win7.ReturnData();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message, "\n Please, check data format");
                    }

                }
                 
                try
                {
                    f1 = Math.Sqrt(Convert.ToDouble(mas[0]));
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("\n Значение Н не может быть нулевым");
                    return;
                }

                try
                {
                    f2 = Convert.ToDouble(mas[1]);
                    f3 = Convert.ToDouble(mas[2]);
                    f4 = Convert.ToDouble(mas[3]);
                    f5 = Convert.ToDouble(mas[4]);
                    f6 = Convert.ToDouble(mas[5]);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("\n Задайте значения всем полям");
                    return;
                }

                
                 res = 9600 * f1 * f2 * f3 * f4 * f5 * f6;


                textBox1.Text = "\r\n   Утечки на водоразборных колонках (при вкл/выкл)\r\n\r\n   Входные данные:\r\n\r\n" +

                    "   H - средний напор воды принимается равным средней величине напора воды в\r\n   трубопроводе;\r\n" +
                    "   w - площадь живого сечения i-го отверстия (кв. м) = pi × d² / 4 = 3,14 × 0.0001\r\n" +
                    "   t - продолжительность утечки по фактическим данным 6 сек = 0,0017 час\r\n" +
                    "   k - количество включений водоразборной колонки в сутки - 20\r\n" +
                    "   n - количество водоразборных колонок\r\n" +
                    "   z - расчетный период (количество суток)\r\n \r\n\r\n   Формула расчета:\r\n   Wᵧᵦₖ = 9600 × √H × t × k × n × z \r\n" +
                    "\r\n\r\n   Результат:\r\n" +
                    "   Wᵧᵦₖ = 9600 × " + f1 + " × " + f2 + " × " + f3 + " × " + f4 + " × " + f5 + " × " + f6 + " = " + res + " м³ ";

            }
            //_____________________________________________________________________________________________________________________________________________________
            //ЕСТЕСТВЕННАЯ УБЫЛЬ
            if (ind == 7)
            {
                choice9 win8 = new choice9();

                // win1.Location = new Point { X = 431, Y = 58 };
                win8.ShowDialog();
                string[,] mas = new string[2, 5];
                if (win8.DialogResult == DialogResult.OK)    // После нажатия кнопки ОК на второй форме результаты передаются сюда
                {

                    try
                    {
                        mas = win8.ReturnData();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message, "\n Please, check data format");
                    }

                }

                //  ТАБЛИЧКА  Материал труб. Входные параметры для расчета

                double n = 0.0;
                if (Convert.ToInt32(mas[0, 1]) == 0)                        //стальная труба
                {
                    n = 0.0;

                    switch (Convert.ToInt32(mas[0, 2]))
                    {
                        case 100:
                            n = 16.8;
                            break;

                        case 125:
                            n = 21;
                            break;

                        case 150:
                            n = 25.2;
                            break;

                        case 200:
                            n = 33.6;
                            break;

                        case 250:
                            n = 42;
                            break;

                        case 300:
                            n = 51;
                            break;

                        case 350:
                            n = 54;
                            break;

                        case 400:
                            n = 60;
                            break;

                        case 450:
                            n = 63;
                            break;

                        case 500:
                            n = 66;
                            break;

                        case 600:
                            n = 72;
                            break;

                        case 700:
                            n = 78;
                            break;

                        case 800:
                            n = 81;
                            break;

                        case 900:
                            n = 87;
                            break;

                        case 1000:
                            n = 90;
                            break;

                        case 1100:
                            n = 93;
                            break;

                        case 1200:
                            n = 99;
                            break;

                        case 1400:
                            n = 105;
                            break;

                        case 1600:
                            n = 111;
                            break;

                        case 1800:
                            n = 117;
                            break;

                        case 2000:
                            n = 126;
                            break;


                    }

                }

                if (Convert.ToInt32(mas[0, 1]) == 1)                        // чугунная труба
                {
                    n = 0.0;

                    switch (Convert.ToInt32(mas[0, 2]))
                    {
                        case 100:
                            n = 42;
                            break;

                        case 125:
                            n = 54;
                            break;

                        case 150:
                            n = 63;
                            break;

                        case 200:
                            n = 84;
                            break;

                        case 250:
                            n = 93;
                            break;

                        case 300:
                            n = 102;
                            break;

                        case 350:
                            n = 108;
                            break;

                        case 400:
                            n = 117;
                            break;

                        case 450:
                            n = 126;
                            break;

                        case 500:
                            n = 132;
                            break;

                        case 600:
                            n = 144;
                            break;

                        case 700:
                            n = 153;
                            break;

                        case 800:
                            n = 162;
                            break;

                        case 900:
                            n = 174;
                            break;

                        case 1000:
                            n = 180;
                            break;


                    }

                }

                if (Convert.ToInt32(mas[0, 1]) == 2)                        // асбестоцементная труба
                {
                    n = 0.0;

                    switch (Convert.ToInt32(mas[0, 2]))
                    {

                        case 200:
                            n = 188.8;
                            break;

                        case 250:
                            n = 133.2;
                            break;

                        case 300:
                            n = 145.2;
                            break;

                        case 350:
                            n = 157.2;
                            break;

                        case 400:
                            n = 168;
                            break;

                        case 450:
                            n = 177.6;
                            break;

                        case 500:
                            n = 188.4;
                            break;


                    }

                }

                if (Convert.ToInt32(mas[0, 1]) == 3)                        // железобетонная труба
                {
                    n = 0.0;

                    switch (Convert.ToInt32(mas[0, 2]))
                    {

                        case 200:
                            n = 120;
                            break;

                        case 250:
                            n = 132;
                            break;

                        case 300:
                            n = 144;
                            break;

                        case 350:
                            n = 156;
                            break;

                        case 400:
                            n = 168;
                            break;

                        case 450:
                            n = 180;
                            break;

                        case 500:
                            n = 192;
                            break;

                        case 600:
                            n = 204;
                            break;

                        case 700:
                            n = 222;
                            break;

                        case 800:
                            n = 234;
                            break;

                        case 900:
                            n = 252;
                            break;

                        case 1000:
                            n = 264;
                            break;

                        case 1100:
                            n = 276;
                            break;

                        case 1200:
                            n = 288;
                            break;

                        case 1400:
                            n = 300;
                            break;

                        case 1600:
                            n = 312;
                            break;

                        case 1800:
                            n = 372;
                            break;

                        case 2000:
                            n = 414;
                            break;

                    }

                }


                if (n == 0)
                    MessageBox.Show(" Данных для такого диаметра и материала трубы нет\n Проверьте правильность ввода этих данных ");

                try
                {
                    f1 = Convert.ToDouble(mas[0, 0]);
                    f3 = Convert.ToDouble(mas[0, 2]);
                    f4 = Convert.ToDouble(mas[0, 3]);
                    f5 = Convert.ToDouble(mas[0, 4]);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("\n Задайте значения всем полям");
                    return;
                }

                
                //double f2 = Convert.ToDouble(mas[0, 1]);
                 

                 res = f4 * f5 * n;


                textBox1.Text = "   Естественная убыль\r\n\r\n   Потери при транспортировке воды для передачи абонентам\r\n\r\n   Входные данные:\r\n\r\n" +
                    "   N - количество участков ВС постоянного диаметра и материала. \r\n" +
                    "   n - норма естественной убыли, кг/км ч, определяемая на основе\r\n   материала и внутренего диаметра трубопровода\r\n" +
                    "   t - продолжительность расчетного периода, ч;\r\n" +
                    "   lᵢ - протяженность i-го участка водоснабжения постоянного\r\n   диаметра и материала, км;" +
                    " \r\n\r\n   Формула расчета:\r\n   G1 = t × ∑ lᵢ × n,  где i = 1...N" +
                    "\r\n\r\n   Результат:\r\n" +
                    "   G1 =  " + f4 + " × " + f5 + " × " + n + " = " + res + " м³          N = " + f1 + "\r\n";

                try
                {
                    f1 = Convert.ToDouble(mas[1, 0]);
                    f2 = Convert.ToDouble(mas[1, 1]);
                    f3 = Convert.ToDouble(mas[1, 2]);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("\n Задайте значения всем полям");
                    return;
                }

                
               

                res = 0.125 * f2 * f3;

                textBox1.Text += "\r\n\r\n   Естественная убыль воды при хранении в РЧВ,\r\n   размещенных на водопроводных сетях\r\n\r\n   Входные данные:\r\n\r\n" +
                    "   N - количество РЧВ. \r\n" +
                    "   t - продолжительность работы i-го РЧВ за расчетный период, ч;\r\n" +
                    "   Fᵢ - площадь смоченной поверхности i-го РЧВ.\r\n   Площадь смоченной поверхности определяется при наполнении\r\n   резервуара до половины рабочей глубины;\r\n   0,125 - норма естественной убыли воды при\r\n   хранении в РЧВ, кг/м² ч." +
                    "\r\n\r\n   Формула расчета\r\n   G2 = 0.125 × t × ∑ Fᵢ,  где i = 1...N" +
                    "\r\n\r\n   Результат:\r\n" +
                    "   G2 = 0.125 × " + f2 + " × " + f3 + " = " + res + " м³           N = " + f1 + "  \r\n";

            }

            //_____________________________________________________________________________________________________________________________________________________
            //Расходы воды на отогрев трубопроводов
            if (ind == 8)
            {
                MessageBox.Show("\n Расходы воды на отогрев трубопроводов определяется по опыту эксплуатации, исходя из фактических значений за последние три года. ");

            }

            //_____________________________________________________________________________________________________________________________________________________
            //СКРЫТЫЕ УТЕЧКИ И ПОТЕРИ ПО НЕВЫЯВЛЕННЫМ ПРИЧИНАМ
            if (ind == 9)
            {
                choice10 win9 = new choice10();

                // win1.Location = new Point { X = 431, Y = 58 };
                win9.ShowDialog();
                string[] mas = new string[6];
                if (win9.DialogResult == DialogResult.OK)    // После нажатия кнопки ОК на второй форме результаты передаются сюда
                {

                    try
                    {
                        mas = win9.ReturnData();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message, "\n Please, check data format");
                    }

                }

                try
                {
                    f1 = Convert.ToDouble(mas[0]);
                    f2 = Convert.ToDouble(mas[1]);
                    f3 = Convert.ToDouble(mas[2]);
                    f4 = Convert.ToDouble(mas[3]);
                    f5 = Convert.ToDouble(mas[4]);
                    f6 = Convert.ToDouble(mas[5]);

                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("\n Задайте значения всем полям");
                    return;
                }
                 

                 res = f3 - f1 - (f4 + f5 + f6 + (0.018 * f1 * f2));


                textBox1.Text = "\r\n   Скрытые утечки и потери по невыявленным причинам\r\n\r\n   Входные данные:\r\n\r\n" +
                    "   Wотп  - объем воды, отпускаемой абонентам. \r\n" +
                    "   k - отношение объема отпущенной воды по показанию\r\n   приборов (узлов) учета абонентов к общему объему отпущенной\r\n   воды (коэффициент приборного учета).\r\n" +
                    "   Wпод - объем воды, поданной в сеть;, \r\n" +
                    "   Wпол - суммарный объем расходов воды; \r\n" +
                    "   Wпот - объем потерь при повреждениях из водопроводных сетей;\r\n" +
                    "   G - объем потерь воды за счет естественной убыли.\r\n" +
                    "\r\n   Формула расчета\r\n   Wскр = Wпод - Wотп - (Wпол + Wпот + G + (0.018 × Wотп × k)" +
                    "\r\n\r\n   Результат\r\n" +
                    "   Wскр =  " + f3 + " - " + f1 + " - ( " + f4 + " + " + f5 + " + " + f6 + " + (0.018 × " + f1 + " × " + f2 + ")) = " + res + " м³ ";

            }
        }
    }
}
