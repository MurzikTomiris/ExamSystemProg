using System.Timers;

namespace Task3
{
    public partial class Form1 : Form
    {
        private System.Timers.Timer timer;
        private bool movingRight;
        private int speed = 5;
        public Form1()
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer = new System.Timers.Timer(20);
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Использование метода Invoke для доступа к элементам управления из потока таймера
            this.Invoke((MethodInvoker)delegate
            {
                // Перемещение кнопки вправо или влево в зависимости от флага movingRight
                if (movingRight)
                {
                    button1.Left += speed;
                    if (button1.Right >= ClientSize.Width)
                    {
                        movingRight = false;
                    }
                }
                else
                {
                    button1.Left -= speed;
                    if (button1.Left <= 0)
                    {
                        movingRight = true;
                    }
                }
            });
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Stop();
            timer.Dispose();
        }
    }
}