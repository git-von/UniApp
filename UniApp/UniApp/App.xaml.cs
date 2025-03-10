﻿using System;
using UniApp.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UniApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            DataAccessLayer.Save();
        }

        protected override void OnResume()
        {
        }
    }
}
