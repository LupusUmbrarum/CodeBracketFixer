using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeBracketFixer
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Used to temporarily store changes to the text in textBox1
        /// </summary>
        private List<string> newLines;

        /// <summary>
        /// Used to find the appropriate index while fixing/screwing up code
        /// </summary>
        int count;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Searches a string for the last empty space, then returns the appropriate indentation as a string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string getIndentationSpace(string str)
        {
            string amt = "";

            for (int x = 0; x < str.Length; x++)
            {
                if (str[x] == ' ')
                {
                    amt += " ";
                }
                else
                {
                    break;
                }
            }
            return amt;
        }

        /// <summary>
        /// Finds all lines of code that end with an open curly bracket '{', and moves the curly bracket down one line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fixButton_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < textBox1.Lines.Length; x++)
            {
                try
                {
                    if (textBox1.Lines[x].Last() == '{' && !String.IsNullOrWhiteSpace(textBox1.Lines[x].Substring(0, textBox1.Lines[x].Length - 1)))
                    {
                        newLines = new List<string>();

                        count = 0;

                        while (count < x)
                        {
                            newLines.Add(textBox1.Lines[count++]);
                        }

                        newLines.Add(textBox1.Lines[count].Substring(0, textBox1.Lines[count].Length - 1));

                        newLines.Add(getIndentationSpace(textBox1.Lines[count]) + "{");

                        while (++count < textBox1.Lines.Length)
                        {
                            newLines.Add(textBox1.Lines[count]);
                        }
                        textBox1.Lines = newLines.ToArray();
                    }
                }
                catch (Exception ex) { }
            }
            newLines = null;
            GC.Collect();
        }

        /// <summary>
        /// Finds all lines that contain only open curly brackets '{', and puts the curly bracket at the end of the line above it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void screwButton_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < textBox1.Lines.Length; x++)
            {
                try
                {
                    if (textBox1.Lines[x].Last() == '{' && String.IsNullOrWhiteSpace(textBox1.Lines[x].Substring(0, textBox1.Lines[x].Length - 1)))
                    {
                        newLines = new List<string>();

                        int count = 0;

                        while (count < x)
                        {
                            newLines.Add(textBox1.Lines[count++]);
                        }

                        newLines[count - 1] += "{";

                        while (++count < textBox1.Lines.Length)
                        {
                            newLines.Add(textBox1.Lines[count]);
                        }

                        textBox1.Lines = newLines.ToArray();
                    }
                }
                catch (Exception ex) { }
            }
            newLines = null;
            GC.Collect();
        }

        /// <summary>
        /// Attempts to paste the contents of the clipboard to textBox1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyButton_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(textBox1.Text);
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// Attempts to copy the contents of textBox1 to the clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pasteButton_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = Clipboard.GetText();
            }
            catch (Exception ex) { }
        }
    }
}
