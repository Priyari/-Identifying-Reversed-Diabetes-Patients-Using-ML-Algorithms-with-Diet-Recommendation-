using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace diseasePrediction.Admin
{
    public partial class frmAdminHome : System.Web.UI.Page
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

                    txtUserId.Focus();

                LoadStaffs();
            }
        }

        //function to load all staffs
        private void LoadStaffs()
        {
            try
            {
                DataTable tab = new DataTable();
                BLL obj = new BLL();

                int serialNo = 1;

                tab = obj.GetOtherUsers();

                if (tab.Rows.Count > 0)
                {
                    tableStaffs.Rows.Clear();

                    tableStaffs.BorderStyle = BorderStyle.Double;
                    tableStaffs.GridLines = GridLines.Both;
                    tableStaffs.BorderColor = System.Drawing.Color.DarkGray;

                    TableRow mainrow = new TableRow();
                    mainrow.Height = 30;
                    mainrow.ForeColor = System.Drawing.Color.WhiteSmoke;

                    mainrow.BackColor = System.Drawing.Color.DarkOrange;

                    TableCell cell1 = new TableCell();
                    cell1.Text = "<b>SerialNo</b>";
                    mainrow.Controls.Add(cell1);

                    TableCell cell21 = new TableCell();
                    cell21.Text = "<b>UserType</b>";
                    mainrow.Controls.Add(cell21);

                    TableCell cell2 = new TableCell();
                    cell2.Text = "<b>Staff Id</b>";
                    mainrow.Controls.Add(cell2);

                    TableCell cell3 = new TableCell();
                    cell3.Text = "<b>Password</b>";
                    mainrow.Controls.Add(cell3);

                    TableCell cell4 = new TableCell();
                    cell4.Text = "<b>EmailId</b>";
                    mainrow.Controls.Add(cell4);

                    TableCell cell5 = new TableCell();
                    cell5.Text = "<b>Edit</b>";
                    mainrow.Controls.Add(cell5);

                    TableCell cell6 = new TableCell();
                    cell6.Text = "<b>Delete</b>";
                    mainrow.Controls.Add(cell6);

                    tableStaffs.Controls.Add(mainrow);

                    for (int i = 0; i < tab.Rows.Count; i++)
                    {
                        TableRow row = new TableRow();

                        TableCell cellSerialNo = new TableCell();
                        cellSerialNo.Width = 50;
                        cellSerialNo.Text = serialNo + i + ".";
                        row.Controls.Add(cellSerialNo);

                        TableCell cellType = new TableCell();
                        cellType.Text = tab.Rows[i]["UserType"].ToString();
                        row.Controls.Add(cellType);

                        TableCell cellUserId = new TableCell();
                        cellUserId.Width = 150;
                        cellUserId.Text = tab.Rows[i]["UserId"].ToString();
                        row.Controls.Add(cellUserId);

                        TableCell cellPassword = new TableCell();
                        cellPassword.Width = 150;
                        cellPassword.Text = tab.Rows[i]["Password"].ToString();
                        row.Controls.Add(cellPassword);

                        TableCell cellEmailId = new TableCell();
                        cellEmailId.Width = 200;
                        cellEmailId.Text = tab.Rows[i]["EmailId"].ToString();
                        row.Controls.Add(cellEmailId);

                        TableCell cell_edit = new TableCell();
                        LinkButton lbtn_edit = new LinkButton();
                        lbtn_edit.ForeColor = System.Drawing.Color.SteelBlue;
                        lbtn_edit.Font.Bold = true;
                        lbtn_edit.Text = "Edit";
                        lbtn_edit.ID = "Edit~" + tab.Rows[i]["UserId"].ToString();
                        lbtn_edit.Click += new EventHandler(lbtn_edit_Click);
                        cell_edit.Controls.Add(lbtn_edit);
                        row.Controls.Add(cell_edit);

                        TableCell cell_delete = new TableCell();
                        LinkButton lbtn_delete = new LinkButton();
                        lbtn_delete.ForeColor = System.Drawing.Color.SteelBlue;
                        lbtn_delete.Font.Bold = true;
                        lbtn_delete.Text = "Delete";

                        lbtn_delete.ID = "Delete~" + tab.Rows[i]["UserId"].ToString();
                        lbtn_delete.OnClientClick = "return confirm('Are you sure want to delete this record?')";
                        lbtn_delete.Click += new EventHandler(lbtn_delete_Click);
                        cell_delete.Controls.Add(lbtn_delete);
                        row.Controls.Add(cell_delete);

                        tableStaffs.Controls.Add(row);
                    }
                }
                else
                {
                    tableStaffs.Rows.Clear();

                    TableHeaderRow rno = new TableHeaderRow();
                    TableHeaderCell cellno = new TableHeaderCell();
                    cellno.ForeColor = System.Drawing.Color.Red;
                    cellno.Text = "No Staffs Found";

                    rno.Controls.Add(cellno);
                    tableStaffs.Controls.Add(rno);
                }
            }
            catch
            {

            }
        }

        //event to delete staff
        void lbtn_delete_Click(object sender, EventArgs e)
        {
            BLL obj = new BLL();
            LinkButton lbtn = (LinkButton)sender;
            string[] s = lbtn.ID.ToString().Split('~');

            try
            {
                obj.DeleteUser(s[1]);

                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('Staff Deleted Successfully')</script>");
                LoadStaffs();

            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('Server Error!!!')</script>");
            }
        }

        //click event to edit the staff details
        void lbtn_edit_Click(object sender, EventArgs e)
        {
            try
            {
                BLL obj = new BLL();

                LinkButton lbtn = (LinkButton)sender;
                string[] s = lbtn.ID.ToString().Split('~');

                Session["userId"] = null;
                Session["userId"] = s[1];

                DataTable tab = new DataTable();
                tab = obj.GetUserById(s[1]);

                if (tab.Rows.Count > 0)
                {
                    txtUserId.Text = tab.Rows[0]["UserId"].ToString();
                    txtPassword.Text = tab.Rows[0]["Password"].ToString();
                    txtEmailId.Text = tab.Rows[0]["EmailId"].ToString();

                    string dataTextField = DropDownListType.Items.FindByValue(tab.Rows[0]["UserType"].ToString()).ToString();

                    ListItem item = new ListItem(dataTextField, dataTextField);
                    int index = DropDownListType.Items.IndexOf(item);

                    if (index != -1)

                        DropDownListType.SelectedIndex = index;                  
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
                    if (obj.CheckUserId(txtUserId.Text))
                    {
                        obj.InsertUser(txtUserId.Text, txtPassword.Text, DropDownListType.SelectedValue, txtEmailId.Text);

                        //Emails.MailSender.SendEmail(" ", " ", txtEmailId.Value, "Credentials", "User Id=" + txtUserId.Value + "and Pwd=" + txtPassword.Value, " ");

                        ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('New Staff Added Successfully')</script>");
                        txtEmailId.Text = string.Empty;
                        txtPassword.Text = string.Empty;
                        txtUserId.Text = string.Empty;
                        LoadStaffs();

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('UserId Exists!!!')</script>");
                    }
                }
                catch
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('Server Error!')</script>");
                }
            }
            else if (btnSubmit.Text.Equals("Update"))
            {
                if (txtUserId.Text.Equals(Session["userId"].ToString()))
                {
                    try
                    {
                        obj.UpdateUser(txtUserId.Text, txtPassword.Text, DropDownListType.SelectedValue, txtEmailId.Text, Session["userId"].ToString());

                        //Emails.MailSender.SendEmail(" ", " ", txtEmailId.Value, "Credentials", "User Id=" + txtUserId.Value + "and Pwd=" + txtPassword.Value, " ");

                        ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('User Updated Successfully')</script>");
                        txtEmailId.Text = string.Empty;
                        txtPassword.Text = string.Empty;
                        txtUserId.Text = string.Empty;
                        LoadStaffs();

                        btnSubmit.Text = "Submit";

                    }
                    catch
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('Server Error!')</script>");
                    }
                }
                else
                {
                    if (obj.CheckUserId(txtUserId.Text))
                    {
                        try
                        {
                            obj.UpdateUser(txtUserId.Text, txtPassword.Text, DropDownListType.SelectedValue, txtEmailId.Text, Session["userId"].ToString());

                            //Emails.MailSender.SendEmail(" ", " ", txtEmailId.Value, "Credentials", "User Id=" + txtUserId.Value + "and Pwd=" + txtPassword.Value, " ");

                            ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('User Updated Successfully')</script>");
                            txtEmailId.Text = string.Empty;
                            txtPassword.Text = string.Empty;
                            txtUserId.Text = string.Empty;
                            LoadStaffs();

                            btnSubmit.Text = "Submit";

                        }
                        catch
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('Server Error!')</script>");
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('UserId Exists')</script>");
                    }
                }
            }
        }

      

    }
}