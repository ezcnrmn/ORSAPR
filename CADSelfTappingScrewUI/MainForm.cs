using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CADSelfTappingScrew;
using KompasWrapper;
using Kompas6API5;

namespace CADSelfTappingScrewUI
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Словарь корректно введенных значений для каждого текстового поля
        /// </summary>
        Dictionary<string, bool> buildEnableDict = new Dictionary<string, bool>(8);
        
        /// <summary>
        /// Главная форма
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            manualInputRadioButton.Checked = true;
        }

        /// <summary>
        /// Функция установки параметров в поля параметров
        /// </summary>
        /// <param name="parameters">Массив параметров </param>
        /// <param name="enable">Значение в buildEnableDict</param>
        private void SetParameters(string[] parameters, bool enable)
        {
            threadDiameterTextBox.Text = parameters[0];
            buildEnableDict["threadDiameterTextBox"] = enable;
            internalThreadDiameterTextBox.Text = parameters[1];
            buildEnableDict["internalThreadDiameterTextBox"] = enable;
            rodLengthTextBox.Text = parameters[2];
            buildEnableDict["rodLengthTextBox"] = enable;
            threadStepTextBox.Text = parameters[3];
            buildEnableDict["threadStepTextBox"] = enable;
            headHightTextBox.Text = parameters[4];
            buildEnableDict["headHightTextBox"] = enable;
            threadLengthTextBox.Text = parameters[5];
            buildEnableDict["threadLengthTextBox"] = enable;
            headDiameterTextBox.Text = parameters[6];
            buildEnableDict["headDiameterTextBox"] = enable;
            rodDiameterTextBox.Text = parameters[7];
            buildEnableDict["rodDiameterTextBox"] = enable;
        }
        
        /// <summary>
        /// Функция проверки ввода данных на возможность преорбразования в double
        /// При невозможности меняет цвет поля на красный и выдает сообщение об ошибке
        /// </summary>
        /// <param name="textBox">Текстовое поля для проверки</param>
        private void CheckInput(TextBox textBox)
        {
            string sourceString = textBox.Text;

            if (sourceString.Length == 0)
            {
                textBox.BackColor = Color.White;
                buildEnableDict[textBox.Name] = false;
            }
            else
            {
                try
                {
                    double value = double.Parse(sourceString);

                    textBox.BackColor = Color.White;
                    buildEnableDict[textBox.Name] = true;
                }
                catch
                {
                    buildEnableDict[textBox.Name] = false;
                    textBox.BackColor = Color.Salmon;

                    string errorMessage = "Значение в текстовом поле должно быть числом!\nДесятичный разделитель - только \"запятая\"!";
                    MessageBox.Show(errorMessage, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }
        
        private void buildButton_Click(object sender, EventArgs e)
        {
            bool buildEnable = true;
            
            foreach (KeyValuePair<string, bool> i in buildEnableDict)
            {
                if (i.Value == false)
                {
                    buildEnable = false;
                }
            }

            if (buildEnable)
            {
                try
                {
                    SelfTappingScrewParameters selfTappingScrewParameters = new SelfTappingScrewParameters();
                    
                    selfTappingScrewParameters.HeadDiameter = double.Parse(headDiameterTextBox.Text);
                    selfTappingScrewParameters.HeadHight = double.Parse(headHightTextBox.Text);
                    selfTappingScrewParameters.RodLength = double.Parse(rodLengthTextBox.Text);
                    selfTappingScrewParameters.ThreadLength = double.Parse(threadLengthTextBox.Text);
                    selfTappingScrewParameters.ThreadDiameter = double.Parse(threadDiameterTextBox.Text);
                    selfTappingScrewParameters.ThreadStep = double.Parse(threadStepTextBox.Text);
                    selfTappingScrewParameters.RodDiameter = double.Parse(rodDiameterTextBox.Text);
                    selfTappingScrewParameters.InternalThreadDiameter =
                        double.Parse(internalThreadDiameterTextBox.Text);
                    
                    Kompas3DWrapper kompas3DWrapper = new Kompas3DWrapper();
                    kompas3DWrapper.OpenKompas();

                    SelfTappingScrewBuilder selfTappingScrewBuilder = new SelfTappingScrewBuilder();
                    selfTappingScrewBuilder.BuildSelfTappingScrew(kompas3DWrapper, selfTappingScrewParameters);
                }
                catch (ArgumentException a)
                {
                    string parameterName = a.ParamName;
                    string textBoxName = parameterName + "TextBox";
                    Control[] erroredTextBox = this.Controls.Find(textBoxName, true);
                    erroredTextBox[0].BackColor = Color.Salmon;
                    MessageBox.Show(a.Message.Split('\n')[0], "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                string errorMessage = "Необходимо заполнить все поля!";
                MessageBox.Show(errorMessage, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void threadDiameterTextBox_Leave(object sender, EventArgs e)
        {
            CheckInput(threadDiameterTextBox);
        }

        private void internalThreadDiameterTextBox_Leave(object sender, EventArgs e)
        {
            CheckInput(internalThreadDiameterTextBox);
        }

        private void rodLengthTextBox_Leave(object sender, EventArgs e)
        {
            CheckInput(rodLengthTextBox);
        }

        private void threadStepTextBox_Leave(object sender, EventArgs e)
        {
            CheckInput(threadStepTextBox);
        }

        private void headHightTextBox_Leave(object sender, EventArgs e)
        {
            CheckInput(headHightTextBox);
        }

        private void threadLengthTextBox_Leave(object sender, EventArgs e)
        {
            CheckInput(threadLengthTextBox);
        }

        private void headDiameterTextBox_Leave(object sender, EventArgs e)
        {
            CheckInput(headDiameterTextBox);
        }

        private void rodDiameterTextBox_Leave(object sender, EventArgs e)
        {
            CheckInput(rodDiameterTextBox);
        }

        private void minParametersRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            string[] parametersValues = new string[8] { "1,61", "1,1", "7", "0,8", "0,96", "3,8", "3", "1,61" };
            SetParameters(parametersValues, true);
        }

        private void defaultParametersRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            string[] parametersValues = new string[8] { "7,5", "5", "70", "2,5", "4", "50", "12", "7"};
            SetParameters(parametersValues, true);
        }

        private void maxParametersRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            string[] parametersValues = new string[8] { "9,9", "7", "100", "4,5", "5", "97", "18", "9,9" };
            SetParameters(parametersValues, true);
        }

        private void manualInputRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            string[] parametersValues = new string[8] { "", "", "", "", "", "", "", "" };
            SetParameters(parametersValues, false);
        }
    }
}
