﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using microcosm.Common;

namespace microcosm.DB
{
    /// <summary>
    /// UserEditWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class UserEditWindow : Window
    {
        public DatabaseWindow dbwindow;
        public UserSearchWindow searchWindow;
        public DateTime defaultDate { get; } = new DateTime(2000, 1, 1, 12, 0, 0);
        public UserEditWindow(DatabaseWindow dbwindow, DbItem item)
        {
            this.dbwindow = dbwindow;
            InitializeComponent();
            UserEditSet(item);
        }

        public void UserEditRefresh(DbItem item)
        {
            UserEditClear();
            UserEditSet(item);
        }

        public void UserEditSet(DbItem item)
        {
            fileName.Text = item.fileName;
            userName.Text = item.userName;
            userFurigana.Text = item.userFurigana;
            userBirth.SelectedDate = item.userBirth;
            userHour.Text = item.userHour;
            userMinute.Text = item.userMinute;
            userSecond.Text = item.userSecond;
            userPlace.Text = item.userPlace;
            userLat.Text = item.userLat;
            userLng.Text = item.userLng;
            userTimezone.SelectedIndex = CommonData.getTimezoneIndex(item.userTimezone);
            userMemo.Text = item.memo;
        }

        public void UserEditClear()
        {
            fileName.Text = "";
            userName.Text = "";
            userFurigana.Text = "";
            userBirth.SelectedDate = defaultDate;
            userHour.Text = "";
            userMinute.Text = "";
            userSecond.Text = "";
            userPlace.Text = "";
            userLat.Text = "";
            userLng.Text = "";
            userTimezone.SelectedIndex = CommonData.getTimezoneIndex("JST");
            userMemo.Text = "";
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            dbwindow.setEnable();
            this.Visibility = Visibility.Hidden;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            dbwindow.newUser_Click_CB(
                fileName.Text,
                userName.Text,
                userFurigana.Text,
                userBirth.DisplayDate,
                int.Parse(userHour.Text),
                int.Parse(userMinute.Text),
                int.Parse(userSecond.Text),
                userPlace.Text,
                double.Parse(userLat.Text),
                double.Parse(userLng.Text),
                userMemo.Text,
                userTimezone.Text
                );
            dbwindow.setEnable();
            this.Visibility = Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            dbwindow.setEnable();
            this.Visibility = Visibility.Hidden;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (searchWindow == null)
            {
                searchWindow = new UserSearchWindow(this, userPlace.Text);
            }
            searchWindow.Visibility = Visibility.Visible;

        }
    }
}