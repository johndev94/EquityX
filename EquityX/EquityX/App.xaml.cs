﻿namespace EquityX
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


            MainPage = new AppShell();
            //MainPage = new FlyoutPage();
        }
    }
}