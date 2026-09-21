Random random = new Random();

string[] field = {" 1", " 2", " 3", " 4", " 5", " 6", " 7", " 8", " 9", "10", "11"
                , "12", "13", "14", "15", "16", "17", "18", "19", "20"
                , "21", "22", "23", "24", "25"};

List<int> mineIndex = [];

int difficulty = 1;
int picked = 0;

bool isRunning = true;
bool gameStarted = false;


// game-loop
while (isRunning)
{
    Console.Clear();
    Draw();

    if (!gameStarted)
    {
        SetDifficulty();
    }
    else
    {
        StartGame();
        TrackPickedIndex();
    }
}

// this draw the square. currently 5x5
void Draw()
{
    int iterate = 0;
    Console.WriteLine(" ");
    Console.WriteLine("------- Cheap Minesweeper! -------\n");
    Console.WriteLine($"       Total Mines(s) : {difficulty}");
    Console.WriteLine("   Guess the number. Q to quit");
    Console.WriteLine("  Hope you don't step on a mine.\n");
    for (int i = 0; i < 5; i++)
    {   
        for (int j = 0; j < 5; j++)
        {
            Console.Write($"  {field[iterate]}  ");

            if (j < 4) { Console.Write("|"); }
            iterate++;
        }
        if (i < 4)
        {
            Console.WriteLine("\n------+------+------+------+------");
        }       
    }
    Console.WriteLine(" ");
    Console.WriteLine("\n----------------------------------\n");
    
}

void TrackPickedIndex()
{
    if(picked == 25 - difficulty)
    {
        GameWin();
    }
}

void SetDifficulty()
{
    string set;
    int randomIndex;

    Console.WriteLine("Set difficulty 1 to 10 : \n");
    set = Console.ReadLine();

    if (set != null)
    {
        if (set == "q" || set == "Q")
        {
            Quit();
        }

        if(int.TryParse(set, out int result))
        {
            result = Math.Clamp(result, 1, 10);
            difficulty = result;

            for (int i = 0; i <= difficulty; i++)
            {
                randomIndex = random.Next(1, 24);

                mineIndex.Add(Convert.ToInt32(field[randomIndex]));
            }

            gameStarted = true;
        }
        else
        {
            set = Console.ReadLine();
        }
    }
    else
    {
        set = Console.ReadLine();
    }
}

void GameOver()
{
    Console.Clear();
    Draw();

    Console.WriteLine("KA-BOOOOM ! You lose.");
    
    gameStarted = false;
    isRunning = false;
}

void GameWin()
{
    Console.Clear();
    Draw();

    Console.WriteLine("WO-HOOOOO ! Congratulations.");
    Console.WriteLine("You survived ! ");
    
    gameStarted = false;
    isRunning = false;
}

void StartGame()
{
    string playerChoice;
    Console.WriteLine("Enter your desired number to step on : ");
    Console.WriteLine($"steps : {picked}");
    playerChoice = Console.ReadLine();

    if (playerChoice != null)
    {   
        if (playerChoice == "q" || playerChoice == "Q")
        {
            Quit();
        }

        if (int.TryParse(playerChoice, out int result))
        {
            picked++;
            for (int i = 0; i < mineIndex.Count; i++)
            {
                if (result == mineIndex[i])
                {
                    field[result - 1] = " X";
                    GameOver();
                }
                else
                {
                    field[result - 1] = " O";
                }
            }
        }
        else
        {
            playerChoice = Console.ReadLine();
        }
    }
    else
    {
        playerChoice = Console.ReadLine();
    }
}

void Quit()
{
    Console.Clear();
    Draw();

    Console.WriteLine("Quit- ?");
    isRunning = false;
}
