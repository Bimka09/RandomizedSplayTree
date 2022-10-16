using SplayRandTree;
using System.Diagnostics;

var test = new RandomizedBST();
CountTime(test);

static RandomizedBST CreateTestData(int amount, RandomizedBST tree)
{
    Random randNum = new Random();
    for (int i = 0; i < amount; i++)
    {
        tree.Insert(i);
        //tree.Insert(randNum.Next(0, 1000));
    }
    return tree;
}
static void CountTime(RandomizedBST tree)
{
    Stopwatch clock = new Stopwatch();
    clock.Start();
    tree = CreateTestData(1000, tree);
    clock.Stop();
    Console.WriteLine($"Наполнение возрастающими числами от 0 до 1000 {clock.ElapsedMilliseconds} ms");
    clock.Start();
    SearchData(100, tree);
    clock.Stop();
    Console.WriteLine($"Поиск случайных чисел от 0 до 1000, 100 раз {clock.ElapsedMilliseconds} ms");
    clock.Start();
    SearchIncreaseData(1000, tree);
    clock.Stop();
    Console.WriteLine($"Поиск чисел от 0 до 10, 1000 раз {clock.ElapsedMilliseconds} ms");
    clock.Start();
    tree = DeleteData(1000, tree);
    clock.Stop();
    Console.WriteLine($"Удаление случайных чисел от 0 до 1000, 100 раз {clock.ElapsedMilliseconds} ms");

}
static void SearchIncreaseData(int amount, RandomizedBST tree)
{
    Random randNum = new Random();
    for(int j = 0; j < amount; j++)
    {
        for (int i = 0; i < 10; i++)
        {
            tree.Search(i);
        }
    }
}
static void SearchData(int amount, RandomizedBST tree)
{
    Random randNum = new Random();
    for (int i = 0; i < amount; i++)
    {
        tree.Search(randNum.Next(0, 1000));
    }
}
static RandomizedBST DeleteData(int amount, RandomizedBST tree)
{
    Random randNum = new Random();
    for (int i = 0; i < amount; i++)
    {
        tree.Remove(randNum.Next(0, 1000));
    }
    return tree;
}