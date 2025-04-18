using System.Numerics;
using System.Xml.Linq;

namespace TextRpg
{
    internal class Program
    {
        static void Main(string[] args)
        { //시작 화면
            Console.WriteLine("스타르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine();

            int input = 0;
            // Character 객체 생성
            Character player = new Character("Chad", "전사", 10, 5, 100, 1500);

            //Inventory 객체 생성
            Inventory inventory = new Inventory();

            //Shop 객체 생성
            Shop shop = new Shop();
    

            // 아이템 생성
            Item TrainingArmor = new Item("수련자 갑옷  ", 0, 5, "수련에 도움을 주는 갑옷입니다.   ", 1000);
            Item SteelArmor = new Item("무쇠 갑옷  ", 0, 9, "수련에 도움을 주는 갑옷입니다.   ", 2300);
            Item SpartanArmor = new Item("스파르타의 갑옷  ", 0, 15, "수련에 도움을 주는 갑옷입니다.   ", 3500);
            Item RustySword = new Item("낡은 검  ", 2, 0, "수련에 도움을 주는 갑옷입니다.   ", 600);
            Item BronzeAxe = new Item("청동 도끼  ", 5, 0, "수련에 도움을 주는 갑옷입니다.   ", 1500);
            Item SpartanSpear = new Item("스파르타의 창  ", 7, 0, "수련에 도움을 주는 갑옷입니다.   ", 3700);
            Item LifeRuby = new Item("생명의 루비 펜던트", 100, 0,"최대 체력을 상승시켜주는 신비한 펜던트입니다.",4000);

            //아이템 생성자로 다 만들어 놓고 삼항 연산자로 0이면 출력 안되게 설정


            // 플레이어가 장착한 아이템 설정
            player.EquippedItem = SteelArmor;
            player.EquippedItem = SpartanSpear;

            // 상점 List에 추가!
            shop.Items.Add(TrainingArmor);
            shop.Items.Add(SteelArmor);
            shop.Items.Add(SpartanArmor);
            shop.Items.Add(RustySword);
            shop.Items.Add(BronzeAxe);
            shop.Items.Add(SpartanSpear);
            shop.Items.Add(LifeRuby);


            while (true)
            {
                //행동 안내문♥
                Console.WriteLine("==========================================");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine();
                

                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

                bool isInt = int.TryParse(Console.ReadLine(), out input);

                if (isInt && input >= 1 && input <= 3)
                {
                    if (input == 1)
                    {
                        player.ShowStatus();
                    }

                    else if (input == 2)
                    {
                        inventory.ShowInventory();
                    }
                    else if (input == 3)
                    {
                        shop.ShowShop(player, inventory);
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다");
                    }
                }
                else 
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.WriteLine("1~3 사이의 숫자를 입력해주세요.");
                    Console.Write(">> ");
                }
                //현재 else부분에 else if를 시용해야 할까?
                //아니면 swith case..?
                //1, 2, 3 각자 입력되면 열리는 정보 생성 후 다른 행동 선택까지 해야 함
                //정보 띄우고 재입력창 코드 다시 써??
                //반복문으로 해야 할거 같은데..
            }


        }
    }
     class Item
     {
         public string Name;
         public int ItemAttack;
         public int ItemDefense;
         public string Description;
         public int Cost;

        public Item (string name, int attack, int defense, string description, int cost) //생성자 사용
        {
            Name = name;
            ItemAttack = attack;
            ItemDefense = defense;
            Description = description;
            Cost = cost;
        }

          public int BonusAttack => ItemAttack;
          public int BonusDefense => ItemDefense; //장착시 

            
     }

    
    class Character 
    {
            public string Name;
            public string Job;
            public int BaseAttack;
            public int BaseDefense;
            public int HP;
            public int Gold;
            public Item EquippedItem;

        public Character (string name, string job,int baseattack, int basedefense, int hp, int gold)
        {
           Name = name;
           Job = job;
           BaseAttack = baseattack;
           BaseDefense = basedefense;
           HP = hp;
           Gold = gold;
        }

            public int TotalAttack => BaseAttack + (EquippedItem?.ItemAttack ?? 0);
            public int TotalDefense => BaseDefense + (EquippedItem?.ItemDefense ?? 0);
            public void ShowStatus()
            {
                //1. 상태보기 캐릭터 정보 표기

                Console.WriteLine($"이름: {Name} ({Job})");
                Console.WriteLine($"공격력: {TotalAttack} + ({EquippedItem.ItemAttack})");
                Console.WriteLine($"방어력: {TotalDefense} + ({EquippedItem.ItemDefense})");
                Console.WriteLine($"체력: {HP}");
                Console.WriteLine($"Gold: {Gold} G");

                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

                Console.WriteLine("0. 나가기");

                //이 안에 숫자는 다 장착 아이템에 따라 수치 변경 

                //나가기 버튼 0 입력시 시작 화면으로 
         
            }

    }
       
    class Inventory
    {
        public List<Item> Items = new List<Item>();
        public Item EquippedItem;
        public void AddItem(Item item)
        {
            Items.Add(item);
            Console.WriteLine($"{item.Name}을(를) 인벤토리에 추가했습니다.");
  
        }


        public void ShowInventory()
        {
            Console.WriteLine("-----인벤토리-----");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            if (Items.Count == 0)
            {
                Console.WriteLine("보유한 아이템이 없습니다.");
            }
            else
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    var item = Items[i];
                    string eqippedMark = (item == EquippedItem) ? "[E]" : "";//장착 아이템 표시 - shop 구매완료와 같은
                    Console.WriteLine($"{i + 1}. {eqippedMark}{Items[i].Name} | 공격력: {Items[i].ItemAttack} | 방어력: {Items[i].ItemDefense} | {Items[i].Description}");
                }
            }
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.Write(">> ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.WriteLine("★ 장착 관리 ★");
                    EquipItem();
                    break;
                case "0":
                    Console.WriteLine("인벤토리에서 나갑니다...(> o `)︎ ");
                    return;
                default:
                    Console.WriteLine("잘못된 입력입니다");
                    break;
            }
        }

            //장착 관리
            public void EquipItem()
        {
            Console.WriteLine("장착할 아이템을 선택하세요.");
            Console.Write(">> ");
            string input = Console.ReadLine();

            if(int.TryParse(input, out int selected) && selected >= 1 && selected <= Items.Count)
            {
                EquippedItem = Items[selected-1];
                Console.WriteLine($"{EquippedItem.Name}을(를) 장착했습니다.");
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Console.Write(">> ");
            }
        }
        
    }
    class Shop
    {
        public List <Item> Items = new List<Item>();

        public void ShowShop(Character player, Inventory inventory)
        {
            while (true) 
            {
                Console.WriteLine("-----상점-----");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine($"[보유 골드] {player.Gold} G\n");


                //아이템 목록
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < Items.Count; i++) 
                {
                    //var 변수는 어떤 자료량을 넣어도 자동 저장- 지역 변수 선언 필수, 초기화 필요
                    //메서드 내부에 선언할 때만 var 키워드를 적용할 수 있디.
                   var item = Items[i];
                    string owned = item.Cost == 0 ? " 구매 완료" : $"{item.Cost} G";
                    Console.WriteLine($"{i + 1}. {item.Name} | 공격력: {item.ItemAttack} | 방어력: {item.ItemDefense} | {item.Description} | {owned}");

                }
                Console.WriteLine() ;
                Console.WriteLine("1. 구매하기");
                Console.WriteLine("0. 나가기");
                Console.Write("\n>> : ");

                string input = Console.ReadLine();
                if (input == "0")
                {
                    Console.WriteLine("상점에서 나갑니다...(> o `)︎ ");
                    Console.WriteLine();
                    break; // 루프 종료
                }
                else if (input == "1")
                {
                    Console.WriteLine("구매 메뉴로 이동합니다!");
                    Console.WriteLine("=========================================");
                    // 구매 처리 로직
                    Console.WriteLine("구매할 아이템을 선택하세요.");
                    Console.Write(">> ");
                    string input2 = Console.ReadLine();
                    int selectedIndex = int.Parse(input2) - 1;
                    Item selectedItem = Items[selectedIndex];

                    if (selectedItem.Cost == 0)
                    {
                        Console.WriteLine("이미 구매한 아이템입니다.");
                    }
                    else if (player.Gold >= selectedItem.Cost)
                    {
                        player.Gold -= selectedItem.Cost;
                        selectedItem.Cost = 0; // 구매 완료 표시
                        inventory.AddItem(selectedItem);
                        Console.WriteLine($"{selectedItem.Name}  ◑ 구매 완료! ◐");
                    }
                    else
                    {
                        Console.WriteLine("◑ Gold ◐ 가 부족합니다. ");
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 선택해주세요.");
                    Console.Write(">> ");
                }

                Console.WriteLine("\n계속하려면 Enter를 누르세요...");
                Console.ReadLine(); // 잠깐 멈춤
            }

            
            
        }
        //1 입력하면 아이템 앞에 각 숫자 생성-생성된 숫자 누르면 구매완료 , 골드 차감, 구매하면 판매 골드 구매완료로 표시 및 인벤토리로 이동, 0누르면 나가기
    }

    
}

