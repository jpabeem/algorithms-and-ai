using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteeringCS
{
    public enum TrackBarType { ALIGNMENT, SEPERATION, COHESION, DISTANCE };
    public partial class Settings : Form
    {
        World World { get; set; }

        public Settings(World World)
        {
            this.World = World;
            InitializeComponent();
            UpdateSettings();
        }

        private void UpdateSettings()
        {
            // update trackbars
            trackBarAlignment.Value = (int)World.FlockingHelper.AlignmentWeight * 10;
            trackBarSeperation.Value = (int)World.FlockingHelper.SeparationWeight * 10;
            trackBarCohesion.Value = (int)World.FlockingHelper.CohesionWeight * 10;
            trackBarDistance.Value = (int)World.FlockingHelper.DistanceFrom * 10;

            // update textboxes
            txtBoxAlignment.Text = World.FlockingHelper.AlignmentWeight.ToString(System.Globalization.CultureInfo.InvariantCulture);
            txtBoxSeperation.Text = World.FlockingHelper.SeparationWeight.ToString(System.Globalization.CultureInfo.InvariantCulture); ;
            txtBoxCohesion.Text = World.FlockingHelper.CohesionWeight.ToString(System.Globalization.CultureInfo.InvariantCulture); ;
            txtBoxDistance.Text = World.FlockingHelper.DistanceFrom.ToString();

            if (World.Settings.Get("SpritesEnabled"))
                chkBoxSprites.CheckState = CheckState.Checked;
            else
                chkBoxSprites.CheckState = CheckState.Unchecked;          
        }

        private void SetValue(TrackBarType type)
        {
            switch(type)
            {
                case TrackBarType.ALIGNMENT:
                    World.FlockingHelper.AlignmentWeight = (trackBarAlignment.Value / 10);
                    break;
                case TrackBarType.SEPERATION:
                    World.FlockingHelper.SeparationWeight = (trackBarSeperation.Value / 10);
                    break;
                case TrackBarType.COHESION:
                    World.FlockingHelper.CohesionWeight = (trackBarCohesion.Value / 10);
                    break;
                case TrackBarType.DISTANCE:
                    World.FlockingHelper.DistanceFrom = (trackBarDistance.Value / 10);
                    break;
            }
            UpdateSettings();
        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        private void trackBarAlignment_ValueChanged(object sender, EventArgs e)
        {
            SetValue(TrackBarType.ALIGNMENT);
        }

        private void trackBarSeperation_ValueChanged(object sender, EventArgs e)
        {
            SetValue(TrackBarType.SEPERATION);
        }

        private void trackBarCohesion_ValueChanged(object sender, EventArgs e)
        {
            SetValue(TrackBarType.COHESION);
        }

        private void trackBarDistance_ValueChanged(object sender, EventArgs e)
        {
            SetValue(TrackBarType.DISTANCE);
        }

        private void chkBoxSprites_Click(object sender, EventArgs e)
        {
            if (World.Settings.Get("SpritesEnabled"))
                World.Settings.Set("SpritesEnabled", false);
            else
                World.Settings.Set("SpritesEnabled", true);
        }
    }
}
