using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MyCouch.Requests;
using MyCouch.Responses;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DarkSoulsCalculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public void Connection()
        {
            using (var client = new MyCouch.MyCouchClient("http://localhost:5984/", "dks_db"))
            {

            }

        }



        public MainPage()
        {
            this.InitializeComponent();
            DataContext = this;
        }


        #region Offence object
        public object [] Offence
            {
                get
                {
                    return new object[] { 0, 0, 0, 0, 0 };
                }

            }
        #endregion

        #region Defence object
        public object [] Defence
        {
            get
            {
                return new object[] { 0, 0, 0, 0, 0 };
            }
        }
        #endregion

    }
}
