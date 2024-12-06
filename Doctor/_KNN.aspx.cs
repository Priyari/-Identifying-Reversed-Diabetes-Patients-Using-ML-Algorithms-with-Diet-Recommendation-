using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

namespace diseasePrediction.Doctor
{
    public partial class _KNN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            _Prediction();
        }               

        private void _Prediction()
        {
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();

                BLL obj = new BLL();
                DataTable tabTestingDataset = new DataTable();
                tabTestingDataset = obj.GetTestingDataset();

                DataTable tabTrainingDataset = new DataTable();
                tabTrainingDataset = GetTrainingDataset();

                if (tabTestingDataset.Rows.Count > 0 && tabTrainingDataset.Rows.Count > 0)
                {
                    Table1.Rows.Clear();

                    Table1.BorderStyle = BorderStyle.Double;
                    Table1.GridLines = GridLines.Both;
                    Table1.BorderColor = System.Drawing.Color.DarkGray;

                    TableRow mainrow = new TableRow();
                    mainrow.Height = 30;
                    mainrow.ForeColor = System.Drawing.Color.WhiteSmoke;

                    mainrow.BackColor = System.Drawing.Color.SteelBlue;

                    TableCell cell2 = new TableCell();
                    cell2.Text = "<b>SlNo</b>";
                    mainrow.Controls.Add(cell2);

                    TableCell cell2gender = new TableCell();
                    cell2gender.Text = "<b>gender</b>";
                    mainrow.Controls.Add(cell2gender);

                    TableCell cell2age = new TableCell();
                    cell2age.Text = "<b>age</b>";
                    mainrow.Controls.Add(cell2age);

                    TableCell cell2hypertension = new TableCell();
                    cell2hypertension.Text = "<b>hypertension</b>";
                    mainrow.Controls.Add(cell2hypertension);

                    TableCell cell2heart_disease = new TableCell();
                    cell2heart_disease.Text = "<b>heart disease</b>";
                    mainrow.Controls.Add(cell2heart_disease);

                    TableCell cell2smoking_history = new TableCell();
                    cell2smoking_history.Text = "<b>smoking history</b>";
                    mainrow.Controls.Add(cell2smoking_history);

                    TableCell cell2bmi = new TableCell();
                    cell2bmi.Text = "<b>bmi</b>";
                    mainrow.Controls.Add(cell2bmi);

                    TableCell cell2HbA1c_level = new TableCell();
                    cell2HbA1c_level.Text = "<b>HbA1c_level</b>";
                    mainrow.Controls.Add(cell2HbA1c_level);

                    TableCell cell2blood_glucose_level = new TableCell();
                    cell2blood_glucose_level.Text = "<b>blood glucose level</b>";
                    mainrow.Controls.Add(cell2blood_glucose_level);

                    TableCell cell3 = new TableCell();
                    cell3.Text = "<b>Prediction</b>";
                    mainrow.Controls.Add(cell3);

                    Table1.Controls.Add(mainrow);

                    int serialNo = 1;

                    for (int i = 0; i < tabTestingDataset.Rows.Count; i++)
                    {
                        DataTable tabPat = new DataTable();
                        tabPat = obj.GetTestingDataById(int.Parse(tabTestingDataset.Rows[i]["PatientId"].ToString()));

                        string _Output = KNN(int.Parse(tabTestingDataset.Rows[i]["PatientId"].ToString()));

                        TableRow row = new TableRow();

                        TableCell cellSerialNo = new TableCell();
                        cellSerialNo.Width = 50;
                        cellSerialNo.Text = serialNo + i + ".";
                        row.Controls.Add(cellSerialNo);

                        TableCell cellgender = new TableCell();
                        cellgender.Width = 100;
                        cellgender.Text = tabTestingDataset.Rows[i]["gender"].ToString();
                        row.Controls.Add(cellgender);

                        TableCell cellage = new TableCell();
                        cellage.Width = 100;
                        cellage.Text = tabTestingDataset.Rows[i]["age"].ToString();
                        row.Controls.Add(cellage);

                        TableCell cellhypertension = new TableCell();
                        cellhypertension.Width = 100;
                        cellhypertension.Text = tabTestingDataset.Rows[i]["hypertension"].ToString();
                        row.Controls.Add(cellhypertension);

                        TableCell cellhd = new TableCell();
                        cellhd.Width = 100;
                        cellhd.Text = tabTestingDataset.Rows[i]["heart_disease"].ToString();
                        row.Controls.Add(cellhd);

                        TableCell cellsmoke = new TableCell();
                        cellsmoke.Width = 100;
                        cellsmoke.Text = tabTestingDataset.Rows[i]["smoking_history"].ToString();
                        row.Controls.Add(cellsmoke);

                        TableCell cellbmi = new TableCell();
                        cellbmi.Width = 100;
                        cellbmi.Text = tabTestingDataset.Rows[i]["bmi"].ToString();
                        row.Controls.Add(cellbmi);

                        TableCell cellhbaic = new TableCell();
                        cellhbaic.Width = 100;
                        cellhbaic.Text = tabTestingDataset.Rows[i]["HbA1c_level"].ToString();
                        row.Controls.Add(cellhbaic);

                        TableCell cellblood = new TableCell();
                        cellblood.Width = 100;
                        cellblood.Text = tabTestingDataset.Rows[i]["blood_glucose_level"].ToString();
                        row.Controls.Add(cellblood);



                        TableCell cellResult = new TableCell();
                        cellResult.Width = 150;
                        cellResult.Text = _Output;
                        row.Controls.Add(cellResult);

                        Table1.Controls.Add(row);
                    }

                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;
                    _timeKNN = elapsedMs.ToString();

                    ResultAnaly();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "key", "<script>alert('Testing/Training Dataset Not Found!!!')</script>");
                }
            }
            catch
            {

            }
        }
                
        double _outcomeCntKNN = 0;
        string _timeKNN;
               
        //function which contains the KNN algorithm steps
        private string KNN(int patId)
        {
            BLL obj = new BLL();
            string _output = null;

            ArrayList _Distance = new ArrayList();
            ArrayList _PatientId = new ArrayList();

            DataTable tabTrainingDataset = new DataTable();
            tabTrainingDataset = GetTrainingDataset();

            ArrayList classify = new ArrayList();

            DataTable tab = new DataTable();
            tab = obj.GetTestingDataById(patId);

            if (tab.Rows.Count > 0)
            {
                classify.Clear();

                //storing current patient parameters into arraylist (classify)
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    //classify.Add(tab.Rows[0]["Name"].ToString());
                    classify.Add(tab.Rows[0]["gender"].ToString());
                    classify.Add(tab.Rows[0]["age"].ToString());
                    classify.Add(tab.Rows[0]["hypertension"].ToString());
                    classify.Add(tab.Rows[0]["heart_disease"].ToString());
                    classify.Add(tab.Rows[0]["smoking_history"].ToString());
                    classify.Add(tab.Rows[0]["bmi"].ToString());
                    classify.Add(tab.Rows[0]["HbA1c_level"].ToString());
                    classify.Add(tab.Rows[0]["blood_glucose_level"].ToString());
                    

                }

                int m = 25;
                _Distance.Clear();
                _PatientId.Clear();

                //finding the distance between the objects
                for (int i = 0; i < tabTrainingDataset.Rows.Count; i++)
                {
                    double _val = 0.0;

                    for (int j = 0; j < classify.Count; j++)
                    {
                        _val += Math.Pow(double.Parse(tabTrainingDataset.Rows[i][j + 1].ToString()) - double.Parse(classify[j].ToString()), 2);
                    }

                    _val = Math.Sqrt(_val);

                    _Distance.Add(Math.Round(_val, 1));
                    _PatientId.Add(tabTrainingDataset.Rows[i]["PatientId"].ToString());
                }

                ArrayList temp = new ArrayList();
                ArrayList arrayPatients = new ArrayList();

                ArrayList arrayExists = new ArrayList();
                int d = 0;

                temp.Clear();
                arrayExists.Clear();
                arrayPatients.Clear();

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

                if (arrayPatients.Count > 0)
                {
                    int cnt;

                    ArrayList arrayCnt = new ArrayList();
                    ArrayList arrayOutcome = new ArrayList();

                    ArrayList s = new ArrayList();                   

                    //get possible outcomes
                    s.Add("0");
                    s.Add("1");


                    arrayCnt.Clear();
                    arrayOutcome.Clear();

                    for (int i = 0; i < s.Count; i++)
                    {
                        cnt = 0;

                        for (int j = 0; j < arrayPatients.Count; j++)
                        {
                            DataTable tabpatientOutcome = new DataTable();
                            tabpatientOutcome = obj.GetTrainingDataById(int.Parse(arrayPatients[j].ToString()));

                            if (tabpatientOutcome.Rows[0]["Result"].ToString().Equals(s[i].ToString()))
                            {
                                ++cnt;
                            }
                        }

                        arrayCnt.Add(cnt);
                        arrayOutcome.Add(s[i].ToString());
                    }

                    ArrayList temp1 = new ArrayList();

                    temp1.Clear();
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
                            _output = arrayOutcome[y].ToString();

                            if (_output.Equals("1"))
                            {
                                _output = "YES";
                            }
                            else
                            {
                                _output = "NO";
                            }

                            DataTable tabActualData = new DataTable();
                            tabActualData = obj.GetActualPatientDataById(patId);

                            if (tabActualData.Rows[0]["Result"].ToString().Equals(arrayOutcome[y].ToString()))
                            {
                                ++_outcomeCntKNN;
                            }

                            return _output;
                        }
                    }
                }
            }
            else
            {

            }

            return _output;
        }
        
        private void ResultAnaly()
        {
            BLL obj = new BLL();
            int _ActualCnt = obj.GetActualCnt();

            tableResults.Rows.Clear();

            tableResults.BorderStyle = BorderStyle.Double;
            tableResults.GridLines = GridLines.Both;
            tableResults.BorderColor = System.Drawing.Color.SteelBlue;

            TableRow mainrow = new TableRow();
            mainrow.Height = 30;
            mainrow.ForeColor = System.Drawing.Color.WhiteSmoke;
            mainrow.BackColor = System.Drawing.Color.SteelBlue;

            TableCell cellC = new TableCell();
            cellC.Text = "<b>KNN</b>";
            mainrow.Controls.Add(cellC);

            TableCell cellB = new TableCell();
            cellB.Text = "<b>Constraint</b>";
            mainrow.Controls.Add(cellB);

            tableResults.Controls.Add(mainrow);

            //1st row
            TableRow row1 = new TableRow();

            TableCell cellAcc = new TableCell();
            cellAcc.Text = "<b>Accuracy</b>";
            row1.Controls.Add(cellAcc);

            TableCell cellAccKNN = new TableCell();
            cellAccKNN.Font.Bold = true;
            //cal
            double _percentageKNN = (_outcomeCntKNN / _ActualCnt) * 100;
            cellAccKNN.Text = _percentageKNN.ToString() + " %";
            row1.Controls.Add(cellAccKNN);

            tableResults.Controls.Add(row1);

            //2nd row           
            TableRow row2 = new TableRow();

            TableCell cellTime = new TableCell();
            cellTime.Text = "<b>Time (milli secs)</b>";
            row2.Controls.Add(cellTime);

            TableCell cellTimeKNN = new TableCell();
            cellTimeKNN.Font.Bold = true;
            cellTimeKNN.Text = _timeKNN;
            row2.Controls.Add(cellTimeKNN);

            tableResults.Controls.Add(row2);

            //3rd row           
            TableRow row3 = new TableRow();

            TableCell cellCorrect = new TableCell();
            cellCorrect.Text = "<b>Correctly Classified</b>";
            row3.Controls.Add(cellCorrect);

            TableCell cellCorrectKNN = new TableCell();
            cellCorrectKNN.Font.Bold = true;
            cellCorrectKNN.Text = _percentageKNN.ToString() + " %";
            row3.Controls.Add(cellCorrectKNN);

            tableResults.Controls.Add(row3);

            //4th row           
            TableRow row4 = new TableRow();

            TableCell cellInCorrect = new TableCell();
            cellInCorrect.Text = "<b>InCorrectly Classified</b>";
            row4.Controls.Add(cellInCorrect);

            TableCell cellInCorrectKNN = new TableCell();
            cellInCorrectKNN.Font.Bold = true;
            cellInCorrectKNN.Text = (100 - _percentageKNN).ToString() + " %";
            row4.Controls.Add(cellInCorrectKNN);

            tableResults.Controls.Add(row4);

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

    }
}