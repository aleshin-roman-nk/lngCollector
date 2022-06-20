using lngCollector.Model;

namespace lngCollector.Tools
{
    public static class LvlMarker
    {
        public static string getLevel(EWord w)
        {
            if(w == null) return "cLevel0";

            int lvl = w.weight / 10 + 1;
            if (lvl > 6) lvl = 6;
            if (lvl <= 0) lvl = 1;

            return $"cLevel{lvl}";
        }
    }
}
