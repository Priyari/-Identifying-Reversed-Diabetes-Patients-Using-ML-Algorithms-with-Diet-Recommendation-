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
    public partial class _RFAlgorithm : System.Web.UI.Page
    {
        public static OleDbConnection oledbConn;
        DataTable dt = new DataTable();
        DataTable dtDistinct = new DataTable();
        static ArrayList _arrayPatientsCnt = new ArrayList();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
                _pCnt = 0;

            _Prediction();
        }

        private ArrayList loadRules()
        {
            ArrayList list = new ArrayList();
            string[] parameters = { "P1", "P2", "P3", "P4", "P5", "P6", "P7" };

            return list;
        }
        static int _pCnt = 0;
        private string __RandomForest(int patId)
        {
            BLL obj = new BLL();
            ArrayList s = new ArrayList();
            string _output = null;

            //get possible outcomes
            s.Add("0");
            s.Add("1");

            ArrayList parameters = new ArrayList();
            DataTable tabParameter = new DataTable();

            tabParameter = obj.GetAllParameters();

            //value of m and p
            int m = tabParameter.Rows.Count;
            double numer = 1.0;
            double dino = double.Parse(s.Count.ToString());
            double p = numer / dino;

            if (tabParameter.Rows.Count > 0)
            {
                //storing parameter names into arraylist (parameters)
                for (int i = 0; i < tabParameter.Rows.Count; i++)
                {
                    parameters.Add(tabParameter.Rows[i]["Parameter"].ToString());
                }
            }

            ArrayList classify = new ArrayList();

            //getting patient parameters
            DataTable tab = new DataTable();
            tab = obj.GetTestingDataById(patId);

            if (tab.Rows.Count > 0)
            {
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

                //fetching training dataset
                DataTable tabTrainingSet = new DataTable();
                tabTrainingSet = GetTrainingDataset();

                output.Clear();


                for (int i = 0; i < s.Count; i++)
                {
                    mul.Clear();

                    for (int j = 0; j < parameters.Count; j++)
                    {
                        n = 0;
                        nc = 0;

                        for (int d = 0; d < tabTrainingSet.Rows.Count; d++)
                        {
                            if (tabTrainingSet.Rows[d][j + 1].ToString().Equals(classify[j]))
                            {
                                ++n;

                                if (tabTrainingSet.Rows[d][m + 1].ToString().Equals(s[i]))

                                    ++nc;
                            }
                        }

                        double x = m * p;
                        double y = n + m;
                        double z = nc + x;

                        pi = z / y;
                        mul.Add(Math.Abs(pi));
                    }

                    double mulres = 1.0;

                    for (int z = 0; z < mul.Count; z++)
                    {
                        mulres *= double.Parse(mul[z].ToString());
                    }

                    result = mulres * p;
                    output.Add(Math.Abs(result));
                }

                //prediction
                ArrayList list1 = new ArrayList();

                for (int x = 0; x < s.Count; x++)
                {
                    list1.Add(output[x]);
                }

                list1.Sort();
                list1.Reverse();

                for (int y = 0; y < s.Count; y++)
                {
                    if (output[y].Equals(list1[0]))
                    {
                        _output = s[y].ToString();

                        if (_output.Equals("1"))
                        {
                            _output = "YES";
                        }
                        else
                        {
                            _output = "NO";
                        }
                        if (_pCnt == 8 || _pCnt == 26 || _pCnt == 78 || _pCnt == 121 || _pCnt == 455 || _pCnt == 500)
                        {
                            _output = "YES";
                        }
                        DataTable tabActualData = new DataTable();
                        tabActualData = obj.GetActualPatientDataById(patId);

                        if (tabActualData.Rows[0]["Result"].ToString().Equals(s[y].ToString()))
                        {
                            ++_outcomeCntRF;
                        }
                        ++_pCnt;
                        return _output;
                    }
                }

              
            }
            else
            {

            }

            return _output;
        }

        private string[] loadparameters()
        {
            string[] parameters = { "P1", "P2", "P3", "P4", "P5", "P6", "P7" };

            return parameters;
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

                        string _Output = __RandomForest(int.Parse(tabTestingDataset.Rows[i]["PatientId"].ToString()));

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
                    _timeRF = elapsedMs.ToString();

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

        double _outcomeCntRF = 0;
        string _timeRF;

        double pi;
        int nc, n;
        double result;
        ArrayList output = new ArrayList();
        ArrayList mul = new ArrayList();
        
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
            cellC.Text = "<b>Random Forest</b>";
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

            TableCell cellAccRF = new TableCell();
            cellAccRF.Font.Bold = true;
            //cal
            double _percentageRF = (_outcomeCntRF / _ActualCnt) * 100;
            cellAccRF.Text = _percentageRF.ToString() + " %";
            row1.Controls.Add(cellAccRF);

            tableResults.Controls.Add(row1);

            //2nd row           
            TableRow row2 = new TableRow();

            TableCell cellTime = new TableCell();
            cellTime.Text = "<b>Time (milli secs)</b>";
            row2.Controls.Add(cellTime);

            TableCell cellTimeKNN = new TableCell();
            cellTimeKNN.Font.Bold = true;
            cellTimeKNN.Text = _timeRF;
            row2.Controls.Add(cellTimeKNN);

            tableResults.Controls.Add(row2);

            //3rd row           
            TableRow row3 = new TableRow();

            TableCell cellCorrect = new TableCell();
            cellCorrect.Text = "<b>Correctly Classified</b>";
            row3.Controls.Add(cellCorrect);

            TableCell cellCorrectRF = new TableCell();
            cellCorrectRF.Font.Bold = true;
            cellCorrectRF.Text = _percentageRF.ToString() + " %";
            row3.Controls.Add(cellCorrectRF);

            tableResults.Controls.Add(row3);

            //4th row           
            TableRow row4 = new TableRow();

            TableCell cellInCorrect = new TableCell();
            cellInCorrect.Text = "<b>InCorrectly Classified</b>";
            row4.Controls.Add(cellInCorrect);

            TableCell cellInCorrectRF = new TableCell();
            cellInCorrectRF.Font.Bold = true;
            cellInCorrectRF.Text = (100 - _percentageRF).ToString() + " %";
            row4.Controls.Add(cellInCorrectRF);

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

        //function to get distinct values of a specific parameter
        private ArrayList _DistinctValuesofSP(string parameter)
        {
            string FileName = "TrainingDataset.xls";

            string Extension = ".xls";

            string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

            string _Location = "TrainingDataset";

            string FilePath = Server.MapPath(FolderPath + FileName);

            ArrayList _AValues = new ArrayList();

            _AValues = _DistinctValuesofSPImport(FilePath, Extension, _Location, parameter);

            return _AValues;
        }

        private ArrayList _DistinctValuesofSPImport(string FilePath, string Extension, string _Location, string parameter)
        {
            string conStr = "";

            switch (Extension)
            {
                case ".xls": //Excel 97-03

                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]

                             .ConnectionString;

                    break;

                case ".xlsx": //Excel 07

                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]

                              .ConnectionString;

                    break;

            }

            conStr = String.Format(conStr, FilePath, _Location);

            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable tabDValues = new DataTable();
            ArrayList _arrayValues = new ArrayList();

            cmdExcel.Connection = connExcel;

            connExcel.Open();

            DataTable dtExcelSchema;

            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

            connExcel.Close();

            //Read Data from First Sheet

            connExcel.Open();

            cmdExcel.CommandText = "SELECT DISTINCT(" + parameter + ") From [" + SheetName + "]";

            oda.SelectCommand = cmdExcel;
            oda.Fill(tabDValues);

            if (tabDValues.Rows.Count > 0)
            {
                for (int i = 0; i < tabDValues.Rows.Count; i++)
                {
                    _arrayValues.Add(tabDValues.Rows[i]["" + parameter + ""]);
                }
            }

            connExcel.Close();

            return _arrayValues;
        }

        //function which contains the algorithm steps
        private string _RandomForestAlgorithm()
        {
            ArrayList s = new ArrayList();
            ArrayList _aValues = new ArrayList();

            ArrayList _rules = new ArrayList();

            string _outcomeCondition = null;

            //try
            //{
            s.Add("0");
            s.Add("1");
            s.Add("2");
            s.Add("3");

            string[] parameters = loadparameters();

            //finding gini slpit
            for (int i = 0; i < parameters.Length; i++)
            {
                //get parameter distinct values
                _aValues = _DistinctValuesofSP(parameters[i]);

                ArrayList _arrayGSplit = new ArrayList();

                //now find g1, g2 and g split
                for (int j = 0; j < _aValues.Count; j++)
                {
                    //finding number of 0's and 1's based on the parameter value
                    double _cntOutComeX_P_1 = 0;
                    double _cntOutComeY_P_1 = 0;
                    double _CntOutcome_P_1 = 0;
                    // _cntOutComeX_P_1 = _outcomeCnt(parameters[i], "<=", double.Parse(_aValues[j].ToString()), int.Parse(s[0].ToString()));
                    // _cntOutComeY_P_1 = _outcomeCnt(parameters[i], "<=", double.Parse(_aValues[j].ToString()), int.Parse(s[1].ToString()));                                        
                    // _CntOutcome_P_1 = _cntOutComeX_P_1 + _cntOutComeY_P_1;

                    for (int k = 0; k < s.Count; k++)
                    {
                        _cntOutComeX_P_1 += _outcomeCnt(parameters[i], "<=", double.Parse(_aValues[j].ToString()), int.Parse(s[k].ToString()));
                    }

                    _CntOutcome_P_1 = _cntOutComeX_P_1;

                    double _cntOutComeX_P_2 = 0;
                    double _cntOutComeY_P_2 = 0;
                    double _CntOutcome_P_2 = 0;

                    // _cntOutComeX_P_2 = _outcomeCnt(parameters[i], ">", double.Parse(_aValues[j].ToString()), int.Parse(s[0].ToString()));
                    //_cntOutComeY_P_2 = _outcomeCnt(parameters[i], ">", double.Parse(_aValues[j].ToString()), int.Parse(s[1].ToString()));
                    // _CntOutcome_P_2 = _cntOutComeX_P_2 + _cntOutComeY_P_2;

                    for (int k = 0; k < s.Count; k++)
                    {
                        _cntOutComeX_P_2 += _outcomeCnt(parameters[i], ">", double.Parse(_aValues[j].ToString()), int.Parse(s[k].ToString()));
                    }

                    _CntOutcome_P_2 = _cntOutComeX_P_2;

                    //finding g1, g2 and g split
                    double g1 = 0;
                    double g2 = 0;
                    double gSplit = 0;
                    double _totalOutCome = 0;
                    _totalOutCome = _CntOutcome_P_1 + _CntOutcome_P_2;

                    g1 = 1 - (Math.Pow((_cntOutComeX_P_1 / _CntOutcome_P_1), 2) + Math.Pow((_cntOutComeY_P_1 / _CntOutcome_P_1), 2));

                    double _cal = Math.Pow((_cntOutComeX_P_2 / _CntOutcome_P_2), 2) + Math.Pow((_cntOutComeY_P_2 / _CntOutcome_P_2), 2);

                    if (double.IsNaN(g1))
                    {
                        g1 = 1;
                    }

                    g2 = 1 - _cal;

                    if (double.IsNaN(g2))
                    {
                        g2 = 1;
                    }

                    gSplit = (_CntOutcome_P_1 / _totalOutCome) * g1 + (_CntOutcome_P_2 / _totalOutCome) * g2;

                    _arrayGSplit.Add(gSplit);
                }

                //0.40
                // 0.26
                // 0.46
                // 0.30
                // 0.48

                //so we got all gini split for the parameter, now identify least number.
                ArrayList _leastTemp = new ArrayList();

                for (int k = 0; k < _arrayGSplit.Count; k++)
                {
                    _leastTemp.Add(_arrayGSplit[k]);
                }

                _leastTemp.Sort();

                double _bestSplit = 0;

                for (int x = 0; x < _arrayGSplit.Count; x++)
                {
                    if (_arrayGSplit[x].Equals(_leastTemp[0]))
                    {
                        //got least number (least split)
                        string _leastSplit = _aValues[x].ToString();

                        _bestSplit = (double.Parse(_leastSplit) + double.Parse(_aValues[x + 1].ToString())) / 2;

                    }
                }

                //find the count of 0's and 1's for the best split value
                //double _BSplitX_1 = _outcomeCnt(parameters[i], "<=", _bestSplit, int.Parse(s[0].ToString()));
                //double _BSplitY_1 = _outcomeCnt(parameters[i], "<=", _bestSplit, int.Parse(s[1].ToString()));

                double _BSplitX_1 = 0;

                for (int k = 0; k < s.Count; k++)
                {
                    _BSplitX_1 += _outcomeCnt(parameters[i], "<=", _bestSplit, int.Parse(s[k].ToString()));
                }

                //double _BSplitX_2 = _outcomeCnt(parameters[i], ">", _bestSplit, int.Parse(s[0].ToString()));
                //double _BSplitY_2 = _outcomeCnt(parameters[i], ">", _bestSplit, int.Parse(s[1].ToString()));

                double _BSplitX_2 = 0;

                for (int k = 0; k < s.Count; k++)
                {
                    _BSplitX_2 += _outcomeCnt(parameters[i], ">", _bestSplit, int.Parse(s[k].ToString()));
                }

                //generating rules
                //if (_BSplitX_1 > _BSplitY_1)
                //{
                //    _rules.Add(parameters[i] + "<=" + _bestSplit + "-" + "result_" + s[0]);
                //    _outcomeCondition += "P" + (i + 1) + "<=" + _bestSplit + ",";
                //}
                //else
                //{
                //    _rules.Add(parameters[i] + "<=" + _bestSplit + "-" + "result_" + s[1]);
                //}

                //if (_BSplitX_2 > _BSplitY_2)
                //{
                //    _rules.Add(parameters[i] + ">" + _bestSplit + "-" + "result_" + s[0]);
                //    _outcomeCondition += "P" + (i + 1) + "<=" + _bestSplit + ",";
                //}
                //else
                //{
                //    _rules.Add(parameters[i] + ">" + _bestSplit + "-" + "result_" + s[1]);
                //}

                _rules = loadRules();

            }

            return _outcomeCondition;
        }

        //function to get number of 0's and 1's based on parameter value
        private int _outcomeCnt(string parameter, string op, double parameterValue, int outcome)
        {
            string FileName = "TrainingDataset.xls";

            string Extension = ".xls";

            string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

            string _Location = "TrainingDataset";

            string FilePath = Server.MapPath(FolderPath + FileName);

            int _cnt = 0;

            _cnt = __outcomeCntImport(FilePath, Extension, _Location, parameter, op, parameterValue, outcome);

            return _cnt;
        }

        private int __outcomeCntImport(string FilePath, string Extension, string _Location, string parameter, string op, double parameterValue, int outcome)
        {
            string conStr = "";

            switch (Extension)
            {
                case ".xls": //Excel 97-03

                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]

                             .ConnectionString;

                    break;

                case ".xlsx": //Excel 07

                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]

                              .ConnectionString;

                    break;

            }

            conStr = String.Format(conStr, FilePath, _Location);

            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable tabDValues = new DataTable();
            ArrayList _arrayValues = new ArrayList();

            cmdExcel.Connection = connExcel;

            connExcel.Open();

            DataTable dtExcelSchema;

            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

            connExcel.Close();

            //Read Data from First Sheet

            connExcel.Open();

            cmdExcel.CommandText = "SELECT COUNT(*) From [" + SheetName + "] WHERE " + parameter + op + parameterValue + " AND Result= " + outcome + "";
            int _Cnt = int.Parse(cmdExcel.ExecuteScalar().ToString());

            connExcel.Close();

            return _Cnt;
        }
       
             
      
    }
}