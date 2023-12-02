namespace WorldOfZuul
{
    public static class TextArtManager
    {
        private static Dictionary<string, string> textArts = new Dictionary<string, string>();

        static TextArtManager()
        {
            // Initialize text arts for each room
            textArts["villageCenter"] = @"
                      ████████████                                     
                  ████░░░░░░░░░░░░████                              
                ██  ░░    ░░    ░░    ██            It's time for you... Let's begin your adventure!                   
              ██  ░░    ░░    ░░    ░░  ██            In this chapter you'll step into the boots of a 
              ██░░    ░░    ░░    ░░    ██              dedicated farmer with a mission that goes beyond the fields. 
              ██    ░░    ░░    ░░    ░░██              
              ██  ░░    ░░    ░░    ░░  ██                Your journey will be not only about developing a farm
            ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██                but also making a difference in the community. 
        ████▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒████        
    ████░░  ░░    ░░    ░░    ░░    ░░    ░░    ████          Throughout your story sustainable farming 
  ██  ░░    ████████████████████████████████  ░░    ██          will be your guiding light.
██  ██████████▓▓▓▓  ▓▓  ▓▓    ▓▓  ▓▓  ▓▓▓▓██████████  ██
  ████░░░░░░██▓▓▓▓  ▓▓            ▓▓  ▓▓▓▓██░░░░░░████            Your efforts to reduce waste and 
      ████████▓▓▓▓                    ▓▓▓▓████████                  adopt eco-friendly practices will have
            ██▓▓      ██        ██      ▓▓██                          a lasting impact on the world around you.
              ██      ██        ██      ██              
              ██                        ██                              You will now be able to make a few 
              ██        ▒▒    ▒▒        ██                                choices about your character.
                ██        ▒▒▒▒        ██                
                  ████            ████                                             
                ██░░░░████████████░░░░██                
              ██  ██░░░░░░▓▓▓▓░░░░░░██  ██              
            ██      ██░░░░░░░░░░░░██      ██                                        Press any key...
            ██    ████░░░░▓▓▓▓░░░░████    ██            
            ██████  ██░░░░░░░░░░░░██  ██████            
                    ████████████████                    
                    ██▓▓▒▒▓▓▓▓▒▒▒▒██                    
                  ████▓▓▒▒████▒▒▒▒████                  
                  ██▒▒▒▒██    ██▒▒▒▒██                  
                  ██████        ██████ 
                  
                  
                  ";

            textArts["Lab"] = @"
                                                                    Pick a number of your character's background: 
                           _.-^-._    .--.
                        .-'   _   '-. |__|
                       /     |_|     \|  |
                      /               \  |                                      • Keep in mind these are base values and you'll
                     /|     _____     |\ |                                        be able to develop them throughout the game.
                      |    |==|==|    |  |                      
  |---|---|---|---|---|    |--|--|    |  |
  |---|---|---|---|---|    |==|==|    |  |



1. Former Enviromental Acitivist                             2. Family Tradition Farmer                            3. Former Wanderer

You hosted sustainability campaigns                          Farming runs in your veins,                        What you seek is a quiet life
all over the world. Now it's time for you               you have inherited your family's farm                 after years of adventure that got
to bring your ideals into action                and your new goal is to develop it while cultivating           you fascinated with both farming 
by managing a small farm.                            tradition and Sustainable Development Goals.                    and sustainability.
                                                      
Comes with a higher base                                      Comes with a higher base                         Comes with a higher base
sustainabilty score.                                               farming score.                                      social score.



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