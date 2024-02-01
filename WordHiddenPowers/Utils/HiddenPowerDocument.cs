using System;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Utils
{
    public static class HiddenPowerDocument
    {
        public static Word.Variable GetVariable(Word.Variables array, string variableName)
        {
            for (int i = 1; i <= array.Count; i++)
            {
                if (array[i].Name.Equals(variableName, StringComparison.CurrentCultureIgnoreCase))
                    return array[i];
            }
            return null;
        }
    }
}
