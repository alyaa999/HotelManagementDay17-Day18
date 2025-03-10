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
using System.Configuration;
using Twilio;
using Twilio.Clients;
using Hotel_Manager.DbContext;
namespace Hotel_Manager
{
    public partial class Frontend : MetroForm
    {
        string AccountSid = "ACcb86dacb791bef978628a2e16b1f7a24";
        string AuthToken = "3f344a07336d2e0ac5e467f72a1e650d";
        string RecvPhoneNumber = "";
        private ResrvationRepo resrvationRepo;
        public Frontend()
        {
            InitializeComponent();
            CenterToScreen();
            entryDatePicker.MinDate = DateTime.Today;
            depDatePicker.MinDate = DateTime.Today.AddDays(1);
            resrvationRepo = new ResrvationRepo();

            LoadForDataGridView();
            GetOccupiedRoom();
            ReservedRoom();
            getChecked();

            this.roomOccupiedListBox.DrawMode = DrawMode.OwnerDrawFixed;
            this.roomOccupiedListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.roomOccupiedListBox_DrawItem);
            this.roomReservedListBox.DrawMode = DrawMode.OwnerDrawFixed;
            this.roomReservedListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.roomReservedListBox_DrawItem);
        }



        private void roomOccupiedListBox_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            this.roomOccupiedListBox.IntegralHeight = false;
            this.roomOccupiedListBox.ItemHeight = 25;
            e.DrawBackground();
            Font listBoxFont;
            Brush brush;

            int i = e.Index;

            listBoxFont = new Font("Segoe UI Symbol", 12.0f, FontStyle.Regular);
            brush = Brushes.Black;
            e.Graphics.DrawString(roomOccupiedListBox.Items[i].ToString(), listBoxFont, brush, e.Bounds, StringFormat.GenericTypographic);

        }
        private void roomReservedListBox_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }

            this.roomReservedListBox.IntegralHeight = false;
            this.roomReservedListBox.ItemHeight = 25;
            e.DrawBackground();
            Font listBoxFont = new Font("Segoe UI Symbol", 12.0f, FontStyle.Regular);
            Brush brush = Brushes.Black;
            e.Graphics.DrawString(roomReservedListBox.Items[e.Index].ToString(), listBoxFont, brush, e.Bounds, StringFormat.GenericTypographic);
        }


        private string getval;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Getval
        {
            get { return getval; }
            set { getval = value; }
        }

        public string towelS, cleaningS, surpriseS;

        public int foodBill;
        public string birthday = "";

        public string food_menu = "";
        public int totalAmount = 0;
        public int checkin = 0;
        public int foodStatus = 0;
        public Int32 primartyID = 0;
        public Boolean taskFinder = false;
        public Boolean editClicked = false;
        public string FPayment, FCnumber, FcardExpOne, FcardExpTwo, FCardCVC;
        private double finalizedTotalAmount = 0.0;
        private string paymentType;
        private string paymentCardNumber;
        private string MM_YY_Of_Card;
        private string CVC_Of_Card;
        private string CardType;

        private int lunch = 0; private int breakfast = 0; private int dinner = 0;
        private string cleaning; private string towel;
        private string surprise;

        private void MainTab_Load(object sender, EventArgs e)
        {
            foodSupplyCheckBox.Enabled = false;
           
        }

        public void foodMenuButton_Click(object sender, EventArgs e)
        {

            FoodMenu food_menu = new FoodMenu();
            if (taskFinder)
            {
                if (breakfast > 0)
                {
                    food_menu.breakfastCheckBox.Checked = true;
                    food_menu.breakfastQTY.Text = Convert.ToString(breakfast);
                }
                if (lunch > 0)
                {
                    food_menu.lunchCheckBox.Checked = true;

                    food_menu.lunchQTY.Text = Convert.ToString(lunch);
                }
                if (dinner > 0)
                {
                    food_menu.dinnerCheckBox.Checked = true;
                    food_menu.dinnerQTY.Text = Convert.ToString(dinner);
                }
                if (cleaning == "1")
                {
                    food_menu.cleaningCheckBox.Checked = true;
                }
                if (towel == "1")
                {
                    food_menu.towelsCheckBox.Checked = true;
                }
                if (surprise == "1")
                {
                    food_menu.surpriseCheckBox.Checked = true;
                }
            }
            food_menu.ShowDialog();

            breakfast = food_menu.BreakfastQ;
            lunch = food_menu.LunchQ;
            dinner = food_menu.DinnerQ;

            cleaning = food_menu.Cleaning.Replace(" ", string.Empty) == "Cleaning" ? "1" : "0";
            towel = food_menu.Towel.Replace(" ", string.Empty) == "Towels" ? "1" : "0";

            surprise = food_menu.Surprise.Replace(" ", string.Empty) == "Sweetestsurprise" ? "1" : "0";

            if (breakfast > 0 || lunch > 0 || dinner > 0)
            {
                int bfast = 7 * breakfast;
                int Lnch = 15 * lunch;
                int di_ner = 15 * dinner;
                foodBill = bfast + Lnch + di_ner;
            }
        }
     
        private void roomTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (roomTypeComboBox.SelectedItem.Equals("Single"))
            {
                totalAmount = 149;
                floorComboBox.SelectedItem = "1";
            }
            else if (roomTypeComboBox.SelectedItem.Equals("Double"))
            {
                totalAmount = 299;
                floorComboBox.SelectedItem = "2";
            }
            else if (roomTypeComboBox.SelectedItem.Equals("Twin"))
            {
                totalAmount = 349;
                floorComboBox.SelectedItem = "3";
            }
            else if (roomTypeComboBox.SelectedItem.Equals("Duplex"))
            {
                totalAmount = 399;
                floorComboBox.SelectedItem = "4";
            }
            else if (roomTypeComboBox.SelectedItem.Equals("Suite"))
            {
                totalAmount = 499;
                floorComboBox.SelectedItem = "5";
            }
            int selectedTemp = 0;
            string selected;
            bool temp = int.TryParse(qtGuestComboBox.SelectedItem.ToString(), out selectedTemp);
            if (!temp)
            {
                MessageBox.Show(this, "Enter # of guests first", "Error parsing", MessageBoxButtons.OK);
            }
            else
            {
                selected = qtGuestComboBox.SelectedItem.ToString();
                selectedTemp = Convert.ToInt32(selected);
                if (selectedTemp >= 3)
                {
                    totalAmount += (selectedTemp * 5);
                }
            }


        }

        private void editButton_Click(object sender, EventArgs e)
        {
            editClicked = true;
            entryDatePicker.MinDate = new DateTime(2014, 4, 1);
            entryDatePicker.CustomFormat = "MM-dd-yyyy";
            entryDatePicker.Format = DateTimePickerFormat.Custom;

            depDatePicker.MinDate = new DateTime(2014, 4, 1);
            depDatePicker.CustomFormat = "MM-dd-yyyy";
            depDatePicker.Format = DateTimePickerFormat.Custom;

            submitButton.Visible = false;
            updateButton.Visible = true;
            deleteButton.Visible = true;
            resEditButton.Visible = true;

            ComboBoxItemsFromDataBase();
            LoadForDataGridView();
            reset_frontend();
        }

        
        private void finalizeButton_Click(object sender, EventArgs e)
        {
            if (breakfast == 0 && lunch == 0 && dinner == 0 && cleaning == "0" && towel == "0" && surprise == "0")
            {
                foodSupplyCheckBox.Checked = true;
            }
            updateButton.Enabled = true;

            FinalizePayment finalizebil = new FinalizePayment();
            finalizebil.totalAmountFromFrontend = totalAmount;
            finalizebil.foodBill = foodBill;
            if (taskFinder)
            {
                finalizebil.paymentComboBox.SelectedItem = FPayment.Replace(" ", string.Empty);
                finalizebil.phoneNComboBox.Text = FCnumber;
                finalizebil.monthComboBox.SelectedItem = FcardExpOne;
                finalizebil.yearComboBox.SelectedItem = FcardExpTwo;
                finalizebil.cvcComboBox.Text = FCardCVC;
            }

            finalizebil.ShowDialog();

            finalizedTotalAmount = finalizebil.FinalTotalFinalized;
            paymentType = finalizebil.PaymentType;
            paymentCardNumber = finalizebil.PaymentCardNumber;
            MM_YY_Of_Card = finalizebil.MM_YY_Of_Card1;
            CVC_Of_Card = finalizebil.CVC_Of_Card1;
            CardType = finalizebil.CardType1;

            if (!editClicked)
            {
                submitButton.Visible = true;
            }
        }

        //private void SendSMS(int secondUserID)
        //{
        //    //creating an instance of twilio rest
        //    var twilio = new TwilioRestClient(AccountSid, AuthToken);
        //    string name = firstNameTextBox.Text + " " + lastNameTextBox.Text;

        //    string end_num = paymentCardNumber.Substring(paymentCardNumber.Length - Math.Min(4, paymentCardNumber.Length));

        //    if (smsCheckBox.Checked)
        //    {
        //        //building message for twilio
        //        string buildMesage = "Hello " + name + "! Your unique ID# " + secondUserID + " Total bill of $" + finalizedTotalAmount + " is charged on your card # ending-" + end_num;
        //        //creating message 
        //        var message = twilio.SendMessage("+12034562736", RecvPhoneNumber, buildMesage, "");
        //        //sending message
        //        Console.WriteLine(message.Sid);
        //        smsCheckBox.Text = "SMS Sent";
        //    }
        //}
      
        private  void submitButton_Click(object sender, EventArgs e)
        {
            birthday = (monthComboBox.SelectedItem) + "-" + (dayComboBox.SelectedIndex + 1) + "-" + yearTextBox.Text;
         
            Reservation reservation = new Reservation();
            reservation.LastName = lastNameTextBox.Text;
            reservation.FirstName = firstNameTextBox.Text;
            reservation.City = cityTextBox.Text;
            reservation.Gender = genderComboBox.SelectedItem.ToString();
            reservation.BirthDay =birthday;
            reservation.PhoneNumber = phoneNumberTextBox.Text;
            reservation.EmailAddress = emailTextBox.Text;
            reservation.NumberGuest = qtGuestComboBox.SelectedIndex + 1;
            reservation.AptSuite = aptTextBox.Text;
            reservation.StreetAddress = addLabel.Text;
            reservation.State = stateComboBox.SelectedItem.ToString();
            reservation.ZipCode = zipComboBox.Text;
            reservation.RoomType = roomTypeComboBox.SelectedItem.ToString();
            reservation.RoomFloor = floorComboBox.SelectedItem.ToString();
            reservation.RoomNumber = roomNComboBox.SelectedItem.ToString();
            reservation.TotalBill = (double)finalizedTotalAmount;
            reservation.PaymentType = paymentType;
            reservation.CardType = CardType;
            reservation.CardNumber = paymentCardNumber;
            reservation.CardExp = MM_YY_Of_Card;
            reservation.CardCvc = CVC_Of_Card;
            reservation.ArrivalTime = Convert.ToDateTime(entryDatePicker.Text);
            reservation.LeavingTime = Convert.ToDateTime(depDatePicker.Text);   
            reservation.BreakFast = breakfast > 0 ? 1 : 0;
            reservation.Lunch = lunch > 0 ? 1: 0;
            reservation.Dinner = dinner > 0 ? 1 : 0;
            reservation.CheckIn = checkin > 0 ? true: false;
            reservation.Cleaning = cleaning == "1" ? true : false;
            reservation.Towel = towel == "1" ? true : false;
            reservation.SpecialSurprise = surprise == "1" ? true : false;
            reservation.SupplyStatus = foodStatus > 0 ? true : false;
            reservation.FoodBill = foodBill;
           

            try
            {
                int getIDBack =  resrvationRepo.AddReservation(reservation);


                string userID = Convert.ToString(getIDBack);
                //SendSMS(getIDBack);
                MessageBox.Show(this, "Entry successfully inserted into database. " + "\n\n" +
                    "Provide this unique ID to the customer." + "\n\n" +
                "USER UNIQUE ID: " + userID, "Report", MessageBoxButtons.OK, MessageBoxIcon.Question);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ComboBoxItemsFromDataBase();
            LoadForDataGridView();
            reset_frontend();
            GetOccupiedRoom();
            ReservedRoom();
            getChecked();
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            submitButton.Visible = false;
            updateButton.Visible = false;
            deleteButton.Visible = false;
            resEditButton.Visible = false;
            reset_frontend();
        }
        public void ClearAllTextBoxes(Control controls)
        {
            foreach (Control control in controls.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Clear();
                }
                if (control.HasChildren)
                {
                    ClearAllTextBoxes(control);
                }
            }
        }
        public void ClearAllComboBox(Control controls){
            foreach(Control control in controls.Controls){
                if (control == roomTypeComboBox)
                {
                    continue;
                }
                else if(control is ComboBox){
                    ((ComboBox)control).SelectedIndex = -1;
                }
                if (control.HasChildren) {
                    ClearAllComboBox(control);
                }
            }
        }
        private void reset_frontend()
        {
            try
            {

                resEditButton.Items.Clear();
                checkinCheckBox.Checked = false;
                foodSupplyCheckBox.Checked = false;

                ClearAllComboBox(this);
                ClearAllTextBoxes(this);

                ComboBoxItemsFromDataBase();

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void frontend_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private  void deleteButton_Click(object sender, EventArgs e)
        {
            if (primartyID > 1000)
            {
                var result =  resrvationRepo.DeleteReservation(primartyID);
                if (result)
                {
                    MessageBox.Show(this, $"Reservation for the UNIQUE USER ID: {primartyID} is DELETED.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    MessageBox.Show(this, "Selected ID doesn't exist.", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(this, "Selected ID doesn't exist.", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
            }

            ComboBoxItemsFromDataBase();
            LoadForDataGridView();
            reset_frontend();
            GetOccupiedRoom();
            ReservedRoom();
            getChecked();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            Reservation reservation = new Reservation();

            reservation.FirstName = firstNameTextBox.Text;
            reservation.LastName = lastNameTextBox.Text;
            reservation.BirthDay = birthday;
            reservation.Gender = genderComboBox.SelectedItem.ToString();
            reservation.PhoneNumber = phoneNumberTextBox.Text;
            reservation.EmailAddress = emailTextBox.Text;
            reservation.NumberGuest = qtGuestComboBox.SelectedIndex + 1;
            reservation.StreetAddress = addLabel.Text;
            reservation.AptSuite = aptTextBox.Text;
            reservation.City = cityTextBox.Text;
            reservation.State = stateComboBox.SelectedItem.ToString();
            reservation.ZipCode = zipComboBox.Text;
            reservation.RoomType = roomTypeComboBox.SelectedItem.ToString();
            reservation.RoomFloor = floorComboBox.SelectedItem.ToString();
            reservation.RoomNumber = roomNComboBox.SelectedItem.ToString();
            reservation.TotalBill = (double)finalizedTotalAmount;
            reservation.PaymentType = paymentType;
            reservation.CardType = CardType;
            reservation.CardNumber = paymentCardNumber;
            reservation.CardExp = MM_YY_Of_Card;
            reservation.CardCvc = CVC_Of_Card;
            reservation.ArrivalTime = Convert.ToDateTime(entryDatePicker.Value) ;
            reservation.LeavingTime = Convert.ToDateTime(depDatePicker.Value) ;
            reservation.BreakFast = breakfast > 0 ? 1 : 0;
            reservation.Lunch = lunch > 0 ? 1 : 0;
            reservation.Dinner = dinner > 0 ? 1 : 0;
            reservation.CheckIn = checkin > 0;
            reservation.Cleaning = cleaning == "1";
            reservation.Towel = towel == "1";
            reservation.SpecialSurprise = surprise == "1";
            reservation.SupplyStatus = foodStatus > 0 ? true : false;
            reservation.FoodBill = foodBill;
            reservation.Id = primartyID;    
            try
            {
                resrvationRepo.UpdateReservation(reservation);

                string userID = Convert.ToString(primartyID);

                MessageBox.Show(this, "Entry successfully updated into database. For the UNIQUE USER ID of: " + "\n\n" +
                " " + userID, "Report", MessageBoxButtons.OK, MessageBoxIcon.Question);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ComboBoxItemsFromDataBase();

            reset_frontend();
            LoadForDataGridView();
            GetOccupiedRoom();
            ReservedRoom();
            getChecked();
        }

        private void checkinCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (checkinCheckBox.Checked)
            {
                checkinCheckBox.Text = "Checked in";
                checkin = 1;
            }
            else
            {
                checkin = 0;
                checkinCheckBox.Text = "Check in ?";
            }
        }
        // ado query
        private  void resEditButton_SelectedIndexChanged(object sender, EventArgs e)
        {
            getChecked();
            string getQuerystring = resEditButton.Text.Substring(0, 4).Replace(" ", string.Empty);



            try
            {
                var item =  resrvationRepo.GetReservationByRoomNumber(getQuerystring);


                taskFinder = true;
                    string ID = item.Id.ToString();
                    string first_name = item.FirstName.ToString();
                    string last_name = item.LastName.ToString();
                    string birth_day = item.BirthDay.ToString();
                    string gender = item.Gender.ToString();
                    string phone_number = item.PhoneNumber.ToString();
                    string email_address = item.EmailAddress.ToString();
                    string number_guest = item.NumberGuest.ToString();
                    string street_address = item.StreetAddress.ToString();
                    string apt_suite = item.AptSuite.ToString();
                    string city = item.City.ToString();
                    string state = item.State.ToString();
                    string zip_code = item.ZipCode.ToString();

                    string room_type = item.RoomType.ToString();
                    string room_floor = item.RoomFloor.ToString();
                    string room_number = item.RoomNumber.ToString();

                    string payment_type = item.PaymentType.ToString();
                    string card_number = item.CardNumber.ToString(
                        
                        );
                    string card_exp = item.CardExp.ToString();
                    string card_cvc = item.CardCvc.ToString();

                    string _cleaning = item.Cleaning.ToString();
                    string _towel = item.Towel.ToString();
                    string _surprise = item.SpecialSurprise.ToString();
                    if (_cleaning == "True")
                    {
                        cleaning = "1";
                    }
                    else { cleaning = "0"; }

                    if (_towel == "True")
                    {
                        towel = "1";
                    }
                    else { towel = "0"; }
                    if (_surprise == "True")
                    {
                        surprise = "1";
                    }
                    else
                    {
                        surprise = "0";
                    }
                    roomNComboBox.Items.Add(room_number);
                    roomNComboBox.SelectedItem = room_number;

                    FPayment = payment_type; FCnumber = card_number;
                    FCardCVC = card_cvc; FcardExpOne = card_exp.Substring(0, card_exp.IndexOf('/'));
                    FcardExpTwo = card_exp.Substring(card_exp.Length - Math.Min(2, card_exp.Length));
                    string check_in = item.CheckIn.ToString();

                    string supply_status =item.SupplyStatus.ToString();
                    string food_billD = item.FoodBill.ToString();

                    string arrival_date = Convert.ToDateTime(item.ArrivalTime).ToString("MM-dd-yyyy").Replace(" ", string.Empty);
                    entryDatePicker.Value = Convert.ToDateTime(arrival_date);

                    string leaving_date = Convert.ToDateTime(item.LeavingTime).ToString("MM-dd-yyyy").Replace(" ", string.Empty);
                    depDatePicker.Value = Convert.ToDateTime(leaving_date);
                    entryDatePicker.Value.ToShortDateString();
                    depDatePicker.Value.ToShortDateString();

                    string _breakfast = item.BreakFast.ToString();
                    string _lunch = item.Lunch.ToString();
                    string _dinner = item.Dinner.ToString();

                    double Num;
                    bool isNum = double.TryParse(_lunch, out Num);
                    if (isNum)
                    {
                        lunch = Int32.Parse(_lunch);
                    }
                    else
                    {
                        lunch = 0;
                    }
                    isNum = double.TryParse(_breakfast, out Num);
                    if (isNum)
                    {
                        breakfast = Int32.Parse(_breakfast);
                    }
                    else
                    {
                        breakfast = 0;
                    }
                    isNum = double.TryParse(_dinner, out Num);
                    if (isNum)
                    {
                        dinner = Int32.Parse(_dinner);
                    }
                    else
                    {
                        dinner = 0;
                    }



                    foodBill = Convert.ToInt32(food_billD);

                    if (supply_status == "True")
                    {
                        foodSupplyCheckBox.Checked = true;
                    }
                    else
                    {
                        foodSupplyCheckBox.Checked = false;
                    }


                    firstNameTextBox.Text = first_name;
                    lastNameTextBox.Text = last_name;
                    phoneNumberTextBox.Text = phone_number;
                    genderComboBox.SelectedItem = gender;

                    monthComboBox.SelectedItem = birth_day.Substring(0, birth_day.IndexOf('-'));
                    dayComboBox.SelectedItem = birth_day.Substring(birth_day.IndexOf('-') + 1, 2);
                    yearTextBox.Text = birth_day.Substring(birth_day.Length - Math.Min(4, birth_day.Length));

                    emailTextBox.Text = email_address;
                    qtGuestComboBox.SelectedItem = number_guest;
                    addLabel.Text = street_address;
                    aptTextBox.Text = apt_suite;
                    cityTextBox.Text = city;
                    stateComboBox.SelectedItem = state;
                    zipComboBox.Text = zip_code;
                    roomTypeComboBox.SelectedItem = room_type.Replace(" ", string.Empty);
                    floorComboBox.SelectedItem = room_floor.Replace(" ", string.Empty);
                    roomNComboBox.SelectedItem = room_number.Replace(" ", string.Empty);

                    if (check_in == "True")
                    {
                        checkinCheckBox.Checked = true;
                    }
                    else
                    {
                        checkinCheckBox.Checked = false;
                    }


                    primartyID = Convert.ToInt32(ID);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("COMBOBOX Selection: + " + ex.Message);
            }
        }

        private  void ComboBoxItemsFromDataBase()
        {
            
            try
            {
                var reservations =  resrvationRepo.GetAllReservations();
                foreach (var room in reservations)
                {
                    var roomDetails = $"{room.RoomNumber} | {room.RoomType} | Floor: {room.RoomFloor} | " +
                                      $"{room.FirstName} {room.LastName} | Guests: {room.NumberGuest} | " +
                                      $"Check-In: {room.ArrivalTime:MM/dd/yyyy} | Check-Out: {room.LeavingTime:MM/dd/yyyy} | " +
                                      $"Total Bill: {room.TotalBill:C} | Payment: {room.PaymentType} | " +
                                      $"Food Bill: {room.FoodBill:C} | Cleaning: {(room.Cleaning == true ? "Yes" : "No")} | " +
                                      $"Special Surprise: {(room.SpecialSurprise == true ? "Yes" : "No")}";

                    resEditButton.Items.Add(roomDetails);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadForDataGridView()
        {
            try
            {
               
                BindingSource bindingSource = new BindingSource();
                var reservations = resrvationRepo.GetAllReservations();
                resTotalDataGridView.DataSource = bindingSource;
            }
            catch (Exception)
            {
                MessageBox.Show(this, "Error loading data", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.None);
            }
        }

        private void foodSupplyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (foodSupplyCheckBox.Checked)
            {
                foodSupplyCheckBox.Text = "Food/Supply: Complete";
                foodStatus = 1;
            }
            else
            {
                foodStatus = 0;
                foodSupplyCheckBox.Text = "Food/Supply status?";
            }
        }

        private  void GetOccupiedRoom()
         {
            roomOccupiedListBox.Items.Clear();

            try
            {
                var OccupiedRooms =  resrvationRepo.GetOccupiedReservation(true);
                foreach (var room in OccupiedRooms)
                {
                    var roomDetails = $"{room.RoomNumber} | {room.RoomType} | Floor: {room.RoomFloor} | " +
                                      $"{room.FirstName} {room.LastName} | Guests: {room.NumberGuest} | " +
                                      $"Check-In: {room.ArrivalTime:MM/dd/yyyy} | Check-Out: {room.LeavingTime:MM/dd/yyyy} | " +
                                      $"Total Bill: {room.TotalBill:C} | Payment: {room.PaymentType} | " +
                                      $"Food Bill: {room.FoodBill:C} | Cleaning: {(room.Cleaning == true ? "Yes" : "No")} | " +
                                      $"Special Surprise: {(room.SpecialSurprise == true ? "Yes" : "No")}";

                   roomOccupiedListBox.Items.Add(roomDetails);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private   void ReservedRoom()
        {
            roomReservedListBox.Items.Clear();

            try
            {

                var reservedRooms =  resrvationRepo.GetOccupiedReservation(false);
                foreach (var room in reservedRooms)
                {
                    var roomDetails = $"{room.RoomNumber} | {room.RoomType} | Floor: {room.RoomFloor} | " +
                                      $"{room.FirstName} {room.LastName} | Guests: {room.NumberGuest} | " +
                                      $"Check-In: {room.ArrivalTime:MM/dd/yyyy} | Check-Out: {room.LeavingTime:MM/dd/yyyy} | " +
                                      $"Total Bill: {room.TotalBill:C} | Payment: {room.PaymentType} | " +
                                      $"Food Bill: {room.FoodBill:C} | Cleaning: {(room.Cleaning == true ? "Yes" : "No")} | " +
                                      $"Special Surprise: {(room.SpecialSurprise == true ? "Yes" : "No")}";

                    roomReservedListBox.Items.Add(roomDetails);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        

        private  void getChecked()
        {
            List<string> TakenRoomList = new List<string>();



            try
            {
                var roomList = resrvationRepo.GetReservationByRoomNumber();

                if (roomList != null && roomList.Any())
                {
                    foreach (var room in roomList)
                    {
                        var roomDetails = $"{room.RoomNumber} | {room.RoomType} | Floor: {room.RoomFloor} | " +
                                          $"{room.FirstName} {room.LastName} | Guests: {room.NumberGuest} | " +
                                          $"Check-In: {room.ArrivalTime:MM/dd/yyyy} | Check-Out: {room.LeavingTime:MM/dd/yyyy} | " +
                                          $"Total Bill: {room.TotalBill:C} | Payment: {room.PaymentType} | " +
                                          $"Food Bill: {room.FoodBill:C} | Cleaning: {(room.Cleaning == true ? "Yes" : "No")} | " +
                                          $"Special Surprise: {(room.SpecialSurprise == true ? "Yes" : "No")}";

                        TakenRoomList.Add(roomDetails);
                    }
                }
                else
                {
                    TakenRoomList.Add("No occupied rooms found.");
                }

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            foreach (string roomList in TakenRoomList)
            {
                if (roomNComboBox.Items.Contains(roomList))
                {

                    int temp = roomNComboBox.Items.IndexOf(roomList);
                    roomNComboBox.Items.RemoveAt(temp);
                }
            }
        }

        private void phoneNumberTextBox_Leave(object sender, EventArgs e)
        {
            RecvPhoneNumber = "+1"+phoneNumberTextBox.Text.Replace(" ", string.Empty);
            long getphn = Convert.ToInt64(phoneNumberTextBox.Text);
            string formatString = String.Format("{0:(000)000-0000}", getphn);
            phoneNumberTextBox.Text = formatString;
        }
        private  void searchButton_Click(object sender, EventArgs e)
        {
            
           
        
             var reservations =   resrvationRepo.GetReservationBySearch(searchTextBox.Text);
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = reservations;
            searchDataGridView.DataSource = bindingSource;

            if (reservations.Count()>0)
            {
                searchButton.Location = new Point(752, 7);
                searchTextBox.Location = new Point(68, 7);
                searchDataGridView.Visible = true;
                SearchError.Visible = false;

            }
            else
            {
                searchDataGridView.Visible = false;
                SearchError.Visible = true;
                SearchError.Text = "SORRY DUDE :(" +"\n"
                    +"I ran out of gas trying to search for "+ searchTextBox.Text +"\n"
                +"I sure will find it next time."; 
            }
        }

    }
}
