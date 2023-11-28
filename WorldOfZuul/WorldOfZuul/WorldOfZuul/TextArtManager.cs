namespace WorldOfZuul
{
    public static class TextArtManager
    {
        private static Dictionary<string, string> textArts = new Dictionary<string, string>();

        static TextArtManager()
        {
            // Initialize text arts for each room
            textArts["outside"] = @"
                // ASCII art for outside
            ";
            // Add more rooms and their text arts here
        }

        public static string GetTextArt(string room)
        {
            if (textArts.TryGetValue(room, out string art))
            {
                return art;
            }
            return room;
        }
    }

}