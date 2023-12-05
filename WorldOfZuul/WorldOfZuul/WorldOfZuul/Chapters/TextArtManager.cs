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
    textArts["EgnineerIntro"]=@"
    
                ██████████████            
              ██░░░░░░░░░░░░░░██          
            ██░░░░░░░░░░░░░░░░░░██     The Engineer Chapter!
          ██░░░░░░░░░░░░░░░░░░░░░░██      
          ██░░░░░░░░░░░░░░░░░░░░░░██     Welcome to the life of the Village Engineer, where ingenuity meets the
          ██░░░░░░░░░░░░░░░░░░░░░░██       heart of a close-knit community. In this chapter, you'll navigate through 
          ██▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██           critical challenges that threaten our charming village. The river, our lifeblood, 
            ██░░░░▓▓▓▓▓▓▓▓▓▓▓▓▓▓██             is mysteriously polluted. The old power station struggles to keep the lights 
          ████░░░░░░░░▓▓▓▓░░▓▓░░██               on, and there's a growing need for sustainable housing. Your skills and 
      ████▓▓▓▓██░░░░████████░░██████               decisions will shape the future of our village. Are you ready to unravel the 
    ██▓▓▓▓▓▓▓▓▓▓██░░░░░░░░░░██▓▓▓▓▓▓██               mysteries, light up lives, and build a sustainable future? Your journey as the 
    ██▓▓▓▓▓▓▓▓▓▓▓▓██████████▓▓▓▓▓▓▓▓██                 Village Engineer begins now.
  ██░░░░▓▓▓▓██▓▓▓▓▓▓▓▓▓▓▓▓▓▓██▓▓▓▓▓▓▓▓██  
  ██▒▒▒▒██████▓▓▓▓▓▓▓▓▓▓▓▓▓▓██████░░░░██  
  ██░░░░▒▒████▓▓▓▓▓▓▓▓▓▓▓▓▓▓████░░░░░░██  
  ██░░░░░░████▓▓▓▓▓▓▓▓▓▓▓▓▓▓████░░░░░░██                PRESS ANY KEY TO CONTINUE...
    ██████  ██████▒▒▓▓▒▒▒▒▒▒██  ██████    
          ██▓▓████▓▓▓▓▓▓▓▓▒▒▒▒██          
        ██░░▓▓▓▓▓▓▓▓██▓▓▓▓▒▒▒▒░░██        
      ████░░▓▓▓▓▓▓██  ██▓▓▓▓▓▓░░████      
  ████▓▓▓▓▓▓▓▓▓▓██      ██▓▓▓▓▓▓▓▓▓▓████  
██▒▒▒▒▒▒▒▒▒▒▒▒▒▒██      ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒██
██████████████████      ██████████████████          
    ";

    textArts["EgineerIntroWater"] = @"
    
                ██████████████            
              ██░░░░░░░░░░░░░░██          
            ██░░░░░░░░░░░░░░░░░░██        
          ██░░░░░░░░░░░░░░░░░░░░░░██        Water Treatment Quest: 'The Mystery of the Polluted River'
          ██░░░░░░░░░░░░░░░░░░░░░░██          
          ██░░░░░░░░░░░░░░░░░░░░░░██           In the heart of our once-thriving village, a crisis looms.
          ██▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██              The river, our lifeline, has turned murky and foul, threatening the health of our people and the wildlife.
            ██░░░░▓▓▓▓▓▓▓▓▓▓▓▓▓▓██                Rumors swirl of dark secrets tied to the abandoned factory upstream.
          ████░░░░░░░░▓▓▓▓░░▓▓░░██                  As our trusted Village Engineer, you must uncover the truth behind this pollution.
      ████▓▓▓▓██░░░░████████░░██████                  Test the waters, delve into the factory's shadowy past, and devise a solution to purify our precious river.
    ██▓▓▓▓▓▓▓▓▓▓██░░░░░░░░░░██▓▓▓▓▓▓██                  The health of our village and its future generations rests in your hands!
    ██▓▓▓▓▓▓▓▓▓▓▓▓██████████▓▓▓▓▓▓▓▓██    
  ██░░░░▓▓▓▓██▓▓▓▓▓▓▓▓▓▓▓▓▓▓██▓▓▓▓▓▓▓▓██  
  ██▒▒▒▒██████▓▓▓▓▓▓▓▓▓▓▓▓▓▓██████░░░░██  
  ██░░░░▒▒████▓▓▓▓▓▓▓▓▓▓▓▓▓▓████░░░░░░██  
  ██░░░░░░████▓▓▓▓▓▓▓▓▓▓▓▓▓▓████░░░░░░██                PRESS ANY KEY TO CONTINUE...
    ██████  ██████▒▒▓▓▒▒▒▒▒▒██  ██████    
          ██▓▓████▓▓▓▓▓▓▓▓▒▒▒▒██          
        ██░░▓▓▓▓▓▓▓▓██▓▓▓▓▒▒▒▒░░██        
      ████░░▓▓▓▓▓▓██  ██▓▓▓▓▓▓░░████      
  ████▓▓▓▓▓▓▓▓▓▓██      ██▓▓▓▓▓▓▓▓▓▓████  
██▒▒▒▒▒▒▒▒▒▒▒▒▒▒██      ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒██
██████████████████      ██████████████████    
    ";

    textArts["EgineerIntroHousing"] = @"
    
                ██████████████            
              ██░░░░░░░░░░░░░░██          
            ██░░░░░░░░░░░░░░░░░░██        
          ██░░░░░░░░░░░░░░░░░░░░░░██        Housing Quest: 'The Eco-Friendly Housing Challenge'
          ██░░░░░░░░░░░░░░░░░░░░░░██          
          ██░░░░░░░░░░░░░░░░░░░░░░██           As our village grows, so does our need for homes, homes that respect our past and protect our future.
          ██▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██              The challenge is to create housing that blends tradition with sustainability, comfort with environmental care.
            ██░░░░▓▓▓▓▓▓▓▓▓▓▓▓▓▓██                Your architectural skills and innovative spirit are our hope.
          ████░░░░░░░░▓▓▓▓░░▓▓░░██                  Source sustainable materials, collaborate with local talents, and build a model eco-friendly house that can inspire our entire village.
      ████▓▓▓▓██░░░░████████░░██████                  Lead us in constructing a living space that is a testament to our community's harmony with nature. 
    ██▓▓▓▓▓▓▓▓▓▓██░░░░░░░░░░██▓▓▓▓▓▓██                
    ██▓▓▓▓▓▓▓▓▓▓▓▓██████████▓▓▓▓▓▓▓▓██    
  ██░░░░▓▓▓▓██▓▓▓▓▓▓▓▓▓▓▓▓▓▓██▓▓▓▓▓▓▓▓██  
  ██▒▒▒▒██████▓▓▓▓▓▓▓▓▓▓▓▓▓▓██████░░░░██  
  ██░░░░▒▒████▓▓▓▓▓▓▓▓▓▓▓▓▓▓████░░░░░░██  
  ██░░░░░░████▓▓▓▓▓▓▓▓▓▓▓▓▓▓████░░░░░░██                PRESS ANY KEY TO CONTINUE...
    ██████  ██████▒▒▓▓▒▒▒▒▒▒██  ██████    
          ██▓▓████▓▓▓▓▓▓▓▓▒▒▒▒██          
        ██░░▓▓▓▓▓▓▓▓██▓▓▓▓▒▒▒▒░░██        
      ████░░▓▓▓▓▓▓██  ██▓▓▓▓▓▓░░████      
  ████▓▓▓▓▓▓▓▓▓▓██      ██▓▓▓▓▓▓▓▓▓▓████  
██▒▒▒▒▒▒▒▒▒▒▒▒▒▒██      ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒██
██████████████████      ██████████████████    
    ";

        textArts["EgineerIntroElectricity"] = @"
    
                ██████████████            
              ██░░░░░░░░░░░░░░██          
            ██░░░░░░░░░░░░░░░░░░██        
          ██░░░░░░░░░░░░░░░░░░░░░░██        Electricity Quest: 'The Power Outage Puzzle'
          ██░░░░░░░░░░░░░░░░░░░░░░██          
          ██░░░░░░░░░░░░░░░░░░░░░░██           Our village, though rich in spirit, faces a challenge of modern times. The old power station, once a 
          ██▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██              beacon of progress, now falters, leaving us in darkness too often. Whispered tales speak of an 
            ██░░░░▓▓▓▓▓▓▓▓▓▓▓▓▓▓██                ancient 'Sun Stone' that harnessed the sun's endless energy. Sam, as our ingenious Village 
          ████░░░░░░░░▓▓▓▓░░▓▓░░██                  Engineer, your task is to bring light to our village. Explore the potential of solar power, confront the
      ████▓▓▓▓██░░░░████████░░██████                  skepticism of tradition, and illuminate our lives with a sustainable future. Your quest to keep our 
    ██▓▓▓▓▓▓▓▓▓▓██░░░░░░░░░░██▓▓▓▓▓▓██                  village bright and thriving begins now.
    ██▓▓▓▓▓▓▓▓▓▓▓▓██████████▓▓▓▓▓▓▓▓██    
  ██░░░░▓▓▓▓██▓▓▓▓▓▓▓▓▓▓▓▓▓▓██▓▓▓▓▓▓▓▓██  
  ██▒▒▒▒██████▓▓▓▓▓▓▓▓▓▓▓▓▓▓██████░░░░██  
  ██░░░░▒▒████▓▓▓▓▓▓▓▓▓▓▓▓▓▓████░░░░░░██  
  ██░░░░░░████▓▓▓▓▓▓▓▓▓▓▓▓▓▓████░░░░░░██                PRESS ANY KEY TO CONTINUE...
    ██████  ██████▒▒▓▓▒▒▒▒▒▒██  ██████    
          ██▓▓████▓▓▓▓▓▓▓▓▒▒▒▒██          
        ██░░▓▓▓▓▓▓▓▓██▓▓▓▓▒▒▒▒░░██        
      ████░░▓▓▓▓▓▓██  ██▓▓▓▓▓▓░░████      
  ████▓▓▓▓▓▓▓▓▓▓██      ██▓▓▓▓▓▓▓▓▓▓████  
██▒▒▒▒▒▒▒▒▒▒▒▒▒▒██      ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒██
██████████████████      ██████████████████    
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