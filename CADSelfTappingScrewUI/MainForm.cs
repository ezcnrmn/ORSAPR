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
        //private KompasWrapper.Kompas3DWrapper _kompas = new KompasWrapper.Kompas3DWrapper();
        
        //private SelfTappingScrewParameters _selfTappingScrew;

        Dictionary<string, bool> buildEnableDict = new Dictionary<string, bool>(8);

        public MainForm()
        {
            InitializeComponent();

            buildEnableDict.Add(threadDiameterTextBox.Name, false);
            buildEnableDict.Add(internalThreadDiameterTextBox.Name, false);
            buildEnableDict.Add(rodLengthTextBox.Name, false);
            buildEnableDict.Add(threadStepTextBox.Name, false);
            buildEnableDict.Add(headHightTextBox.Name, false);
            buildEnableDict.Add(threadLengthTextBox.Name, false);
            buildEnableDict.Add(headDiameterTextBox.Name, false);
            buildEnableDict.Add(rodDiameterTextBox.Name, false);

            //тестовые данные
            threadDiameterTextBox.Text = "8";
            internalThreadDiameterTextBox.Text = "7";
            rodLengthTextBox.Text = "90";
            threadStepTextBox.Text = "3,1";
            headHightTextBox.Text = "5";
            threadLengthTextBox.Text = "63";
            headDiameterTextBox.Text = "14";
            rodDiameterTextBox.Text = "9,95";
        }

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

                    string errorText = "Значение в текстовом поле должно быть числом!\nДесятичный разделитель - только \"запятая\"!";
                    MessageBox.Show(
                        errorText,
                        "Внимание!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
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
                    selfTappingScrewParameters.InternalThreadDiameter = double.Parse(internalThreadDiameterTextBox.Text);

                    Kompas3DWrapper kompas3DWrapper = new Kompas3DWrapper();
                    kompas3DWrapper.OpenKompas();
                    
                    SelfTappingScrewBuilder selfTappingScrewBuilder = new SelfTappingScrewBuilder();
                    selfTappingScrewBuilder.BuildSelfTappingScrew(kompas3DWrapper, selfTappingScrewParameters);
                    
                }
                catch (ArgumentException a)
                {
                    MessageBox.Show(
                        a.Message,
                        "Внимание!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                string message = "Необходимо заполнить все поля!";
                MessageBox.Show(
                    message,
                    "Внимание!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            //_kompClass1.OpenKompas();

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
    }
}
