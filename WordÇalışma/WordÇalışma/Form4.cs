using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WordÇalışma
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sözlükDataSet1.Test' table. You can move, or remove it, as needed.
            //this.testTableAdapter.Fill(this.sözlükDataSet1.Test);

        }
        SqlConnection baglanti = new SqlConnection("Data Source = DESKTOP-070QPQR; Initial Catalog = Sözlük; User ID = sa; Password=63405EMre");
        private void Button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("Select Date,SessionID,SUM(_TrueCount) as _TrueCount,SUM(_FalseCount) as _FalseCount "+
                                                   "FROM(" +
                                                   "Select Date, SessionID, Count(*) as _TrueCount, 0 as _FalseCount " +
                                                   "From Test " +
                                                   "Where Answer = 'D' GROUP BY Date, SessionID " +
                                                   "UNION ALL " +
                                                   "Select Date, SessionID, 0 as _TrueCount, Count(*) as _FalseCount " +
                                                   "From Test " +
                                                   "Where Answer = 'Y' GROUP BY Date, SessionID " +
                                                   ") tt GROUP BY Date, SessionID ORDER BY Date, SessionID", baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
    }
}
