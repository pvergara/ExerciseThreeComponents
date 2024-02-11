using System;
using System.Windows.Forms;

namespace ExerciseThreeComponents
{
    public partial class UserControl1: UserControl
    {
        private readonly System.Timers.Timer _timer = new System.Timers.Timer(1000);

        public ushort SS { get; private set; }

        public ushort MM { get; private set; }

        public UserControl1()
        {
            InitializeComponent();
            _timer.Elapsed += (timerSender, timerEvent) => label1.Invoke((MethodInvoker)delegate
            {
                Send(timerSender, timerEvent);
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
            var result = @"Play";
            if (button1.Text == @"Play")
            {
                _timer.Enabled = true;
                result = @"Pause";
            }
            else
            {
                _timer.Enabled = false;
            }

            this.button1.Text = result;
            
            OnPlayClick(EventArgs.Empty);
        }

        private void Send(object source, System.Timers.ElapsedEventArgs e)
        {
            this.IncreaseMMSS();
            label1.Text = $@"{MM:D2}:{SS:D2}"; 
        }

        private void IncreaseMMSS()
        {
            SS++;
            if (SS != 60) return;
            OnDesbordaTiempo(EventArgs.Empty);
            
            SS = 0;
            MM++;
        }

        private void OnDesbordaTiempo(EventArgs empty)
        {
            DesbordaTiempo?.Invoke(this, empty);
        }
    }
}
