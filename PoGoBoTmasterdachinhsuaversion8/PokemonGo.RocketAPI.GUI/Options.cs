using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokemonGo.RocketAPI.GUI
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
            SettingsLoad();
        }

        private void SettingsLoad()
        {
            tbDelayPokestops.Text = Convert.ToString(UserSettings.Default.pokestopDelay);
            tbDelayPokemons.Text = Convert.ToString(UserSettings.Default.pokemonDelay);
            tbCpKeepPokemon.Text = Convert.ToString(UserSettings.Default.KeepMinCP);
            tbIvKeepPokemon.Text = Convert.ToString(UserSettings.Default.KeepMinIVPercentage);
            tbMinBerry.Text = Convert.ToString(UserSettings.Default.minBerry);

            tbMinGreatBall.Text = Convert.ToString(UserSettings.Default.minGreatBall);
            tbMinUltraBall.Text = Convert.ToString(UserSettings.Default.minUltraBall);
            tbMinMasterBall.Text = Convert.ToString(UserSettings.Default.minMasterBall);

            lbWalkingSpeed.Text = Convert.ToString(UserSettings.Default.WalkingSpeedInKilometerPerHour);

        }

        private void btnsavesettings_Click(object sender, EventArgs e)
        {
            UserSettings.Default.pokestopDelay = Convert.ToInt32(tbDelayPokestops.Text);
            UserSettings.Default.pokemonDelay = Convert.ToInt32(tbDelayPokemons.Text);
            UserSettings.Default.KeepMinCP = Convert.ToInt32(tbCpKeepPokemon.Text);
            UserSettings.Default.KeepMinIVPercentage = Convert.ToInt32(tbIvKeepPokemon.Text);
            UserSettings.Default.minBerry = Convert.ToInt32(tbMinBerry.Text);

            UserSettings.Default.minGreatBall = Convert.ToInt32(tbMinGreatBall.Text);
            UserSettings.Default.minUltraBall = Convert.ToInt32(tbMinUltraBall.Text);
            UserSettings.Default.minMasterBall = Convert.ToInt32(tbMinMasterBall.Text);

            UserSettings.Default.WalkingSpeedInKilometerPerHour = Convert.ToInt32(lbWalkingSpeed.Text);

            UserSettings.Default.Save();

            this.Close();
        }

     
    }
}