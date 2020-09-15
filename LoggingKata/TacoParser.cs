namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();

        public ITrackable Parse(string line)
        {
            logger.LogInfo("Begin parsing");

            // Take your line and use line.Split(',') to split it up into an array of strings, separated by the char ','
            var cells = line.Split(',');

            // If your array.Length is less than 3, something went wrong
            if (cells.Length < 3)
            {
                // Log that and return null
                // Do not fail if one record parsing fails, return null
                // TODO Implement
                logger.LogError("Less than 3 elements in record.");
            }

            var tacoBell = new TacoBell();
            string name = cells[2];
            // grab the name from your array at index 2
            tacoBell.Name = name;
            Point location = new Point();

            double latitude;
            double longitude;

            // grab the latitude from your array at index 0
            if (double.TryParse(cells[0], out latitude))
                location.Latitude = latitude;
            else
            {
                logger.LogError("Could not parse latitude.");
            }

            // grab the longitude from your array at index 1
            if (double.TryParse(cells[1], out longitude))
                location.Longitude = longitude;
            else
            {
                logger.LogError("Could not parse longitude.");
            }
            
            tacoBell.Location = location;

            return tacoBell;

        }
    }
}