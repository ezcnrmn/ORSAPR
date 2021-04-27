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
        
        //TODO: XML комментарии?
        /// <summary>
        /// Событие переключения на radio button с минимальными параметрами
        /// </summary>
        private void minParametersRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            string[] parametersValues = new string[8]
            {
                SelfTappingScrewParameters.MinValues[SelfTappingScrewParameters.ParametersName.ThreadDiameter].ToString(),
                SelfTappingScrewParameters.MinValues[SelfTappingScrewParameters.ParametersName.InternalThreadDiameter].ToString(),
                SelfTappingScrewParameters.MinValues[SelfTappingScrewParameters.ParametersName.RodLength].ToString(),
                SelfTappingScrewParameters.MinValues[SelfTappingScrewParameters.ParametersName.ThreadStep].ToString(),
                SelfTappingScrewParameters.MinValues[SelfTappingScrewParameters.ParametersName.HeadHight].ToString(),
                SelfTappingScrewParameters.MinValues[SelfTappingScrewParameters.ParametersName.ThreadLength].ToString(),
                SelfTappingScrewParameters.MinValues[SelfTappingScrewParameters.ParametersName.HeadDiameter].ToString(),
                SelfTappingScrewParameters.MinValues[SelfTappingScrewParameters.ParametersName.RodDiameter].ToString()
            };

            SetParameters(parametersValues, true);
        }

        //TODO: XML комментарии?
        /// <summary>
        /// Событие переключения на radio button с параметрами по-умолчанию
        /// </summary>
        private void defaultParametersRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            string[] parametersValues = new string[8]
            {
                SelfTappingScrewParameters.DefaultValues[SelfTappingScrewParameters.ParametersName.ThreadDiameter].ToString(),
                SelfTappingScrewParameters.DefaultValues[SelfTappingScrewParameters.ParametersName.InternalThreadDiameter].ToString(),
                SelfTappingScrewParameters.DefaultValues[SelfTappingScrewParameters.ParametersName.RodLength].ToString(),
                SelfTappingScrewParameters.DefaultValues[SelfTappingScrewParameters.ParametersName.ThreadStep].ToString(),
                SelfTappingScrewParameters.DefaultValues[SelfTappingScrewParameters.ParametersName.HeadHight].ToString(),
                SelfTappingScrewParameters.DefaultValues[SelfTappingScrewParameters.ParametersName.ThreadLength].ToString(),
                SelfTappingScrewParameters.DefaultValues[SelfTappingScrewParameters.ParametersName.HeadDiameter].ToString(),
                SelfTappingScrewParameters.DefaultValues[SelfTappingScrewParameters.ParametersName.RodDiameter].ToString()
            };
            
            SetParameters(parametersValues, true);
        }

        //TODO: XML комментарии?
        /// <summary>
        /// Событие переключения на radio button с максимальными параметрами
        /// </summary>
        private void maxParametersRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            string[] parametersValues = new string[8]
            {
                SelfTappingScrewParameters.MaxValues[SelfTappingScrewParameters.ParametersName.ThreadDiameter].ToString(),
                SelfTappingScrewParameters.MaxValues[SelfTappingScrewParameters.ParametersName.InternalThreadDiameter].ToString(),
                SelfTappingScrewParameters.MaxValues[SelfTappingScrewParameters.ParametersName.RodLength].ToString(),
                SelfTappingScrewParameters.MaxValues[SelfTappingScrewParameters.ParametersName.ThreadStep].ToString(),
                SelfTappingScrewParameters.MaxValues[SelfTappingScrewParameters.ParametersName.HeadHight].ToString(),
                SelfTappingScrewParameters.MaxValues[SelfTappingScrewParameters.ParametersName.ThreadLength].ToString(),
                SelfTappingScrewParameters.MaxValues[SelfTappingScrewParameters.ParametersName.HeadDiameter].ToString(),
                SelfTappingScrewParameters.MaxValues[SelfTappingScrewParameters.ParametersName.RodDiameter].ToString()
            };
            
            SetParameters(parametersValues, true);
        }

        //TODO: XML комментарии?
        /// <summary>
        /// Событие переключения на radio button с вводом параметров вручную
        /// </summary>
        private void manualInputRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            string[] parametersValues = new string[8] { "", "", "", "", "", "", "", "" };
            SetParameters(parametersValues, false);
        }
    }
}
