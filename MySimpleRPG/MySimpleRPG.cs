using Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySimpleRPG
{
    public partial class MySimpleRPG : Form
    {
        private Player _player;
        public MySimpleRPG()
        {
            InitializeComponent();

            Location location = new Location(1,"Home", "This is your house");



            _player = new Player();

            _player.CurrentHitPoints = 10;
            _player.MaximumHitPoints = 10;
            _player.Gold = 20;
            _player.ExperiencePoints = 0;
            _player.Level = 1;

            lblExperience.Text = _player.ExperiencePoints.ToString();
            lblHitPoints.Text = _player.CurrentHitPoints.ToString();
            lblGold.Text = _player.Gold.ToString();
            lblLevel.Text = _player.Level.ToString();
        }

    }
}
