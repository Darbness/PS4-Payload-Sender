﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace PS4_Payload_Sender
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public static Socket _psocket;
        public static bool pDConnected;
        public static string Exception;

        public static bool Connect2PS4(string ip, string port)
        {
            try
            {
                _psocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _psocket.ReceiveTimeout = 3000;
                _psocket.SendTimeout = 3000;
                _psocket.Connect(new IPEndPoint(IPAddress.Parse(ip), Int32.Parse(port)));
                pDConnected = true;
                return true;
            }
            catch (Exception ex)
            {
                pDConnected = false;
                Exception = ex.ToString();
                return false;
            }
        }

        public static void SendPayload(string filename)
        {
            _psocket.SendFile(filename);
        }

        public static void DisconnectPayload()
        {
            pDConnected = false;
            _psocket.Close();
        }

        public static string path;

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            path = openFileDialog1.FileName;
            button2.Text = path;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please enter an IP address.");
            }
            else
            {
                bool result = Connect2PS4(textBox1.Text, textBox2.Text);
                if (!result)
                {
                    MessageBox.Show("Error while connecting.\n" + Exception);
                }
            }
            try
            {
                SendPayload(path);
                DisconnectPayload();
                MessageBox.Show("Payload sent!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while sending payload!\n" + ex);
            }
        }

        private void pictureBox60_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://customprotocol.com");
        }


    }
}
