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



            _player = new Player(10,10,20,0,1);

            lblExperience.Text = _player.ExperiencePoints.ToString();
            lblHitPoints.Text = _player.CurrentHitPoints.ToString();
            lblGold.Text = _player.Gold.ToString();
            lblLevel.Text = _player.Level.ToString();
        }


        //TODO: 13.1
    }
}
