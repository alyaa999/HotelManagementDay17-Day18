using MetroFramework.Forms;
using MetroFramework.Fonts;
using MetroFramework.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Hotel_Manager.DbContext;
using Twilio.Types;
namespace Hotel_Manager
{
    public partial class Kitchen : MetroForm
    {
        bool cleaning, towel, surprise;
        string queryString;
        int breakfast, lunch, dinner, foodBill;
        public Int32 primaryID;
        double totalBill;
        bool supply_status = false;

        SqlConnection connection = new SqlConnection(Hotel_Manager.Properties.Settings.Default.frontend_reservationConnectionString);
        SqlCommand query;
        SqlDataReader reader;

        ResrvationRepo resrvationRepo = new ResrvationRepo();   
        public Kitchen()
        {
            InitializeComponent();

         
        }
        private void kitchen_Load(object sender, EventArgs e)
        {
            LoadForDataGridView();
            listBoxFromDataBase();
        }

        private  void LoadForDataGridView()
        {
            if (connection.State != ConnectionState.Open)
            {

                try
                {

                    var reservations = resrvationRepo.GetReservationBySupplyStatus();
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = reservations;
                    overviewDataGridView.DataSource = bindingSource;
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "Error loading data", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.None);
                }
            }
            else
            {
                MessageBox.Show(this, "SQL Connection is already open", "ERROR", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
            }
        }

        private void resetEntries(Control controls)
        {
            foreach (Control control in controls.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Clear();
                }
                if (control.HasChildren)
                {
                    resetEntries(control);
                }
            }

        }
        private  void listBoxFromDataBase()
        {

            queueListBox.Items.Clear();
            
                try
                {
                    var reservations =  resrvationRepo.GetReservationBySupplyStatus();
                    foreach(var item in reservations)
                    {
                    queueListBox.Items.Add(item.Id + "  | " + item.FirstName + "  " + item.LastName + " | " + item.PhoneNumber);

                    }



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            
          
        }

        private  void queueListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
             resetEntries(this);
                string getQuerystring = queueListBox.Text.Substring(0, 4).Replace(" ", string.Empty);
                //queryString = "Select * from reservation where Id= '" + getQuerystring + "'";
                
                try
                {
                     var item = resrvationRepo.GetReservationById(Convert.ToInt32 (getQuerystring));

               
                        string ID = item.Id.ToString();
                        string first_name = item.FirstName.ToString();
                        string last_name = item.LastName.ToString();
                        string phone_number = item.PhoneNumber.ToString();
                        string foodBillD = item.FoodBill.ToString();
                        string total_bill = item.TotalBill.ToString().Replace(" ", string.Empty);
                        string room_type = item.RoomType.ToString();
                        string room_floor = item.RoomFloor.ToString();
                        string room_number = item.RoomNumber.ToString();

                        string _cleaning = item.Cleaning.ToString();
                        string _towel = item.Towel.ToString();
                        string _surprise = item.SpecialSurprise.ToString();



                        if (_cleaning == "True")
                        {
                            cleaning = true;
                            cleaningCheckBox.Checked = true;
                        }
                        else { cleaning = false; }

                        if (_towel == "True")
                        {
                            towel = true;
                            towelCheckBox.Checked = true;
                        }
                        else { towel = false; }
                        if (_surprise == "True")
                        {
                            surprise = true;
                            surpriseCheckBox.Checked = true;
                        }
                        else
                        {
                            surprise = false;
                        }

                        string supply_status =item.SupplyStatus.ToString();
                        string food_billD = item.FoodBill.ToString();

                        string _breakfast = item.BreakFast.ToString();
                        string _lunch = item.Lunch.ToString();
                        string _dinner = item.Dinner.ToString();

                        double Num;
                        bool isNum = double.TryParse(_lunch, out Num);
                        if (isNum)
                        {
                            lunch = Int32.Parse(_lunch);
                            lunchTextBox.Text = Convert.ToString(lunch);
                        }
                        else
                        {
                            lunch = 0;
                            lunchTextBox.Text = "NONE";
                        }
                        isNum = double.TryParse(_breakfast, out Num);
                        if (isNum)
                        {
                            breakfast = Int32.Parse(_breakfast);
                            breakfastTextBox.Text = Convert.ToString(breakfast);
                        }
                        else
                        {
                            breakfast = 0;
                            breakfastTextBox.Text = "NONE";
                        }
                        isNum = double.TryParse(_dinner, out Num);
                        if (isNum)
                        {
                            dinner = Int32.Parse(_dinner);
                            dinnerTextBox.Text = Convert.ToString(dinner);
                        }
                        else
                        {
                            dinner = 0;
                            dinnerTextBox.Text = "NONE";
                        }

                        if (supply_status == "True")
                        {
                            supplyCheckBox.Checked = true;
                        }
                        else
                        {
                            supplyCheckBox.Checked = false;
                        }

                        firstNameTextBox.Text = first_name;
                        lastNameTextBox.Text = last_name;
                        phoneNTextBox.Text = phone_number;
                        roomTypeTextBox.Text = room_type;
                        floorNTextBox.Text = room_floor;
                        roomNTextBox.Text = room_number;
                        totalBill = Convert.ToDouble(total_bill);
                        foodBill = Convert.ToInt32(foodBillD);
                        totalBill -= foodBill;
                        primaryID = Convert.ToInt32(ID);

                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("COMBOBOX Selection: + " + ex.Message);
                }
        }
            
        

        private void foodSelectionButton_Click(object sender, EventArgs e)
        {

            FoodMenu food_menu = new FoodMenu();
            food_menu.needPanel.Visible = false;

            food_menu.ShowDialog();

            breakfast = food_menu.BreakfastQ;
            lunch = food_menu.LunchQ;
            dinner = food_menu.DinnerQ;

            int bfast= 0, Lnch= 0, di_ner = 0;
            if (breakfast > 0)
            {
                bfast = 7 * breakfast;
            } if (lunch > 0)
            {
                Lnch = 15 * lunch;
            } if (dinner > 0)
            {
                di_ner = 15 * dinner;
            }
            foodBill += (bfast + Lnch + di_ner);
        }

        private   void updateButton_Click(object sender, EventArgs e)
        {
           
            try
            {
                 resrvationRepo.UpdateReservationSupplyStatus(primaryID ,totalBill , breakfast,lunch ,dinner,supply_status,cleaning,towel,surprise,foodBill);
                string userID = Convert.ToString(primaryID);
                reader = query.ExecuteReader();

                MessageBox.Show(this, "Entry successfully updated into database. For the UNIQUE USER ID of: " + "\n\n" +
                " " + userID, "Report", MessageBoxButtons.OK, MessageBoxIcon.Question);

                listBoxFromDataBase();
                LoadForDataGridView();
                resetEntries(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
               
           
        }

        private void supplyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            cleaningCheckBox.Checked = false;
            cleaningCheckBox.Text = "Cleaned";
            towelCheckBox.Checked = false;
            towelCheckBox.Text = "Toweled";
            surpriseCheckBox.Checked = false;
            surpriseCheckBox.Text = "Surprised";
            supply_status = true;
        }
        private void kitchen_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        
    }
}
