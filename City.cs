namespace WebApp
{
    public class City
    {
        public City(string cname, int ccode, int tcode)
        {
            Name = cname;
            CallCode = ccode;
            TrafficCode = tcode;
        }

        public string Name { get; set; }

        public int CallCode { get; set; }

        public int TrafficCode { get; set; }

    }
}
