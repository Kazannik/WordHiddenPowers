using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Panes
{
    class NotesPane : WordHiddenPowersPane
    {
        private NotesPane(): base() { }

        public NotesPane(Document Doc) : base(Doc)
        {
        }
    }
}
