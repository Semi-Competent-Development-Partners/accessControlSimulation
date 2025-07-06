using System.IO.Ports;
using System.Data.SqlClient;

namespace UserRegistration {
    public partial class Form1 : Form {
        private readonly SerialPort serialPort;

        private string cardId = string.Empty;
        private readonly string ConnectionString = Properties.Settings.Default.DbConnString;

        public Form1() {
            InitializeComponent();

            // Lock the form size
            MaximumSize = Size;
            MinimumSize = Size;

            serialPort = new SerialPort("COM3", 9600);
            serialPort.DataReceived += SerialPort_DataReceived;
            serialPort.Open();
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e) {
            // Read the message from the serial port
            cardId = serialPort.ReadLine();
            // Update the UI on the main thread
            if (labelCardId.InvokeRequired) {
                labelCardId.Invoke(new Action(() => {

                    labelCardId.Text = $"Detected Card ID: {cardId}";
                }));
            }
            else {
                labelCardId.Text = $"Detected Card ID: {cardId}";
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e) {
            if (serialPort != null && serialPort.IsOpen) {
                serialPort.Close();
                serialPort.Dispose();
            }
            base.OnFormClosing(e);
        }

        private void BtnClear_Click(object sender, EventArgs e) {
            cardId = string.Empty;
            labelCardId.Text = "Detected Card ID:";
            txtBoxFullName.Text = string.Empty;
            txtBoxPosition.Text = string.Empty;
        }

        [Obsolete]
        private void BtnSubmit_Click(object sender, EventArgs e) {
            if (!ValidateFields()) {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Read form fields and labels, connect to the database, and insert the user data
            InserUsertIntoDb(sender, e);
        }

        private bool ValidateFields() {
            return cardId.Trim() != string.Empty && txtBoxFullName.Text.Trim() != string.Empty && txtBoxPosition.Text.Trim() != string.Empty;
        }

        [Obsolete]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>")]
        private void InserUsertIntoDb(object sender, EventArgs e) {
            using (SqlConnection connection = new(ConnectionString)) {
                connection.Open();
                string query = "INSERT INTO dbo.Employees1 (ISIC_ID, FullName, Position) VALUES (@CardId, @FullName, @Position)";
                SqlCommand cmd = new(query, connection);
                cmd.Parameters.AddWithValue("@CardId", int.Parse(cardId.Trim()));
                cmd.Parameters.AddWithValue("@FullName", txtBoxFullName.Text.Trim());
                cmd.Parameters.AddWithValue("@Position", txtBoxPosition.Text.Trim());
                try {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BtnClear_Click(sender, e); // Clear the form after successful registration
                }
                catch (SqlException ex) {
                    MessageBox.Show($"Error registering user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
