using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace diseasePrediction.Doctor
{
    public partial class frmTreatment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["StaffId"] == null)
            {
                Session.Abandon();
                Response.Redirect("~/frmLogin.aspx");
            }
            else
            {

                if (!this.IsPostBack)
                {
                    GetPatientIds();
                    txtTreatment.Focus();
                }

                LoadTreatment();
            }
        }

        private void GetPatientIds()
        {
            BLL obj = new BLL();
            DataTable tab = new DataTable();

            tab = obj.GetAllNewPatients();

            if (tab.Rows.Count > 0)
            {
                DropDownListPatients.Items.Clear();
                DropDownListPatients.DataSource = tab;
                DropDownListPatients.DataTextField = "Name";
                DropDownListPatients.DataValueField = "PatientId";

                DropDownListPatients.DataBind();               
            }
        }
       
        //function to display treatment details
        private void LoadTreatment()
        {
            DataTable tab = new DataTable();
            BLL obj = new BLL();

            tab = obj.GetTreatmentByPatientId(int.Parse(DropDownListPatients.SelectedValue));

            if (tab.Rows.Count > 0)
            {
                int serialNO = 1;

                tableTreatment.Rows.Clear();

                tableTreatment.BorderStyle = BorderStyle.Double;
                tableTreatment.GridLines = GridLines.Both;
                tableTreatment.BorderColor = System.Drawing.Color.DarkGray;

                TableRow mainrow = new TableRow();
                mainrow.Height = 30;
                mainrow.ForeColor = System.Drawing.Color.WhiteSmoke;

                mainrow.BackColor = System.Drawing.Color.SteelBlue;

                TableCell cell1 = new TableCell();
                cell1.Text = "<b>SL No</b>";
                mainrow.Controls.Add(cell1);

                TableCell cell2 = new TableCell();
                cell2.Text = "<b>Patient Name</b>";
                mainrow.Controls.Add(cell2);

                TableCell cell3 = new TableCell();
                cell3.Text = "<b>Treatment Details</b>";
                mainrow.Controls.Add(cell3);

                TableCell cell4 = new TableCell();
                cell4.Text = "<b>LasteUpdated</b>";
                mainrow.Controls.Add(cell4);

               
                    TableCell cell11 = new TableCell();
                    cell11.Text = "<b>Edit</b>";
                    mainrow.Controls.Add(cell11);

                    TableCell cell12 = new TableCell();
                    cell12.Text = "<b>Delete</b>";
                    mainrow.Controls.Add(cell12);
                

                tableTreatment.Controls.Add(mainrow);

                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    TableRow row = new TableRow();

                    TableCell cellSerialNo = new TableCell();
                    cellSerialNo.Text = i + serialNO + ".";
                    row.Controls.Add(cellSerialNo);

                    TableCell cellDisease = new TableCell();
                    cellDisease.Width = 150;

                    DataTable tabPatient = new DataTable();
                    tabPatient = obj.GetNewPatientById(int.Parse(tab.Rows[i]["PatientId"].ToString()));

                    cellDisease.Text = tabPatient.Rows[0]["Name"].ToString();
                    row.Controls.Add(cellDisease);

                    TableCell cellTreatment = new TableCell();
                    cellTreatment.Width = 450;
                    cellTreatment.Text = tab.Rows[i]["TreatmentDetails"].ToString();
                    row.Controls.Add(cellTreatment);
                                                                               
                    TableCell cellDate = new TableCell();
                    cellDate.Width = 100;
                    string[] s = tab.Rows[i]["LastUpdated"].ToString().Split(' ');
                    cellDate.Text = s[0];
                    row.Controls.Add(cellDate);

                    TableCell cell_edit = new TableCell();
                    LinkButton lbtn_edit = new LinkButton();
                    lbtn_edit.ForeColor = System.Drawing.Color.Red;
                    lbtn_edit.Text = "Edit";
                    lbtn_edit.Font.Bold = true;
                    lbtn_edit.ID = "Edit~" + tab.Rows[i]["TreatmentId"].ToString();
                    lbtn_edit.Click += new EventHandler(lbtn_edit_Click);
                    cell_edit.Controls.Add(lbtn_edit);
                    row.Controls.Add(cell_edit);

                    TableCell cell_delete = new TableCell();
                    LinkButton lbtn_delete = new LinkButton();
                    lbtn_delete.ForeColor = System.Drawing.Color.Red;
                    lbtn_delete.Text = "Delete";
                    lbtn_delete.Font.Bold = true;
                    lbtn_delete.ID = "Delete~" + tab.Rows[i]["TreatmentId"].ToString();
                    lbtn_delete.OnClientClick = "return confirm('Are you sure want to delete this record?')";
                    lbtn_delete.Click += new EventHandler(lbtn_delete_Click);
                    cell_delete.Controls.Add(lbtn_delete);
                    row.Controls.Add(cell_delete);

                    tableTreatment.Controls.Add(row);
                }
            }
            else
            {
                tableTreatment.Rows.Clear();
                tableTreatment.GridLines = GridLines.None;
                tableTreatment.BorderStyle = BorderStyle.None;

                TableHeaderRow rno = new TableHeaderRow();
                TableHeaderCell cellno = new TableHeaderCell();
                cellno.ForeColor = System.Drawing.Color.Red;
                cellno.Text = "No Treatment Details Found!!!";

                rno.Controls.Add(cellno);
                tableTreatment.Controls.Add(rno);
            }
        }

        //event to delete treatment
        void lbtn_delete_Click(object sender, EventArgs e)
        {
            BLL obj = new BLL();
            LinkButton lbtn = (LinkButton)sender;
            string[] s = lbtn.ID.ToString().Split('~');

            try
            {
                obj.DeleteTreatment(int.Parse(s[1].ToString()));

                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('Treatment Details Deleted Successfully')</script>");
                LoadTreatment();

            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('Server Error!!!')</script>");
            }
        }

        //click event to edit the patient details
        void lbtn_edit_Click(object sender, EventArgs e)
        {
            BLL obj = new BLL();
            LinkButton lbtn = (LinkButton)sender;
            string[] s = lbtn.ID.ToString().Split('~');

            Session["treatmentId"] = null;
            Session["treatmentId"] = s[1];

            DataTable tab = new DataTable();
            tab = obj.GetTreatmentByid(int.Parse(s[1]));

            if (tab.Rows.Count > 0)
            {
                txtTreatment.Text = tab.Rows[0]["TreatmentDetails"].ToString();

                string dataTextField = DropDownListPatients.Items.FindByValue(tab.Rows[0]["PatientId"].ToString()).ToString();

                ListItem item = new ListItem(dataTextField, tab.Rows[0]["PatientId"].ToString());
                int index = DropDownListPatients.Items.IndexOf(item);

                if (index != -1)

                    DropDownListPatients.SelectedIndex = index;           
            
            }

            btnSubmit.Text = "Update";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BLL obj = new BLL();

                if (btnSubmit.Text.Equals("Submit"))
                {
                    obj.InsertTreatment(int.Parse(DropDownListPatients.SelectedValue), txtTreatment.Text, DateTime.Now);
                    ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('New Treatment Details Added Successfully!!!')</script>");
                    txtTreatment.Text = string.Empty;
                    LoadTreatment();
                }
                else if (btnSubmit.Text.Equals("Update"))
                {
                    obj.UpdateTreatment(int.Parse(DropDownListPatients.SelectedValue), txtTreatment.Text, DateTime.Now, int.Parse(Session["treatmentId"].ToString()));

                    btnSubmit.Text = "Submit";
                    txtTreatment.Text = string.Empty;
                    LoadTreatment();
                    ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('Treatment Updated Successfully!!!')</script>");
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('Server Error!')</script>");
            }
        }

        protected void DropDownListPatients_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTreatment();
        }
    }
}