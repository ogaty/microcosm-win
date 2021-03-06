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
using Microsoft.Win32;
using System.IO;

namespace microcosm.DB
{
    /// <summary>
    /// UserEditWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class UserEditWindow : Window
    {
        public DatabaseWindow dbwindow;
        public PlaceSearchWindow searchWindow;
        public GoogleSearchWindow googleSearchWindow;
        public DateTime defaultDate { get; } = new DateTime(2000, 1, 1, 12, 0, 0);
        public bool isEdit = false;
        public TreeViewItem selected = null;
        public UserEditWindow(DatabaseWindow dbwindow, DbItem item)
        {
            this.dbwindow = dbwindow;
            InitializeComponent();
            UserEditSet(item);
        }

        // 表示文字リフレッシュ
        public void UserEditRefresh(DbItem item)
        {
            UserEditClear();
            UserEditSet(item);
        }

        // 表示文字設定
        public void UserEditSet(DbItem item)
        {
            fileName.Text = item.fileNameNoExt;
            if (userName.Text.IndexOf("- ") == 0)
            {
                userName.Text = item.userName.Substring(2);
            }
            else
            {
                userName.Text = item.userName;
            }
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

        // 表示文字設定(CB)
        public void UserEditSet(string place, string lat, string lng)
        {
            userPlace.Text = place;
            userLat.Text = lat;
            userLng.Text = lng;
        }

        // 表示文字クリア
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
            DateTime d;
            
            if (userBirth.SelectedDate != null)
            {
                d = new DateTime(
                        (int)userBirth.SelectedDate?.Year,
                        (int)userBirth.SelectedDate?.Month,
                        (int)userBirth.SelectedDate?.Day
                    );
            }
            else
            {
                d = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            }
            dbwindow.newUser_Click_CB(
                fileName.Text,
                userName.Text,
                userFurigana.Text,
                d,
                int.Parse(userHour.Text),
                int.Parse(userMinute.Text),
                int.Parse(userSecond.Text),
                userPlace.Text,
                double.Parse(userLat.Text),
                double.Parse(userLng.Text),
                userMemo.Text,
                userTimezone.Text,
                isEdit,
                selected
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
                searchWindow = new PlaceSearchWindow(this, userPlace.Text);
            }
            searchWindow.Visibility = Visibility.Visible;

        }

        private void GoogleSearch_Click(object sender, RoutedEventArgs e)
        {
            if (googleSearchWindow == null)
            {
                googleSearchWindow = new GoogleSearchWindow(this, userPlace.Text);
            }
            googleSearchWindow.Visibility = Visibility.Visible;

        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog oFD = new OpenFileDialog();
            oFD.FilterIndex = 1;
            bool? result = oFD.ShowDialog();
            if (result == true)
            {
                string fileName = oFD.FileName;
                using (Stream fileStream = oFD.OpenFile())
                {
                    StreamReader sr = new StreamReader(fileStream, true);
                    string line = sr.ReadLine();
                }
            }
        }

        private void fileName_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            fileName.SelectAll();
        }

        private void userName_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            userName.SelectAll();
        }

        private void userFurigana_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            userFurigana.SelectAll();
        }

        private void userHour_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            userHour.SelectAll();
        }

        private void userMinute_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            userMinute.SelectAll();
        }

        private void userSecond_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            userSecond.SelectAll();
        }

        private void userPlace_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            userPlace.SelectAll();
        }

        private void userLat_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            userLat.SelectAll();
        }

        private void userLng_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            userLng.SelectAll();
        }

        private void userMemo_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            userMemo.SelectAll();
        }
    }
}
