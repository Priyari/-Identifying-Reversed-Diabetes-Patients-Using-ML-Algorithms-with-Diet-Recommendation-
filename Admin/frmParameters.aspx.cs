using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace diseasePrediction.Admin
{
    public partial class frmParameters : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminId"] == null)
            {
                Session.Abandon();
                Response.Redirect("~/frmLogin.aspx");
            }
            else
            {
                if (!this.IsPostBack)

                    txtType.Focus();

                LoadParameters();
            }     
        }

        //function to load all parameters
        private void LoadParameters()
        {
            try
            {
                DataTable tab = new DataTable();
                BLL obj = new BLL();

                int serialNo = 1;

                tab = obj.GetAllParameters();

                if (tab.Rows.Count > 0)
                {
                    tableParameters.Rows.Clear();

                    tableParameters.BorderStyle = BorderStyle.Double;
                    tableParameters.GridLines = GridLines.Both;
                    tableParameters.BorderColor = System.Drawing.Color.DarkGray;

                    TableRow mainrow = new TableRow();
                    mainrow.Height = 30;
                    mainrow.ForeColor = System.Drawing.Color.WhiteSmoke;

                    mainrow.BackColor = System.Drawing.Color.DarkOrange;

                    TableCell cell1 = new TableCell();
                    cell1.Text = "<b>SerialNo</b>";
                    mainrow.Controls.Add(cell1);

                    TableCell cell2 = new TableCell();
                    cell2.Text = "<b>Parameter</b>";
                    mainrow.Controls.Add(cell2);

                    //TableCell cell3 = new TableCell();
                    //cell3.Text = "<b>Edit</b>";
                    //mainrow.Controls.Add(cell3);

                    //TableCell cell4 = new TableCell();
                    //cell4.Text = "<b>Delete</b>";
                    //mainrow.Controls.Add(cell4);

                    tableParameters.Controls.Add(mainrow);

                    for (int i = 0; i < tab.Rows.Count; i++)
                    {
                        TableRow row = new TableRow();

                        TableCell cellSerialNo = new TableCell();
                        cellSerialNo.Width = 50;
                        cellSerialNo.Text = serialNo + i + ".";
                        row.Controls.Add(cellSerialNo);

                        TableCell cellType = new TableCell();
                        cellType.Width = 250;
                        cellType.Text = tab.Rows[i]["Parameter"].ToString();
                        row.Controls.Add(cellType);

                        //TableCell cell_edit = new TableCell();
                        //LinkButton lbtn_edit = new LinkButton();
                        //lbtn_edit.ForeColor = System.Drawing.Color.SteelBlue;
                        //lbtn_edit.Font.Bold = true;
                        //lbtn_edit.Text = "Edit";
                        //lbtn_edit.ID = "Edit~" + tab.Rows[i]["ParameterId"].ToString();
                        //lbtn_edit.Click += new EventHandler(lbtn_edit_Click);
                        //cell_edit.Controls.Add(lbtn_edit);
                        //row.Controls.Add(cell_edit);

                        //TableCell cell_delete = new TableCell();
                        //LinkButton lbtn_delete = new LinkButton();
                        //lbtn_delete.ForeColor = System.Drawing.Color.SteelBlue;
                        //lbtn_delete.Text = "Delete";
                        //lbtn_delete.Font.Bold = true;
                        //lbtn_delete.ID = "Delete~" + tab.Rows[i]["ParameterId"].ToString();
                        //lbtn_delete.OnClientClick = "return confirm('Are you sure want to delete this record?')";
                        //lbtn_delete.Click += new EventHandler(lbtn_delete_Click);
                        //cell_delete.Controls.Add(lbtn_delete);
                        //row.Controls.Add(cell_delete);

                        tableParameters.Controls.Add(row);
                    }
                }
                else
                {
                    tableParameters.Rows.Clear();

                    tableParameters.BorderStyle = BorderStyle.None;
                    tableParameters.GridLines = GridLines.None;

                    TableHeaderRow rno = new TableHeaderRow();
                    TableHeaderCell cellno = new TableHeaderCell();
                    cellno.ForeColor = System.Drawing.Color.Red;
                    cellno.Text = "No Parameters Found!!!";

                    rno.Controls.Add(cellno);
                    tableParameters.Controls.Add(rno);
                }
            }
            catch
            {

            }
        }

        //event to delete parameter
        void lbtn_delete_Click(object sender, EventArgs e)
        {
            BLL obj = new BLL();
            LinkButton lbtn = (LinkButton)sender;
            string[] s = lbtn.ID.ToString().Split('~');

            try
            {
                obj.DeleteParameter(int.Parse(s[1].ToString()));

                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('Parameter Deleted Successfully!!!')</script>");
                LoadParameters();
                txtType.Text = string.Empty;
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('Server Error!!!')</script>");
            }
        }

        //click event to edit the parameter
        void lbtn_edit_Click(object sender, EventArgs e)
        {
            try
            {
                BLL obj = new BLL();
                LinkButton lbtn = (LinkButton)sender;
                string[] s = lbtn.ID.ToString().Split('~');

                Session["typeId"] = null;
                Session["typeId"] = s[1];

                DataTable tab = new DataTable();
                tab = obj.GetParameterById(int.Parse(s[1].ToString()));

                if (tab.Rows.Count > 0)
                {
                    Session["type"] = null;
                    Session["type"] = tab.Rows[0]["Parameter"].ToString();

                    txtType.Text = tab.Rows[0]["ParameterId"].ToString();
                }

                btnSubmit.Text = "Update";
            }
            catch
            {

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BLL obj = new BLL();

            if (btnSubmit.Text.Equals("Submit"))
            {
                try
                {
                    if (obj.CheckParameter(txtType.Text))
                    {
                        obj.InsertParameter(txtType.Text);

                        ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('New Parameter Added Successfully!!')</script>");
                        txtType.Text = string.Empty;
                        LoadParameters();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('Parameter Exists!!!')</script>");
                    }
                }
                catch
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('Server Error!')</script>");
                }
            }
            else if (btnSubmit.Text.Equals("Update"))
            {
                if (txtType.Text.Equals(Session["type"].ToString()))
                {
                    try
                    {
                        obj.UpdateParameter(txtType.Text, int.Parse(Session["typeId"].ToString()));

                        ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('Parameter Updated Successfully!!')</script>");
                        txtType.Text = string.Empty;
                        LoadParameters();

                        btnSubmit.Text = "Submit";
                    }
                    catch
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('Server Error!')</script>");
                    }
                }
                else
                {
                    if (obj.CheckParameter(txtType.Text))
                    {
                        try
                        {
                            obj.UpdateParameter(txtType.Text, int.Parse(Session["typeId"].ToString()));

                            ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('Parameter Updated Successfully!!')</script>");
                            txtType.Text = string.Empty;
                            LoadParameters();

                            btnSubmit.Text = "Submit";
                        }
                        catch
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('Server Error!')</script>");
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('Parameter Exists!!')</script>");
                    }
                }
            }
        }

    }
}