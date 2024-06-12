namespace NTDLS.WinFormsHelpers
{
    /// <summary>
    /// Various functions for getting and validating values from WinForms textbox controls.
    /// </summary>
    public static class TextBoxValidationExtensions
    {
        /// <summary>
        /// Gets an integer value from a windows textbox. Ensures that the value falls within the given ranges.
        /// </summary>
        public static int GetAndValidate(this TextBox textBox, int minValue, int maxValue, string message)
        {
            message = message.Replace("[min]", $"{minValue:n0}", StringComparison.InvariantCultureIgnoreCase);
            message = message.Replace("[max]", $"{maxValue:n0}", StringComparison.InvariantCultureIgnoreCase);

            if (int.TryParse(textBox.Text.Replace(",", ""), out var value))
            {
                if (value < minValue || value > maxValue)
                {
                    throw new Exception(message);
                }
                return value;
            }
            throw new Exception(message);
        }


        /// <summary>
        /// Gets a double floating value from a windows textbox. Ensures that the value falls within the given ranges.
        /// </summary>
        public static double GetAndValidate(this TextBox textBox, double minValue, double maxValue, string message)
        {
            message = message.Replace("[min]", $"{minValue:n0}", StringComparison.InvariantCultureIgnoreCase);
            message = message.Replace("[max]", $"{maxValue:n0}", StringComparison.InvariantCultureIgnoreCase);

            if (double.TryParse(textBox.Text.Replace(",", ""), out var value))
            {
                if (value < minValue || value > maxValue)
                {
                    throw new Exception(message);
                }
                return value;
            }
            throw new Exception(message);
        }

        /// <summary>
        /// Gets a float floating value from a windows textbox. Ensures that the value falls within the given ranges.
        /// </summary>
        public static float GetAndValidate(this TextBox textBox, float minValue, float maxValue, string message)
        {
            message = message.Replace("[min]", $"{minValue:n0}", StringComparison.InvariantCultureIgnoreCase);
            message = message.Replace("[max]", $"{maxValue:n0}", StringComparison.InvariantCultureIgnoreCase);

            if (float.TryParse(textBox.Text.Replace(",", ""), out var value))
            {
                if (value < minValue || value > maxValue)
                {
                    throw new Exception(message);
                }
                return value;
            }
            throw new Exception(message);
        }
    }
}
