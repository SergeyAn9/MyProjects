using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Игра
{
    public partial class Form1 : Form
    {
        public Bitmap Krug1Texure = Resource2.krug,
                      Krug2Texure = Resource2.krug1;        
        private Point _targetPosition = new Point(300, 300);
        private Point _direction = Point.Empty;
        private int _score = 0;
        public Form1()
        {
            InitializeComponent();           
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint, true);

            UpdateStyles();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Random r = new Random();
            timer2.Interval = r.Next(20, 300);
            _direction.X = r.Next(-1, 2);
            _direction.Y = r.Next(-1, 2);

        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            var localPosition = this.PointToClient(Cursor.Position);

            _targetPosition.X += _direction.X * 20;
            _targetPosition.Y += _direction.Y * 20;

            if(_targetPosition.X < 10 || _targetPosition.X > 1000)
            {
                _direction.X *= -1;
            }
            if(_targetPosition.Y < 10 || _targetPosition.Y > 600)
            {
                _direction.Y *= -1;
            }

            Point between = new Point(localPosition.X - _targetPosition.X, localPosition.Y - _targetPosition.Y);
            float distance = (float)Math.Sqrt((between.X * between.X) + (between.Y * between.X));

            if(distance < 10)
            {
                AddScore(1);
                if (_score == 50)
                {
                    Over("Over");
                }
            }

            var krugRect = new Rectangle(localPosition.X - 50, localPosition.Y - 50, 100, 100);
            var krug1Rect = new Rectangle(_targetPosition.X - 50, _targetPosition.Y - 50, 100, 100);

            g.DrawImage(Krug1Texure, krugRect);
            g.DrawImage(Krug2Texure, krug1Rect);
        }
        private void Over(string str)
        {
            label1.Text = str;
            label1.Location = new Point(500, 50);
            label1.Visible = true;
            timer1.Stop();
            timer2.Stop();
        }
        private void AddScore(int score)
        {
            _score += score;
            scoreLabel.Text = _score.ToString(); 
        }
    }
}
