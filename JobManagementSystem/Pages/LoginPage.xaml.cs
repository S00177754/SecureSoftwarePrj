using JMS_DAL;
using JMS_DAL.Firebase_Wrapper.ResponsePayload;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JobManagementSystem.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            
            Task<bool> task = FirestoreHelper.SignIn(TxtBxLoginEmail.Text, TxtBxLoginPassword.Text);
            bool response = await task;
            if(response)
            {
                Task<bool> task2 = FirestoreHelper.PostData(new JMS_DAL.Data_Objects.ClientDTO() { CompanyName = "CN", Address = "ad", ID = "12444" }, "sd");
            }
        }


    }
}
