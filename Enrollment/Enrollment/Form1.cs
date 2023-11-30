using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Enrollment
{
    public partial class ENROLL : Form
    {
        // Set all value going to use                                                                                     
        // Set the COST PER UNIT  
        double perUnits = 1811.37;

        // Set FEES and its value                                                                                      
        // 1. LABORATORY FEE with the value of ₱ 6504.00                                                                  
        // 2. REGISTRAR CARD with the value of ₱ 1463.35                                                                  
        // 3. PUBLICATION??? with the value of ₱ 216.80                                                                   
        // 4. STUDENT COUNCIL with the value of ₱ 500.00                                                                  
        // 5. STUDENT ID with the value of ₱ 108.40                                                                       
        // 6. MISCELLANEOUS with the value of ₱ 7571.80
        double laboratoryFee = 6504.00;
        double registrarCard = 1463.35;
        double publication = 216.80;
        double studentCouncil = 500.00;
        double studentID = 108.40;
        double miscellaneous = 7571.80;

        // Set SCHOLARSHIP and its value                                                                                  
        // 1. NON-SCHOLAR with the value of 0%                                                                            
        // 2. FULL-SCHOLAR with the value of 100%                                                                         
        // 3. PARTIAL-SCHOLAR with the value of 50%  
        int nonScholar = 0;
        int fullScholar = 100;
        int partialScholar = 50;
        
        double totalUnits;
        double totalFees;
        int totalDiscount;
        string totalAmount;

        //Set USERNAME and PASSWORD in one Dictionary
        private Dictionary<string, string> userAcounts = new Dictionary<string, string>()
        {
            {"Master_Yoda_143", "darthVader"},
            {"Walter-White", "TheOneWhoKnocks"},
            {"RealRyanGosling48", "I_Drive"},
            {"admin123", "admin123"},
            {"1", "2"}
        };

        private void CheckSubmit_btn()
        {
            if (username_tbx.Text == "" || password_tbx.Text == "")
            {
                submit_btn.Enabled = false;
            }
            else
            {
                submit_btn.Enabled = true;
            }
        }

        private void CheckStudentInfo()
        {
            if (studentName_tbx.Text == "" || unitsEnrolled_tbx.Text == "" || yearLevel_cmbbx.SelectedIndex == -1)
            {
                nextPanel_btn.Visible = false;
                compute_btn.Enabled = false;
            }
            else
            {
                nextPanel_btn.Visible = true;
                CheckToCompute();
            }


            if (studentName_tbx.Text != "" || unitsEnrolled_tbx.Text != "" || yearLevel_cmbbx.SelectedIndex != -1)
            {
                clear_btn.Enabled = true;
            }
            else
            {
                clear_btn.Enabled = false;
            }
        }

        private void CheckToCompute()
        {
            if ((laboratoryFee_chckbx.Checked == true || registrarCard_chckbx.Checked == true ||
                publication_chckbx.Checked == true || studentCouncil_chckbx.Checked == true ||
                studentID_chckbx.Checked == true || miscellaneous_chckbx.Checked == true) &&
                (nonScholarship_rbtn.Checked == true || fullScholarship_rbtn.Checked == true ||
                partialScholarship_rbtn.Checked == true))
            {
                compute_btn.Enabled = true;
            }
            else
            {
                compute_btn.Enabled = false;
            }
        }

        private void Next_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (username_tbx.Focused)
                {
                    e.SuppressKeyPress = true;
                    password_tbx.Focus();
                }
                else if (studentName_tbx.Focused)
                {
                    e.SuppressKeyPress = true;
                    unitsEnrolled_tbx.Focus();
                }
            }
        }

        private void Enter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                submit_btn.PerformClick();
            }
        }
        
        private void intOnly(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public ENROLL()
        {
            InitializeComponent();
            this.Size = new Size(871, 521);
            this.AutoScroll = false;
            submit_btn.Enabled = false;
            compute_btn.Enabled = false;
            clear_btn.Enabled = false;
            password_tbx.Size = new Size(346, 34);
            username_tbx.KeyDown += Next_KeyDown;
            password_tbx.KeyDown += Enter_KeyDown;
            studentName_tbx.KeyDown += Next_KeyDown;
            unitsEnrolled_tbx.KeyPress += intOnly;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void openPassword_btn_Click(object sender, EventArgs e)
        {
            if (password_tbx.PasswordChar == '•')
            {
                password_tbx.PasswordChar = '\0';
                openPassword_btn.FillColor = Color.Aqua;
            }
            else
            {
                password_tbx.PasswordChar = '•';
                openPassword_btn.FillColor = Color.FromArgb(179, 198, 255);
            }
        }

        private void submit_btn_Click_1(object sender, EventArgs e)
        {

            if (username_tbx.Text == "" && password_tbx.Text == "")
            {
            }
            else if (userAcounts.ContainsKey(username_tbx.Text) && userAcounts[username_tbx.Text] == password_tbx.Text)
            {
                username_tbx.BorderColor = Color.FromArgb(65, 252, 3);
                password_tbx.BorderColor = Color.FromArgb(65, 252, 3);
                username_tbx.BorderThickness = 2;
                password_tbx.BorderThickness = 2;
                MessageBox.Show("Login successful!");
                panel1.Visible = false;
                panel2.Visible = true;
                panel2.Location = new Point(0, 0);
                loadingTime.Start();
            }
            else if (userAcounts.ContainsKey(username_tbx.Text))
            {
                password_tbx.BorderColor = Color.FromArgb(252, 3, 3);
                password_tbx.BorderThickness = 2;
                MessageBox.Show("Login faild!");
            }
            else
            {
                username_tbx.BorderColor = Color.FromArgb(252, 3, 3);
                password_tbx.BorderColor = Color.FromArgb(252, 3, 3);
                username_tbx.BorderThickness = 2;
                password_tbx.BorderThickness = 2;
                MessageBox.Show("Login faild!");

            }
        }
        
        private void username_tbx_TextChanged(object sender, EventArgs e)
        {
            username_tbx.BorderThickness = 0;
            password_tbx.BorderThickness = 0;
            CheckSubmit_btn();
        }
        
        private void password_tbx_TextChanged(object sender, EventArgs e)
        {
            username_tbx.BorderThickness = 0;
            password_tbx.BorderThickness = 0;

            if (password_tbx.Text == "")
            {
                password_tbx.Size = new Size(346, 34);
                openPassword_btn.Visible = false;
            }
            else
            {
                password_tbx.Size = new Size(298, 34);
                openPassword_btn.Visible = true;
            }
            CheckSubmit_btn();

        }

        private void loadingTime_Tick(object sender, EventArgs e)
        {
            loadingProgressBar.Value += 1;
            if (loadingProgressBar.Value == 100)
            {
                loadingTime.Stop();
                panel2.Visible = false;
                panel3.Visible = true;
                panel3.Location = new Point(0, 0);
                compute_btn.Location = new Point(239, 414);
                clear_btn.Location = new Point(477, 414);
                studentName_tbx.Focus();
            }
        }

        private void nextPanel_btn_Click(object sender, EventArgs e)
        {
            if (studentInfo_Panel.Visible == true)
            {
                studentInfo_Panel.Visible = false;
                fees_Panel.Visible = true;
                fees_Panel.Location = new Point(147, 95);
                prevPanel_btn.Visible = true;
            }
            else if (fees_Panel.Visible == true)
            {
                fees_Panel.Visible = false;
                scholarGrant_Panel.Visible = true;
                scholarGrant_Panel.Location = new Point(147, 95);
                nextPanel_btn.Visible = false;
            }
        }

        private void prevPanel_btn_Click(object sender, EventArgs e)
        {
            if (scholarGrant_Panel.Visible == true)
            {
                scholarGrant_Panel.Visible = false;
                fees_Panel.Visible = true;
                nextPanel_btn.Visible = true;
            }
            else if (fees_Panel.Visible == true)
            {
                fees_Panel.Visible = false;
                studentInfo_Panel.Visible = true;
                prevPanel_btn.Visible = false;
            }
        }

        // Computer for TOTAL AMOUNT with this computation:                                                               
        // TOTAL AMOUNT = ( TOTAL UNITS + TOTAL FEES ) - (( TOTAL UNITS + TOTAL FEES ) / SCHOLARSHIP )                                                                                          
        // Print TOTAL AMOUNT
        private void compute_btn_Click_1(object sender, EventArgs e)
        {
            if (laboratoryFee_chckbx.Checked)
            {
                totalFees += laboratoryFee;
            }

            if (registrarCard_chckbx.Checked)
            {
                totalFees += registrarCard;
            }

            if (publication_chckbx.Checked)
            {
                totalFees += publication;
            }

            if (studentCouncil_chckbx.Checked)
            {
                totalFees += studentCouncil;
            }

            if (studentID_chckbx.Checked)
            {
                totalFees += studentID;
            }

            if (miscellaneous_chckbx.Checked)
            {
                totalFees += miscellaneous;
            }

            if (nonScholarship_rbtn.Checked)
            {
                totalDiscount = nonScholar;
            }
            else if (fullScholarship_rbtn.Checked)
            {
                totalDiscount = fullScholar;
            }
            else if (partialScholarship_rbtn.Checked)
            {
                totalDiscount = partialScholar;
            }

            double unitsEnrolled = double.Parse(unitsEnrolled_tbx.Text);
            totalUnits = unitsEnrolled * perUnits;

            totalAmount = "₱ " + (Math.Round((totalFees + totalUnits) - ((totalFees + totalUnits) / (totalDiscount)), 2)).ToString("N2");
            totalAmountValue_lbl.Text = totalAmount;

            studentInfo_Panel.Visible = false;
            fees_Panel.Visible = false;
            scholarGrant_Panel.Visible = false;
            totalAmount_Panel.Visible = true;
            totalAmount_Panel.Location = new Point(147, 131);
            compute_btn.Enabled = false;
            prevPanel_btn.Visible = false;
        }

        private void clear_btn_Click_1(object sender, EventArgs e)
        {
            totalFees = 0;
            totalDiscount = 0;
            studentName_tbx.Text = "";
            unitsEnrolled_tbx.Text = "";
            yearLevel_cmbbx.SelectedIndex = -1;
            prevPanel_btn.Visible = false;

            laboratoryFee_chckbx.Checked = false;
            registrarCard_chckbx.Checked = false;
            publication_chckbx.Checked = false;
            studentCouncil_chckbx.Checked = false;
            studentID_chckbx.Checked = false;
            miscellaneous_chckbx.Checked = false;

            nonScholarship_rbtn.Checked = false;
            fullScholarship_rbtn.Checked = false;
            partialScholarship_rbtn.Checked = false;

            studentInfo_Panel.Visible = true;
            fees_Panel.Visible = false;
            scholarGrant_Panel.Visible = false;
            totalAmount_Panel.Visible = false;

            compute_btn.Enabled = false;
            clear_btn.Enabled = false;

            totalAmountValue_lbl.Enabled = false;
        }

        private void studentName_tbx_TextChanged(object sender, EventArgs e)
        {
            CheckStudentInfo();
        }

        private void unitsEnrolled_tbx_TextChanged(object sender, EventArgs e)
        {
            CheckStudentInfo();
        }

        private void yearLevel_cmbbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckStudentInfo();
        }

        private void laboratoryFee_chckbx_CheckedChanged(object sender, EventArgs e)
        {
            CheckToCompute();
        }

        private void registrarCard_chckbx_CheckedChanged(object sender, EventArgs e)
        {
            CheckToCompute();
        }

        private void publication_chckbx_CheckedChanged(object sender, EventArgs e)
        {
            CheckToCompute();
        }

        private void studentCouncil_chckbx_CheckedChanged(object sender, EventArgs e)
        {
            CheckToCompute();
        }

        private void studentID_chckbx_CheckedChanged(object sender, EventArgs e)
        {
            CheckToCompute();
        }

        private void miscellaneous_chckbx_CheckedChanged(object sender, EventArgs e)
        {
            CheckToCompute();
        }

        private void nonScholarship_rbtn_CheckedChanged(object sender, EventArgs e)
        {
            CheckToCompute();
        }

        private void fullScholarship_rbtn_CheckedChanged(object sender, EventArgs e)
        {
            CheckToCompute();
        }

        private void partialScholarship_rbtn_CheckedChanged(object sender, EventArgs e)
        {
            CheckToCompute();
        }

        // Stop the program
        private void logout_btn_Click(object sender, EventArgs e)
        {
            DialogResult logoutResult = MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (logoutResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
