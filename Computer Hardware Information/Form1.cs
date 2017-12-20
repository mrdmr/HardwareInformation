using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;

namespace Computer_Hardware_Information
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string[] table = { "Processor", "BaseBoard", "DiskDrive", "VideoController", "PhysicalMedia", "BIOS", "OperatingSystem" };
            comboBox1.Items.AddRange(table);
            string[] feature = { "ProcessorId", "Product", "Manufacturer", "Signature", "Caption", "SerialNumber", "Version" };
            comboBox2.Items.AddRange(feature);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            labelfeatured.Text = Getir(comboBox1.Text, comboBox2.Text);
        }
        private static string Getir(string TableName, string MethodName)
        {
            ManagementObjectSearcher MOS = new ManagementObjectSearcher("Select * from Win32_" + TableName);
       
            foreach (ManagementObject MO in MOS.Get())
           
            {
                try
                {
                    return MO[MethodName].ToString();
           
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message);
                }
            }
            return "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\WMI", "SELECT * FROM MSAcpi_ThermalZoneTemperature");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Double temp = Convert.ToDouble(queryObj["CurrentTemperature"].ToString());
                    temp = temp / 10 - 273;
                    labelcpu.Text = temp.ToString();
                }
            }
            catch (Exception ex)
            {
                labelcpu.Text = ex.Message;
            }
        }
    }
}
