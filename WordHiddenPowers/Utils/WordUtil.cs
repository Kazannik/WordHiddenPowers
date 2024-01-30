using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Utils
{
    public static class WordUtil
    {


        public static void AddField(Word.Document document,  Word.Selection selection, int categoryIndex)
        {
            string name = "WORD_HIDDEN_" + categoryIndex.ToString("000");

            document.Variables.Add(name, 1000);

            //    Dim Selection As  = app.Selection
            //Dim VarName As String = "r" & Format(DataGridView1.SelectedCells.Item(0).RowIndex, "000") & "c" & Format(DataGridView1.SelectedCells.Item(0).ColumnIndex, "000")
           Word.Field f = selection.Fields.Add(Range: selection.Range, Type: Word.WdFieldType.wdFieldEmpty, Text: "DOCVARIABLE  " + name, PreserveFormatting: true);

            document.Fields.Update();

        }

    }
}
