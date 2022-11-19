namespace QLSV_3layers
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            var db = new Database();
            //dgvData.DataSource = db.SelectData(null);
        }
    }
}