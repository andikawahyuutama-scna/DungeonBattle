using System;

class Player
{
    public string Name { get; set; }
    public int Hp { get; set; }
    public int MaxHp { get; set; }
    public int Attack { get; set; }
    public int Potion { get; set; }
    public int Gold { get; set; }

    public Player(string name)
    {
        Name = name;
        MaxHp = 100;
        Hp = MaxHp;
        Attack = 20;
        Potion = 3;
        Gold = 0;
    }

    public void ShowStatus()
    {
        Console.WriteLine();
        Console.WriteLine("=== PLAYER STATUS ===");
        Console.WriteLine($"Nama    : {Name}");
        Console.WriteLine($"Hp        : {Hp}/{MaxHp}");
        Console.WriteLine($"Attack    : {Attack}");
        Console.WriteLine($"Potion    : {Potion}");
        Console.WriteLine($"Gold    : {Gold}");
        Console.WriteLine("=====================");

    }

    public void Heal()
    {
        if (Potion <= 0)
        {
            Console.WriteLine("Potion kamu sudah habis");
            return;
        }

        if (Hp == MaxHp)
        {
            Console.WriteLine("HP kamu sudah penuh");
            return;
        }

        int healAmount = 30;

        Hp += healAmount;

        if (Hp > MaxHp)
        {
            Hp = MaxHp;
        }

        Potion--;

        Console.WriteLine($"Kamu menggunakan potion.");
        Console.WriteLine($"HP bertambah {healAmount}.");
        Console.WriteLine($"HP sekarang: {Hp}/{MaxHp}");
    }
}

class Monster
{
    public string Name {  get; set; }
    public int Hp { get; set; }
    public int Attack { get; set; }

    public Monster( string name, int hp,  int attack)
    {
        Name = name;
        Hp = hp;
        Attack = attack;
    }
}

class Program
{
    static Random random = new Random();

    static void Main()
    {
        Console.Title = "Dungeon Battle";

        Console.WriteLine("==============================");
        Console.WriteLine("        DUNGEON BATTLE");
        Console.WriteLine("==============================");

        Console.Write("Masukkan nama karakter: ");
        string? playerName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(playerName))
        {
            playerName = "Hero";
        }

        Player player = new Player(playerName);

        Console.WriteLine();
        Console.WriteLine($"Selamat datang, {player.Name}!");
        Console.WriteLine("Kamu akan memasuki dungeon....");

        Pause();

        Monster[] monsters =
        {
            new Monster("Goblin", 50, 10),
            new Monster("Orc", 80, 15),
            new Monster("Dragon", 120, 20)
        };

        for (int i = 0; i < monsters.Length; i++) 
        {
            Monster monster = monsters[i];

            bool victory = Battle(player, monster);

            if (!victory)
            {
                GameOver(player);
                return;

            }

            player.Gold += 50;

            Console.WriteLine();
            Console.WriteLine($"Kamu mendapatkan 50 gold");
            Console.WriteLine($"Total Gold: {player.Gold}");

            if (i < monsters.Length - 1)
            {
                Console.WriteLine();
                Console.WriteLine("Bersiap untuk monster berikutnya");

                Pause();
            }
        }
        Victory(player);
    }

    static bool Battle(Player player, Monster monster)
    {
        while (player.Hp > 0 && monster.Hp > 0)
        {
            Console.WriteLine("============================");
            Console.WriteLine("     Battle: {monster.Name}");
            Console.WriteLine("============================");

            Console.WriteLine();
            Console.WriteLine($"{player.Name}");
            Console.WriteLine($"HP: {player.Hp}/{player.MaxHp}");

            Console.WriteLine();

            Console.WriteLine($"{monster.Name}");
            Console.WriteLine($"HP: {monster.Hp}");

            Console.WriteLine();
            Console.WriteLine("==============================");
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Use Potion");
            Console.WriteLine("3. Status");
            Console.WriteLine("==============================");

            Console.WriteLine("Pilih Aksi: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    PlayerAttack(player, monster);

                    if (monster.Hp > 0)
                    {
                        MonsterAttack(player, monster);
                    }
                    break;

                 case "2":
                    player.Heal();

                    if (player.Hp > 0)
                    {
                        MonsterAttack(player, monster);
                    }

                    Pause();

                    break;

                  case "3":
                    player.ShowStatus();

                    Pause();
                    break;

                default:
                    Console.WriteLine("Pilihan tidak valid");
                    Pause();
                    break;

            }
        }
        return player.Hp > 0;
    }

    static void PlayerAttack(Player player, Monster monster)
    {
        int damage = player.Attack;

        int criticalChance = random.Next(1, 101);

        if (criticalChance  <= 20)
        {
            damage *= 2;

            Console.WriteLine();
            Console.WriteLine("CRITICAL HIT!!");
        }

        monster.Hp -= damage;

        if (monster.Hp < 0)
        {
            monster.Hp = 0;
        }

        Console.WriteLine();
        Console.WriteLine($"{player.Name} menyerang {monster.Name}!");
        Pause();
    }

    static void MonsterAttack(Player player, Monster monster)
    {
        int damage = monster.Attack;

        player.Hp -= damage;

        if (player.Hp < 0)
        {
            player.Hp = 0;
        }

        Console.WriteLine();
        Console.WriteLine(
            $"{monster.Name} menyerang {player.Name}!"
            );
        Console.WriteLine(
            $"Damage: {damage}");
        Console.WriteLine($"HP kamu: {player.Hp}");

        Pause();
    }

    static void GameOver(Player player)
    {
        Console.Clear();

        Console.WriteLine("=============================");
        Console.WriteLine("         GAME OVER");
        Console.WriteLine("=============================");

        Console.WriteLine();
        Console.WriteLine($"{player.Name} telah dikalahkan");

        Console.WriteLine();
        Console.WriteLine($"Gold telah diperoleh: {player.Gold}");

        Console.WriteLine();
        Console.WriteLine("Terima kasih telah bermain");

        Console.WriteLine();
        Console.WriteLine("Tekan tombol apa aja untuk keluar...");
        Console.ReadKey();
    }

    static void Victory(Player player)
    {
        Console.Clear();

        Console.WriteLine("=================================");
        Console.WriteLine("         VICTORY!!!");
        Console.WriteLine("=================================");

        Console.WriteLine();
        Console.WriteLine(
            $"Selamat {player.Name}!");
        Console.WriteLine("Kamu berhasil mengalahkan semua monster");

        Console.WriteLine();
        Console.WriteLine($"Gold: {player.Gold}");
        Console.WriteLine($"HP: { player.Hp}/{player.MaxHp}");

        Console.WriteLine();
        Console.WriteLine("Kamu adalah pahlawan dungeon");

        Console.WriteLine();
        Console.WriteLine("Tekan tombol apa aja");
        Console.ReadKey();
    }

    static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Tekan tombol apa aja untuk melanjutkan....");
        Console.ReadKey();
    }
}


