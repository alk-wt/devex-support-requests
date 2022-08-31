using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;


namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
        private DataColumn _KeyField;
        private DataTable _Tbl;
        private MyCache _Cache = new MyCache("ID");
        private DataTable CreateTable(int RowCount)
        {
            _Tbl = new DataTable();
            _KeyField = _Tbl.Columns.Add("ID", typeof(int));
            _KeyField.ReadOnly = true;
            _KeyField.AutoIncrement = true;
            _Tbl.Columns.Add("Name", typeof(string));
            _Tbl.Columns.Add("Number", typeof(int));
            _Tbl.Columns.Add("Date", typeof(DateTime));
            for (int i = 0; i < RowCount; i++)
                _Tbl.Rows.Add(new object[] { null, String.Format("Name{0}", i), 3 - i, DateTime.Now.AddDays(i) });
            return _Tbl;
        }

        public Form1()
        {
            InitializeComponent();
            gridControl1.DataSource = CreateTable(20);
            CreateUnboundColumn();
            
            gridView1.ShowCustomizationForm += (sender, args) =>
            {
                var columnChooserForm = ((GridView)sender).CustomizationForm; 
                
                columnChooserForm.Appearance.BorderColor = Color.Crimson;  // THIS DOESN'T WORK!!!
                columnChooserForm.Appearance.Options.UseBorderColor = true;
            };
        }
        private void CreateUnboundColumn()
        {
            GridColumn col = gridView1.Columns.AddVisible("Unbound", "Unbound column");
            col.UnboundType = DevExpress.Data.UnboundColumnType.String;
        }



        private void gridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
                e.Value = _Cache.GetValue(e.Row);
            if (e.IsSetData)
                _Cache.SetValue(e.Row, e.Value);
        }
    }
}