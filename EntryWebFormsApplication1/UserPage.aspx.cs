using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EntryWebFormsApplication1 {
    public partial class UserPage : System.Web.UI.Page {
        // Declare controls
        protected DropDownList ddlEmployees;
        protected Table tblEntries;
        protected Label lblErrorMessage;
        protected TextBox txtEmployeeId;

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                LoadEmployees();
            }
        }

        private void LoadEmployees() {
            try {
                using (var db = new DataContext()) {
                    var employees = db.Employees.OrderBy(emp => emp.FullName).ToList();
                    ddlEmployees.Items.Clear(); // Clear existing items
                    foreach (var employee in employees) {
                        ddlEmployees.Items.Add(new ListItem(employee.FullName, employee.Id.ToString()));
                    }

                    ddlEmployees.Items.Insert(0, new ListItem("-- Select --", ""));
                }
            }
            catch (Exception ex) {
                lblErrorMessage.Text = "Error loading employees: " + ex.Message;
                lblErrorMessage.Visible = true;
            }
        }

        private void LoadEntries(int employeeId) {
            try {
                using (var db = new DataContext()) {
                    var entries = db.Entries
                                    .Where(entry => entry.EmployeeId == employeeId)
                                    .OrderByDescending(entry => entry.Timestamp)
                                    .ToList();

                    tblEntries.Rows.Clear();
                    TableHeaderRow headerRow = new TableHeaderRow();
                    //headerRow.Cells.Add(new TableHeaderCell { Text = "Entry ID" });
                    headerRow.Cells.Add(new TableHeaderCell { Text = "Event Type" });
                    headerRow.Cells.Add(new TableHeaderCell { Text = "Timestamp" });
                    tblEntries.Rows.Add(headerRow);

                    if (entries.Any()) {
                        foreach (var entry in entries) {
                            TableRow dataRow = new TableRow();
                            //dataRow.Cells.Add(new TableCell { Text = entry.EntryId.ToString() });
                            dataRow.Cells.Add(new TableCell { Text = entry.EventType });
                            dataRow.Cells.Add(new TableCell { Text = entry.Timestamp.ToString() });
                            tblEntries.Rows.Add(dataRow);
                        }
                        lblErrorMessage.Visible = false;
                    }
                    else {
                        lblErrorMessage.Text = "No entries found for the selected employee.";
                        lblErrorMessage.Visible = true;
                    }
                }
            }
            catch (Exception ex) {
                lblErrorMessage.Text = "Error loading entries: " + ex.Message;
                lblErrorMessage.Visible = true;
            }
        }

        protected void ddlEmployees_SelectedIndexChanged(object sender, EventArgs e) {
            if (int.TryParse(ddlEmployees.SelectedValue, out int employeeId)) {
                LoadEntries(employeeId);
            }
            else {
                tblEntries.Rows.Clear();
                TableHeaderRow headerRow = new TableHeaderRow();
                //headerRow.Cells.Add(new TableHeaderCell { Text = "Entry ID" });
                headerRow.Cells.Add(new TableHeaderCell { Text = "Event Type" });
                headerRow.Cells.Add(new TableHeaderCell { Text = "Timestamp" });
                tblEntries.Rows.Add(headerRow);
                lblErrorMessage.Visible = false;
            }
        }

        protected void btnSearchById_Click(object sender, EventArgs e) {
            if (int.TryParse(txtEmployeeId.Text, out int employeeId)) {
                LoadEntries(employeeId);
            }
            else {
                lblErrorMessage.Text = "Please enter a valid Employee ID.";
                lblErrorMessage.Visible = true;
                tblEntries.Rows.Clear();
                TableHeaderRow headerRow = new TableHeaderRow();
                //headerRow.Cells.Add(new TableHeaderCell { Text = "Entry ID" });
                headerRow.Cells.Add(new TableHeaderCell { Text = "Event Type" });
                headerRow.Cells.Add(new TableHeaderCell { Text = "Timestamp" });
                tblEntries.Rows.Add(headerRow);
            }
        }
    }
}
