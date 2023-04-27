namespace ExamenBeClever
{
    public static class strings
    {
        /// <summary>
        /// Capitaliza la primera letra de un string
        /// </summary>
        /// <param name="word"></param>
        /// <returns>String con mayúscula la 1era letra</returns>
        public static string CapitalizeFirstLetter(string word)
        {
            char letra = char.ToUpper(word[0]);
            word = word.Substring(1);
            string a = Convert.ToString(letra);
            a += word;
            return a;
        }
    }
}
