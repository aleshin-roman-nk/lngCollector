using lngCollector.Model;
using lngCollector.Services;
using lngCollector.Services.sqliteDb;

namespace lngCollector.WinForm
{
    public partial class Form1 : Form, IMainView
    {
        IEWordRepo repo = new EWordRepo(new AppDataDbFactory(new DbConfigSQLite()));
        BindingSource bindingSource;

        EWord? currentWord => bindingSource.Current as EWord;

        public Form1()
        {
            InitializeComponent();

            bindingSource = new BindingSource();
            dataGridView1.DataSource = bindingSource;
            bindingSource.CurrentItemChanged += BindingSource_CurrentItemChanged;
            dataGridView1.AutoGenerateColumns = false;
        }

        public event EventHandler<EWord> SaveWord;
        public event EventHandler<EWord> DeleteWord;

        public void Display(IEnumerable<EWord> c)
        {
            bindingSource.DataSource = null;
            bindingSource.DataSource = c;
        }

        private void BindingSource_CurrentItemChanged(object? sender, EventArgs e)
        {
            _set(currentWord);
        }

        EWord _get()
        {
            // todo: collect all the fields into currentWord.

            currentWord.description = rtxtDescription.Text;
            currentWord.text = txtText.Text;

            return currentWord;
        }

        void _set(EWord w)
        {
            if(w == null) return;

            txtText.Text = w.text;
            rtxtDescription.Text = w.description;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveWord?.Invoke(this, _get());
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            EnterWordForm f = new EnterWordForm(new EWord { lng_type = LngType.eng });
            if(f.ShowDialog() == DialogResult.OK)
                SaveWord?.Invoke(this, f.Word);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}