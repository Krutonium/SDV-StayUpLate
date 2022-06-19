using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace StayUpLate
{
    public class ModEntry : Mod
    {
        private ModConfig config;
        public override void Entry(IModHelper helper)
        {
            config = this.Helper.ReadConfig<ModConfig>();
            Helper.Events.GameLoop.TimeChanged += GameLoopOnTimeChanged;
        }

        private void ChangeTime(int amount)
        {
            int proposedTime = Game1.timeOfDay + amount;
            //Ensure time is Valid.
            if (amount > 0)
            {
                if (proposedTime.ToString().EndsWith("60"))
                {
                    proposedTime += 40;
                }
            }
            else
            {
                if (proposedTime.ToString().EndsWith("90"))
                {
                    proposedTime -= 40;
                }
            }
            Game1.timeOfDay = proposedTime;
        }

        private void GameLoopOnTimeChanged(object sender, TimeChangedEventArgs e)
        {
            if (!Context.IsWorldReady)
            {
                return;
            }
            //this.Monitor.Log(Game1.timeOfDay.ToString());
            //2550 is 1:50 AM
            if (Game1.timeOfDay == config.TimeToStopAt)
            {
                ChangeTime(-10);
                Game1.player.Stamina -= config.StaminaDrain;
            }
        }

        class ModConfig
        {
            public float StaminaDrain = 14;
            public int TimeToStopAt = 2550;
        }
    }
}
