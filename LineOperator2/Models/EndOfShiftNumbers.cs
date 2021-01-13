using System;
using System.Collections.Generic;
using System.Text;

namespace LineOperator2.Models
{
    public class EndOfShiftNumbers
    {

        public EndOfShiftNumbers(int jobID)
        {
            UpdateNumbersFromJob(jobID);
        }

        public void UpdateNumbersFromJob(int jobID)
        {
        // Get 
        //  For each job on the line
        //  Find the first and last pin
        //  Calculate the exact box number each was on.
        //  Subtract to get the total number of boxes finished in that time frame.
        //      This yields the % of waste / down time and the % of selling
        //     Next, subtract the end of shift time from the last pin time for the last job
  
        //    Use the difference and the line speed to estimate how many pieces will be completed between the last pin and end of shift.
        // Finally, subtract the beginning of shift time from the first pin time
  
        //Use the difference and the line speed to determine how many boxes should have been completed
  
        //  compare this number of boxes to how many were possible to do from the start of the job(in case the job was started after shift start).
        //Use whichever number if smaller as the count.

        //The report should show
        //   The start and end pin information and the total number of pieces between them with % efficiency.
        //   The estimated last pieces
        //   The estimated first pieces.
        }
    }
}
