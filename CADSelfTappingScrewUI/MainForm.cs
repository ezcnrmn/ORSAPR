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
    public partial class MainForm : Form
    {
        /// <summary>
        /// Словарь корректно введенных значений для каждого текстового поля
        /// </summary>
        Dictionary<string, bool> buildEnableDict 
            = new Dictionary<string, bool>(8);
        
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
            threadDiameterTextBox.BackColor = Color.White;
            buildEnableDict["threadDiameterTextBox"] = enable;
            
            internalThreadDiameterTextBox.Text = parameters[1];
            internalThreadDiameterTextBox.BackColor = Color.White;
            buildEnableDict["internalThreadDiameterTextBox"] = enable;
            
            rodLengthTextBox.Text = parameters[2];
            rodLengthTextBox.BackColor = Color.White;
            buildEnableDict["rodLengthTextBox"] = enable;

            threadStepTextBox.Text = parameters[3];
            threadStepTextBox.BackColor = Color.White;
            buildEnableDict["threadStepTextBox"] = enable;
            
            headHightTextBox.Text = parameters[4];
            headHightTextBox.BackColor = Color.White;
            buildEnableDict["headHightTextBox"] = enable;
            
            threadLengthTextBox.Text = parameters[5];
            threadLengthTextBox.BackColor = Color.White;
            buildEnableDict["threadLengthTextBox"] = enable;
            
            headDiameterTextBox.Text = parameters[6];
            headDiameterTextBox.BackColor = Color.White;
            buildEnableDict["headDiameterTextBox"] = enable;
            
            rodDiameterTextBox.Text = parameters[7];
            rodDiameterTextBox.BackColor = Color.White;
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

                    string errorMessage = "Значение в текстовом поле должно быть числом!" +
                                          "\nДесятичный разделитель - только \"запятая\"!";
                    MessageBox.Show(errorMessage, "Внимание!", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                    Control[] erroredTextBox = this.Controls.Find(textBoxName, true);
                    erroredTextBox[0].BackColor = Color.Salmon;
                    MessageBox.Show(a.Message.Split('\n')[0], "Внимание!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                string errorMessage = "Необходимо заполнить все поля!";
                MessageBox.Show(errorMessage, "Внимание!", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //TODO: Duplication
        private void TextBox_Leave(object sender, EventArgs e)
        {
            CheckInput((TextBox)sender);
        }
        
        //TODO: Должно быть в модели данных
        private void minParametersRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            string[] parametersValues = new string[8]
            {
                SelfTappingScrewParameters.MinValues["ThreadDiameter"].ToString(),
                SelfTappingScrewParameters.MinValues["InternalThreadDiameter"].ToString(),
                SelfTappingScrewParameters.MinValues["RodLength"].ToString(),
                SelfTappingScrewParameters.MinValues["ThreadStep"].ToString(),
                SelfTappingScrewParameters.MinValues["HeadHight"].ToString(),
                SelfTappingScrewParameters.MinValues["ThreadLength"].ToString(),
                SelfTappingScrewParameters.MinValues["HeadDiameter"].ToString(),
                SelfTappingScrewParameters.MinValues["RodDiameter"].ToString()
            };

            SetParameters(parametersValues, true);
        }

        private void defaultParametersRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            string[] parametersValues = new string[8]
            {
                SelfTappingScrewParameters.DefaultValues["ThreadDiameter"].ToString(),
                SelfTappingScrewParameters.DefaultValues["InternalThreadDiameter"].ToString(),
                SelfTappingScrewParameters.DefaultValues["RodLength"].ToString(),
                SelfTappingScrewParameters.DefaultValues["ThreadStep"].ToString(),
                SelfTappingScrewParameters.DefaultValues["HeadHight"].ToString(),
                SelfTappingScrewParameters.DefaultValues["ThreadLength"].ToString(),
                SelfTappingScrewParameters.DefaultValues["HeadDiameter"].ToString(),
                SelfTappingScrewParameters.DefaultValues["RodDiameter"].ToString()
            };
            
            SetParameters(parametersValues, true);
        }

        private void maxParametersRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            string[] parametersValues = new string[8]
            {
                SelfTappingScrewParameters.MaxValues["ThreadDiameter"].ToString(),
                SelfTappingScrewParameters.MaxValues["InternalThreadDiameter"].ToString(),
                SelfTappingScrewParameters.MaxValues["RodLength"].ToString(),
                SelfTappingScrewParameters.MaxValues["ThreadStep"].ToString(),
                SelfTappingScrewParameters.MaxValues["HeadHight"].ToString(),
                SelfTappingScrewParameters.MaxValues["ThreadLength"].ToString(),
                SelfTappingScrewParameters.MaxValues["HeadDiameter"].ToString(),
                SelfTappingScrewParameters.MaxValues["RodDiameter"].ToString()
            };
            
            SetParameters(parametersValues, true);
        }

        private void manualInputRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            string[] parametersValues = new string[8] { "", "", "", "", "", "", "", "" };
            SetParameters(parametersValues, false);
        }
    }
}
