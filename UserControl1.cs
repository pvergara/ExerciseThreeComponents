using System;
using System.Windows.Forms;

namespace ExerciseThreeComponents
{
    public partial class UserControl1: UserControl
    {
        public System.Timers.Timer timer = new System.Timers.Timer(1000);
        private ushort _mm;
        private ushort _ss;

        public ushort SS => _ss;

        public ushort MM => _mm;

        public UserControl1()
        {
            InitializeComponent();
            timer.Elapsed += (timerSender, timerEvent) => label1.Invoke((MethodInvoker)delegate
            {
                send(timerSender, timerEvent);
            });
        }

        event EventHandler<EventArgs> PlayClick;
        event EventHandler<EventArgs> DesbordaTiempo;

        protected virtual void OnPlayClick(EventArgs e)
        {
            PlayClick?.Invoke(this, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string result = @"Play";
            if (button1.Text == @"Play")
            {
                timer.Enabled = true;
                result = @"Pause";
            }
            else
            {
                timer.Enabled = false;
            }

            this.button1.Text = result;
            
            OnPlayClick(EventArgs.Empty);
        }

        public void send(object source, System.Timers.ElapsedEventArgs e)
        {
            this.increaseMMSS();
            label1.Text = $@"{MM:D2}:{SS:D2}"; 
        }

        private void increaseMMSS()
        {
            _ss++;
            if (_ss != 60) return;
            OnDesbordaTiempo(EventArgs.Empty);
            
            _ss = 0;
            _mm++;
        }

        private void OnDesbordaTiempo(EventArgs empty)
        {
            DesbordaTiempo?.Invoke(this, empty);
        }
    }
}
