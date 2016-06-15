using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

using System.Xml.Serialization;
using microcosm.DB;
using microcosm.ViewModel;
using microcosm.Config;
using microcosm.Calc;
using microcosm.Planet;
using System.Drawing;
using microcosm.Common;
using microcosm.Aspect;

namespace microcosm
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel mainWindowVM;
        public PlanetListViewModel firstPList;
        public HouseListViewModel houseList;

        public SettingData[] settings = new SettingData[10];
        public SettingData currentSetting;
        public ConfigData config = new ConfigData();
        public TempSetting tempSettings;

        public AstroCalc calc;
        public RingCanvasViewModel rcanvas;
        public ReportViewModel reportVM;

        public UserData targetUser;
        public UserEventData userdata;

        List<PlanetData> list1;
        List<PlanetData> list2;
        List<PlanetData> list3;
        List<PlanetData> list4;
        List<PlanetData> list5;
        List<PlanetData> list6;

        public double[] houseList1;
        public double[] houseList2;
        public double[] houseList3;
        public double[] houseList4;
        public double[] houseList5;
        public double[] houseList6;

        public CommonConfigWindow configWindow;
        public SettingWIndow setWindow;
        public ChartSelectorWindow chartSelecterWindow;
        public DatabaseWindow dbWindow;
        public CustomRingWindow ringWindow;

        public MainWindow()
        {
            InitializeComponent();

            DataInit();
            DataCalc();
            SetViewModel();
            
        }

        // データ初期化
        private void DataInit()
        {
            tempSettings = new TempSetting(config);
            Enumerable.Range(0, 10).ToList().ForEach(i =>
            {
                string filename = @"system\setting" + i + ".csm";
                settings[i] = new SettingData(i);

                if (!File.Exists(filename))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(SettingXml));
                    FileStream fs = new FileStream(filename, FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    serializer.Serialize(sw, settings[i].xmlData);
                    sw.Close();
                    fs.Close();
                } else
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(SettingXml));
                    FileStream fs = new FileStream(filename, FileMode.Open);
                    settings[i].xmlData = (SettingXml)serializer.Deserialize(fs);
                    fs.Close();
                }

            });
            currentSetting = settings[0];

            {
                string filename = @"system\config.csm";
                if (!File.Exists(filename))
                {
                    // 生成も
                    XmlSerializer serializer = new XmlSerializer(typeof(ConfigData));
                    FileStream fs = new FileStream(filename, FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    serializer.Serialize(sw, config);
                    sw.Close();
                    fs.Close();
                } else
                {
                    // 読み込み
                    XmlSerializer serializer = new XmlSerializer(typeof(ConfigData));
                    FileStream fs = new FileStream(filename, FileMode.Open);
                    config = (ConfigData)serializer.Deserialize(fs);
                    fs.Close();
                }
            }

            rcanvas = new RingCanvasViewModel(config);
            ringStack.Background = System.Windows.Media.Brushes.GhostWhite;

        }

        private void DataCalc()
        {
            calc = new AstroCalc(config);

        }

        private void SetViewModel()
        {
            targetUser = new UserData(config);
            userdata = new UserEventData()
            {
                name = targetUser.name,
                birth_year = targetUser.birth_year,
                birth_month = targetUser.birth_month,
                birth_day = targetUser.birth_day,
                birth_hour = targetUser.birth_hour,
                birth_minute = targetUser.birth_minute,
                birth_second = targetUser.birth_second,
                birth_place = targetUser.birth_place,
                birth_str = targetUser.birth_str,
                lat = targetUser.lat,
                lng = targetUser.lng,
                lat_lng = targetUser.lat_lng,
                timezone = targetUser.timezone,
                memo = targetUser.memo,
                fullpath = targetUser.filename
            };
            UserBinding ub = new UserBinding(targetUser);
            TransitBinding tb = new TransitBinding(targetUser);
            mainWindowVM = new MainWindowViewModel()
            {
                userName = targetUser.name,
                userBirthStr = ub.birthStr,
                userTimezone = targetUser.timezone,
                userBirthPlace = targetUser.birth_place,
                userLat = String.Format("{0:f4}", targetUser.lat),
                userLng = String.Format("{0:f4}", targetUser.lng),
                transitName = "イベント未設定",
                transitBirthStr = tb.birthStr,
                transitTimezone = targetUser.timezone,
                transitPlace = targetUser.birth_place,
                transitLat = String.Format("{0:f4}", targetUser.lat),
                transitLng = String.Format("{0:f4}", targetUser.lng),
            };
            this.DataContext = mainWindowVM;

            ReCalc();

            //viewmodel設定
            firstPList = new PlanetListViewModel(this, list1, list2, list3, list4, list5, list6);
            planetList.DataContext = firstPList;

            houseList = new HouseListViewModel(houseList1, houseList2, houseList3, houseList4, houseList5, houseList6);
            cuspList.DataContext = houseList;

            explanation.DataContext = mainWindowVM;

            reportVM = new ReportViewModel(
                list1,
                list2, 
                list3,
                list4,
                list5,
                list6,
                houseList1, 
                houseList2, 
                houseList3, 
                houseList4, 
                houseList5, 
                houseList6);
            houseDown.DataContext = reportVM;
            houseRight.DataContext = reportVM;
            houseUp.DataContext = reportVM;
            houseLeft.DataContext = reportVM;
            signFire.DataContext = reportVM;
            signEarth.DataContext = reportVM;
            signAir.DataContext = reportVM;
            signWater.DataContext = reportVM;
            signCardinal.DataContext = reportVM;
            signFixed.DataContext = reportVM;
            signMutable.DataContext = reportVM;
            houseAngular.DataContext = reportVM;
            houseSuccedent.DataContext = reportVM;
            houseCadent.DataContext = reportVM;

        }

        public void ReCalc()
        {
            list1 = calc.PositionCalc(targetUser.birth_year, targetUser.birth_month, targetUser.birth_day, targetUser.birth_hour, targetUser.birth_minute, targetUser.birth_second, targetUser.lat, targetUser.lng);
            list2 = calc.PositionCalc(targetUser.birth_year, targetUser.birth_month, targetUser.birth_day, targetUser.birth_hour, targetUser.birth_minute, targetUser.birth_second, targetUser.lat, targetUser.lng);
            list3 = calc.PositionCalc(targetUser.birth_year, targetUser.birth_month, targetUser.birth_day, targetUser.birth_hour, targetUser.birth_minute, targetUser.birth_second, targetUser.lat, targetUser.lng);
            list4 = calc.PositionCalc(targetUser.birth_year, targetUser.birth_month, targetUser.birth_day, targetUser.birth_hour, targetUser.birth_minute, targetUser.birth_second, targetUser.lat, targetUser.lng);
            list5 = calc.PositionCalc(targetUser.birth_year, targetUser.birth_month, targetUser.birth_day, targetUser.birth_hour, targetUser.birth_minute, targetUser.birth_second, targetUser.lat, targetUser.lng);
            list6 = calc.PositionCalc(targetUser.birth_year, targetUser.birth_month, targetUser.birth_day, targetUser.birth_hour, targetUser.birth_minute, targetUser.birth_second, targetUser.lat, targetUser.lng);

            houseList1 = calc.CuspCalc(targetUser.birth_year, targetUser.birth_month, targetUser.birth_day, targetUser.birth_hour, targetUser.birth_minute, targetUser.birth_second, targetUser.lat, targetUser.lng, config.houseCalc);
            houseList2 = calc.CuspCalc(targetUser.birth_year, targetUser.birth_month, targetUser.birth_day, targetUser.birth_hour, targetUser.birth_minute, targetUser.birth_second, targetUser.lat, targetUser.lng, config.houseCalc);
            houseList3 = calc.CuspCalc(targetUser.birth_year, targetUser.birth_month, targetUser.birth_day, targetUser.birth_hour, targetUser.birth_minute, targetUser.birth_second, targetUser.lat, targetUser.lng, config.houseCalc);
            houseList4 = calc.CuspCalc(targetUser.birth_year, targetUser.birth_month, targetUser.birth_day, targetUser.birth_hour, targetUser.birth_minute, targetUser.birth_second, targetUser.lat, targetUser.lng, config.houseCalc);
            houseList5 = calc.CuspCalc(targetUser.birth_year, targetUser.birth_month, targetUser.birth_day, targetUser.birth_hour, targetUser.birth_minute, targetUser.birth_second, targetUser.lat, targetUser.lng, config.houseCalc);
            houseList6 = calc.CuspCalc(targetUser.birth_year, targetUser.birth_month, targetUser.birth_day, targetUser.birth_hour, targetUser.birth_minute, targetUser.birth_second, targetUser.lat, targetUser.lng, config.houseCalc);

            AspectCalc aspect = new AspectCalc();
            list1 = aspect.AspectCalcSame(currentSetting, list1);
        }

        private void mainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ReRender();
        }

        // レンダリングメイン
        public void ReRender()
        {
            AllClear();
            rcanvas.innerLeft = config.zodiacWidth / 2;
            rcanvas.innerTop = config.zodiacWidth / 2;
            if (ringCanvas.ActualWidth > ringStack.ActualHeight)
            {
                tempSettings.zodiacCenter = (int)(ringStack.ActualHeight * 0.7 / 2);

                rcanvas.outerWidth = ringStack.ActualHeight;
                rcanvas.outerHeight = ringStack.ActualHeight;
                rcanvas.innerWidth = ringStack.ActualHeight - config.zodiacWidth;
                rcanvas.innerHeight = ringStack.ActualHeight - config.zodiacWidth;
                rcanvas.centerLeft = ringStack.ActualHeight / 2 - tempSettings.zodiacCenter / 2;
                rcanvas.centerTop = ringStack.ActualHeight / 2 - tempSettings.zodiacCenter / 2;
            }
            else
            {
                tempSettings.zodiacCenter = ringCanvas.ActualWidth * 0.7 / 2;

                rcanvas.outerWidth = ringCanvas.ActualWidth;
                rcanvas.outerHeight = ringCanvas.ActualWidth;
                rcanvas.innerWidth = ringCanvas.ActualWidth - config.zodiacWidth;
                rcanvas.innerHeight = ringCanvas.ActualWidth - config.zodiacWidth;
                rcanvas.centerLeft = ringCanvas.ActualWidth / 2 - tempSettings.zodiacCenter / 2;
                rcanvas.centerTop = ringCanvas.ActualWidth / 2 - tempSettings.zodiacCenter / 2;
            }


            // Console.WriteLine(ringCanvas.ActualWidth.ToString() + "," + ringStack.ActualHeight.ToString());

            firstPList.ReRender(list1, list2, list3, list4, list5, list6);
            houseList.ReRender(houseList1, houseList2, houseList3, houseList4, houseList5, houseList6);

            circleRender();
            houseCuspRender(houseList1, houseList2, houseList3, houseList4, houseList5);

            // houseCuspRender2(houseList1);
            signCuspRender(houseList1[1]);
            zodiacRender(houseList1[1]);
            planetRender(houseList1[1], list1, list2, list3, list4, list5);
            planetLine(houseList1[1], list1, list2, list3, list4, list5);
            aspectsRendering(houseList1[1], list1, list2, list3, list4, list5);
        }

        // 円レンダリング
        private void circleRender()
        {
            // 獣帯外側
            Ellipse outerEllipse = new Ellipse()
            {
                StrokeThickness = 3,
                Margin = new Thickness(15, 15, 15, 15),
                Stroke = System.Windows.SystemColors.WindowTextBrush
            };
            if (ringCanvas.ActualWidth > ringStack.ActualHeight)
            {
                // 横長(Heightのほうが短い)
                outerEllipse.Width = ringStack.ActualHeight - 30;
                outerEllipse.Height = ringStack.ActualHeight - 30;

            }
            else
            {
                // 縦長(Widthのほうが短い)
                outerEllipse.Width = ringStack.ActualWidth - 30;
                outerEllipse.Height = ringStack.ActualWidth - 30;
            }
            ringCanvas.Children.Add(outerEllipse);

            // 獣帯内側
            Ellipse innerEllipse = new Ellipse()
            {
                StrokeThickness = 3,
                Margin = new Thickness(45, 45, 45, 45),
                Stroke = System.Windows.SystemColors.WindowTextBrush
            };
            if (ringCanvas.ActualWidth > ringStack.ActualHeight)
            {
                // 横長(Heightのほうが短い)
                innerEllipse.Width = ringStack.ActualHeight - 90;
                innerEllipse.Height = ringStack.ActualHeight - 90;

            }
            else
            {
                // 縦長(Widthのほうが短い)
                innerEllipse.Width = ringCanvas.ActualWidth - 90;
                innerEllipse.Height = ringCanvas.ActualWidth - 90;
            }
            ringCanvas.Children.Add(innerEllipse);

            // 中心
            int marginSize;
            if (ringCanvas.ActualWidth > ringStack.ActualHeight)
            {
                // 横長
                marginSize = (int)((ringStack.ActualHeight - tempSettings.zodiacCenter) / 2);
            }
            else
            {
                // 縦長
                marginSize = (int)((ringCanvas.ActualWidth - tempSettings.zodiacCenter) / 2);
            }
            Ellipse centerEllipse = new Ellipse()
            {
                StrokeThickness = 3,
                Stroke = System.Windows.SystemColors.WindowTextBrush,
                Width = tempSettings.zodiacCenter,
                Height = tempSettings.zodiacCenter
            };
            centerEllipse.Margin = new Thickness(marginSize, marginSize, marginSize, marginSize);
            ringCanvas.Children.Add(centerEllipse);

            // 二重円
            if (tempSettings.bands == 2)
            {
                int margin2Size;
                Ellipse ring2Ellipse = new Ellipse()
                {
                    StrokeThickness = 1,
                    Stroke = System.Windows.Media.Brushes.Gray,
                };
                if (ringCanvas.ActualWidth > ringStack.ActualHeight)
                {
                    // 横長
                    margin2Size = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) / 4 + 45;
                    ring2Ellipse.Width = (int)(ringStack.ActualHeight + tempSettings.zodiacCenter - 90) / 2;
                    ring2Ellipse.Height = (int)(ringStack.ActualHeight + tempSettings.zodiacCenter - 90) / 2;
                }
                else
                {
                    // 縦長
                    margin2Size = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) / 4 + 45;
                    ring2Ellipse.Width = (int)(ringStack.ActualWidth + tempSettings.zodiacCenter - 90) / 2;
                    ring2Ellipse.Height = (int)(ringStack.ActualWidth + tempSettings.zodiacCenter - 90) / 2;
                }
                ring2Ellipse.Margin = new Thickness(margin2Size, margin2Size, margin2Size, margin2Size);
                ringCanvas.Children.Add(ring2Ellipse);
            }

            // 三重円
            if (tempSettings.bands == 3)
            {
                int margin2Size;
                Ellipse ring2Ellipse = new Ellipse()
                {
                    StrokeThickness = 1,
                    Stroke = System.Windows.Media.Brushes.Gray,
                };
                if (ringCanvas.ActualWidth > ringStack.ActualHeight)
                {
                    // 横長
                    margin2Size = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) / 3 + 45;
                    ring2Ellipse.Width = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) / 3 + tempSettings.zodiacCenter;
                    ring2Ellipse.Height = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) / 3 + tempSettings.zodiacCenter;
                }
                else
                {
                    // 縦長
                    margin2Size = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) / 3 + 45;
                    ring2Ellipse.Width = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) / 3 + tempSettings.zodiacCenter;
                    ring2Ellipse.Height = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) / 3 + tempSettings.zodiacCenter;
                }
                ring2Ellipse.Margin = new Thickness(margin2Size, margin2Size, margin2Size, margin2Size);
                ringCanvas.Children.Add(ring2Ellipse);

                int margin3Size;
                Ellipse ring3Ellipse = new Ellipse()
                {
                    StrokeThickness = 1,
                    Stroke = System.Windows.Media.Brushes.Gray,
                };
                if (ringCanvas.ActualWidth > ringStack.ActualHeight)
                {
                    // 横長
                    margin3Size = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) / 6 + 45;
                    ring3Ellipse.Width = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 2 / 3 + tempSettings.zodiacCenter;
                    ring3Ellipse.Height = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 2 / 3 + tempSettings.zodiacCenter;
                }
                else
                {
                    // 縦長
                    margin3Size = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) / 6 + 45;
                    ring3Ellipse.Width = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 2 / 3 + tempSettings.zodiacCenter;
                    ring3Ellipse.Height = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 2 / 3 + tempSettings.zodiacCenter;
                }
                ring3Ellipse.Margin = new Thickness(margin3Size, margin3Size, margin3Size, margin3Size);
                ringCanvas.Children.Add(ring3Ellipse);

            }

            // 四重円
            if (tempSettings.bands == 4)
            {
                int margin2Size;
                Ellipse ring2Ellipse = new Ellipse()
                {
                    StrokeThickness = 1,
                    Stroke = System.Windows.Media.Brushes.Gray,
                };
                if (ringCanvas.ActualWidth > ringStack.ActualHeight)
                {
                    // 横長
                    margin2Size = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 3 / 8 + 45;
                    ring2Ellipse.Width = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) / 4 + tempSettings.zodiacCenter;
                    ring2Ellipse.Height = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) / 4 + tempSettings.zodiacCenter;
                }
                else
                {
                    // 縦長
                    margin2Size = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 3 / 8 + 45;
                    ring2Ellipse.Width = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) / 4 + tempSettings.zodiacCenter;
                    ring2Ellipse.Height = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) / 4 + tempSettings.zodiacCenter;
                }
                ring2Ellipse.Margin = new Thickness(margin2Size, margin2Size, margin2Size, margin2Size);
                ringCanvas.Children.Add(ring2Ellipse);

                int margin3Size;
                Ellipse ring3Ellipse = new Ellipse()
                {
                    StrokeThickness = 1,
                    Stroke = System.Windows.Media.Brushes.Gray,
                };
                if (ringCanvas.ActualWidth > ringStack.ActualHeight)
                {
                    // 横長
                    margin3Size = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) / 4 + 45;
                    ring3Ellipse.Width = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) / 2 + tempSettings.zodiacCenter;
                    ring3Ellipse.Height = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) / 2 + tempSettings.zodiacCenter;
                }
                else
                {
                    // 縦長
                    margin3Size = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) / 4 + 45;
                    ring3Ellipse.Width = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) / 2 + tempSettings.zodiacCenter;
                    ring3Ellipse.Height = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) / 2 + tempSettings.zodiacCenter;
                }
                ring3Ellipse.Margin = new Thickness(margin3Size, margin3Size, margin3Size, margin3Size);
                ringCanvas.Children.Add(ring3Ellipse);

                int margin4Size;
                Ellipse ring4Ellipse = new Ellipse()
                {
                    StrokeThickness = 1,
                    Stroke = System.Windows.Media.Brushes.Gray,
                };
                if (ringCanvas.ActualWidth > ringStack.ActualHeight)
                {
                    // 横長
                    margin4Size = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) / 8 + 45;
                    ring4Ellipse.Width = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 3 / 4 + tempSettings.zodiacCenter;
                    ring4Ellipse.Height = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 3 / 4 + tempSettings.zodiacCenter;
                }
                else
                {
                    // 縦長
                    margin4Size = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) / 8 + 45;
                    ring4Ellipse.Width = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 3 / 4 + tempSettings.zodiacCenter;
                    ring4Ellipse.Height = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 3 / 4 + tempSettings.zodiacCenter;
                }
                ring4Ellipse.Margin = new Thickness(margin4Size, margin4Size, margin4Size, margin4Size);
                ringCanvas.Children.Add(ring4Ellipse);
            }

            // 五重円
            if (tempSettings.bands == 5)
            {
                int margin2Size;
                Ellipse ring2Ellipse = new Ellipse()
                {
                    StrokeThickness = 1,
                    Stroke = System.Windows.Media.Brushes.Gray,
                };
                if (ringCanvas.ActualWidth > ringStack.ActualHeight)
                {
                    // 横長
                    margin2Size = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 2 / 5 + 45;
                    ring2Ellipse.Width = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) / 5 + tempSettings.zodiacCenter;
                    ring2Ellipse.Height = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) / 5 + tempSettings.zodiacCenter;
                }
                else
                {
                    // 縦長
                    margin2Size = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 2 / 5 + 45;
                    ring2Ellipse.Width = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) / 5 + tempSettings.zodiacCenter;
                    ring2Ellipse.Height = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) / 5 + tempSettings.zodiacCenter;
                }
                ring2Ellipse.Margin = new Thickness(margin2Size, margin2Size, margin2Size, margin2Size);
                ringCanvas.Children.Add(ring2Ellipse);

                int margin3Size;
                Ellipse ring3Ellipse = new Ellipse()
                {
                    StrokeThickness = 1,
                    Stroke = System.Windows.Media.Brushes.Gray,
                };
                if (ringCanvas.ActualWidth > ringStack.ActualHeight)
                {
                    // 横長
                    margin3Size = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 3 / 10 + 45;
                    ring3Ellipse.Width = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 2 / 5 + tempSettings.zodiacCenter;
                    ring3Ellipse.Height = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 2 / 5 + tempSettings.zodiacCenter;
                }
                else
                {
                    // 縦長
                    margin3Size = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 3 / 10 + 45;
                    ring3Ellipse.Width = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 2 / 5 + tempSettings.zodiacCenter;
                    ring3Ellipse.Height = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 2 / 5 + tempSettings.zodiacCenter;
                }
                ring3Ellipse.Margin = new Thickness(margin3Size, margin3Size, margin3Size, margin3Size);
                ringCanvas.Children.Add(ring3Ellipse);

                int margin4Size;
                Ellipse ring4Ellipse = new Ellipse()
                {
                    StrokeThickness = 1,
                    Stroke = System.Windows.Media.Brushes.Gray,
                };
                if (ringCanvas.ActualWidth > ringStack.ActualHeight)
                {
                    // 横長
                    margin4Size = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 1 / 5 + 45;
                    ring4Ellipse.Width = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 3 / 5 + tempSettings.zodiacCenter;
                    ring4Ellipse.Height = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 3 / 5 + tempSettings.zodiacCenter;
                }
                else
                {
                    // 縦長
                    margin4Size = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 1 / 5 + 45;
                    ring4Ellipse.Width = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 3 / 5 + tempSettings.zodiacCenter;
                    ring4Ellipse.Height = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 3 / 5 + tempSettings.zodiacCenter;
                }
                ring4Ellipse.Margin = new Thickness(margin4Size, margin4Size, margin4Size, margin4Size);
                ringCanvas.Children.Add(ring4Ellipse);

                int margin5Size;
                Ellipse ring5Ellipse = new Ellipse()
                {
                    StrokeThickness = 1,
                    Stroke = System.Windows.Media.Brushes.Gray,
                };
                if (ringCanvas.ActualWidth > ringStack.ActualHeight)
                {
                    // 横長
                    margin5Size = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 1 / 10 + 45;
                    ring5Ellipse.Width = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 4 / 5 + tempSettings.zodiacCenter;
                    ring5Ellipse.Height = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 4 / 5 + tempSettings.zodiacCenter;
                }
                else
                {
                    // 縦長
                    margin5Size = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 1 / 10 + 45;
                    ring5Ellipse.Width = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 4 / 5 + tempSettings.zodiacCenter;
                    ring5Ellipse.Height = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 4 / 5 + tempSettings.zodiacCenter;
                }
                ring5Ellipse.Margin = new Thickness(margin5Size, margin5Size, margin5Size, margin5Size);
                ringCanvas.Children.Add(ring5Ellipse);
            }
        }
        // a = (x1 - x2) / 2
        // b = (x2 + a) / 2 = (x2 + (x1 - x2) / 2 ) / 2
        // = (x2 /2 + x1 / 2) / 2

        // ハウスカスプレンダリング
        private void houseCuspRender(double[] natalcusp,
            double[] cusp2,
            double[] cusp3,
            double[] cusp4,
            double[] cusp5)
        {
            //内側がstart, 外側がend
            double startX = tempSettings.zodiacCenter / 2;
            double endX = (ringCanvas.ActualWidth - 90) / 2;

            double startY = 0;
            double endY = 0;
            List<PointF[]> pList = new List<PointF[]>();
            Enumerable.Range(1, 12).ToList().ForEach(i =>
            {
                double degree = natalcusp[i] - natalcusp[1];

                PointF newStart = rotate(startX, startY, degree);
                newStart.X += (float)rcanvas.outerWidth / 2;
                // Formの座標は下がプラス、数学では上がマイナス
                newStart.Y = newStart.Y * -1;
                newStart.Y += (float)rcanvas.outerHeight / 2;

                PointF newEnd = rotate(endX, endY, degree);
                newEnd.X += (float)rcanvas.outerWidth / 2;
                // Formの座標は下がプラス、数学では上がマイナス
                newEnd.Y = newEnd.Y * -1;
                newEnd.Y += (float)rcanvas.outerHeight / 2;

                PointF[] pointList = new PointF[2];
                pointList[0] = newStart;
                pointList[1] = newEnd;
                pList.Add(pointList);

            });

            Enumerable.Range(0, 12).ToList().ForEach(i =>
            {
                Line l = new Line();
                l.X1 = pList[i][0].X;
                l.Y1 = pList[i][0].Y;
                l.X2 = pList[i][1].X;
                l.Y2 = pList[i][1].Y;
                if (i % 3 == 0)
                {
                    l.Stroke = System.Windows.Media.Brushes.Gray;
                }
                else
                {
                    l.Stroke = System.Windows.Media.Brushes.LightGray;
                    l.StrokeDashArray = new DoubleCollection();
                    l.StrokeDashArray.Add(4.0);
                    l.StrokeDashArray.Add(4.0);
                }
                l.StrokeThickness = 2.0;
                l.Tag = new Explanation()
                {
                    before = (i + 1).ToString() + "ハウス　",
                    sign = CommonData.getSignTextJp(natalcusp[i + 1]),
                    degree = DecimalToHex((natalcusp[i + 1] % 30).ToString())
                };
                l.MouseEnter += houseCuspMouseEnter;
                ringCanvas.Children.Add(l);
            });
        }

        // サインカスプレンダリング
        private void signCuspRender(double startdegree)
        {
            // 内側がstart、外側がend
            // margin + thickness * 3だけ実際のリングの幅と差がある
            double startX = (rcanvas.innerWidth - 29) / 2;
            double endX = (rcanvas.outerWidth - 29)/ 2;

            double startY = 0;
            double endY = 0;
            List<PointF[]> pList = new List<PointF[]>();
            
            Enumerable.Range(1, 12).ToList().ForEach(i =>
            {
                double degree = (30.0 * i) - startdegree;

                PointF newStart = rotate(startX, startY, degree);
                newStart.X += (float)(rcanvas.outerWidth) / 2;
                // Formの座標は下がプラス、数学では上がマイナス
                newStart.Y = newStart.Y * -1;
                newStart.Y += (float)(rcanvas.outerHeight) / 2;

                PointF newEnd = rotate(endX, endY, degree);
                newEnd.X += (float)(rcanvas.outerWidth) / 2;
                // Formの座標は下がプラス、数学では上がマイナス
                newEnd.Y = newEnd.Y * -1;
                newEnd.Y += (float)(rcanvas.outerHeight) / 2;

                PointF[] pointList = new PointF[2];
                pointList[0] = newStart;
                pointList[1] = newEnd;
                pList.Add(pointList);
            });

            Enumerable.Range(0, 12).ToList().ForEach(i =>
            {
                Line l = new Line();
                l.X1 = pList[i][0].X;
                l.Y1 = pList[i][0].Y;
                l.X2 = pList[i][1].X;
                l.Y2 = pList[i][1].Y;
                l.Stroke = System.Windows.Media.Brushes.Black;
                l.StrokeThickness = 1.0;
                ringCanvas.Children.Add(l);
            });
        }

        // zodiac文字列描画
        // 獣帯に乗る文字ね
        private void zodiacRender(double startdegree)
        {
            List<PointF> pList = new List<PointF>();
            Enumerable.Range(0, 12).ToList().ForEach(i =>
            {
                PointF point = rotate(rcanvas.outerWidth / 2 - 33, 0, (30 * (i + 1)) - startdegree - 15.0);
                point.X += (float)rcanvas.outerWidth / 2 - 10;
//                point.X -= (float)rcanvas.outerWidth - (float)rcanvas.innerWidth;
                point.Y *= -1;
                point.Y += (float)rcanvas.outerHeight / 2 - 12;
//                point.Y -= (float)rcanvas.outerHeight - (float)rcanvas.innerHeight;
                pList.Add(point);
            });

            Enumerable.Range(0, 12).ToList().ForEach(i =>
            {
                Label zodiacLabel = new Label();
                zodiacLabel.Content = CommonData.getSignSymbol(i);
                zodiacLabel.Margin = new Thickness(pList[i].X, pList[i].Y, 0, 0);
                zodiacLabel.Foreground = CommonData.getSignColor(i * 30);
                ringCanvas.Children.Add(zodiacLabel);
            });
        }

        // 天体から中心円への線
        private void planetLine(double startdegree, 
            List<PlanetData> list1,
            List<PlanetData> list2,
            List<PlanetData> list3,
            List<PlanetData> list4,
            List<PlanetData> list5
            )
        {
            List<bool> dispList = new List<bool>();
            List<PlanetDisplay> pDisplayList = new List<PlanetDisplay>();

            if (tempSettings.bands == 1)
            {
                int[] box = new int[60];
                list1.ForEach(planet =>
                {
                    if (planet.isDisp == false)
                    {
                        return;
                    }
                    // 重ならないようにずらしを入れる
                    // 1サインに6度単位5個までデータが入る
                    int index = (int)(planet.absolute_position / 6);
                    if (box[index] == 1)
                    {
                        while (box[index] == 1)
                        {
                            index++;
                            if (index == 60)
                            {
                                index = 0;
                            }
                        }
                        box[index] = 1;
                    }
                    else
                    {
                        box[index] = 1;
                    }

                    PointF pointPlanet;
                    PointF pointRing;
                    pointPlanet = rotate(rcanvas.outerWidth / 3 - 65, 0, 6 * index - startdegree);
                    pointRing = rotate(tempSettings.zodiacCenter / 2, 0, planet.absolute_position - startdegree);

                    pointPlanet.X += (float)(rcanvas.outerWidth / 2);
                    pointPlanet.Y *= -1;
                    pointPlanet.Y += (float)(rcanvas.outerHeight / 2);
                    pointRing.X += (float)(rcanvas.outerWidth / 2);
                    pointRing.Y *= -1;
                    pointRing.Y += (float)(rcanvas.outerHeight / 2);

                    Line l = new Line();
                    l.X1 = pointPlanet.X;
                    l.Y1 = pointPlanet.Y;
                    l.X2 = pointRing.X;
                    l.Y2 = pointRing.Y;
                    l.Stroke = System.Windows.Media.Brushes.Gray;
                    l.StrokeThickness = 1.0;
                    ringCanvas.Children.Add(l);
                });
            }
        }

        public void AllClear()
        {
            rcanvas.natalSunTxt = "";
            rcanvas.natalSunDegreeTxt = "";
            rcanvas.natalSunSignTxt = "";
            rcanvas.natalSunMinuteTxt = "";
            rcanvas.natalSunRetrogradeTxt = "";
            rcanvas.natalEarthTxt = "";
            rcanvas.natalEarthDegreeTxt = "";
            rcanvas.natalEarthSignTxt = "";
            rcanvas.natalEarthMinuteTxt = "";
            rcanvas.natalEarthRetrogradeTxt = "";
            // 最終的に
            ringCanvas.Children.Clear();
        }

        // 天体表示
        public void SetSign(PlanetDisplay displayData)
        {
            Label txtLbl = new Label();
            txtLbl.Content = displayData.planetTxt;
            txtLbl.Margin = new Thickness(displayData.planetPt.X, displayData.planetPt.Y, 0, 0);
            txtLbl.Foreground = displayData.planetColor;
            txtLbl.Tag = displayData.explanation;
            txtLbl.FontSize = 16;
            txtLbl.MouseEnter += planetMouseEnter;
            ringCanvas.Children.Add(txtLbl);

            Label degreeLbl = new Label();
            degreeLbl.Content = displayData.degreeTxt;
            degreeLbl.Margin = new Thickness(displayData.degreePt.X, displayData.degreePt.Y, 0, 0);
            degreeLbl.Tag = displayData.explanation;
            degreeLbl.MouseEnter += planetMouseEnter;
            ringCanvas.Children.Add(degreeLbl);

            Label signLbl = new Label();
            signLbl.Content = displayData.symbolTxt;
            signLbl.Margin = new Thickness(displayData.symbolPt.X, displayData.symbolPt.Y, 0, 0);
            signLbl.Foreground = displayData.symbolColor;
            signLbl.Tag = displayData.explanation;
            signLbl.MouseEnter += planetMouseEnter;
            ringCanvas.Children.Add(signLbl);

            Label minuteLbl = new Label();
            minuteLbl.Content = displayData.minuteTxt;
            minuteLbl.Margin = new Thickness(displayData.minutePt.X, displayData.minutePt.Y, 0, 0);
            minuteLbl.Tag = displayData.explanation;
            minuteLbl.MouseEnter += planetMouseEnter;
            ringCanvas.Children.Add(minuteLbl);

            Label retrogradeLbel = new Label();
            retrogradeLbel.Content = displayData.retrogradeTxt;
            retrogradeLbel.Margin = new Thickness(displayData.retrogradePt.X, displayData.retrogradePt.Y, 0, 0);
            retrogradeLbel.Tag = displayData.explanation;
            retrogradeLbel.MouseEnter += planetMouseEnter;
            ringCanvas.Children.Add(retrogradeLbel);
        }

        // 天体表示
        // 天体だけ
        public void SetOnlySign(PlanetDisplay displayData)
        {
            Label txtLbl = new Label();
            txtLbl.Content = displayData.planetTxt;
            txtLbl.Margin = new Thickness(displayData.planetPt.X, displayData.planetPt.Y, 0, 0);
            txtLbl.Foreground = displayData.planetColor;
            txtLbl.Tag = displayData.explanation;
            txtLbl.FontSize = 16;
            txtLbl.MouseEnter += planetMouseEnter;
            ringCanvas.Children.Add(txtLbl);
        }

        // アスペクト表示
        public void aspectsRendering(
                double startDegree, 
                List<PlanetData> list1, 
                List<PlanetData> list2, 
                List<PlanetData> list3,
                List<PlanetData> list4,
                List<PlanetData> list5
            )
        {
            aspectRender(startDegree, list1, 1, 1, 1);
            /*
            if (aspectSetting.n_n)
            {
                aspectRender(startDegree, natallist, 1, 1, 1);
            }
            if (aspectSetting.p_p && setting.bands > 1)
            {
                aspectRender(startDegree, progresslist, 2, 2, 1);
            }
            if (aspectSetting.t_t && setting.bands > 2)
            {
                aspectRender(startDegree, transitlist, 3, 3, 1);
            }
            if (aspectSetting.n_p && setting.bands > 1)
            {
                aspectRender(startDegree, natallist, 1, 2, 2);
            }
            if (aspectSetting.n_t && setting.bands > 2)
            {
                aspectRender(startDegree, natallist, 1, 3, 3);
            }
            if (aspectSetting.p_t && setting.bands > 2)
            {
                aspectRender(startDegree, progresslist, 2, 3, 3);
            }
            */

        }

        // startPosition、endPosition : n-pの線は1-2となる
        private void aspectRender(double startDegree, List<PlanetData> list, int startPosition, int endPosition, int aspectKind)
        {
            if (list == null)
            {
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].isAspectDisp)
                {
                    continue;
                }
                PointF startPoint;
                PointF endPoint;
                if (startPosition == 1)
                {
                    startPoint = rotate(tempSettings.zodiacCenter / 2, 0, list[i].absolute_position - startDegree);
                }
                else if (startPosition == 2)
                {
                    //                    startPoint = rotate(setting.calcThirdInnerRadius() / 2, 0, list[i].absolute_position - startDegree);
                    startPoint = rotate(tempSettings.zodiacCenter / 2, 0, list[i].absolute_position - startDegree);
                }
                else
                {
                    //                    startPoint = rotate(setting.calcSecondInnerRadius() / 2, 0, list[i].absolute_position - startDegree);
                    startPoint = rotate(tempSettings.zodiacCenter / 2, 0, list[i].absolute_position - startDegree);
                }
                startPoint.X += (float)((rcanvas.outerWidth) / 2);
                startPoint.Y *= -1;
                startPoint.Y += (float)((rcanvas.outerHeight) / 2);

                if (aspectKind == 1) // natal
                {
                    for (int j = 0; j < list[i].aspects.Count; j++)
                    {
                        if (!list[list[i].aspects[j].targetPlanetNo].isAspectDisp)
                        {
                            continue;
                        }
                        if (endPosition == 1)
                        {
                            endPoint = rotate((float)tempSettings.zodiacCenter / 2, 0, list[i].aspects[j].targetPosition - startDegree);
                        }
                        else if (endPosition == 2)
                        {
                            endPoint = rotate((float)tempSettings.zodiacCenter / 2, 0, list[i].aspects[j].targetPosition - startDegree);
                        }
                        else
                        {
                            endPoint = rotate((float)tempSettings.zodiacCenter, 0, list[i].aspects[j].targetPosition - startDegree);
                        }
                        endPoint.X += (float)((rcanvas.outerWidth) / 2);
                        endPoint.Y *= -1;
                        endPoint.Y += (float)((rcanvas.outerHeight) / 2);

                        Line aspectLine = new Line()
                        {
                            X1 = startPoint.X,
                            Y1 = startPoint.Y,
                            X2 = endPoint.X,
                            Y2 = endPoint.Y
                        };
                        TextBlock aspectLbl = new TextBlock();
                        aspectLbl.Margin = new Thickness(Math.Abs(startPoint.X + endPoint.X) / 2 - 5, Math.Abs(endPoint.Y + startPoint.Y) / 2 - 8, 0, 0);
                        if (list[i].aspects[j].aspectKind == Aspect.AspectKind.OPPOSITION)
                        {
                            aspectLine.Stroke = System.Windows.Media.Brushes.Red;
                            aspectLbl.Foreground = System.Windows.Media.Brushes.Red;
                            aspectLbl.Text = "☍";
                            aspectLbl.HorizontalAlignment = HorizontalAlignment.Left;
                            aspectLbl.TextAlignment = TextAlignment.Left;
                            aspectLbl.VerticalAlignment = VerticalAlignment.Top;
                        }
                        else if (list[i].aspects[j].aspectKind == Aspect.AspectKind.TRINE)
                        {
                            aspectLine.Stroke = System.Windows.Media.Brushes.Orange;
                            aspectLbl.Foreground = System.Windows.Media.Brushes.Orange;
                            aspectLbl.Text = "△";
                            aspectLbl.HorizontalAlignment = HorizontalAlignment.Left;
                            aspectLbl.TextAlignment = TextAlignment.Left;
                            aspectLbl.VerticalAlignment = VerticalAlignment.Top;
                        }
                        else if (list[i].aspects[j].aspectKind == Aspect.AspectKind.SQUARE)
                        {
                            aspectLine.Stroke = System.Windows.Media.Brushes.DarkGray;
                            aspectLbl.Foreground = System.Windows.Media.Brushes.DarkGray;
                            aspectLbl.Text = "□";
                            aspectLbl.HorizontalAlignment = HorizontalAlignment.Left;
                            aspectLbl.TextAlignment = TextAlignment.Left;
                            aspectLbl.VerticalAlignment = VerticalAlignment.Top;
                        }
                        else if (list[i].aspects[j].aspectKind == Aspect.AspectKind.SEXTILE)
                        {
                            aspectLine.Stroke = System.Windows.Media.Brushes.Green;
                            aspectLbl.Foreground = System.Windows.Media.Brushes.Green;
                            aspectLbl.Text = "⚹";
                            aspectLbl.HorizontalAlignment = HorizontalAlignment.Left;
                            aspectLbl.TextAlignment = TextAlignment.Left;
                            aspectLbl.VerticalAlignment = VerticalAlignment.Top;
                        }
                        else if (list[i].aspects[j].aspectKind == Aspect.AspectKind.INCONJUNCT)
                        {
                            aspectLine.Stroke = System.Windows.Media.Brushes.Gray;
                            aspectLbl.Foreground = System.Windows.Media.Brushes.Gray;
                            aspectLbl.Text = "⚻";
                            aspectLbl.HorizontalAlignment = HorizontalAlignment.Left;
                            aspectLbl.TextAlignment = TextAlignment.Left;
                            aspectLbl.VerticalAlignment = VerticalAlignment.Top;
                        }
                        else
                        {
                            aspectLine.Stroke = System.Windows.Media.Brushes.Black;
                            aspectLbl.Foreground = System.Windows.Media.Brushes.Black;
                            aspectLbl.Text = "⚼";
                            aspectLbl.HorizontalAlignment = HorizontalAlignment.Left;
                            aspectLbl.TextAlignment = TextAlignment.Left;
                            aspectLbl.VerticalAlignment = VerticalAlignment.Top;
                        }
                        aspectLine.MouseEnter += new MouseEventHandler(aspectMouseEnter);
                        aspectLine.MouseLeave += new MouseEventHandler(explanationClear);
                        aspectLine.Tag = list[i].aspects[j];
                        ringCanvas.Children.Add(aspectLine);
                        ringCanvas.Children.Add(aspectLbl);

                    }

                }
                else if (aspectKind == 2) // progress
                {
                }
                else if (aspectKind == 3) // transit
                {
                }
            }
        }

        private void houseCuspMouseEnter(object sender, System.EventArgs e)
        {
            Line l = (Line)sender;
            Explanation data = (Explanation)l.Tag;
            mainWindowVM.explanationTxt = data.before + data.sign + DecimalToHex(data.degree.ToString("0.000")).ToString("0.000") + "\'";
        }

        private void planetMouseEnter(object sender, System.EventArgs e)
        {
            Label l = (Label)sender;
            Explanation data = (Explanation)l.Tag;
            mainWindowVM.explanationTxt = data.sign + DecimalToHex(data.degree.ToString("0.000")).ToString("0.000") + "\'";
        }
        private void aspectMouseEnter(object sender, System.EventArgs e)
        {
            Line l = (Line)sender;
            AspectInfo info = (AspectInfo)l.Tag;
            mainWindowVM.explanationTxt = info.aspectKind.ToString();
        }
        public void explanationClear(object sender, System.EventArgs e)
        {
            mainWindowVM.explanationTxt = "";
        }

        public void UsernameRefresh()
        {

        }

        private void OpenDatabase_Click(object sender, RoutedEventArgs e)
        {
            if (dbWindow == null)
            {
                dbWindow = new DatabaseWindow(this);
            }
            dbWindow.Visibility = Visibility.Visible;
        }

        private void mainWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        // ポイントの回転
        // 左サイドバーのwidthマイナスして呼び、後で足すこと
        protected PointF rotate(double x, double y, double degree)
        {
            // ホロスコープは180°から始まる
            degree += 180.0;

            double rad = (degree / 180.0) * Math.PI;
            double newX = x * Math.Cos(rad) - y * Math.Sin(rad);
            double newY = x * Math.Sin(rad) + y * Math.Cos(rad);


            return new PointF((float)newX, (float)newY);
        }

        public double DecimalToHex(string decimalStr)
        {
            double tmp = double.Parse(decimalStr);
            double ftmp = tmp - (int)tmp;
            ftmp = ftmp / 100 * 60;
            int itmp = (int)tmp;
            return itmp + ftmp;
        }

        private void OpenCommonConfig_Click(object sender, RoutedEventArgs e)
        {
            if (configWindow == null)
            {
                configWindow = new CommonConfigWindow(this);
            }
            configWindow.Visibility = Visibility.Visible;
        }

        private void SingleRing_Click(object sender, RoutedEventArgs e)
        {
            tempSettings.bands = 1;
            ReRender();
        }

        private void TripleRing_Click(object sender, RoutedEventArgs e)
        {
            tempSettings.bands = 3;
            ReRender();
        }

        private void MultipleRing_Click(object sender, RoutedEventArgs e)
        {
            if (ringWindow == null)
            {
                ringWindow = new CustomRingWindow(this);
            }
            ringWindow.Visibility = Visibility.Visible;
        }

        private void OpenDisplayConfig_Click(object sender, RoutedEventArgs e)
        {
            if (setWindow == null)
            {
                setWindow = new SettingWIndow(this);
            }
            setWindow.Visibility = Visibility.Visible;
        }

        private void ChartSelector_Click(object sender, RoutedEventArgs e)
        {
            if (chartSelecterWindow == null)
            {
                chartSelecterWindow = new ChartSelectorWindow(this);
            }
            chartSelecterWindow.Visibility = Visibility.Visible;

        }

        private void mainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.IsKeyDown(Key.LeftCtrl) || e.KeyboardDevice.IsKeyDown(Key.RightCtrl))
            {
                if (e.Key == Key.A)
                {
                    // MessageBox.Show("Ctrl + A");
                }
            }
        }

        private void Natal_Current_Click(object sender, RoutedEventArgs e)
        {
            natalSet();
            ReCalc();
            ReRender();
        }

        private void Transit_Current_Click(object sender, RoutedEventArgs e)
        {
            transitSet();
            ReCalc();
            ReRender();
        }

        private void Both_Current_Click(object sender, RoutedEventArgs e)
        {
            natalSet();
            transitSet();
            ReCalc();
            ReRender();
        }

        public void natalSet()
        {
            mainWindowVM.userName = "現在時刻";
            mainWindowVM.userBirthStr =
                DateTime.Now.Year
                + "/"
                + DateTime.Now.Month.ToString("00")
                + "/"
                + DateTime.Now.Day.ToString("00")
                + " "
                + DateTime.Now.Hour.ToString("00")
                + ":"
                + DateTime.Now.Minute.ToString("00")
                + ":"
                + DateTime.Now.Second.ToString("00")
                + " "
                + config.defaultTimezone;
            mainWindowVM.userBirthPlace = config.defaultPlace;
            mainWindowVM.userLat = config.lat.ToString("00.0000");
            mainWindowVM.userLng = config.lng.ToString("000.0000");

            targetUser.name = "現在時刻";
            targetUser.birth_year = DateTime.Now.Year;
            targetUser.birth_month = DateTime.Now.Month;
            targetUser.birth_day = DateTime.Now.Day;
            targetUser.birth_hour = DateTime.Now.Hour;
            targetUser.birth_minute = DateTime.Now.Minute;
            targetUser.birth_second = DateTime.Now.Second;
            targetUser.birth_place = config.defaultPlace;
            targetUser.lat = config.lat;
            targetUser.lng = config.lng;
            targetUser.timezone = config.defaultTimezone;
        }

        public void transitSet()
        {
            mainWindowVM.transitName = "現在時刻";
            mainWindowVM.transitBirthStr =
                DateTime.Now.Year
                + "/"
                + DateTime.Now.Month.ToString("00")
                + "/"
                + DateTime.Now.Day.ToString("00")
                + " "
                + DateTime.Now.Hour.ToString("00")
                + ":"
                + DateTime.Now.Minute.ToString("00")
                + ":"
                + DateTime.Now.Second.ToString("00")
                + " "
                + config.defaultTimezone;
            mainWindowVM.transitPlace = config.defaultPlace;
            mainWindowVM.transitLat = config.lat.ToString("00.0000");
            mainWindowVM.transitLng = config.lng.ToString("000.0000");

            userdata.name = "現在時刻";
            userdata.birth_year = DateTime.Now.Year;
            userdata.birth_month = DateTime.Now.Month;
            userdata.birth_day = DateTime.Now.Day;
            userdata.birth_hour = DateTime.Now.Hour;
            userdata.birth_minute = DateTime.Now.Minute;
            userdata.birth_second = DateTime.Now.Second;
            userdata.birth_place = config.defaultPlace;
            userdata.lat = config.lat;
            userdata.lng = config.lng;
            userdata.timezone = config.defaultTimezone;

        }

        private void Png_Export(object sender, RoutedEventArgs e)
        {
            DrawingVisual dv = new DrawingVisual();
            DrawingContext dc = dv.RenderOpen();
            Canvas cnvs = ringCanvas;
            cnvs.Background = System.Windows.Media.Brushes.White;
            cnvs.LayoutTransform = null;
            System.Windows.Size oldSize = new System.Windows.Size(ringCanvas.ActualWidth, ringCanvas.ActualHeight);
            System.Windows.Size renderSize;
            if (cnvs.ActualHeight > cnvs.ActualWidth)
            {
                renderSize = new System.Windows.Size(cnvs.ActualHeight, cnvs.ActualHeight);
            }
            else
            {
                renderSize = new System.Windows.Size(cnvs.ActualWidth, cnvs.ActualWidth);
            }
            cnvs.Measure(renderSize);
            cnvs.Arrange(new Rect(renderSize));
            RenderTargetBitmap render = new RenderTargetBitmap((Int32)renderSize.Width, (Int32)renderSize.Height, 96, 96, PixelFormats.Default);
            render.Render(cnvs);

            // PNGフォーマットで画像を保存するので
            var enc = new PngBitmapEncoder();
            enc.Frames.Add(BitmapFrame.Create(render));

            using (FileStream fs = new FileStream("test.png", FileMode.Create))
            {
                enc.Save(fs);    // 中身が透明じゃない画像が出力されるはず！！
                fs.Close();
            }

            ringCanvas.Measure(oldSize);
            ringCanvas.Arrange(new Rect(oldSize));

        }
    }
}
