using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicControls
{
    public partial class Stuff : System.Web.UI.Page
    {
        public int SelectedFilter
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(FilterField.Text))
                {
                    int filter;
                    if (Int32.TryParse(FilterField.Text, out filter))
                    {
                        return filter;
                    }
                }

                return 0;
            }

            set { FilterField.Text = value.ToString(); }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            Debug.WriteLine("Page_Init: IsPostBack:{0}", IsPostBack);
            CreateControls();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Debug.WriteLine("Page_Load: IsPostBack:{0}", IsPostBack);
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Submit_Click");
            Debug.WriteLine($"FilterField: {SelectedFilter}");
            for (var i = 1; i <= 5; i++)
            {
                var textBox = (TextBox) DataPanel.FindControl($"Name-{i}");
                if (textBox == null)
                {
                    continue;
                }
                var name = textBox.Text;
                Debug.WriteLine($"Group {i} - Name:{name}");
            }
        }

        protected void FilterButton_Click(object sender, EventArgs e)
        {
            var button = (Button) sender;

            if (button == null)
            {
                return;
            }

            var filter = button.Attributes["data-filter"];
            SelectedFilter = int.Parse(filter);

            var parentControl = DataPanel.FindControl($"Name-{filter}").Parent;
            parentControl.Visible = !parentControl.Visible;

            Debug.WriteLine($"Filter = {filter}");
        }

        private void CreateControls()
        {
            Debug.WriteLine("CreateControls");

            // Filter Buttons ================================================================================
            var divFilter = new Panel {CssClass = "panel-body"};
            for (var i = 0; i <= 5; i++)
            {
                var filterButton = new Button
                {
                    CssClass = "btn btn-primary btn-sm",
                    Text = i == 0 ? "All" : $"Filter {i}",
                    CausesValidation = true
                };
                filterButton.Attributes["data-filter"] = i.ToString();
                filterButton.Click += new EventHandler(FilterButton_Click);
                divFilter.Controls.Add(filterButton);
            }
            FilterPanel.Controls.Add(divFilter);

            // Data Entry Controls ===========================================================================

            var filter = SelectedFilter;

            // Data groups
            for (var i = 1; i <= 5; i++)
            {
                if (filter > 0 && i != filter)
                {
                    continue;
                }

                var div = new Panel {CssClass = "form-group"};
                DataPanel.Controls.Add(div);
                var label = new Label {Text = $"Group {i}"};
                var textBox = new TextBox {CssClass = "form-control", ID = $"Name-{i}"};
                textBox.Attributes["placeholder"] = "Name";
                div.Controls.Add(label);
                div.Controls.Add(textBox);
            }

            var div2 = new Panel {CssClass = "form-group"};
            var submit = new Button { Text = "Submit", CausesValidation = true, ID = "Submit" };
            submit.Click += new EventHandler(Submit_Click);
            div2.Controls.Add(submit);
            DataPanel.Controls.Add(div2);
        }
    }
}