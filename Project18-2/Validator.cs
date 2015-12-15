using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project18_2
{
    public static class Validator
    {
        //This is a static class used for validation on user entries
        private static string title = "Entry Error";

        public static string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        //validation for presence
        public static bool IsPresent(TextBox textbox)
        {
            if (textbox.Text == "")
            {
                MessageBox.Show(textbox.Tag + " is a required field.", Title);
                return false;
            }
            return true;
        }

        //validation for decimal data type
        public static bool IsDecimal(TextBox textbox)
        {
            decimal number = 0m;
            if (Decimal.TryParse(textbox.Text, out number))
            {
                return true;
            }
            else
            {
                MessageBox.Show(textbox.Tag + " must be a decimal value.", "Entry Error");
                return false;
            }
        }

        //validation for correct length of product code
        public static bool IsCorrectLength(TextBox textbox)
        {
            string comparison = textbox.Text;
            if (comparison.Length > 10)
            {
                MessageBox.Show(textbox.Tag + " cannot be more than 10 characters.", "Entry Error");
                return false;
            }
            return true;
        }
    }
}
