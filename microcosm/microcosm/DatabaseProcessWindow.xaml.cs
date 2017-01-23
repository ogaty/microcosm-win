using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace microcosm
{
    /// <summary>
    /// DatabaseProcessWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class DatabaseProcessWindow : Window
    {
        public DatabaseWindow dbwindow;
        public bool complete = true;
        public CancellationTokenSource cancelToken;

        public DatabaseProcessWindow(DatabaseWindow dbwindow)
        {
            InitializeComponent();
            this.dbwindow = dbwindow;
        }

        public async void startAmateru(OpenFileDialog oFD)
        {
            cancelToken = new CancellationTokenSource();
            complete = false;
            int err = 0;
            List<string> dataStr = new List<string>();
            // ファイルハンドラはすぐ閉じる
            using (Stream fileStream = oFD.OpenFile())
            {
                StreamReader sr = new StreamReader(fileStream, true);
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    dataStr.Add(line);
                }
                sr.Close();
            }

            Progress<int> p = new Progress<int>(showProgress);

            await Task.Run( () =>
            {
                err = processAmateru(p, dataStr, cancelToken);
            }
            );
            if (err == 0)
            {
                lbl.Content = "終了しました。";
            }
            else
            {
                lbl.Content = String.Format("終了しました。(エラー{0}件)", err);
            }
            complete = true;
            stopBtn.Content = "閉じる";
        }

        public async void startSG(OpenFileDialog oFD)
        {
            cancelToken = new CancellationTokenSource();
            complete = false;
            int err = 0;
            List<string> dataStr = new List<string>();
            // ファイルハンドラはすぐ閉じる
            using (Stream fileStream = oFD.OpenFile())
            {
                StreamReader sr = new StreamReader(fileStream, System.Text.Encoding.GetEncoding("shift_jis"));
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    dataStr.Add(line);
                }
                sr.Close();
            }

            Progress<int> p = new Progress<int>(showProgress);

            await Task.Run(() =>
            {
                err = processSG(p, dataStr, cancelToken);
            }
            );
            if (err == 0)
            {
                lbl.Content = "終了しました。";
            }
            else
            {
                lbl.Content = String.Format("終了しました。(エラー{0}件)", err);
            }
            complete = true;
            stopBtn.Content = "閉じる";
        }

        private void showProgress(int percent)
        {
            pg.Value = percent;
        }

        private int processAmateru(IProgress<int> p, List<string> dataStr, CancellationTokenSource token)
        {
            int err = 0;

            int success = 0;
            List<string> errMsgs = new List<string>();
            int lineCnt = 0;
            string ecsm = "";
            foreach (string line in dataStr)
            {
                if (line.IndexOf("NATAL") != 0)
                {
                    continue;
                }
                try
                {
                    string[] data = line.Split('\t');
                    string[] days = data[6].Split('-');
                    string[] hours = new string[3];
                    double lat;
                    double lng;
                    if (data[7] == "")
                    {
                        hours[0] = "12";
                        hours[1] = "0";
                        hours[2] = "0";
                    }
                    else
                    {
                        hours = data[7].Split(':');
                    }
                    if (data[9] == "")
                    {
                        lat = Common.CommonData.defaultLat;
                    }
                    else
                    {
                        lat = double.Parse(data[9]);
                    }
                    if (data[10] == "")
                    {
                        lng = Common.CommonData.defaultLng;
                    }
                    else
                    {
                        lng = double.Parse(data[10]);
                    }

                    ecsm += String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}\n",
                        data[1],
                        data[2],
                        days[0],
                        days[1],
                        days[2],
                        hours[0],
                        hours[1],
                        hours[2],
                        lat.ToString(),
                        lng.ToString(),
                        data[8],
                        data[6],
                        data[11]
                        );
                    lineCnt++;
                    p.Report(lineCnt * 100 / dataStr.Count());
                }
                catch (IOException exception)
                {
                    string errMsg = String.Format("{0} {1}", lineCnt, exception.Message);
                    errMsgs.Add(errMsg);
                    err++;
                }
                if (token.IsCancellationRequested)
                {
                    return err;
                }
            }
            p.Report(100);

            try
            {
                string filename = "Amateru" + DateTime.Now.ToString("yyyyMMdd") + ".ecsm";
                Assembly myAssembly = Assembly.GetEntryAssembly();
                string path = System.IO.Path.GetDirectoryName(myAssembly.Location) + @"\data\AMATERU\" + filename;
                if (!Directory.Exists(System.IO.Path.GetDirectoryName(path)))
                {
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
                }
                FileStream fs = new FileStream(path, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(ecsm);
                sw.Close();
                fs.Close();

                string errLog = "Amateru" + DateTime.Now.ToString("yyyyMMdd") + ".ecsm";
                string errPath = System.IO.Path.GetDirectoryName(myAssembly.Location) + @"\data\AMATERU\" + errLog;
                FileStream fs2 = new FileStream(errPath, FileMode.Create);
                StreamWriter sw2 = new StreamWriter(fs2);
                sw2.Write(ecsm);
                sw2.Close();
                fs2.Close();

                success++;
            }
            catch (IOException)
            {
                err++;
            }

            return err;
        }

        private int processSG(IProgress<int> p, List<string> dataStr, CancellationTokenSource token)
        {
            int err = 0;

            int success = 0;
            List<string> errMsgs = new List<string>();
            int lineCnt = 0;
            string ecsm = "";
            foreach (string line in dataStr)
            {
                if (line.IndexOf(",") > 0)
                {
                    try
                    {
                        string trimdata = line.Replace("  ", " ");
                        string[] data = trimdata.Split(' ');
                        // data[0] ymd
                        // data[1] his
                        // data[2] lat
                        // data[3] lng
                        // data[4] other

                        int year = int.Parse(data[0].Substring(0, 4));
                        int month = int.Parse(data[0].Substring(4, 2));
                        int day = int.Parse(data[0].Substring(6, 2));

                        int hour = int.Parse(data[1].Substring(0, 2));
                        int minute = int.Parse(data[1].Substring(2, 2));
                        int second = int.Parse(data[1].Substring(4, 2));

                        // stargazerはUTCで記録されるため、+9:00する
                        DateTime d = new DateTime(year, month, day, hour, minute, second);
                        d = d.AddHours(9.0);

                        string[] name = data[4].Split(',');
                        name[0] = name[0].Replace("\"", "");
                        name[1] = name[1].Replace("\"", "");

                        ecsm += String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}\n",
                            name[1],
                            "",
                            d.Year,
                            d.Month,
                            d.Day,
                            d.Hour,
                            d.Minute,
                            d.Second,
                            double.Parse(data[2]),
                            double.Parse(data[3]),
                            name[0],
                            "",
                            "JST"
                            );
                        lineCnt++;
                        p.Report(lineCnt * 100 / dataStr.Count());

                    }
                    catch (Exception exception)
                    {
                        string errMsg = String.Format("{0} {1}", lineCnt, exception.Message);
                        errMsgs.Add(errMsg);
                        err++;
                    }
                }
                if (token.IsCancellationRequested)
                {
                    return err;
                }
            }
            p.Report(100);

            try
            {
                string filename = "Stargazer" + DateTime.Now.ToString("yyyyMMdd") + ".ecsm";
                Assembly myAssembly = Assembly.GetEntryAssembly();
                string path = System.IO.Path.GetDirectoryName(myAssembly.Location) + @"\data\Stargazer\" + filename;
                if (!Directory.Exists(System.IO.Path.GetDirectoryName(path)))
                {
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
                }
                FileStream fs = new FileStream(path, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(ecsm);
                sw.Close();
                fs.Close();

                string errLog = "Stargazer" + DateTime.Now.ToString("yyyyMMdd") + ".ecsm";
                string errPath = System.IO.Path.GetDirectoryName(myAssembly.Location) + @"\data\Stargazer\" + errLog;
                FileStream fs2 = new FileStream(errPath, FileMode.Create);
                StreamWriter sw2 = new StreamWriter(fs2);
                sw2.Write(ecsm);
                sw2.Close();
                fs2.Close();

                success++;
            }
            catch (IOException)
            {
                err++;
            }

            return err;

        }

        private void stopBtn_Click(object sender, RoutedEventArgs e)
        {
            cancelToken.Cancel();
            dbwindow.vm.CreateTree();
            this.Visibility = Visibility.Hidden;
        }
    }
}
