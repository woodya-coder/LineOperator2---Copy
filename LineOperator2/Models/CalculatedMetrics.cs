using LineOperator2.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LineOperator2.Models
{
    public class CalculatedMetrics
    {
        private const float minutesPerShift = 720;
        public float MinutesPerBox { get; set; }
        public float MinutesPerPallet { get; set; }

        public float CurrentBox { get; set; }
        public float ShiftBoxNeeds { get; set; }
        public float ShiftPalletNeeds { get; set; }
        public float ShiftCrateNeeds { get; set; }
        public DateTime ChangeOver { get; set; }
        public DateTime NextCrateUp { get; set; }


        public CalculatedMetrics(Job job)
        {
            UpdateCalculationsFrom(job);
        }


        public void UpdateCalculationsFrom(Job job)
        {
            if (job == null || job.PinPoint == null || job.Part == null)
                return;

            if (job.Part.Multiplier != 0 && job.PinPoint.LineSpeed != 0 && job.Part.PartsPerBox != 0 && job.BoxesPerCrate != 0)
            {
                this.CurrentBox = job.PinPoint.BoxNumber;
                this.MinutesPerBox = (job.Part.CutLength * job.Part.PartsPerBox) / job.PinPoint.LineSpeed;
                this.MinutesPerPallet = this.MinutesPerBox * job.BoxesPerCrate / job.Part.Multiplier;
                this.ShiftBoxNeeds = job.Part.Multiplier * minutesPerShift / this.MinutesPerBox;
                this.ShiftPalletNeeds = minutesPerShift / this.MinutesPerPallet;

                if (!string.IsNullOrWhiteSpace(job.Part.CrateSize))
                {
                    this.ShiftCrateNeeds = this.ShiftBoxNeeds;
                }

                float boxesUntilCrateUp = job.BoxesPerCrate - this.CurrentBox % job.BoxesPerCrate;
                float minutes = boxesUntilCrateUp * this.MinutesPerBox / job.Part.Multiplier;
                this.NextCrateUp = job.PinPoint.PinTime.AddMinutes(minutes);

                if (job.TotalBoxes > 0)
                {
                    float boxesUntilChangeOver = job.TotalBoxes - this.CurrentBox;
                    minutes = boxesUntilChangeOver * this.MinutesPerBox / job.Part.Multiplier;
                    this.ChangeOver = job.PinPoint.PinTime.AddMinutes(minutes);
                }

                var elapsedMinutes = DateTime.Now - job.PinPoint.PinTime;
                float boxesSincePin = job.Part.Multiplier * (float)elapsedMinutes.TotalMinutes / this.MinutesPerBox;
                float pinPartialBox = (float)job.PinPoint.PartialCount / (float)job.Part.PartsPerBox;
                this.CurrentBox = (job.PinPoint.BoxNumber - 1) + pinPartialBox + boxesSincePin;
            }
        }
    }
}
