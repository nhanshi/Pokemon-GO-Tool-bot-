using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PokemonGo.RocketAPI.Enums;
using PokemonGo.RocketAPI.Logic;
using PokemonGo.RocketAPI.GeneratedCode;
using PokemonGo.RocketAPI.Extensions;
using PokemonGo.RocketAPI.Logic.Utils;
using System.IO;
using PokemonGo.RocketAPI.Exceptions;
using GMap.NET.MapProviders;
using System.Drawing;
using System.Net;

namespace PokemonGo.RocketAPI.GUI
{
    public partial class MainForm : Form
    {
        private Client _client;
        private Settings _settings;
        private Inventory _inventory;
        private GetPlayerResponse _profile;

        // Persisting Login Info
        private AuthType _loginMethod;
        private string _username;
        private string _password;

        private bool _isFarmingActive;

        public MainForm()
        {
            InitializeComponent();
            StartLogger();
            CleanUp();
        }

        private void CleanUp()
        {
            // Clear Labels
            lbExpHour.Text = string.Empty;
            lbPkmnCaptured.Text = string.Empty;
            lbPkmnHr.Text = string.Empty;

            // Clear Labels
            lbName.Text = string.Empty;
            lbLevel.Text = string.Empty;
            lbExperience.Text = string.Empty;
            lbItemsInventory.Text = string.Empty;
            lbPokemonsInventory.Text = string.Empty;
            lbLuckyEggs.Text = string.Empty;
            lbIncense.Text = string.Empty;

            // Clear Experience
            _totalExperience = 0;
            _pokemonCaughtCount = 0;
        }

        private void SetupLocationMap()
        {
            MainMap.DragButton = MouseButtons.Left;
            MainMap.MapProvider = GMapProviders.BingMap;
            MainMap.Position = new GMap.NET.PointLatLng(UserSettings.Default.DefaultLatitude, UserSettings.Default.DefaultLongitude);
            MainMap.MinZoom = 0;
            MainMap.MaxZoom = 24;
            MainMap.Zoom = 15;
        }

        private void UpdateMap(double lat, double lng)
        {
            MainMap.Position = new GMap.NET.PointLatLng(lat, lng);
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                await DisplayLoginWindow();
                DisplayPositionSelector();
                await GetCurrentPlayerInformation();
                await PreflightCheck();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.Write(ex.Message);
            }
        }

        private void DisplayPositionSelector()
        {
            // Display Position Selector
            LocationSelector locationSelect = new LocationSelector();
            locationSelect.ShowDialog();

            // Check if Position was Selected
            try
            {
                if (!locationSelect.setPos)
                    throw new ArgumentException();

                // Persisting the Initial Position
                _client.SaveLatLng(locationSelect.lat, locationSelect.lng);
                _client.SetCoordinates(locationSelect.lat, locationSelect.lng, UserSettings.Default.DefaultAltitude);
            }
            catch
            {
                MessageBox.Show(@"You need to declare a valid starting location.", @"Safety Check");
                MessageBox.Show(@"To protect your account of a possible soft ban, the software will close.", @"Safety Check");
                Application.Exit();
            }

            // Display Starting Location
            Logger.Write($"Starting in Location Lat: {UserSettings.Default.DefaultLatitude} Lng: {UserSettings.Default.DefaultLongitude}");

            // Close the Location Window
            locationSelect.Close();

            // Setup MiniMap
            SetupLocationMap();
        }

        private async Task DisplayLoginWindow()
        {
            // Display Login
            Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            Show();

            // Check if an Option was Selected
            if (!loginForm.loginSelected)
                Application.Exit();

            // Determine Login Method
            if (loginForm.auth == AuthType.Ptc)
                await LoginPtc(loginForm.boxUsername.Text, loginForm.boxPassword.Text);
            if (loginForm.auth == AuthType.Google)
                await LoginGoogle(loginForm.boxUsername.Text, loginForm.boxPassword.Text);

            // Select the Location
            Logger.Write("Select Starting Location...");

            // Close the Login Form
            loginForm.Close();
        }

        private void StartLogger()
        {
            GUILogger guiLog = new GUILogger(LogLevel.Info);
            guiLog.setLoggingBox(loggingBox);
            Logger.SetLogger(guiLog);
        }

        private async Task LoginGoogle(string username, string password)
        {
            try
            {
                // Login Method
                _loginMethod = AuthType.Google;
                _username = username;
                _password = password;

                // Creating the Settings
                Logger.Write("Adjusting the Settings.");
                UserSettings.Default.AuthType = AuthType.Google.ToString();
                _settings = new Settings();

                // Begin Login
                Logger.Write("Trying to Login with Google Token...");
                Client client = new Client(_settings);
                client.DoGoogleLogin(username, password);
                await client.SetServer();

                // Server Ready
                Logger.Write("Connected! Server is Ready.");
                _client = client;

                Logger.Write("Attempting to Retrieve Inventory and Player Profile...");
                _inventory = new Inventory(client);
                _profile = await client.GetProfile();
                EnableButtons();
            }
            catch
            {
                Logger.Write("Unable to Connect using the Google Token.");
                MessageBox.Show(@"Unable to Authenticate with Login Server.", @"Login Problem");
                Application.Exit();
            }
        }

        private async Task LoginPtc(string username, string password)
        {
            try
            {
                // Login Method
                _loginMethod = AuthType.Ptc;
                _username = username;
                _password = password;

                // Creating the Settings
                Logger.Write("Adjusting the Settings.");
                UserSettings.Default.AuthType = AuthType.Ptc.ToString();
                UserSettings.Default.PtcUsername = username;
                UserSettings.Default.PtcPassword = password;
                _settings = new Settings();

                // Begin Login
                Logger.Write("Trying to Login with PTC Credentials...");
                Client client = new Client(_settings);
                await client.DoPtcLogin(_settings.PtcUsername, _settings.PtcPassword);
                await client.SetServer();

                // Server Ready
                Logger.Write("Connected! Server is Ready.");
                _client = client;

                Logger.Write("Attempting to Retrieve Inventory and Player Profile...");
                _inventory = new Inventory(client);
                _profile = await client.GetProfile();
                EnableButtons();
            }
            catch
            {
                Logger.Write("Unable to Connect using the PTC Credentials.");
                MessageBox.Show(@"Unable to Authenticate with Login Server.", @"Login Problem");
                Application.Exit();
            }
        }

        private void EnableButtons()
        {
            btnStartFarming.Enabled = true;
            btnTransferDuplicates.Enabled = true;
            btnRecycleItems.Enabled = true;
            btnEvolvePokemons.Enabled = true;
            cbKeepPkToEvolve.Enabled = true;
            btnMyPokemon.Enabled = true;
            btnExtraPlayerInformation.Enabled = true;
            cbTransfer.Enabled = true;
            cbEvolve.Enabled = true;
            btnadvoptions.Enabled = true;

            Logger.Write("Ready to Work.");
        }

        private void SetLuckyEggBtnText(int nrOfLuckyEggs)
        {
            btnLuckyEgg.Text = $"Use Lucky Egg ({ nrOfLuckyEggs.ToString() })";
            if (nrOfLuckyEggs == 0)
            {
                btnLuckyEgg.Enabled = false;
            }
            else
            {
                btnLuckyEgg.Enabled = true;
            }
        }

        private void SetIncensesBtnText(int nrOfIncenses)
        {
            btnUseIncense.Text = $"Use Incense ({ nrOfIncenses.ToString() })";
            if (nrOfIncenses == 0)
            {
                btnUseIncense.Enabled = false;
            }
            else
            {
                btnUseIncense.Enabled = true;
            }
        }

        private async Task<bool> PreflightCheck()
        {
            // Get Pokemons and Inventory
            var myItems = await _inventory.GetItems();
            var myPokemons = await _inventory.GetPokemons();

            // Write to Console
            var items = myItems as IList<Item> ?? myItems.ToList();
            var pokemon = myPokemons as IList<PokemonData> ?? myPokemons.ToList();

            Logger.Write($"Items in Bag: {items.Select(i => i.Count).Sum()}/350.");
            Logger.Write($"Lucky Eggs in Bag: {items.FirstOrDefault(p => (ItemId)p.Item_ == ItemId.ItemLuckyEgg)?.Count ?? 0 }");
            Logger.Write($"Pokemons in Bag: {pokemon.Count()}/250.");

            // Checker for Inventory
            if (items.Select(i => i.Count).Sum() >= 350)
            {
                Logger.Write("Unable to Start Farming: You need to have free space for Items.");
                // Clear Grid
                dGrid.Rows.Clear();
                // Prepare Grid
                dGrid.ColumnCount = 3;
                dGrid.Columns[0].Name = "Action";
                dGrid.Columns[1].Name = "Count";
                dGrid.Columns[2].Name = "Item";
                // Recycle Items
                await RecycleItems();
                Logger.Write("deleted some items");
                //return false;
            }

            // Checker for Pokemons
            if (pokemon.Count() >= 241) // Eggs are Included in the total count (9/9)
            {
                Logger.Write("Unable to Start Farming: You need to have free space for Pokemons.");
                return false;
            }

            // Ready to Fly
            Logger.Write("Inventory and Pokemon Space, Ready.");
            return true;
        }



        ///////////////////
        // Buttons Logic //
        ///////////////////

        private async void btnStartFarming_Click(object sender, EventArgs e)
        {
            if (!await PreflightCheck())
                return;

            // Disable Buttons
            disableButtonsDuringFarming();

            // Setup the Timer
            _isFarmingActive = true;
            SetUpTimer();
            StartBottingSession();

            // Clear Grid
            dGrid.Rows.Clear();

            // Prepare Grid
            PrepareGrid();
        }

        private void disableButtonsDuringFarming()
        {
            // Disable Button
            btnStartFarming.Enabled = false;
            btnEvolvePokemons.Enabled = false;
            btnRecycleItems.Enabled = false;
            btnTransferDuplicates.Enabled = false;
            cbKeepPkToEvolve.Enabled = false;
            cbTransfer.Enabled = false;
            cbEvolve.Enabled = false;
            btnadvoptions.Enabled = false;


            btnStopFarming.Enabled = true;
        }



        private void btnStopFarming_Click(object sender, EventArgs e)
        {
            // Disable Button
            btnStartFarming.Enabled = true;
            btnEvolvePokemons.Enabled = true;
            btnRecycleItems.Enabled = true;
            btnTransferDuplicates.Enabled = true;
            cbKeepPkToEvolve.Enabled = true;
            cbTransfer.Enabled = true;
            cbEvolve.Enabled = true;
            btnadvoptions.Enabled = true;

            btnStopFarming.Enabled = false;

            // Close the Timer
            _isFarmingActive = false;
            StopBottingSession();
        }

        private async void btnLuckyEgg_Click(object sender, EventArgs e)
        {
            await UseLuckyEgg();
        }

        private async void btnUseIncense_Click(object sender, EventArgs e)
        {
            await UseIncense();
        }

        private async void btnEvolvePokemons_Click(object sender, EventArgs e)
        {
            await EvolveAllPokemonWithEnoughCandy();
        }

        private async void btnTransferDuplicates_Click(object sender, EventArgs e)
        {
            await TransferDuplicatePokemon(cbKeepPkToEvolve.Checked);
        }

        private async void btnRecycleItems_Click(object sender, EventArgs e)
        {
            // Clear Grid
            dGrid.Rows.Clear();

            // Prepare Grid
            dGrid.ColumnCount = 3;
            dGrid.Columns[0].Name = "Action";
            dGrid.Columns[1].Name = "Count";
            dGrid.Columns[2].Name = "Item";

            // Recycle Items
            await RecycleItems();
        }

        ////////////////////////
        // EXP COUNTER MODULE //
        ////////////////////////

        private double _totalExperience;
        private int _pokemonCaughtCount;
        private int _pokestopsCount;
        private DateTime _sessionStartTime;
        private readonly Timer _sessionTimer = new Timer();

        private void SetUpTimer()
        {
            _sessionTimer.Tick += TimerTick;
            _sessionTimer.Enabled = true;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            lbExpHour.Text = GetExpPerHour();
            lbPkmnHr.Text = GetPokemonPerHour();
            lbPkmnCaptured.Text = @"Pokemons Captured: " + _pokemonCaughtCount.ToString();
        }

        private string GetExpPerHour()
        {
            double expPerHour = (_totalExperience * 3600) / (DateTime.Now - _sessionStartTime).TotalSeconds;
            return $"Exp/Hr: {expPerHour:0.0}";
        }

        private string GetPokemonPerHour()
        {
            double pkmnPerHour = (_pokemonCaughtCount * 3600) / (DateTime.Now - _sessionStartTime).TotalSeconds;
            return $"Pkmn/Hr: {pkmnPerHour:0.0}";
        }

        private async void StartBottingSession()
        {
            // Setup the Timer
            _sessionTimer.Interval = 11000;
            _sessionTimer.Start();
            _sessionStartTime = DateTime.Now;

            // Loop Until we Manually Stop
            while (_isFarmingActive)
            {
                try
                {
                    // Start Farming Pokestops/Pokemons.
                    await ExecuteFarmingPokestopsAndPokemons();
                    //await Task.Delay(500);
                    // Only Auto-Evolve/Transfer when Continuous.
                    if (_isFarmingActive)
                    {
                        if (cbEvolve.Checked == true)
                        {
                            // Evolve Pokemons.
                            await EvolveAllPokemonWithEnoughCandy();
                        }

                        if (cbTransfer.Checked == true)
                        {
                            // Transfer Duplicates.
                            await TransferDuplicatePokemon();
                        }
                    }
                }
                catch (InvalidResponseException)
                {
                    Logger.Write("------------> InvalidReponseException");
                    Logger.Write("<------------ Recovering");

                    // Re-Authenticate with Server
                    switch (_loginMethod)
                    {
                        case AuthType.Ptc:
                            await LoginPtc(_username, _password);
                            break;

                        case AuthType.Google:
                            await LoginGoogle(_username, _password);
                            break;
                    }

                    // Disable Buttons
                    disableButtonsDuringFarming();
                }
                catch (Exception)
                {
                    Logger.Write("------------> GeneralException");
                    Logger.Write("<------------ Recovering");

                    // Re-Authenticate with Server
                    switch (_loginMethod)
                    {
                        case AuthType.Ptc:
                            await LoginPtc(_username, _password);
                            break;

                        case AuthType.Google:
                            await LoginGoogle(_username, _password);
                            break;
                    }

                    // Disable Buttons
                    disableButtonsDuringFarming();
                }
            }
        }

        private void StopBottingSession()
        {
            _sessionTimer.Stop();

            boxPokestopName.Clear();
            boxPokestopInit.Clear();
            boxPokestopCount.Clear();

            MessageBox.Show(@"Please allow a few seconds for the pending tasks to complete.");
        }

        ///////////////////////
        // API LOGIC MODULES //
        ///////////////////////

        public async Task GetCurrentPlayerInformation()
        {
            var playerName = _profile.Profile.Username ?? "";
            var playerStats = await _inventory.GetPlayerStats();
            var playerStat = playerStats.FirstOrDefault();
            if (playerStat != null)
            {
                var xpDifference = GetXpDiff(playerStat.Level);
                //var message = $"{playerName} | Level {playerStat.Level}: {playerStat.Experience - playerStat.PrevLevelXp - xpDifference}/{playerStat.NextLevelXp - playerStat.PrevLevelXp - xpDifference}XP"; // Redundant?
                lbName.Text = $"Name: {playerName}";
                lbLevel.Text = $"Level: {playerStat.Level}";
                lbExperience.Text = $"Experience: {playerStat.Experience - playerStat.PrevLevelXp - xpDifference}/{playerStat.NextLevelXp - playerStat.PrevLevelXp - xpDifference} XP";
            }

            // Get Pokemons and Inventory
            var myItems = await _inventory.GetItems();
            var myPokemons = await _inventory.GetPokemons();

            // Write to Console
            var items = myItems as IList<Item> ?? myItems.ToList();
            lbItemsInventory.Text = $"Inventory: {items.Select(i => i.Count).Sum()}/350";
            lbPokemonsInventory.Text = $"Pokemons: {myPokemons.Count()}/250";
            lbLuckyEggs.Text = $"Lucky Eggs: {items.FirstOrDefault(p => (ItemId)p.Item_ == ItemId.ItemLuckyEgg)?.Count ?? 0}";
            lbIncense.Text = $"Incenses: {items.FirstOrDefault(p => (ItemId)p.Item_ == ItemId.ItemIncenseOrdinary)?.Count ?? 0}";
            SetLuckyEggBtnText(items.FirstOrDefault(p => (ItemId)p.Item_ == ItemId.ItemLuckyEgg)?.Count ?? 0);
            SetIncensesBtnText(items.FirstOrDefault(p => (ItemId)p.Item_ == ItemId.ItemIncenseOrdinary)?.Count ?? 0);
        }

        public static int GetXpDiff(int level)
        {
            switch (level)
            {
                case 1:
                    return 0;
                case 2:
                    return 1000;
                case 3:
                    return 2000;
                case 4:
                    return 3000;
                case 5:
                    return 4000;
                case 6:
                    return 5000;
                case 7:
                    return 6000;
                case 8:
                    return 7000;
                case 9:
                    return 8000;
                case 10:
                    return 9000;
                case 11:
                    return 10000;
                case 12:
                    return 10000;
                case 13:
                    return 10000;
                case 14:
                    return 10000;
                case 15:
                    return 15000;
                case 16:
                    return 20000;
                case 17:
                    return 20000;
                case 18:
                    return 20000;
                case 19:
                    return 25000;
                case 20:
                    return 25000;
                case 21:
                    return 50000;
                case 22:
                    return 75000;
                case 23:
                    return 100000;
                case 24:
                    return 125000;
                case 25:
                    return 150000;
                case 26:
                    return 190000;
                case 27:
                    return 200000;
                case 28:
                    return 250000;
                case 29:
                    return 300000;
                case 30:
                    return 350000;
                case 31:
                    return 500000;
                case 32:
                    return 500000;
                case 33:
                    return 750000;
                case 34:
                    return 1000000;
                case 35:
                    return 1250000;
                case 36:
                    return 1500000;
                case 37:
                    return 2000000;
                case 38:
                    return 2500000;
                case 39:
                    return 1000000;
                case 40:
                    return 1000000;
            }
            return 0;
        }

        private async Task EvolveAllPokemonWithEnoughCandy()
        {
            // Clear Grid
            dGrid.Rows.Clear();

            // Prepare Grid
            dGrid.ColumnCount = 3;
            dGrid.Columns[0].Name = "Action";
            dGrid.Columns[1].Name = "Pokemon";
            dGrid.Columns[2].Name = "Experience";

            // Logging
            Logger.Write("Selecting Pokemons available for Evolution.");

            var pokemonToEvolve = await _inventory.GetPokemonToEvolve();

            foreach (var pokemon in pokemonToEvolve)
            {
                var evolvePokemonOutProto = await _client.EvolvePokemon(pokemon.Id);

                if (evolvePokemonOutProto.Result == EvolvePokemonOut.Types.EvolvePokemonStatus.PokemonEvolvedSuccess)
                {
                    Logger.Write($"Evolved {pokemon.PokemonId} successfully for {evolvePokemonOutProto.ExpAwarded}xp");

                    // GUI Experience
                    _totalExperience += evolvePokemonOutProto.ExpAwarded;
                    dGrid.Rows.Insert(0, "Evolved", pokemon.PokemonId.ToString(), evolvePokemonOutProto.ExpAwarded);
                }
                else
                {
                    Logger.Write($"Failed to evolve {pokemon.PokemonId}. EvolvePokemonOutProto.Result was {evolvePokemonOutProto.Result}, stopping evolving {pokemon.PokemonId}");
                }

                await GetCurrentPlayerInformation();
                await Task.Delay(2000);
            }

            // Logging
            Logger.Write("Finished Evolving Pokemons.");
        }

        private async Task TransferDuplicatePokemon(bool keepPokemonsThatCanEvolve = false)
        {
            PrepareGrid();

            // Logging
            Logger.Write("Selecting Pokemons available for Transfer.");

            var duplicatePokemons = await _inventory.GetDuplicatePokemonToTransfer(keepPokemonsThatCanEvolve);
            await Task.Delay(400);
            foreach (var duplicatePokemon in duplicatePokemons)
            {
                var iv = Logic.Logic.CalculatePokemonPerfection(duplicatePokemon);
                //if (iv < UserSettings.Default.KeepMinIVPercentage && duplicatePokemon.Cp < UserSettings.Default.KeepMinCP)
                if (iv < UserSettings.Default.KeepMinIVPercentage)
                {
                    var transfer = await _client.TransferPokemon(duplicatePokemon.Id);
                    await Task.Delay(100);
                    // Add Row to DataGrid
                    InsertDataToGrid("Transferred", (int)duplicatePokemon.PokemonId, duplicatePokemon.Cp, iv);
                    await Task.Delay(100);
                    Logger.Write($"Transfer {duplicatePokemon.PokemonId} with {duplicatePokemon.Cp} CP and an IV of { iv }");

                    await GetCurrentPlayerInformation();

                    //await Task.Delay(500);
                }
            }

            // Logging
            Logger.Write("Finished Transfering Pokemons.");

            #region RecycleItems
            // Get Pokemons and Inventory
            var myItems = await _inventory.GetItems();
            // Write to Console
            var items = myItems as IList<Item> ?? myItems.ToList();
            if (items.Select(i => i.Count).Sum() >= 300)
            {
                // Recycle Items
                await RecycleItems(false);
            }
            #endregion
        }

        private async Task RecycleItems(bool IsShowGrid = true)
        {
            try
            {
                // Logging
                Logger.Write("Recycling Items to Free Space");

                var items = await _inventory.GetItemsToRecycle(_settings);
                await Task.Delay(200);
                foreach (var item in items)
                {
                    var transfer = await _client.RecycleItem((ItemId)item.Item_, item.Count);

                    Logger.Write($"Recycled {item.Count}x {(ItemId)item.Item_}", LogLevel.Info);

                    if (IsShowGrid)
                    {
                        // GUI Experience
                        dGrid.Rows.Insert(0, "Recycled", item.Count, ((ItemId)item.Item_).ToString());
                    }

                    await Task.Delay(200);
                }

                await GetCurrentPlayerInformation();

                // Logging
                Logger.Write("Recycling Complete.");
            }
            catch (Exception ex)
            {
                Logger.Write($"Error Details: {ex.Message}");
                Logger.Write("Unable to Complete Items Recycling.");
            }
        }

        //lựa banh theo Cp Wild Pokemon
        private async Task<MiscEnums.Item> GetBestBall(int? pokemonCp)
        {
            var pokeBallsCount = await _inventory.GetItemAmountByType(MiscEnums.Item.ITEM_POKE_BALL);
            var greatBallsCount = await _inventory.GetItemAmountByType(MiscEnums.Item.ITEM_GREAT_BALL);
            var ultraBallsCount = await _inventory.GetItemAmountByType(MiscEnums.Item.ITEM_ULTRA_BALL);
            var masterBallsCount = await _inventory.GetItemAmountByType(MiscEnums.Item.ITEM_MASTER_BALL);

            if (masterBallsCount > 0 && pokemonCp >= 1000)
                return MiscEnums.Item.ITEM_MASTER_BALL;
            else if (ultraBallsCount > 0 && pokemonCp >= 1000)
                return MiscEnums.Item.ITEM_ULTRA_BALL;
            else if (greatBallsCount > 0 && pokemonCp >= 1000)
                return MiscEnums.Item.ITEM_GREAT_BALL;

            if (ultraBallsCount > 0 && pokemonCp >= 600)
                return MiscEnums.Item.ITEM_ULTRA_BALL;
            else if (greatBallsCount > 0 && pokemonCp >= 600)
                return MiscEnums.Item.ITEM_GREAT_BALL;

            if (greatBallsCount > 0 && pokemonCp >= 350)
                return MiscEnums.Item.ITEM_GREAT_BALL;

            if (pokeBallsCount > 0)
                return MiscEnums.Item.ITEM_POKE_BALL;
            if (greatBallsCount > 0)
                return MiscEnums.Item.ITEM_GREAT_BALL;
            if (ultraBallsCount > 0)
                return MiscEnums.Item.ITEM_ULTRA_BALL;
            if (masterBallsCount > 0)
                return MiscEnums.Item.ITEM_MASTER_BALL;

            return MiscEnums.Item.ITEM_POKE_BALL;
        }

        //lựa banh theo captureProbability
        private async Task<MiscEnums.Item> GetBestBall(float? captureProbability, MapPokemon pokemonCapturing, int minGreatBall, int minUltraBall, int minMasterBall)
        {
            var pokeBallsCount = await _inventory.GetItemAmountByType(MiscEnums.Item.ITEM_POKE_BALL);
            var greatBallsCount = await _inventory.GetItemAmountByType(MiscEnums.Item.ITEM_GREAT_BALL);
            var ultraBallsCount = await _inventory.GetItemAmountByType(MiscEnums.Item.ITEM_ULTRA_BALL);
            var masterBallsCount = await _inventory.GetItemAmountByType(MiscEnums.Item.ITEM_MASTER_BALL);

            //List<MapPokemon> pokemonRare = new List<MapPokemon>;


            if (pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Abra ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Squirtle ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Wartortle ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Blastoise ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Machop ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Bellsprout ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Shellder ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Gastly ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Staryu ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Grimer ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Geoduge ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Oddish ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Growlithe ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Dratini)
            {
                //dong if qua dai co the se xay ra loi trong tuong lai luc do them Delay
                //vao
                if (greatBallsCount > 0)
                    return MiscEnums.Item.ITEM_GREAT_BALL;

            }

            if (pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Bulbasaur ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Ivysaur ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Venusaur ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Charmander ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Charmeleon ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Charizard ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Machoke ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Machamp ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Weepinbell ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Victreebell ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Cloyster ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Haunter ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Gengar ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Starmie ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Muk ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Graveler ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Golem ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Gloom ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Vileplume ||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Arcanine||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Dragonair||
                pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Dragonite)
            {
                //dong if qua dai co the se xay ra loi trong tuong lai luc do them Delay
                //vao
                if (ultraBallsCount > 0)
                    return MiscEnums.Item.ITEM_ULTRA_BALL;
                else if (greatBallsCount > 0)
                    return MiscEnums.Item.ITEM_GREAT_BALL;
            }
            
            if (captureProbability <= minMasterBall)
            {
                if (masterBallsCount > 0)
                    return MiscEnums.Item.ITEM_MASTER_BALL;
                else if (ultraBallsCount > 0)
                    return MiscEnums.Item.ITEM_ULTRA_BALL;
                else if (greatBallsCount > 0)
                    return MiscEnums.Item.ITEM_GREAT_BALL;
            }

            if (captureProbability <= minUltraBall)
            {
                if (ultraBallsCount > 0)
                    return MiscEnums.Item.ITEM_ULTRA_BALL;
                else if (greatBallsCount > 0)
                    return MiscEnums.Item.ITEM_GREAT_BALL;
            }


            if (captureProbability <= minGreatBall)
            {
                if (greatBallsCount > 0)
                    return MiscEnums.Item.ITEM_GREAT_BALL;
            }

            if (pokeBallsCount > 0)
                return MiscEnums.Item.ITEM_POKE_BALL;
            if (greatBallsCount > 0)
                return MiscEnums.Item.ITEM_GREAT_BALL;
            if (ultraBallsCount > 0)
                return MiscEnums.Item.ITEM_ULTRA_BALL;
            if (masterBallsCount > 0)
                return MiscEnums.Item.ITEM_MASTER_BALL;

            return MiscEnums.Item.ITEM_POKE_BALL;
        }


        public async Task UseBerry(ulong encounterId, string spawnPointId)
        {
            var inventoryItems = await _inventory.GetItems();
            var berries = inventoryItems.Where(p => (ItemId)p.Item_ == ItemId.ItemRazzBerry);
            var berry = berries.FirstOrDefault();

            if (berry == null)
                return;

            var useRaspberry = await _client.UseCaptureItem(encounterId, ItemId.ItemRazzBerry, spawnPointId); // Redundant?
            Logger.Write($"Used Rasperry. Remaining: {berry.Count}");
            await Task.Delay(3000);
        }

        public async Task UseLuckyEgg()
        {
            var inventoryItems = await _inventory.GetItems();
            var luckyEggs = inventoryItems.Where(p => (ItemId)p.Item_ == ItemId.ItemLuckyEgg);
            var luckyEgg = luckyEggs.FirstOrDefault();

            if (luckyEgg == null)
                return;

            var useLuckyEgg = await _client.UseItemExpBoost(ItemId.ItemLuckyEgg); // Redundant?
            Logger.Write($"Used LuckyEgg. Remaining: {luckyEgg.Count - 1}");

            await GetCurrentPlayerInformation();
        }

        public async Task UseIncense()
        {
            var inventoryItems = await _inventory.GetItems();
            var incenses = inventoryItems.Where(p => (ItemId)p.Item_ == ItemId.ItemIncenseOrdinary);
            var incense = incenses.FirstOrDefault();

            if (incense == null)
                return;

            var useIncense = await _client.UseItemIncense(ItemId.ItemIncenseOrdinary); // Redundant?
            Logger.Write($"Used Incense. Remaining: {incense.Count - 1}");

            await GetCurrentPlayerInformation();
        }

        Random r = new Random();

        private async Task ExecuteFarmingPokestopsAndPokemons()
        {
            var mapObjects = await _client.GetMapObjects();
            await Task.Delay(500);
            var pokeStops = mapObjects.MapCells.SelectMany(i => i.Forts).Where(i => i.Type == FortType.Checkpoint && i.CooldownCompleteTimestampMs < DateTime.UtcNow.ToUnixTime());

            var fortDatas = pokeStops as IList<FortData> ?? pokeStops.ToList();
            _pokestopsCount = fortDatas.Count<FortData>();
            int count = 1;

            foreach (var pokeStop in fortDatas)
            {
                
                var update = await _client.UpdatePlayerLocation(pokeStop.Latitude, pokeStop.Longitude, _settings.DefaultAltitude); // Redundant?
                UpdateMap(pokeStop.Latitude, pokeStop.Longitude);
                var fortInfo = await _client.GetFort(pokeStop.Id, pokeStop.Latitude, pokeStop.Longitude);

                boxPokestopName.Text = fortInfo.Name;
                boxPokestopInit.Text = count.ToString();
                boxPokestopCount.Text = _pokestopsCount.ToString();
                count++;

                var fortSearch = await _client.SearchFort(pokeStop.Id, pokeStop.Latitude, pokeStop.Longitude);
                Logger.Write($"Loot -> Gems: { fortSearch.GemsAwarded}, Eggs: {fortSearch.PokemonDataEgg} Items: {StringUtils.GetSummedFriendlyNameOfItemAwardList(fortSearch.ItemsAwarded)}");
                Logger.Write("Gained " + fortSearch.ExperienceAwarded + " XP.");

                // Experience Counter
                _totalExperience += fortSearch.ExperienceAwarded;

                await GetCurrentPlayerInformation();
                Logger.Write("Attempting to Capture Nearby Pokemons.");
                await ExecuteCatchAllNearbyPokemons();

                if (!_isFarmingActive)
                {
                    Logger.Write("Stopping Farming Pokestops.");
                    return;
                }

                Logger.Write("Waiting before moving to the next Pokestop.");
                await Task.Delay(UserSettings.Default.pokestopDelay * 1000);
            }
        }

        private async Task ExecuteCatchAllNearbyPokemons()
        {
            var mapObjects = await _client.GetMapObjects();
            await Task.Delay(1000);
            var pokemons = mapObjects.MapCells.SelectMany(i => i.CatchablePokemons);

            var mapPokemons = pokemons as IList<MapPokemon> ?? pokemons.ToList();
            Logger.Write("Found " + mapPokemons.Count<MapPokemon>() + " Pokemons in the area.");
            foreach (var pokemon in mapPokemons)
            {
                var update = await _client.UpdatePlayerLocation(pokemon.Latitude, pokemon.Longitude, _settings.DefaultAltitude); // Redundant?
                await Task.Delay(100);
                var encounterPokemonResponse = await _client.EncounterPokemon(pokemon.EncounterId, pokemon.SpawnpointId);
                var pokemonIv = Logic.Logic.CalculatePokemonPerfection(encounterPokemonResponse?.WildPokemon?.PokemonData);
                //lựa pokeball theo CP 
                //var pokemonCp = encounterPokemonResponse?.WildPokemon?.PokemonData?.Cp;
                //var pokeball = await GetBestBall(pokemonCp);
                //lựa pokeball theo captureProbability
                var captureProbability = encounterPokemonResponse?.CaptureProbability.CaptureProbability_.First() * 100;
                var pokeball = await GetBestBall(captureProbability, pokemon, UserSettings.Default.minGreatBall, UserSettings.Default.minUltraBall, UserSettings.Default.minMasterBall);
                Logger.Write($"Fighting {pokemon.PokemonId} with Capture Probability of {(encounterPokemonResponse?.CaptureProbability.CaptureProbability_.First()) * 100:0.0}%");

                boxPokemonName.Text = pokemon.PokemonId.ToString();
                boxPokemonCaughtProb.Text = (encounterPokemonResponse?.CaptureProbability.CaptureProbability_.First() * 100).ToString() + @"%";

                CatchPokemonResponse caughtPokemonResponse;
                do
                {
                    if (encounterPokemonResponse?.CaptureProbability.CaptureProbability_.First() < (UserSettings.Default.minBerry / 100))
                    {
                        //Throw berry if we can
                        await UseBerry(pokemon.EncounterId, pokemon.SpawnpointId);
                    } else 
                    {
                        // neu ko phai duoi thong so IV da chon de su dung ruzzberry nhung nam trong
                        // nhung con pokemon nay thi ngoai le su dung buzzberry
                        var pokemonCapturing = pokemon;
                        if (pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Bulbasaur ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Ivysaur ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Venusaur ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Charmander ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Charmeleon ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Charizard ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Squirtle ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Wartortle ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Blastoise ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Machoke ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Machamp ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Weepinbell ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Victreebell ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Cloyster ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Haunter ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Gengar ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Starmie ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Muk ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Graveler ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Golem ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Gloom ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Vileplume ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Arcanine||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Abra ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Machop ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Bellsprout ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Shellder ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Gastly ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Staryu ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Grimer ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Geoduge ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Oddish ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Growlithe||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Dragonair ||
                            pokemonCapturing.PokemonId == PokemonGo.RocketAPI.GeneratedCode.PokemonId.Dragonite)
                        {
                            //dong if qua dai co the se xay ra loi trong tuong lai luc do them Delay
                            //vao
                            await UseBerry(pokemon.EncounterId, pokemon.SpawnpointId);
                        }

                    }

                    caughtPokemonResponse = await _client.CatchPokemon(pokemon.EncounterId, pokemon.SpawnpointId, pokemon.Latitude, pokemon.Longitude, pokeball);
                    await Task.Delay(5000);
                }
                while (caughtPokemonResponse.Status == CatchPokemonResponse.Types.CatchStatus.CatchMissed);

                Logger.Write(caughtPokemonResponse.Status == CatchPokemonResponse.Types.CatchStatus.CatchSuccess ? $"We caught a {pokemon.PokemonId} with CP {encounterPokemonResponse?.WildPokemon?.PokemonData?.Cp} using a {pokeball}" : $"{pokemon.PokemonId} with CP {encounterPokemonResponse?.WildPokemon?.PokemonData?.Cp} got away while using a {pokeball}..");

                var PokemonImage = await GetPokemonImageAsync((int)pokemon.PokemonId);

                int? cp = encounterPokemonResponse?.WildPokemon?.PokemonData?.Cp;
                string capturePercent = (encounterPokemonResponse?.CaptureProbability.CaptureProbability_.First() * 100)?.ToString("0.00");

                if (caughtPokemonResponse.Status == CatchPokemonResponse.Types.CatchStatus.CatchSuccess)
                {
                    // Calculate Experience
                    int fightExperience = 0;
                    foreach (int exp in caughtPokemonResponse.Scores.Xp)
                        fightExperience += exp;
                    _totalExperience += fightExperience;
                    Logger.Write("Gained " + fightExperience + " XP.");
                    _pokemonCaughtCount++;

                    // Add Row to the DataGrid
                    InsertDataToGrid("Captured", (int)pokemon.PokemonId, cp.HasValue ? cp.Value : 0, pokemonIv, capturePercent);

                }
                else
                {
                    // Add Row to the DataGrid
                    InsertDataToGrid("Flew Away", (int)pokemon.PokemonId, cp.HasValue ? cp.Value : 0, pokemonIv, capturePercent);
                }

                boxPokemonName.Clear();
                boxPokemonCaughtProb.Clear();

                await GetCurrentPlayerInformation();

                if (!_isFarmingActive)
                {
                    Logger.Write("Stopping Farming Pokemons.");
                    return;
                }

                Logger.Write("Waiting before moving to the next Pokemon.");

                //sau khi bat xong thi tranfer may con ko dat yeu cau ve IV luon
                await TransferDuplicatePokemon(false);
                await Task.Delay(1500);

                //check coi inventory full hay ko , full thi recycle item di
                var myItems = await _inventory.GetItems();
                var items = myItems as IList<Item> ?? myItems.ToList();
                if (items.Select(i => i.Count).Sum() >= 350)
                {
                    Logger.Write("Unable to Start Farming: You need to have free space for Items.");
                    // Clear Grid
                    dGrid.Rows.Clear();
                    // Prepare Grid
                    dGrid.ColumnCount = 3;
                    dGrid.Columns[0].Name = "Action";
                    dGrid.Columns[1].Name = "Count";
                    dGrid.Columns[2].Name = "Item";
                    // Recycle Items
                    await RecycleItems();
                    await Task.Delay(2000);
                    Logger.Write("deleted some items");
                    //return false;
                    await Task.Delay(UserSettings.Default.pokemonDelay * 1000);
                }
                else
                {
                    await Task.Delay((UserSettings.Default.pokemonDelay - 2) * 1000);
                }
               
            }
        }

        private async void btnExtraPlayerInformation_Click(object sender, EventArgs e)
        {
            var stuff = await _inventory.GetPlayerStats();
            var stats = stuff.FirstOrDefault();
            if (stats != null)
                MessageBox.Show($"Battle Attack Won: {stats.BattleAttackTotal}\n" +
                                $"Battle Attack Total: {stats.BattleAttackTotal}\n" +
                                $"Battle Defended Won: {stats.BattleDefendedWon}\n" +
                                $"Battle Training Won: {stats.BattleTrainingWon}\n" +
                                $"Battle Training Total: {stats.BattleTrainingTotal}\n" +
                                $"Big Magikarp Caught: {stats.BigMagikarpCaught}\n" +
                                $"Eggs Hatched: {stats.EggsHatched}\n" +
                                $"Evolutions: {stats.Evolutions}\n" +
                                $"Km Walked: {stats.KmWalked}\n" +
                                $"Pokestops Visited: {stats.PokeStopVisits}\n" +
                                $"Pokeballs Thrown: {stats.PokeballsThrown}\n" +
                                $"Pokemon Deployed: {stats.PokemonDeployed}\n" +
                                $"Pokemon Captured: {stats.PokemonsCaptured}\n" +
                                $"Pokemon Encountered: {stats.PokemonsEncountered}\n" +
                                $"Prestige Dropped Total: {stats.PrestigeDroppedTotal}\n" +
                                $"Prestige Raised Total: {stats.PrestigeRaisedTotal}\n" +
                                $"Small Rattata Caught: {stats.SmallRattataCaught}\n" +
                                $"Unique Pokedex Entries: {stats.UniquePokedexEntries}");
        }

        private void btnMyPokemon_Click(object sender, EventArgs e)
        {
            var myPokemonsListForm = new PokemonForm(_client);
            myPokemonsListForm.ShowDialog();
        }

        private void btnadvoptions_Click(object sender, EventArgs e)
        {
            Options opts = new Options();
            opts.Show();
        }

        private async Task<Bitmap> GetPokemonImageAsync(int pokemonId)
        {
            WebRequest req = WebRequest.Create("http://pokeapi.co/media/sprites/pokemon/" + pokemonId + ".png");
            WebResponse res = await req.GetResponseAsync();
            Stream resStream = res.GetResponseStream();
            return new Bitmap(resStream);
        }

        private void PrepareGrid()
        {
            if (dGrid.ColumnCount != 5)
            {
                dGrid.ColumnCount = 0;
                // Prepare Grid
                DataGridViewTextBoxColumn text = new DataGridViewTextBoxColumn();
                text.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                text.HeaderText = "Capture";
                text.Name = "Capture";
                dGrid.Columns.Add(text);
                
                text = new DataGridViewTextBoxColumn();
                text.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                text.HeaderText = "CP";
                text.Name = "CP";
                dGrid.Columns.Add(text);

                text = new DataGridViewTextBoxColumn();
                text.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                text.HeaderText = "IV";
                text.Name = "IV";
                dGrid.Columns.Add(text);

                text = new DataGridViewTextBoxColumn();
                text.HeaderText = "Action";
                text.Name = "Action";
                dGrid.Columns.Add(text);

                DataGridViewImageColumn img = new DataGridViewImageColumn();
                img.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                img.HeaderText = "Image";
                img.Name = "img";
                dGrid.Columns.Add(img);

               
            }
        }

        private async void InsertDataToGrid(string action, int pokemonId, int cp, float iv, string capturePercent = null)
        {
            var PokemonImage = await GetPokemonImageAsync(pokemonId);
            capturePercent = string.IsNullOrWhiteSpace(capturePercent) ? string.Empty : capturePercent + " %";
            dGrid.Rows.Insert(0, capturePercent, cp.ToString("#,###"), iv.ToString("0.00") + " %", action,PokemonImage);
            ChangeColorRow(iv);
        }

        private void ChangeColorRow(float iv)
        {
            string Action = dGrid.Rows[0].Cells["Action"].Value.ToString();
            switch (Action)
            {
                case "Flew Away":
                    dGrid.Rows[0].DefaultCellStyle = new DataGridViewCellStyle
                    {
                        BackColor = Color.IndianRed,
                    };
                    break;
                case "Transferred":
                    dGrid.Rows[0].DefaultCellStyle = new DataGridViewCellStyle
                    {
                        BackColor = Color.IndianRed,
                    };
                    break;

                default:
                    break;
            }
            if(iv >= 85)
            {
                dGrid.Rows[0].DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.LightGreen,
                };
            }
            
        }
    }
}