using lngCollector.Model;

namespace lngCollector.Tools
{
    public static class LvlMarker
    {
        public static string getLevel(EWord w)
        {
            if(w == null) return "cLevel0";

            return $"cLevel{w.weight}";
        }
    }
}
