using System;
using System.Collections.Generic;
using System.Text;

namespace LineOperator2.Models
{
    public class Pin
    {
        #region DatabaseStuff
        public int JobID { get; set; }
        #endregion


        public DateTime PinTime { get; set; } = DateTime.Now;
        public int BoxNumber { get; set; } = 0;
        public int PartialCount { get; set; } = 0;
        public float LineSpeed { get; set; } = 0.0f;
        public float SampleWeight { get; set; } = 0.0f;

        public Pin()
        {
            
        }

        public Pin(Pin fromPin)
        {
            JobID = fromPin.JobID;
            PinTime = fromPin.PinTime;
            BoxNumber = fromPin.BoxNumber;
            PartialCount = fromPin.PartialCount;
            LineSpeed = fromPin.LineSpeed;
            SampleWeight = fromPin.SampleWeight;
        }
    }
}
