using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordHiddenPowers.Repositoryes;

namespace WordHiddenPowers.Controls
{
    public partial class CategoriesListBox : UserControl
    {

        private RepositoryDataSet source;
        private Data.Table table;
        
        public RepositoryDataSet PowersDataSet
        {
            get
            {
                return source;
            }

            set
            {
                source = value;
                ReadData();
            }
        }

        public CategoriesListBox()
        {
            InitializeComponent();
        }

        public void ReadData()
        {
            listBox1.Items.Clear();
            if (source == null) return;

            foreach (DataRow item in source.StringPowers.Rows)
            {
                listBox1.Items.Add(item["Value"]);
            }
        }

    }
}
