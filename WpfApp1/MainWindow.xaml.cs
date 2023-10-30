using System; // 引入基礎的系統類別庫
using System.Collections.Generic; // 引入集合類別，比如List
using System.Windows; // 引入WPF的Windows名字空間
using System.Windows.Controls; // 引入控制元件，比如TextBox和Button
using System.Windows.Media; // 引入媒體（顏色、筆刷等）名字空間

namespace WpfApp1 // 定義名字空間
{
    public partial class MainWindow : Window // MainWindow類別繼承自WPF的Window類別
    {
        public MainWindow() // MainWindow的建構函式
        {
            InitializeComponent(); // 初始化界面元件
        }

        public class Triangle // 定義Triangle類別用於表示三角形
        {
            public double Side1 { set; get; } // 定義Side1屬性
            public double Side2 { set; get; } // 定義Side2屬性
            public double Side3 { set; get; } // 定義Side3屬性
            public bool IsValid { set; get; } // 定義IsValid屬性用於判斷是否為有效的三角形
        }

        List<Triangle> triangles = new List<Triangle>(); // 創建Triangle對象的列表

        private void button1_Click(object sender, RoutedEventArgs e) // button1的點擊事件處理函數
        {
            double side1, side2, side3; // 定義三個邊長變數
            bool success1 = double.TryParse(textbox1.Text, out side1); // 嘗試將textbox1的文本轉換為double，結果存儲在side1中
            bool success2 = double.TryParse(textbox2.Text, out side2); 
            bool success3 = double.TryParse(textbox3.Text, out side3); 

            textbox1.ClearValue(TextBox.BorderBrushProperty); // 清除textbox1邊框顏色的設置
            textbox2.ClearValue(TextBox.BorderBrushProperty); // 清除textbox2邊框顏色的設置
            textbox3.ClearValue(TextBox.BorderBrushProperty); // 清除textbox3邊框顏色的設置

            // 檢查三個邊長是否都成功轉換
            if (!success1 || !success2 || !success3)
            {
                MessageBox.Show("請輸入有效的數值", "輸入錯誤"); // 彈出錯誤對話框
                if (!success1) textbox1.BorderBrush = Brushes.Red; // 如果textbox1轉換失敗，將其邊框設為紅色
                if (!success2) textbox2.BorderBrush = Brushes.Red; 
                if (!success3) textbox3.BorderBrush = Brushes.Red; 
                return; // 終止函數執行
            }

            // 檢查邊長是否都大於0
            if (side1 < 1 || side2 < 1 || side3 < 1)
            {
                MessageBox.Show("請輸入有效的數值", "輸入錯誤"); // 彈出錯誤對話框
                if (side1 < 1) textbox1.BorderBrush = Brushes.Red; // 如果side1小於1，將textbox1邊框設為紅色
                if (side2 < 1) textbox2.BorderBrush = Brushes.Red; 
                if (side3 < 1) textbox3.BorderBrush = Brushes.Red; 
                return; // 終止函數執行
            }

            // 檢查是否能構成三角形
            if (side1 + side2 > side3 && side1 + side3 > side2 && side2 + side3 > side1)
            {
                textblock_multiple.Background = new SolidColorBrush(Colors.Green); // 將textblock的背景設為綠色
                textblock_multiple.Text = $"邊長 {side1}, {side2}, {side3} 可構成三角形"; // 設置textblock的文本
                triangles.Add(new Triangle() { Side1 = side1, Side2 = side2, Side3 = side3, IsValid = true }); // 將新的三角形對象添加到列表中
            }
            else
            {
                textblock_multiple.Background = new SolidColorBrush(Colors.Red); // 將textblock的背景設為紅色
                textblock_multiple.Text = $"邊長 {side1}, {side2}, {side3} 不可構成三角形"; // 設置textblock的文本
                triangles.Add(new Triangle() { Side1 = side1, Side2 = side2, Side3 = side3, IsValid = false }); // 將新的三角形對象添加到列表中
            }

            ListResult(); // 調用ListResult函數來更新結果列表
        }

        private void ListResult() // 更新結果列表的函數
        {
            textblock_multiple2.Clear(); // 清除textblock_multiple2的文本

            // 遍歷triangles列表，將每個三角形的信息添加到textblock_multiple2中
            foreach (Triangle tri in triangles)
            {
                textblock_multiple2.Text += $"{tri.Side1}\t{tri.Side2}\t{tri.Side3}\t{tri.IsValid}\n";
            }
        }
    }
}
