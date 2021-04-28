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

namespace CADSelfTappingScrewUI
{
    /// <summary>
    /// Класс главной формы
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Словарь корректно введенных значений для каждого текстового поля
        /// </summary>
        Dictionary<string, bool> buildEnableDict 
            = new Dictionary<string, bool>();
        
        /// <summary>
        /// Главная форма
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            manualInputRadioButton.Checked = true;
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

                    string errorMessage = "Значение в текстовом поле должно быть числом!" +
                                          "\nДесятичный разделитель - только \"запятая\"!";
                    MessageBox.Show(errorMessage, "Внимание!", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }
        
        //TODO: XML комментарии?
        /// <summary>
        /// Событие нажатия кнопки постройки
        /// </summary>
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
                    SelfTappingScrewParameters selfTappingScrewParameters 
                        = new SelfTappingScrewParameters();
                    
                    selfTappingScrewParameters.HeadDiameter 
                        = double.Parse(headDiameterTextBox.Text);
                    selfTappingScrewParameters.HeadHight 
                        = double.Parse(headHightTextBox.Text);
                    selfTappingScrewParameters.RodLength 
                        = double.Parse(rodLengthTextBox.Text);
                    selfTappingScrewParameters.ThreadLength 
                        = double.Parse(threadLengthTextBox.Text);
                    selfTappingScrewParameters.ThreadDiameter 
                        = double.Parse(threadDiameterTextBox.Text);
                    selfTappingScrewParameters.ThreadStep 
                        = double.Parse(threadStepTextBox.Text);
                    selfTappingScrewParameters.RodDiameter 
                        = double.Parse(rodDiameterTextBox.Text);
                    selfTappingScrewParameters.InternalThreadDiameter =
                        double.Parse(internalThreadDiameterTextBox.Text);
                    selfTappingScrewParameters.Washer = washerCheckBox.Checked;
                    
                    Kompas3DWrapper kompas3DWrapper = new Kompas3DWrapper();
                    kompas3DWrapper.OpenKompas();

                    SelfTappingScrewBuilder selfTappingScrewBuilder 
                        = new SelfTappingScrewBuilder();
                    selfTappingScrewBuilder.BuildSelfTappingScrew(kompas3DWrapper, 
                        selfTappingScrewParameters);
                }
                catch (ArgumentException a)
                {
                    string parameterName = a.ParamName;
                    string textBoxName = parameterName + "TextBox";
                    Control[] erroredTextBox = this.Controls.Find(textBoxName,
                        true);
                    erroredTextBox[0].BackColor = Color.Salmon;
                    MessageBox.Show(a.Message.Split('\n')[0],
                        "Внимание!",
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                string errorMessage = "Необходимо заполнить все поля!";
                MessageBox.Show(errorMessage, "Внимание!", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

         //TODO: RSDN
        /// <summary>
        /// Событие покидания текстового поля
        /// </summary>
        private void TextBox_Leave(object sender, EventArgs e)
        {
            CheckInput((TextBox)sender);
        }

        /// <summary>
        /// Функция установки параметров в поля параметров
        /// </summary>
        /// <param name="enable">Значение в buildEnableDict</param>
        /// /// <param name="type">0 - пустое, 1 - минимальное, 2 - максимальное, 3 - по-умолчанию</param>
        private void SetParameters(bool enable, int type)
        {
            Dictionary<string, string> parametersDictionary = new Dictionary<string, string>();
            foreach (Enum par in Enum.GetValues(typeof(ParametersName)))
            {
                if(type == 0)
                    parametersDictionary[par.ToString()] = "";
                else if (type == 1)
                    parametersDictionary[par.ToString()] = 
                        SelfTappingScrewParameters.MinValues[par].ToString();
                else if (type == 2)
                    parametersDictionary[par.ToString()] = 
                        SelfTappingScrewParameters.MaxValues[par].ToString();
                else if (type == 3)
                    parametersDictionary[par.ToString()] = 
                        SelfTappingScrewParameters.DefaultValues[par].ToString();
            }
            
            string tempName;
            foreach (Control c in parametersGroupBox.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    tempName = c.Name.ToUpper()[0] + 
                               c.Name.Substring(1, c.Name.Length - 8);
                    c.Text = parametersDictionary[tempName];

                    c.BackColor = Color.White;
                    buildEnableDict[c.Name] = enable;
                }
            }
        }
        
        //TODO: XML комментарии?
        /// <summary>
        /// Событие переключения на radio button с минимальными параметрами
        /// </summary>
        private void minParametersRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SetParameters(true, 1);
        }

        //TODO: XML комментарии?
        /// <summary>
        /// Событие переключения на radio button с параметрами по-умолчанию
        /// </summary>
        private void defaultParametersRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SetParameters(true, 3);
        }

        //TODO: XML комментарии?
        /// <summary>
        /// Событие переключения на radio button с максимальными параметрами
        /// </summary>
        private void maxParametersRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SetParameters(true, 2);
        }

        //TODO: XML комментарии?
        /// <summary>
        /// Событие переключения на radio button с вводом параметров вручную
        /// </summary>
        private void manualInputRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SetParameters(true, 0);
        }
    }
}
