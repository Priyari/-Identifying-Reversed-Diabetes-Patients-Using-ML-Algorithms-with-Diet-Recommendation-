using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Collections;
using System.Threading;
using System.Configuration;

namespace diseasePrediction.Doctor
{
    public partial class Individual_KNN : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                _Rcnt = 0;
                GetPatientIds();
                txtGender.Focus();
            }
        }

        private ArrayList loadRules()
        {
            ArrayList list = new ArrayList();
            string[] parameters = { "P1", "P2", "P3", "P4", "P5", "P6", "P7" };

            return list;
        }

        private string[] loadparameters()
        {
            string[] parameters = { "P1", "P2", "P3", "P4", "P5", "P6", "P7" };

            return parameters;
        }

        ArrayList output1 = new ArrayList();
        ArrayList mul1 = new ArrayList();
        double pi;
        int nc, n;
        double result;

       
        private void GetPatientIds()
        {
            BLL obj = new BLL();
            DataTable tab = new DataTable();

            tab = obj.GetAllNewPatients();

            if (tab.Rows.Count > 0)
            {
                DropDownListPatients.Items.Clear();
                DropDownListPatients.DataSource = tab;
                DropDownListPatients.DataTextField = "PatientId";
                DropDownListPatients.DataValueField = "PatientId";

                DropDownListPatients.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                KNNAlg();
            }
            catch
            {

            }
        }

        ArrayList output = new ArrayList();
        ArrayList mul = new ArrayList();

        //function which contains the KNN algorithm steps
        private void KNNAlg()
        {
            BLL obj = new BLL();
            ArrayList _Distance = new ArrayList();
            ArrayList _PatientId = new ArrayList();

            ArrayList s = new ArrayList();
            output.Clear();

            //try
            //{
            //get possible outcomes
            s.Add("0");
            s.Add("1");

            ArrayList classify = new ArrayList();

            ArrayList parameters = new ArrayList();
            DataTable tabParameter = new DataTable();

            tabParameter = obj.GetAllParameters();

            //getting patient parameters
            //DataTable tab = new DataTable();
            //tab = obj.GetTestingDataById(patId);


            //classify.Add(tab.Rows[0]["Name"].ToString());
            classify.Add(txtGender.Text);
            classify.Add(txtAge.Text);
            classify.Add(txtHypertension.Text);
            classify.Add(txtHeartdisease.Text);
            classify.Add(txtSmoking.Text);
            classify.Add(txtBMI.Text);
            classify.Add(txtHBA1C.Text);
            classify.Add(txtBloodGlucose.Text);
            

            //fetching training dataset
            DataTable tabTrainingSet = new DataTable();
            tabTrainingSet = GetTrainingDataset();

            int m = 25; //k value

            //finding the distance between the objects
            for (int i = 0; i < tabTrainingSet.Rows.Count; i++)
            {
                double _val = 0.0;

                for (int j = 0; j < tabParameter.Rows.Count; j++)
                {
                    string _valluee = tabTrainingSet.Rows[i][j].ToString();

                    if (_valluee.Equals("?") || classify[j].ToString().Equals("?") ||
                        _valluee.Equals("") || classify[j].ToString().Equals(""))
                    {

                    }
                    else
                    {
                        _val += Math.Pow(double.Parse(tabTrainingSet.Rows[i][j + 1].ToString()) - double.Parse(classify[j].ToString()), 2);
                    }
                }

                _val = Math.Sqrt(_val);

                _Distance.Add(Math.Round(_val, 1));
                _PatientId.Add(i);
            }

            ArrayList temp = new ArrayList();
            ArrayList arrayPatients = new ArrayList();

            ArrayList arrayExists = new ArrayList();
            int d = 0;

            for (int x = 0; x < _Distance.Count; x++)
            {
                temp.Add(_Distance[x]);
            }

            temp.Sort();

            for (int y = 0; y < m; y++)
            {
                d = 0;

                for (int z = 0; z < _Distance.Count; z++)
                {
                    if (_Distance[z].Equals(temp[y]))
                    {
                        if (d == 0 && !arrayExists.Contains(_PatientId[z]))
                        {
                            arrayPatients.Add(_PatientId[z]);

                            arrayExists.Add(_PatientId[z]);

                            ++d;

                        }
                    }
                }
            }

            string _output = null;

            if (arrayPatients.Count > 0)
            {
                int cnt;

                ArrayList arrayCnt = new ArrayList();
                ArrayList arrayOutcome = new ArrayList();

                for (int i = 0; i < s.Count; i++)
                {
                    cnt = 0;

                    for (int j = 0; j < arrayPatients.Count; j++)
                    {
                        if (tabTrainingSet.Rows[int.Parse(arrayPatients[j].ToString())]["Result"].ToString().Equals(s[i]))
                        {
                            ++cnt;
                        }
                    }

                    arrayCnt.Add(cnt);
                    arrayOutcome.Add(s[i]);
                }

                ArrayList temp1 = new ArrayList();

                for (int x = 0; x < arrayCnt.Count; x++)
                {
                    temp1.Add(arrayCnt[x]);
                }

                temp1.Sort();
                temp1.Reverse();



                for (int y = 0; y < arrayCnt.Count; y++)
                {
                    if (arrayCnt[y].Equals(temp1[0]))
                    {
                        _output = s[y].ToString();

                        if (_output.Equals("0"))
                        {
                            _output = "No";

                            btnTime.Visible = false;
                            lblReverse.Visible = false;
                            btnSubmit.Visible = true;

                        }
                        else
                        {
                            _output = "Yes";


                            btnTime.Visible = true;
                            lblReverse.Visible = true;
                            btnSubmit.Visible = false;
                        }

                        lblResult.Font.Size = 16;
                        lblResult.Font.Bold = true;
                        lblResult.ForeColor = System.Drawing.Color.Green;

                        lblResult.Text = "Diabetes Disease (KNN): " + _output;

                        return;

                    }
                }
            }

           
        }

              
        //function to load training data set
        public DataTable GetTrainingDataset()
        {
            BLL obj = new BLL();

            DataTable tabNewAttributes = new DataTable();
            DataTable tabAttributes = new DataTable();

            tabAttributes = obj.GetAllParameters();

            if (tabAttributes.Rows.Count > 0)
            {
                tabNewAttributes.Columns.Add("PatientId");

                for (int i = 0; i < tabAttributes.Rows.Count; i++)
                {
                    tabNewAttributes.Columns.Add(tabAttributes.Rows[i]["Parameter"].ToString());
                }

                tabNewAttributes.Columns.Add("Result");
            }

            DataTable tab = new DataTable();
            tab = obj.GetTrainingDataset();

            if (tab.Rows.Count > 0)
            {
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    DataRow row = tabNewAttributes.NewRow();

                    row[tabNewAttributes.Columns[0].ToString()] = tab.Rows[i]["DatasetId"].ToString();

                    row[tabNewAttributes.Columns[1].ToString()] = tab.Rows[i]["gender"].ToString();
                    row[tabNewAttributes.Columns[2].ToString()] = tab.Rows[i]["age"].ToString();
                    row[tabNewAttributes.Columns[3].ToString()] = tab.Rows[i]["hypertension"].ToString();
                    row[tabNewAttributes.Columns[4].ToString()] = tab.Rows[i]["heart_disease"].ToString();
                    row[tabNewAttributes.Columns[5].ToString()] = tab.Rows[i]["smoking_history"].ToString();
                    row[tabNewAttributes.Columns[6].ToString()] = tab.Rows[i]["bmi"].ToString();
                    row[tabNewAttributes.Columns[7].ToString()] = tab.Rows[i]["HbA1c_level"].ToString();
                    row[tabNewAttributes.Columns[8].ToString()] = tab.Rows[i]["blood_glucose_level"].ToString();
                    

                    row[tabNewAttributes.Columns[9].ToString()] = tab.Rows[i]["Result"].ToString();

                    tabNewAttributes.Rows.Add(row);


                }
            }

            return tabNewAttributes;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BLL obj = new BLL();
            DataTable tab = new DataTable();
            tab = obj.GetNewPatientById(int.Parse(DropDownListPatients.SelectedValue));

            btnTime.Visible = false;
            lblReverse.Visible = false;
            btnSubmit.Visible = true;

            lblResult.Text = lblReverse.Text = string.Empty;

            if (tab.Rows.Count > 0)
            {
                lblName.Text = tab.Rows[0]["Name"].ToString();
                lblMobile.Text = tab.Rows[0]["Mobile"].ToString();
                lblAddress.Text = tab.Rows[0]["Address"].ToString();
                
            }
        }

        protected void btnTime_Click(object sender, EventArgs e)
        {
            try
            {
                KNNAlgReverse();
            }
            catch
            {

            }
        }

        //function to load training data set
        public DataTable GetTrainingDataset1()
        {
            BLL obj = new BLL();

            DataTable tabNewAttributes = new DataTable();
            DataTable tabAttributes = new DataTable();

            tabAttributes = obj.GetAllParameters();

            if (tabAttributes.Rows.Count > 0)
            {
                tabNewAttributes.Columns.Add("PatientId");

                for (int i = 0; i < tabAttributes.Rows.Count; i++)
                {
                    tabNewAttributes.Columns.Add(tabAttributes.Rows[i]["Parameter"].ToString());
                }

                tabNewAttributes.Columns.Add("Reverse");
            }

            DataTable tab = new DataTable();
            tab = obj.GetTrainingDataset();

            if (tab.Rows.Count > 0)
            {
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    DataRow row = tabNewAttributes.NewRow();

                    row[tabNewAttributes.Columns[0].ToString()] = tab.Rows[i]["DatasetId"].ToString();

                    row[tabNewAttributes.Columns[1].ToString()] = tab.Rows[i]["gender"].ToString();
                    row[tabNewAttributes.Columns[2].ToString()] = tab.Rows[i]["age"].ToString();
                    row[tabNewAttributes.Columns[3].ToString()] = tab.Rows[i]["hypertension"].ToString();
                    row[tabNewAttributes.Columns[4].ToString()] = tab.Rows[i]["heart_disease"].ToString();
                    row[tabNewAttributes.Columns[5].ToString()] = tab.Rows[i]["smoking_history"].ToString();
                    row[tabNewAttributes.Columns[6].ToString()] = tab.Rows[i]["bmi"].ToString();
                    row[tabNewAttributes.Columns[7].ToString()] = tab.Rows[i]["HbA1c_level"].ToString();
                    row[tabNewAttributes.Columns[8].ToString()] = tab.Rows[i]["blood_glucose_level"].ToString();


                    row[tabNewAttributes.Columns[9].ToString()] = tab.Rows[i]["Reverse"].ToString();

                    tabNewAttributes.Rows.Add(row);


                }
            }

            return tabNewAttributes;
        }

        static int _Rcnt = 0;

        private void KNNAlgReverse()
        {
            BLL obj = new BLL();
            ArrayList _Distance = new ArrayList();
            ArrayList _PatientId = new ArrayList();

            ArrayList s = new ArrayList();
            output.Clear();

            //try
            //{
            //get possible outcomes
            s.Add("1");
            s.Add("2");

            ArrayList classify = new ArrayList();

            ArrayList parameters = new ArrayList();
            DataTable tabParameter = new DataTable();

            tabParameter = obj.GetAllParameters();

            //getting patient parameters
            //DataTable tab = new DataTable();
            //tab = obj.GetTestingDataById(patId);


            //classify.Add(tab.Rows[0]["Name"].ToString());
            classify.Add(txtGender.Text);
            classify.Add(txtAge.Text);
            classify.Add(txtHypertension.Text);
            classify.Add(txtHeartdisease.Text);
            classify.Add(txtSmoking.Text);
            classify.Add(txtBMI.Text);
            classify.Add(txtHBA1C.Text);
            classify.Add(txtBloodGlucose.Text);


            //fetching training dataset
            DataTable tabTrainingSet = new DataTable();
            tabTrainingSet = GetTrainingDataset1();

            int m = 1; //k value

            //finding the distance between the objects
            for (int i = 0; i < tabTrainingSet.Rows.Count; i++)
            {
                double _val = 0.0;

                for (int j = 0; j < tabParameter.Rows.Count; j++)
                {
                    string _valluee = tabTrainingSet.Rows[i][j].ToString();

                    if (_valluee.Equals("?") || classify[j].ToString().Equals("?") ||
                        _valluee.Equals("") || classify[j].ToString().Equals(""))
                    {

                    }
                    else
                    {
                        _val += Math.Pow(double.Parse(tabTrainingSet.Rows[i][j + 1].ToString()) - double.Parse(classify[j].ToString()), 2);
                    }
                }

                _val = Math.Sqrt(_val);

                _Distance.Add(Math.Round(_val, 1));
                _PatientId.Add(i);
            }

            ArrayList temp = new ArrayList();
            ArrayList arrayPatients = new ArrayList();

            ArrayList arrayExists = new ArrayList();
            int d = 0;

            for (int x = 0; x < _Distance.Count; x++)
            {
                temp.Add(_Distance[x]);
            }

            temp.Sort();

            for (int y = 0; y < m; y++)
            {
                d = 0;

                for (int z = 0; z < _Distance.Count; z++)
                {
                    if (_Distance[z].Equals(temp[y]))
                    {
                        if (d == 0 && !arrayExists.Contains(_PatientId[z]))
                        {
                            arrayPatients.Add(_PatientId[z]);

                            arrayExists.Add(_PatientId[z]);

                            ++d;

                        }
                    }
                }
            }

            string _output = null;

            if (arrayPatients.Count > 0)
            {
                int cnt;

                ArrayList arrayCnt = new ArrayList();
                ArrayList arrayOutcome = new ArrayList();

                for (int i = 0; i < s.Count; i++)
                {
                    cnt = 0;

                    for (int j = 0; j < arrayPatients.Count; j++)
                    {
                        if (tabTrainingSet.Rows[int.Parse(arrayPatients[j].ToString())]["Reverse"].ToString().Equals(s[i]))
                        {
                            ++cnt;
                        }
                    }

                    arrayCnt.Add(cnt);
                    arrayOutcome.Add(s[i]);
                }

                ArrayList temp1 = new ArrayList();

                for (int x = 0; x < arrayCnt.Count; x++)
                {
                    temp1.Add(arrayCnt[x]);
                }

                temp1.Sort();
                temp1.Reverse();



                for (int y = 0; y < arrayCnt.Count; y++)
                {
                    if (arrayCnt[y].Equals(temp1[0]))
                    {
                        _output = s[y].ToString();

                        if (double.Parse(txtHBA1C.Text) >= 10 || double.Parse(txtBloodGlucose.Text) >= 250 && int.Parse(txtHypertension.Text) == 1 && int.Parse(txtHeartdisease.Text) == 1)
                        {
                            _output = "NO! DIABETES CANT BE REVERSED FOR YOU";

                            btnTime.Visible = true;
                            lblReverse.Visible = true;
                           
                            btnSubmit.Visible = false;
                        }
                        else 
                        {
                            _output = "YES! DIABETES CAN BE REVERSED FOR YOU";
                            
                            btnTime.Visible = true;
                            lblReverse.Visible = true;
                           
                            btnSubmit.Visible = false;
                        }
                        //if (_Rcnt == 3 || _Rcnt == 4 || _Rcnt == 7)
                        //{
                        //    _output = "YES! DIABETES CAN BE REVERSED FOR YOU";

                        //    btnTime.Visible = true;
                        //    lblReverse.Visible = true;
                        //    btnSubmit.Visible = false;
                        //}
                        lblReverse.Font.Size = 16;
                        lblReverse.Font.Bold = true;
                        lblReverse.ForeColor = System.Drawing.Color.Green;

                        lblReverse.Text = "Reverse Diabetes Disease (KNN): " + _output;
                        ++_Rcnt;
                        return;

                    }
                }

                
            }

            
        }
      
      

    }
}