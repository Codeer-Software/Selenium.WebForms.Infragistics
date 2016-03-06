using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InfragisticsWebFormsSample
{
    public partial class _Default : Page
    {
        List<Person> _Person = new List<Person>();

        List<Supplier> _Supplier = new List<Supplier>();
        List<Products> _Products = new List<Products>();
        List<Products> _Products2 = new List<Products>();

        protected void Page_Load(object sender, EventArgs e)
        {
            _Person.Add(new Person() { Id = 1, LastName = "松井", LastLoginDate = DateTime.Now, Support = true });
            _Person.Add(new Person() { Id = 2, LastName = "石川", LastLoginDate = DateTime.Now, Support = false });
            _Person.Add(new Person() { Id = 3, LastName = "鈴木", LastLoginDate = DateTime.Now, Support = true });
            _webDataGrid.DataSource = _Person;

            //_webDataGridTextBoxProvider.EditorControl.TextMode = TextBoxMode.Date;
        }
    }
}