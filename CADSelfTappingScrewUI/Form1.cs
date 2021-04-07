using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CADSelfTappingScrew;
using Kompas3DWrapper;

namespace CADSelfTappingScrewUI
{
    public partial class Form1 : Form
    {

        private Class1 _kompClass1 = new Class1();
        
        private SelfTappingScrewParameters _selfTappingScrew = new SelfTappingScrewParameters();
        
        public Form1()
        {
            InitializeComponent();
            
            _selfTappingScrew.HeadDiameter = 5;
            _selfTappingScrew.HeadHight = 1;
            _selfTappingScrew.RodLength = 96;
            _selfTappingScrew.ThreadLength = 95;
            _selfTappingScrew.ThreadDiameter = 4.7;
            _selfTappingScrew.ThreadStep = 4;
            _selfTappingScrew.RodDiameter = 4.5;
            _selfTappingScrew.InternalThreadDiameter = 4;
        }

        private void CheckInput(double min, double max, TextBox textBox)
        {
            try
            {
                string sourceString = textBox.Text;
                double value = double.Parse(sourceString);
                if (value > max || value < min)
                {
                    textBox.BackColor = Color.Salmon;
                }
                else
                {
                    textBox.BackColor = Color.White;
                }
                
            }
            catch
            {
                textBox.BackColor = Color.Salmon;
            }
        }
        
        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            double min = 1.6;
            double max = 10.0; 
            CheckInput(min, max, textBox8);
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            double min = 1.1;
            double max = 7.0;
            CheckInput(min, max, textBox7);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            double min = 7.0;
            double max = 100.0;
            CheckInput(min, max, textBox1);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            double min = 0.8;
            double max = 4.5;
            CheckInput(min, max, textBox3);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            double min = 0.96;
            double max = 5.0;
            CheckInput(min, max, textBox2);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            double min = 3.0;
            double max = 97.0;
            CheckInput(min, max, textBox4);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            double min = 3.0;
            double max = 18.0;
            CheckInput(min, max, textBox5);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            double min = 1.6;
            double max = 10.0;
            CheckInput(min, max, textBox6);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            string errorText = "Ошибка в поле d - Пустое поле\nОшибка в поле d2 - Нечисловое значение\nОшибка в поле l - Значение меньше минимума\nОшибка в поле P - Значение больше максимума\n";
            MessageBox.Show(
                errorText,
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            */
            _kompClass1.OpenKompas();
        }
    }
}
