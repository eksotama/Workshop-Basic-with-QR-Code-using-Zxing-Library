using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Step 1: Using Zxing Library
// Zxing library
using ZXing.Common;
using ZXing;
using ZXing.QrCode;

namespace WindowsFormsApp1_QRCode_Zxing
{
	public partial class Form1 : Form
	{
		private QrCodeEncodingOptions options;

		public Form1()
		{
			InitializeComponent();

			// Step 2: Coding in your Form Load
			//For Width or Height, you can change it to any value you want. :)
			//Note: Please write this first:
			//QrCodeEncodingOptions options = new QrCodeEncodingOptions();
			options = new QrCodeEncodingOptions
			{
				DisableECI = true,
				CharacterSet = "UTF-8",
				Width = 250,
				Height = 250,
			};
			var writer = new BarcodeWriter();
			writer.Format = BarcodeFormat.QR_CODE;
			writer.Options = options;


		}

		private void Button1_Click(object sender, EventArgs e)
		{
			// Step 3: Create Generate QR Code Button
			if (String.IsNullOrWhiteSpace(textBox1.Text) || String.IsNullOrEmpty(textBox1.Text))
			{
				pictureBox1.Image = null;
				MessageBox.Show("Text not found", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				var qr = new ZXing.BarcodeWriter();
				qr.Options = options;
				qr.Format = ZXing.BarcodeFormat.QR_CODE;
				var result = new Bitmap(qr.Write(textBox1.Text.Trim()));
				pictureBox1.Image = result;
				textBox1.Clear();
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{

			// Step 4: Create Decode QR Code Button
			try
			{
				Bitmap bitmap = new Bitmap(pictureBox1.Image);
				BarcodeReader reader = new BarcodeReader { AutoRotate = true, TryInverted = true };
				Result result = reader.Decode(bitmap);
				string decoded = result.ToString().Trim();
				textBox1.Text = decoded;
			}
			catch (Exception)
			{
				MessageBox.Show("Image not found", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			// Step 5: Create Browse a Local Image Button
			OpenFileDialog open = new OpenFileDialog();
			if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				var qr = new ZXing.BarcodeWriter();
				qr.Options = options;
				qr.Format = ZXing.BarcodeFormat.QR_CODE;
				pictureBox1.ImageLocation = open.FileName;
			}

		}

		private void button4_Click(object sender, EventArgs e)
		{
			// Step 6: Create Download Button
			if (pictureBox1.Image == null)
			{
				MessageBox.Show("Image not found", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				SaveFileDialog save = new SaveFileDialog();
				save.CreatePrompt = true;
				save.OverwritePrompt = true;
				save.FileName = "MyQR";
				save.Filter = "PNG|*.png|JPEG|*.jpg|BMP|*.bmp|GIF|*.gif";
				if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					pictureBox1.Image.Save(save.FileName);
					save.InitialDirectory = Environment.GetFolderPath
								(Environment.SpecialFolder.Desktop);
				}
			}
		}
	}
}
