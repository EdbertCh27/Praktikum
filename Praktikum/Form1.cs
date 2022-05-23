using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Praktikum
{
    public partial class FormUpdatePemain : Form
    {
        public FormUpdatePemain()
        {
            InitializeComponent();
        }

        MySqlConnection sqlConnect = new MySqlConnection("server=localhost;uid=root;pwd=;database=premier_league");
        MySqlCommand sqlCommand;
        MySqlDataAdapter sqlAdapter;
        String sqlQuery;
        DataTable dtTypePemain = new DataTable();
        int PosisiSekarang = 0;

        public static int counter = 0;

        private void FormViewPemain_Load(object sender, EventArgs e)
        {

            sqlQuery = "select p.player_id as `Player`, p.player_name as `Player Name`, p.birthdate as `Birthdate`, n.nation as `Nationality`, n.nationality_id as `Nationality ID`, t.team_name as `Team`, t.team_id as `Team ID`,p.team_number as `Team Number` from player p, nationality n, team t where n.nationality_id = p.nationality_id and t.team_id = p.team_id";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtTypePemain);
            

            sqlQuery = "SELECT nation as 'Nama Negara', nationality_id FROM nationality";
            DataTable dtNationalitycBox = new DataTable();
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtNationalitycBox);

            sqlQuery = "SELECT team_name as 'Nama Team', team_id FROM team";
            DataTable dtTeamcBox = new DataTable();
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtTeamcBox);

            comboBoxNation.DataSource = dtNationalitycBox;
            comboBoxTeam.DataSource = dtTeamcBox;

            comboBoxNation.ValueMember = "nationality_id";
            comboBoxTeam.ValueMember = "team_id";

            comboBoxNation.DisplayMember = "Nama Negara";
            comboBoxTeam.DisplayMember = "Nama Team";


           
            comboBoxNation.ValueMember = "nationality_id";
            comboBoxTeam.ValueMember = "team_id";

            comboBoxNation.DisplayMember = "Nama Negara";
            comboBoxTeam.DisplayMember = "Nama Team";

            IsiDataPemain(0);

        }

        public void IsiDataPemain(int Posisi)
        {
            tBox_ID.Text = dtTypePemain.Rows[Posisi][0].ToString();
            tBox_Nama.Text = dtTypePemain.Rows[Posisi][1].ToString();
            dateTimePickerBirthDate.Text = dtTypePemain.Rows[Posisi][2].ToString();
            comboBoxNation.Text = dtTypePemain.Rows[Posisi][3].ToString();
            comboBoxTeam.Text = dtTypePemain.Rows[Posisi][5].ToString();
            numericUpDownTeamNum.Text = dtTypePemain.Rows[Posisi][7].ToString();
            PosisiSekarang = Posisi;


        }

        private void btn_First_Click(object sender, EventArgs e)
        {
            IsiDataPemain(0);
            
        }

        private void btn_Last_Click(object sender, EventArgs e)
        {
            IsiDataPemain(dtTypePemain.Rows.Count - 1);
            
            
        }

        private void btn_Prev_Click(object sender, EventArgs e)
        {
            if (PosisiSekarang > 0)
            {
                comboBoxNation.ValueMember = "nationality_id";
                comboBoxTeam.ValueMember = "team_id";

                comboBoxNation.DisplayMember = "Nama Negara";
                comboBoxTeam.DisplayMember = "Nama Team";

                PosisiSekarang--;
                IsiDataPemain(PosisiSekarang);
                counter--;
                
            }
            else
            {
                MessageBox.Show("Data Sudah Data Pertama");
            }

        }

        private void btn_Next_Click(object sender, EventArgs e)
        {
            if (PosisiSekarang < dtTypePemain.Rows.Count - 1)
            {
                comboBoxNation.ValueMember = "nationality_id";
                comboBoxTeam.ValueMember = "team_id";

                comboBoxNation.DisplayMember = "Nama Negara";
                comboBoxTeam.DisplayMember = "Nama Team";

                PosisiSekarang++;
                IsiDataPemain(PosisiSekarang);               
                
            }
            else
            {
                MessageBox.Show("Data Sudah Data Terakhir");
            }

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if(labelTeamNumAv.Text == "Not Available")
            {
                MessageBox.Show("Nomor Punggung Sudah digunakan");
            }
            else
            {
                string sqlUpdateQuery = "UPDATE player SET('" + tBox_ID.Text + "', '" + tBox_Nama.Text + "', '" + dateTimePickerBirthDate.Value.ToString("yyyyMMdd") + "', '" + comboBoxNation.SelectedValue.ToString() + "', '" + comboBoxTeam.SelectedValue.ToString() + "', '" + numericUpDownTeamNum.Value.ToString() + "' where player_id = '" + tBox_ID.Text + "'";
                //sqlConnect.Open();
                sqlCommand = new MySqlCommand(sqlUpdateQuery, sqlConnect);
                sqlCommand.ExecuteNonQuery();
                //sqlConnect.Close();
                MessageBox.Show("Data pelajar baru bernama " + tBox_Nama.Text + " berhasil di Update.");
            }
            
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void numericUpDownTeamNum_ValueChanged(object sender, EventArgs e)
        {
            sqlQuery = $"SELECT * FROM player WHERE team_id ='{comboBoxTeam.SelectedValue}' and team_number = {numericUpDownTeamNum.Value}";
            DataTable dtTeamNumber = new DataTable();
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtTeamNumber);

            if (dtTeamNumber.Rows.Count > 0)
            {
                labelTeamNumAv.Text = "Not Available";
            }
            else
            {
                labelTeamNumAv.Text = "Available";
            }
        }
    }
}
