using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InfragisticsWebFormsSample
{
    public partial class _Default : Page
    {
        List<Person> _Person = new List<Person>();
        protected void Page_Load(object sender, EventArgs e)
        {
            _Person.Add(new Person() { Id = 1, LastName = "Matsui", LastLoginDate = DateTime.Now, Support = true });
            _Person.Add(new Person() { Id = 2, LastName = "Ishikawa", LastLoginDate = DateTime.Now, Support = false });
            _Person.Add(new Person() { Id = 3, LastName = "Suzuki", LastLoginDate = DateTime.Now, Support = true });
            _webDataGrid.DataSource = _Person;
        }
    }
}